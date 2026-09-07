using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using BCASClassLibrary;

public partial class Forms_DetailsForms_TaxInvoiceUpload : System.Web.UI.Page
{
    private const string PaymentDetailsViewStateKey = "TaxInvoiceUploadDetails";
    private readonly TYP_PayeeOrganisationPayment objPay = new TYP_PayeeOrganisationPayment();

    private bool isAdminRole = false;
    private bool isPayeeRole = false;
    private bool isFinanceRole = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Convert.ToString(HttpContext.Current.Session["UserId"]).Trim().Length == 0)
        {
            HttpContext.Current.Response.Redirect("~/Home.aspx");
            return;
        }

        string roleName = GetPaymentRoleName();
        isAdminRole = roleName.ToUpperInvariant().Contains("ADMIN");
        isPayeeRole = roleName.ToUpperInvariant().Contains("PAYEE");
        isFinanceRole = roleName.ToUpperInvariant().Contains("FINANCE");

        // Only Admin, Payee, and Finance can access this page
        if (!isAdminRole && !isPayeeRole && !isFinanceRole)
        {
            Response.Redirect("~/Home.aspx");
            return;
        }

        if (!IsPostBack)
        {
            BindCourseSchedules();
            grdPaymentDetails.Visible = false;
            pnlReceipt.Visible = false;
        }
    }

    protected void ddlSchedule_SelectedIndexChanged(object sender, EventArgs e)
    {
        lbl_ErrorMsg.Text = string.Empty;
        pnlReceipt.Visible = false;
        BindPaymentDetails();
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        ddlSchedule.SelectedValue = "0";
        lbl_ErrorMsg.Text = string.Empty;
        grdPaymentDetails.Visible = false;
        pnlReceipt.Visible = false;
        ViewState[PaymentDetailsViewStateKey] = null;
    }

    protected void grdPaymentDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdPaymentDetails.PageIndex = e.NewPageIndex;
        DataTable paymentDetails = ViewState[PaymentDetailsViewStateKey] as DataTable;
        BindGrid(paymentDetails);
    }

    protected void grdPaymentDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.DataRow)
            return;

        DataRowView rowView = e.Row.DataItem as DataRowView;
        if (rowView == null)
            return;

        Label lblUploadStatus = (Label)e.Row.FindControl("lblUploadStatus");
        Panel pnlUploadSection = (Panel)e.Row.FindControl("pnlUploadSection");
        Panel pnlDownloadSection = (Panel)e.Row.FindControl("pnlDownloadSection");
        Button btnDownloadInvoice = (Button)e.Row.FindControl("btnDownloadInvoice");
        LinkButton lnkDownloadInvoice = (LinkButton)e.Row.FindControl("lnkDownloadInvoice");
        Label lblNoInvoice = (Label)e.Row.FindControl("lblNoInvoice");

        string invoiceFile = GetColumnValueSafe(rowView, "InvoiceUploadFile");
        string uploadStatusText = GetColumnValueSafe(rowView, "Upload_Status");
        bool hasInvoice = !string.IsNullOrEmpty(invoiceFile);

        if (lblUploadStatus != null)
        {
            // InvoiceUploadFile (not the free-text Upload_Status column) is the source of
            // truth for whether a file actually exists - Upload_Status is just for display.
            lblUploadStatus.Text = hasInvoice
                ? (string.IsNullOrEmpty(uploadStatusText) ? "Tax Invoice uploaded" : uploadStatusText)
                : "Not uploaded yet";
            lblUploadStatus.ForeColor = hasInvoice ? System.Drawing.Color.Green : System.Drawing.Color.Red;
        }

        // UPLOAD: Only ADMIN and FINANCE can see upload section
        if (pnlUploadSection != null)
        {
            pnlUploadSection.Visible = isAdminRole || isFinanceRole;
        }

        // DOWNLOAD: ADMIN, PAYEE, and FINANCE can see download section
        if (pnlDownloadSection != null)
        {
            pnlDownloadSection.Visible = isAdminRole || isPayeeRole || isFinanceRole;
        }

        // Download button availability (for both Admin and Payee)
        if (btnDownloadInvoice != null)
        {
            btnDownloadInvoice.Enabled = hasInvoice;
            btnDownloadInvoice.ToolTip = hasInvoice
                ? "Download the invoice currently uploaded for this transaction"
                : "No invoice has been uploaded for this transaction yet";
        }

        // Link button for download (visible to both Admin and Payee)
        if (lnkDownloadInvoice != null)
        {
            lnkDownloadInvoice.Visible = hasInvoice;
        }

        if (lblNoInvoice != null)
        {
            lblNoInvoice.Visible = !hasInvoice;
        }
    }

    protected void grdPaymentDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        DataTable paymentDetails = ViewState[PaymentDetailsViewStateKey] as DataTable;
        if (paymentDetails == null)
        {
            lbl_ErrorMsg.Text = "Please search payment details again.";
            return;
        }

        string transactionId = Convert.ToString(e.CommandArgument);
        DataRow matchedRow = null;
        foreach (DataRow row in paymentDetails.Rows)
        {
            if (Convert.ToString(row["Transaction_id"]) == transactionId)
            {
                matchedRow = row;
                break;
            }
        }

        if (matchedRow == null)
        {
            lbl_ErrorMsg.Text = "Payment record was not found for the selected row.";
            return;
        }

        switch (e.CommandName)
        {
            case "ShowReceipt":
                ShowReceipt(matchedRow);
                break;
            case "UploadInvoice":
                // Only ADMIN and FINANCE can upload
                if (!isAdminRole && !isFinanceRole)
                {
                    lbl_ErrorMsg.ForeColor = System.Drawing.Color.Red;
                    lbl_ErrorMsg.Text = "You do not have permission to upload invoices.";
                    return;
                }
                UploadInvoice(e, matchedRow);
                break;
            case "DownloadInvoice":
                // ADMIN, PAYEE, and FINANCE can download
                if (!isAdminRole && !isPayeeRole && !isFinanceRole)
                {
                    lbl_ErrorMsg.ForeColor = System.Drawing.Color.Red;
                    lbl_ErrorMsg.Text = "You do not have permission to download invoices.";
                    return;
                }
                DownloadInvoiceFile(matchedRow);
                break;
        }
    }

    private void BindCourseSchedules()
    {
        string connectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = new SqlCommand(@"SELECT Course_Schedule.Schedule_ID,
Course_Master.Course_Name + ' - Exam Date: ' + CONVERT(VARCHAR(10), Course_Schedule.Date_of_Exam, 103) AS CourseSchedule
FROM Course_Master INNER JOIN Course_Schedule ON Course_Master.Course_Code = Course_Schedule.Course_Code
ORDER BY Course_Schedule.Schedule_ID DESC", con))
            {
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    ddlSchedule.DataSource = reader;
                    ddlSchedule.DataTextField = "CourseSchedule";
                    ddlSchedule.DataValueField = "Schedule_ID";
                    ddlSchedule.DataBind();
                }
            }
        }
    }

    private void BindPaymentDetails()
    {
        int organisationCode = GetOrganisationCode();
        int scheduleId = Convert.ToInt32(ddlSchedule.SelectedValue);
        string roleName = GetPaymentRoleName();

        using (DataTable paymentDetails = GetCourseWisePaymentDetails(roleName, organisationCode, scheduleId))
        {
            AddDisplayDateColumns(paymentDetails);
            ViewState[PaymentDetailsViewStateKey] = paymentDetails.Copy();
            BindGrid(paymentDetails);
        }
    }

    /// <summary>
    /// Calls dbo.GetCourseWisePaymentDetails proc which returns payment details.
    /// </summary>
    private DataTable GetCourseWisePaymentDetails(string roleName, int organisationCode, int scheduleId)
    {
        string constr = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
        DataTable dt = new DataTable();

        using (SqlConnection conn = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("GetCourseWisePaymentDetails", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RoleName", roleName);
                cmd.Parameters.AddWithValue("@OrganisationCode", organisationCode);
                cmd.Parameters.AddWithValue("@ScheduleID", scheduleId);
                cmd.Parameters.AddWithValue("@FromDate", (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@ToDate", (object)DBNull.Value);

                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                        adapter.Fill(dt);
                }
            }
        }

        return dt;
    }

    private void BindGrid(DataTable paymentDetails)
    {
        grdPaymentDetails.DataSource = paymentDetails;
        grdPaymentDetails.DataBind();
        grdPaymentDetails.Visible = paymentDetails != null;

        if (paymentDetails != null && paymentDetails.Rows.Count == 0)
        {
            lbl_ErrorMsg.Text = "No payment records found for the selected criteria.";
        }
    }

    private void AddDisplayDateColumns(DataTable paymentDetails)
    {
        if (!paymentDetails.Columns.Contains("PaymentDate"))
        {
            paymentDetails.Columns.Add("PaymentDate", typeof(string));
        }

        if (!paymentDetails.Columns.Contains("PaymentTime"))
        {
            paymentDetails.Columns.Add("PaymentTime", typeof(string));
        }

        foreach (DataRow row in paymentDetails.Rows)
        {
            if (row["Date_of_Payment"] == DBNull.Value)
            {
                row["PaymentDate"] = string.Empty;
                row["PaymentTime"] = string.Empty;
                continue;
            }

            DateTime paymentDate = Convert.ToDateTime(row["Date_of_Payment"]);
            row["PaymentDate"] = paymentDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            row["PaymentTime"] = paymentDate.ToString("hh:mm:ss tt", CultureInfo.InvariantCulture);
        }
    }

    private int GetOrganisationCode()
    {
        int organisationCode;
        if (Session["UserOrg"] == null || !Int32.TryParse(Convert.ToString(Session["UserOrg"]), out organisationCode))
        {
            return 0;
        }

        return organisationCode;
    }

    private string GetPaymentRoleName()
    {
        string[] roles = Roles.GetRolesForUser();
        string roleList = String.Join(",", roles).ToUpperInvariant();

        if (roleList.Contains("PAYEE"))
        {
            return "PAYEE";
        }

        if (roleList.Contains("SPON"))
        {
            return "SPONSORING";
        }

        if (roleList.Contains("FINANCE"))
        {
            return "FINANCE";
        }

        // Admin falls here
        return "ADMIN";
    }

    private void ShowReceipt(DataRow receiptRow)
    {
        lblReceiptCourse.Text = Convert.ToString(receiptRow["Course_Name"]);
        lblReceiptSchedule.Text = Convert.ToString(receiptRow["CourseSchedule"]);
        lblReceiptSponsoringOrg.Text = Convert.ToString(receiptRow["Sponsoring_Organisation"]);
        lblReceiptPayeeOrg.Text = Convert.ToString(receiptRow["Payee_Organisation_Name"]);
        lblReceiptInvoice.Text = Convert.ToString(receiptRow["ProformaInvoiceNo"]);
        lblReceiptTransaction.Text = Convert.ToString(receiptRow["Transaction_id"]);
        lblReceiptReference.Text = Convert.ToString(receiptRow["Reference_No"]);
        lblReceiptAmount.Text = Convert.ToString(receiptRow["Amount_Recieved"]);
        lblReceiptPaymentDate.Text = Convert.ToString(receiptRow["PaymentDate"]);
        lblReceiptPaymentTime.Text = Convert.ToString(receiptRow["PaymentTime"]);
        lblReceiptStatus.Text = GetColumnValue(receiptRow, "Payment_Status");
        lblReceiptDescription.Text = GetColumnValue(receiptRow, "ResponseDescription");
        pnlReceipt.Visible = true;
    }

    private void UploadInvoice(GridViewCommandEventArgs e, DataRow row)
    {
        try
        {
            GridViewRow gvRow = (GridViewRow)((Control)e.CommandSource).NamingContainer;
            FileUpload fuInvoice = (FileUpload)gvRow.FindControl("fuInvoice");

            if (fuInvoice == null || !fuInvoice.HasFile)
            {
                lbl_ErrorMsg.Text = "Please choose a PDF file before clicking Upload.";
                return;
            }

            string ext = Path.GetExtension(fuInvoice.FileName).ToLowerInvariant();
            if (ext != ".pdf")
            {
                lbl_ErrorMsg.Text = "Only PDF files are allowed.";
                return;
            }

            const int maxSizeBytes = 10 * 1024 * 1024; // 10 MB        // File size limit for upload is set to 10 MB. If the file size exceeds this limit, an error message is displayed.
            if (fuInvoice.PostedFile.ContentLength > maxSizeBytes)
            {
                lbl_ErrorMsg.Text = "File size must not exceed 10 MB.";
                return;
            }

            string transactionId = Convert.ToString(row["Transaction_id"]);

            int scheduleId;
            if (!TryGetRowScheduleId(row, out scheduleId))
            {
                lbl_ErrorMsg.Text = "Could not determine the Course Schedule for this row. " +
                    "Please select a specific Course Schedule (not 'All') from the filter above and try again.";
                return;
            }

            int orgCode;
            if (!TryGetRowOrgCode(row, out orgCode))
            {
                lbl_ErrorMsg.Text = "Could not determine the Payee Organisation code for this row.";
                return;
            }

            // This is the ONE place the file path is built - Download reads the path
            // straight back out of the DB, but since Upload always writes here and
            // saves this exact same relative path, the two can never drift apart.
            string relativeFolder = BuildInvoiceRelativeFolder(orgCode, scheduleId);
            string safeFileName = BuildInvoiceFileName(orgCode, scheduleId, transactionId);

            string folderPath = Server.MapPath(relativeFolder);
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string fullPath = Path.Combine(folderPath, safeFileName);
            fuInvoice.SaveAs(fullPath);

            string dbRelativePath = relativeFolder + safeFileName;

            SaveSignedTaxInvoiceFile(orgCode, scheduleId, transactionId, dbRelativePath);

            lbl_ErrorMsg.ForeColor = System.Drawing.Color.Green;
            lbl_ErrorMsg.Text = "Invoice uploaded successfully for Transaction ID " + transactionId + ".";

            BindPaymentDetails();
        }
        catch (Exception ex)
        {
            lbl_ErrorMsg.ForeColor = System.Drawing.Color.Red;
            lbl_ErrorMsg.Text = "An error occurred while uploading: " + ex.Message;
        }
    }

    private static string BuildInvoiceRelativeFolder(int orgCode, int scheduleId)
    {
        return "~/Download/TaxInvoice/" + orgCode + "/" + scheduleId + "/";
    }

    private static string BuildInvoiceFileName(int orgCode, int scheduleId, string transactionId)
    {
        return "TaxInvoice_" + orgCode + "_" + scheduleId + "_" + transactionId + ".pdf";
    }

    private void DownloadInvoiceFile(DataRow row)
    {
        try
        {
            string relativePath = GetColumnValue(row, "InvoiceUploadFile");
            if (string.IsNullOrEmpty(relativePath))
            {
                lbl_ErrorMsg.ForeColor = System.Drawing.Color.Red;
                lbl_ErrorMsg.Text = "No invoice has been uploaded for this transaction yet.";
                return;
            }

            string filePath = Server.MapPath(relativePath);
            if (!System.IO.File.Exists(filePath))
            {
                lbl_ErrorMsg.ForeColor = System.Drawing.Color.Red;
                lbl_ErrorMsg.Text = "The invoice file was not found on the server.";
                return;
            }

            string payeeOrgName = GetColumnValue(row, "Payee_Organisation_Name");
            string downloadFileName = "SignedTaxInvoiceOf_" + SanitizeFileNameForHeader(payeeOrgName) + ".pdf";

            // Clear everything - including any header already queued earlier in the page
            // lifecycle - before writing our own single Content-Disposition header. This,
            // combined with sanitizing the org name below, is what fixes the browser's
            // ERR_RESPONSE_HEADERS_MULTIPLE_CONTENT_DISPOSITION error: an org name containing
            // a raw comma (e.g. "BLUE DART AVIATION LTD,") produced an unquoted/unescaped
            // header value that some browsers parse as two separate headers.
            Response.Clear();
            Response.ClearHeaders();
            Response.ClearContent();
            Response.ContentType = "application/pdf";
            Response.AppendHeader("Content-Disposition", "attachment; filename=\"" + downloadFileName + "\"");
            Response.TransmitFile(filePath);
            Response.Flush();

            // Use CompleteRequest instead of Response.End to avoid ThreadAbortException.
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
        catch (Exception ex)
        {
            lbl_ErrorMsg.ForeColor = System.Drawing.Color.Red;
            lbl_ErrorMsg.Text = "An error occurred while downloading: " + ex.Message;
        }
    }

    /// <summary>
    /// Strips characters that are unsafe inside an HTTP header value / filename (commas,
    /// quotes, semicolons, control characters, path-invalid characters) so the resulting
    /// Content-Disposition header is always well-formed. Falls back to "Invoice" if nothing
    /// safe is left.
    /// </summary>
    private static string SanitizeFileNameForHeader(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return "Invoice";

        char[] invalidChars = Path.GetInvalidFileNameChars();
        StringBuilder sb = new StringBuilder(value.Length);
        foreach (char c in value)
        {
            if (c == ',' || c == '"' || c == ';' || c == '\r' || c == '\n' || Array.IndexOf(invalidChars, c) >= 0)
                continue;
            sb.Append(c);
        }

        string cleaned = sb.ToString().Trim();
        return cleaned.Length == 0 ? "Invoice" : cleaned;
    }

    private void SaveSignedTaxInvoiceFile(int pOrg, int scheduleId, string transactionId, string fileName)
    {
        string constr = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
        using (SqlConnection conn = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("SaveSignedTaxInvoiceFile", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@pOrg", pOrg);
                cmd.Parameters.AddWithValue("@scheduleId", scheduleId);
                cmd.Parameters.AddWithValue("@TransactionID", transactionId);
                cmd.Parameters.AddWithValue("@FileName", fileName);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        } 
    }

    private bool TryGetRowScheduleId(DataRow row, out int scheduleId)
    {
        foreach (string candidate in new[] { "Schedule_ID", "ScheduleID", "Schedule_Id" })
        {
            if (row.Table.Columns.Contains(candidate) && row[candidate] != DBNull.Value
                && Int32.TryParse(Convert.ToString(row[candidate]), out scheduleId))
            {
                return true;
            }
        }

        if (ddlSchedule.SelectedValue != "0" && Int32.TryParse(ddlSchedule.SelectedValue, out scheduleId))
        {
            return true;
        }

        scheduleId = 0;
        return false;
    }

    private bool TryGetRowOrgCode(DataRow row, out int orgCode)
    {
        foreach (string candidate in new[] { "Payee_OrgCode", "Payee_Organisation_Code", "Payee_Org_Code", "PayeeOrganisationCode" })
        {
            if (row.Table.Columns.Contains(candidate) && row[candidate] != DBNull.Value
                && Int32.TryParse(Convert.ToString(row[candidate]), out orgCode))
            {
                return true;
            }
        }

        orgCode = 0;
        return false;
    }

    private string GetColumnValue(DataRow row, string columnName)
    {
        if (!row.Table.Columns.Contains(columnName) || row[columnName] == DBNull.Value)
        {
            return string.Empty;
        }

        return Convert.ToString(row[columnName]);
    }

    private string GetColumnValueSafe(DataRowView rowView, string columnName)
    {
        if (rowView == null || !rowView.Row.Table.Columns.Contains(columnName) || rowView[columnName] == DBNull.Value)
        {
            return string.Empty;
        }

        return Convert.ToString(rowView[columnName]);
    }
}
