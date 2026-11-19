using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI.WebControls;
using BCASClassLibrary;
using System.Web.UI;

public partial class Forms_DetailsForms_CourseWisePaymentDetails : System.Web.UI.Page
{
    private const string PaymentDetailsViewStateKey = "CourseWisePaymentDetails";
    private readonly TYP_PayeeOrganisationPayment objPay = new TYP_PayeeOrganisationPayment();
    private bool _exportingExcel = false;
    private bool? _isFinanceUser = null;   // remembers the answer so we ask "is this finance?" only once

    // True when the logged-in person is FINANCE (they may only LOOK, not verify)
    protected bool IsFinanceUser
    {
        get
        {
            if (!_isFinanceUser.HasValue)
            {
                _isFinanceUser = GetPaymentRoleName().Contains("FINANCE");
            }
            return _isFinanceUser.Value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Convert.ToString(HttpContext.Current.Session["UserId"]).Trim().Length == 0)
        {
            HttpContext.Current.Response.Redirect("~/Home.aspx");
        }

        if (!IsPostBack)
        {
            BindCourseSchedules();
            grdPaymentDetails.Visible = false;
            pnlReceipt.Visible = false;
        }
    }

    protected override void Render(HtmlTextWriter writer)
    {
        if (_exportingExcel)
        {
            return;
        }

        base.Render(writer);
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        lbl_ErrorMsg.Text = string.Empty;
        pnlReceipt.Visible = false;
        BindPaymentDetails(null, null);
    }

    protected void ddlSchedule_SelectedIndexChanged(object sender, EventArgs e)
    {
        lbl_ErrorMsg.Text = string.Empty;
        pnlReceipt.Visible = false;
        pnlCandidates.Visible = false;
        BindPaymentDetails(null, null);
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        ddlSchedule.SelectedValue = "0";
        lbl_ErrorMsg.Text = string.Empty;
        grdPaymentDetails.Visible = false;
        pnlReceipt.Visible = false;
        pnlCandidates.Visible = false;
        ViewState[PaymentDetailsViewStateKey] = null;
    }

    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        lbl_ErrorMsg.Text = string.Empty;

        DataTable paymentDetails = ViewState[PaymentDetailsViewStateKey] as DataTable;
        if (paymentDetails == null || paymentDetails.Rows.Count == 0)
        {
            lbl_ErrorMsg.Text = "No records available to export. Please search payment details first.";
            return;
        }

        ExportPaymentDetailsToExcel(paymentDetails);
    }

    protected void grdPaymentDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdPaymentDetails.PageIndex = e.NewPageIndex;
        DataTable paymentDetails = ViewState[PaymentDetailsViewStateKey] as DataTable;
        BindGrid(paymentDetails);
    }

    protected void grdPaymentDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName != "ShowReceipt" && e.CommandName != "ToggleVerify" && e.CommandName != "DownloadCandidates" && e.CommandName != "SelectCandidates")
        {
            return;
        }

        DataTable paymentDetails = ViewState[PaymentDetailsViewStateKey] as DataTable;
        if (paymentDetails == null)
        {
            lbl_ErrorMsg.Text = "Please search payment details again.";
            return;
        }

        string transactionId = Convert.ToString(e.CommandArgument);

        if (e.CommandName == "DownloadCandidates")
        {
            DownloadCandidatesExcel(paymentDetails, transactionId);
            return;
        }

        if (e.CommandName == "SelectCandidates")
        {
            OpenCandidatePanel(paymentDetails, transactionId);
            return;
        }

        if (e.CommandName == "ToggleVerify")
        {
            // Button click: flip the current state
            DataRow current = FindPaymentRow(paymentDetails, transactionId);
            bool newState = current == null || !IsVerified(current["Exam_Details_Verified"]);
            SetExamVerification(transactionId, newState);
            return;
        }

        DataRow receiptRow = null;
        foreach (DataRow row in paymentDetails.Rows)
        {
            if (Convert.ToString(row["Transaction_id"]) == transactionId)
            {
                receiptRow = row;
                break;
            }
        }

        if (receiptRow == null)
        {
            lbl_ErrorMsg.Text = "Receipt details were not found for the selected payment.";
            return;
        }

        ShowReceipt(receiptRow);
    }

    // Checkbox click: set state to whatever the checkbox now shows
    protected void chkVerified_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chk = (CheckBox)sender;
        GridViewRow row = (GridViewRow)chk.NamingContainer;
        string transactionId = Convert.ToString(grdPaymentDetails.DataKeys[row.RowIndex].Value);
        SetExamVerification(transactionId, chk.Checked);
    }

    protected bool IsVerified(object value)
    {
        if (value == null || value == DBNull.Value)
        {
            return false;
        }

        string text = Convert.ToString(value);
        bool result;
        return text == "1" || (bool.TryParse(text, out result) && result);
    }

    private DataRow FindPaymentRow(DataTable paymentDetails, string transactionId)
    {
        foreach (DataRow row in paymentDetails.Rows)
        {
            if (Convert.ToString(row["Transaction_id"]) == transactionId)
            {
                return row;
            }
        }

        return null;
    }

    private void SetExamVerification(string transactionId, bool verified)
    {
        lbl_ErrorMsg.Text = string.Empty;

        // Safety lock: finance can never change verification, even if someone fakes a click
        if (IsFinanceUser)
        {
            lbl_ErrorMsg.Text = "Finance users can only view verified payments.";
            return;
        }

        DataTable paymentDetails = ViewState[PaymentDetailsViewStateKey] as DataTable;
        if (paymentDetails == null)
        {
            lbl_ErrorMsg.Text = "Please search payment details again.";
            return;
        }

        // Only rows already returned (and role-filtered) for this user can be changed
        DataRow row = FindPaymentRow(paymentDetails, transactionId);
        if (row == null)
        {
            lbl_ErrorMsg.Text = "The selected payment was not found. Please search again.";
            return;
        }

        try
        {
            string connectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("dbo.UpdateExamDetailsVerification", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                // Transaction_id is an INT in Online_Transaction, so we send an INT
                cmd.Parameters.Add("@TransactionId", SqlDbType.Int).Value = Convert.ToInt32(transactionId);
                cmd.Parameters.Add("@IsVerified", SqlDbType.Bit).Value = verified;
                cmd.Parameters.Add("@VerifiedBy", SqlDbType.VarChar, 100).Value = Convert.ToString(Session["UserId"]);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
        catch (Exception)
        {
            lbl_ErrorMsg.Text = "Unable to update verification status. Please try again.";
            return;
        }

        row["Exam_Details_Verified"] = verified;
        ViewState[PaymentDetailsViewStateKey] = paymentDetails;
        BindGrid(paymentDetails);
    }

    // Text shown on the "Verify Candidates" link, e.g. "Select Candidates (5/10)"
    protected string GetCandidateButtonText(object selected, object total)
    {
        int s = 0, t = 0;
        Int32.TryParse(Convert.ToString(selected), out s);
        Int32.TryParse(Convert.ToString(total), out t);
        return "Select Candidates (" + s + "/" + t + ")";
    }

    // Asks the database for the candidates of one payment.
    // Finance gets ONLY the ticked candidates, everybody else gets all of them.
    // Returned table has: Candidate_ID, ...details..., Is_Selected (bit)
    private DataTable LoadCandidates(string transactionId)
    {
        DataTable candidates = new DataTable();
        string connectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(connectionString))
        using (SqlCommand cmd = new SqlCommand("dbo.GetCandidatesByTransaction", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@TransactionId", SqlDbType.Int).Value = Convert.ToInt32(transactionId);
            cmd.Parameters.Add("@OnlyVerified", SqlDbType.Bit).Value = IsFinanceUser;
            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
            {
                adapter.Fill(candidates);
            }
        }
        return candidates;
    }

    // Click on "Number of Candidates" -> Excel with ALL details of those candidates
    private void DownloadCandidatesExcel(DataTable paymentDetails, string transactionId)
    {
        lbl_ErrorMsg.Text = string.Empty;

        // Only payments already shown to this user (role-filtered) can be downloaded
        if (FindPaymentRow(paymentDetails, transactionId) == null)
        {
            lbl_ErrorMsg.Text = "The selected payment was not found. Please search again.";
            return;
        }

        DataTable candidates;
        try
        {
            candidates = LoadCandidates(transactionId);
        }
        catch (Exception)
        {
            lbl_ErrorMsg.Text = "Unable to load candidate details. Please contact the administrator.";
            return;
        }

        if (candidates.Rows.Count == 0)
        {
            lbl_ErrorMsg.Text = IsFinanceUser
                ? "No candidates have been selected for verification for this payment."
                : "No candidate details found for this payment.";
            return;
        }

        ExportAnyTableToExcel(candidates, "Candidates_Txn" + transactionId);
    }

    // ---------------- STEP 2: choose candidates ----------------

    // Opens the box that lists the candidates with a checkbox each
    private void OpenCandidatePanel(DataTable paymentDetails, string transactionId)
    {
        lbl_ErrorMsg.Text = string.Empty;
        lblCandMsg.Text = string.Empty;

        if (IsFinanceUser)
        {
            lbl_ErrorMsg.Text = "Finance users cannot select candidates.";
            return;
        }

        DataRow payment = FindPaymentRow(paymentDetails, transactionId);
        if (payment == null)
        {
            lbl_ErrorMsg.Text = "The selected payment was not found. Please search again.";
            return;
        }

        // step 1 must be finished first
        if (!IsVerified(payment["Exam_Details_Verified"]))
        {
            lbl_ErrorMsg.Text = "Please verify the exam details first, then select candidates.";
            return;
        }

        try
        {
            BindCandidateGrid(transactionId);
        }
        catch (Exception)
        {
            pnlCandidates.Visible = false;
            lbl_ErrorMsg.Text = "Unable to load candidate details. Please contact the administrator.";
            return;
        }

        ViewState["CandidateTxnId"] = transactionId;
        lblCandTitle.Text = "Select candidates for Transaction ID " + transactionId
            + " (" + GetColumnValue(payment, "Course_Name") + ")";
        pnlCandidates.Visible = true;
    }

    private void BindCandidateGrid(string transactionId)
    {
        DataTable candidates = LoadCandidates(transactionId);

        // remember who is already ticked, then hide that helper column from the screen
        System.Collections.Generic.HashSet<string> ticked = new System.Collections.Generic.HashSet<string>();
        foreach (DataRow r in candidates.Rows)
        {
            if (candidates.Columns.Contains("Is_Selected") && IsVerified(r["Is_Selected"]))
            {
                ticked.Add(Convert.ToString(r["Candidate_ID"]));
            }
        }
        if (candidates.Columns.Contains("Is_Selected"))
        {
            candidates.Columns.Remove("Is_Selected");
        }

        grdCandidates.DataSource = candidates;
        grdCandidates.DataBind();

        foreach (GridViewRow row in grdCandidates.Rows)
        {
            CheckBox chk = (CheckBox)row.FindControl("chkCand");
            chk.Checked = ticked.Contains(Convert.ToString(grdCandidates.DataKeys[row.RowIndex].Value));
        }
    }

    protected void btnCandSelectAll_Click(object sender, EventArgs e)
    {
        SetAllCandidateCheckboxes(true);
    }

    protected void btnCandClearAll_Click(object sender, EventArgs e)
    {
        SetAllCandidateCheckboxes(false);
    }

    private void SetAllCandidateCheckboxes(bool value)
    {
        foreach (GridViewRow row in grdCandidates.Rows)
        {
            ((CheckBox)row.FindControl("chkCand")).Checked = value;
        }
    }

    protected void btnCandClose_Click(object sender, EventArgs e)
    {
        pnlCandidates.Visible = false;
        lblCandMsg.Text = string.Empty;
    }

    // Excel of the candidates for the payment shown in the box
    protected void btnCandDownload_Click(object sender, EventArgs e)
    {
        DataTable paymentDetails = ViewState[PaymentDetailsViewStateKey] as DataTable;
        string transactionId = Convert.ToString(ViewState["CandidateTxnId"]);
        if (paymentDetails == null || transactionId.Length == 0)
        {
            lbl_ErrorMsg.Text = "Please search payment details again.";
            return;
        }

        DownloadCandidatesExcel(paymentDetails, transactionId);
    }

    // Save the ticked candidates
    protected void btnCandSave_Click(object sender, EventArgs e)
    {
        lbl_ErrorMsg.Text = string.Empty;
        lblCandMsg.Text = string.Empty;

        if (IsFinanceUser)
        {
            lbl_ErrorMsg.Text = "Finance users cannot select candidates.";
            return;
        }

        DataTable paymentDetails = ViewState[PaymentDetailsViewStateKey] as DataTable;
        string transactionId = Convert.ToString(ViewState["CandidateTxnId"]);
        DataRow payment = paymentDetails == null ? null : FindPaymentRow(paymentDetails, transactionId);
        if (payment == null || !IsVerified(payment["Exam_Details_Verified"]))
        {
            lbl_ErrorMsg.Text = "Please search again and verify the exam details first.";
            return;
        }

        // collect the ticked candidates
        DataTable ids = new DataTable();
        ids.Columns.Add("Candidate_ID", typeof(string));
        System.Collections.Generic.HashSet<string> added = new System.Collections.Generic.HashSet<string>();
        foreach (GridViewRow row in grdCandidates.Rows)
        {
            CheckBox chk = (CheckBox)row.FindControl("chkCand");
            string candidateId = Convert.ToString(grdCandidates.DataKeys[row.RowIndex].Value);
            // the same candidate is sent only once
            if (chk != null && chk.Checked && added.Add(candidateId))
            {
                ids.Rows.Add(candidateId);
            }
        }

        try
        {
            string connectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("dbo.SaveVerifiedCandidates", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@TransactionId", SqlDbType.Int).Value = Convert.ToInt32(transactionId);
                cmd.Parameters.Add("@VerifiedBy", SqlDbType.VarChar, 100).Value = Convert.ToString(Session["UserId"]);
                SqlParameter list = cmd.Parameters.Add("@Candidates", SqlDbType.Structured);
                list.TypeName = "dbo.CandidateIdList";
                list.Value = ids;
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
        catch (SqlException ex)
        {
            // 50000 = a message written on purpose in our own procedure
            lbl_ErrorMsg.Text = ex.Number == 50000 ? ex.Message : "Unable to save selected candidates. Please try again.";
            return;
        }
        catch (Exception)
        {
            lbl_ErrorMsg.Text = "Unable to save selected candidates. Please try again.";
            return;
        }

        // update the number shown on the main grid
        payment["Candidates_Selected"] = ids.Rows.Count;
        ViewState[PaymentDetailsViewStateKey] = paymentDetails;
        BindGrid(paymentDetails);

        lblCandMsg.Text = ids.Rows.Count + " candidate(s) saved for verification.";
    }

    // Writes every column of any table into an Excel (tab separated) download
    private void ExportAnyTableToExcel(DataTable table, string fileNamePrefix)
    {
        StringBuilder content = new StringBuilder();
        int index;
        for (index = 0; index < table.Columns.Count; index++)
        {
            content.Append(SanitizeExcelValue(table.Columns[index].ColumnName));
            if (index < table.Columns.Count - 1)
            {
                content.Append('\t');
            }
        }
        content.AppendLine();

        foreach (DataRow row in table.Rows)
        {
            for (index = 0; index < table.Columns.Count; index++)
            {
                content.Append(SanitizeExcelValue(GetColumnValue(row, table.Columns[index].ColumnName)));
                if (index < table.Columns.Count - 1)
                {
                    content.Append('\t');
                }
            }
            content.AppendLine();
        }

        string fileName = fileNamePrefix + "_" + DateTime.Now.ToString("yyyyMMddHHmmss", CultureInfo.InvariantCulture) + ".xls";

        Response.Clear();
        Response.Buffer = true;
        Response.Charset = string.Empty;
        Response.ContentType = "application/vnd.ms-excel";
        Response.AppendHeader("Content-Disposition", "attachment; filename=\"" + fileName + "\"");
        Response.Write(content.ToString());
        _exportingExcel = true;
        HttpContext.Current.ApplicationInstance.CompleteRequest();
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

    private void BindPaymentDetails(DateTime? fromDate, DateTime? toDate)
    {
        int organisationCode = GetOrganisationCode();
        int scheduleId = Convert.ToInt32(ddlSchedule.SelectedValue);
        string roleName = GetPaymentRoleName();

        using (DataTable paymentDetails = objPay.GetCourseWisePaymentDetails(roleName, organisationCode, scheduleId, fromDate, toDate))
        {
            NormalizePaymentDetailColumns(paymentDetails);
            AddDisplayDateColumns(paymentDetails);
            ViewState[PaymentDetailsViewStateKey] = paymentDetails.Copy();
            BindGrid(paymentDetails);
        }
    }

    private void BindGrid(DataTable paymentDetails)
    {
        grdPaymentDetails.DataSource = paymentDetails;
        grdPaymentDetails.DataBind();
        grdPaymentDetails.Visible = paymentDetails != null;
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

    private void NormalizePaymentDetailColumns(DataTable paymentDetails)
    {
        if (!paymentDetails.Columns.Contains("Exam_Details_Verified"))
        {
            paymentDetails.Columns.Add("Exam_Details_Verified", typeof(bool));
        }

        if (!paymentDetails.Columns.Contains("Candidates_Selected"))
        {
            paymentDetails.Columns.Add("Candidates_Selected", typeof(int));
        }

        EnsureDisplayColumn(paymentDetails, "GST_Number", "GST_No");
        EnsureDisplayColumn(paymentDetails, "Payee_Pincode", "Pin_Code");
        EnsureDisplayColumn(paymentDetails, "Payee_Address", "Complete_Address");
        EnsureDisplayColumn(paymentDetails, "No_of_candidates", "NoOfCandidates");
    }

    private static void EnsureDisplayColumn(DataTable paymentDetails, string sourceColumnName, string displayColumnName)
    {
        if (!paymentDetails.Columns.Contains(displayColumnName))
        {
            paymentDetails.Columns.Add(displayColumnName, typeof(string));
        }

        bool sourceColumnExists = paymentDetails.Columns.Contains(sourceColumnName);
        foreach (DataRow row in paymentDetails.Rows)
        {
            if (sourceColumnExists && row[sourceColumnName] != DBNull.Value)
            {
                row[displayColumnName] = Convert.ToString(row[sourceColumnName]);
            }
            else if (row[displayColumnName] == DBNull.Value)
            {
                row[displayColumnName] = string.Empty;
            }
        }
    }

    private DateTime? ParseDate(string value, string fieldName)
    {
        if (value.Length == 0)
        {
            return null;
        }

        DateTime parsedDate;
        if (!DateTime.TryParseExact(value, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate))
        {
            lbl_ErrorMsg.Text = fieldName + " must be in dd/MM/yyyy format.";
            return null;
        }

        return parsedDate;
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

        if (roleList.Contains("ADMIN"))
        {
            return "ADMIN";
        }

        if (roleList.Contains("PAYEE"))
        {
            return "PAYEE";
        }

        if (roleList.Contains("SPON"))
        {
            return "SPONSORING";
        }

        return roleList;
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

    private void ExportPaymentDetailsToExcel(DataTable paymentDetails)
    {
        string[] columns = new[]
        {
            "Course_Name",
            "CourseSchedule",
            "Sponsoring_Organisation",
            "Payee_Organisation_Name",
            "ProformaInvoiceNo",
            "Transaction_id",
            "Reference_No",
            "NoOfCandidates",
            "Amount_Recieved",
            "PaymentDate",
            "PaymentTime",
            "Payment_Status",
            "ResponseDescription",
            "GST_No",
            "Pin_Code",
            "Complete_Address"
        };

        string[] headers = new[]
        {
            "Course",
            "Course Schedule",
            "Sponsoring Organisation",
            "Payee Organisation",
            "Proforma Invoice No.",
            "Transaction ID",
            "Reference No.",
            "No. of Candidates",
            "Amount",
            "Payment Date",
            "Payment Time",
            "Payment Status",
            "Payment Description",
            "GST No.",
            "Pin Code",
            "Complete Address"
        };

        StringBuilder content = new StringBuilder();
        int index;
        for (index = 0; index < headers.Length; index++)
        {
            content.Append(headers[index]);
            if (index < headers.Length - 1)
            {
                content.Append('\t');
            }
        }

        content.AppendLine();

        foreach (DataRow row in paymentDetails.Rows)
        {
            for (index = 0; index < columns.Length; index++)
            {
                content.Append(SanitizeExcelValue(GetColumnValue(row, columns[index])));
                if (index < columns.Length - 1)
                {
                    content.Append('\t');
                }
            }

            content.AppendLine();
        }

        string fileName = "CourseWisePaymentDetails_" + DateTime.Now.ToString("yyyyMMddHHmmss", CultureInfo.InvariantCulture) + ".xls";

        Response.Clear();
        Response.Buffer = true;
        Response.Charset = string.Empty;
        Response.ContentType = "application/vnd.ms-excel";
        Response.AppendHeader("Content-Disposition", "attachment; filename=\"" + fileName + "\"");
        Response.Write(content.ToString());
        //Response.Flush();
        _exportingExcel = true;
        HttpContext.Current.ApplicationInstance.CompleteRequest();
    }

    private static string SanitizeExcelValue(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return string.Empty;
        }

        string cleanedValue = value.Replace("\r", " ").Replace("\n", " ").Replace("\t", " ");
        if (cleanedValue.StartsWith("=") || cleanedValue.StartsWith("+") || cleanedValue.StartsWith("-") || cleanedValue.StartsWith("@"))
        {
            cleanedValue = "'" + cleanedValue;
        }

        return cleanedValue;
    }

    private string GetColumnValue(DataRow row, string columnName)
    {
        if (!row.Table.Columns.Contains(columnName) || row[columnName] == DBNull.Value)
        {
            return string.Empty;
        }

        return Convert.ToString(row[columnName]);
    }
}



/*
=====================================================================
 STORY OF THIS CODE FILE (explained like you are 10 years old)
 Last updated: 19 Nov 2026 (FINAL version)
=====================================================================

 WHAT THIS FILE IS
 -----------------
 The .aspx file is the "face" (what you see). This file is the "brain"
 (what happens when you click).

 WHAT WE HAD ALREADY DONE
 ------------------------
 1. List of successful payments, View receipt, Download Excel of the list.
 2. "Verify Exam Details" checkbox + button (red <-> green), saved by
    UpdateExamDetailsVerification (who + when too).
 3. FINANCE = look only (IsFinanceUser + a lock in SetExamVerification);
    the database also shows finance only Verified payments.
 4. The first SQL is running on the live database.

 WHAT WE DID NOW (FINAL)
 -----------------------
 1. DownloadCandidatesExcel(): click on "Number of Candidates" -> asks the
    database (GetCandidatesByTransaction) for all candidates of that payment
    and ExportAnyTableToExcel() writes ALL columns to Excel.
    The database joins NOMINATIONS_FOR_COURSES + Candidate_Registration +
    Candidate_Registration_Screeners, so different column names in those
    tables are already handled inside the SQL.
 2. OpenCandidatePanel(): the "Verify Candidates" link opens a box with a
    checkbox per candidate - only after the payment is "Verified".
 3. btnCandSave_Click(): collects the ticked candidates (each only once) and
    sends them to SaveVerifiedCandidates. The database also checks that they
    really belong to that payment. The "5/10" count on the grid updates.
 4. Finance: cannot open the box or save (checked here and in the database).
    Finance's candidate Excel has ONLY the ticked candidates
    (LoadCandidates sends @OnlyVerified = 1 for finance).
 5. Only the two page files changed - BCASClassLibrary is untouched.

 WHAT IS LEFT TO DO
 ------------------
 1. Backup, then run CourseWisePaymentDetails_FINAL_LiveDB_Script.sql on
    LIVE before uploading these two page files.
 2. Run the checks at the bottom of the SQL file. Check (b) must return no
    rows. If it does, the invoice <-> nomination link needs a fix.
 3. Test: admin, payee/sponsoring, finadmin.
 4. Ask Varun sir: should ticked candidates be cleared if a payment goes
    back to "Not Verified"? (Now they are kept.)
 5. Later idea: show names instead of codes (Sex_Code, Designation_Code ...)
    in the Excel by joining the master tables.
=====================================================================
*/
