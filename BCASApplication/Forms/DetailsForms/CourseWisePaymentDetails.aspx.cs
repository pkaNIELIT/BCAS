using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Web;
using System.Web.Security;
using System.Web.UI.WebControls;
using BCASClassLibrary;

public partial class Forms_DetailsForms_CourseWisePaymentDetails : System.Web.UI.Page
{
    private const string PaymentDetailsViewStateKey = "CourseWisePaymentDetails";
    private readonly TYP_PayeeOrganisationPayment objPay = new TYP_PayeeOrganisationPayment();

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
        BindPaymentDetails(null, null);
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

    protected void grdPaymentDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName != "ShowReceipt")
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

    private string GetColumnValue(DataRow row, string columnName)
    {
        if (!row.Table.Columns.Contains(columnName) || row[columnName] == DBNull.Value)
        {
            return string.Empty;
        }

        return Convert.ToString(row[columnName]);
    }
}
