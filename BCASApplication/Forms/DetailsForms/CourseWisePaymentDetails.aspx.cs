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
            // Plain SQL inside the page (no stored procedure needed on the server)
            using (SqlConnection con = new SqlConnection(GetConnectionString()))
            using (SqlCommand cmd = new SqlCommand(@"UPDATE dbo.Online_Transaction
SET Exam_Details_Verified    = @IsVerified,
    Exam_Details_Verified_By = CASE WHEN @IsVerified = 1 THEN @VerifiedBy ELSE NULL END,
    Exam_Details_Verified_On = CASE WHEN @IsVerified = 1 THEN GETDATE()    ELSE NULL END
WHERE Transaction_id = @TransactionId", con))
            {
                cmd.CommandType = CommandType.Text;
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

    private static string GetConnectionString()
    {
        return ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
    }

    // Text shown on the "Verify Candidates" link, e.g. "Select Candidates (5/10)".
    // Finance can only LOOK, so for finance it says "View Candidates (5/10)".
    protected string GetCandidateButtonText(object selected, object total)
    {
        int s = 0, t = 0;
        Int32.TryParse(Convert.ToString(selected), out s);
        Int32.TryParse(Convert.ToString(total), out t);
        return (IsFinanceUser ? "View Candidates (" : "Select Candidates (") + s + "/" + t + ")";
    }

    // Asks the database for the candidates of one payment (plain SQL, no stored procedure).
    // Finance gets ONLY the ticked candidates (and only of Verified payments),
    // everybody else gets all of them.
    // Returned table has: Candidate_ID, ...details..., Is_Selected (bit)
    private DataTable LoadCandidates(string transactionId)
    {
        const string sql = @"
SELECT  cand.*,
        CAST(CASE WHEN v.Candidate_ID IS NULL THEN 0 ELSE 1 END AS BIT) AS Is_Selected
FROM
(
    SELECT
        CAST(n.Registration_No AS VARCHAR(50))                          AS Candidate_ID,
        CASE WHEN cr.Registration_Number  IS NOT NULL THEN 'Regular'
             WHEN crs.Registration_Number IS NOT NULL THEN 'Screener'
             ELSE 'Registration not found' END                         AS Candidate_Type,
        COALESCE(cr.Candidate_Name, crs.Candidate_Name)                 AS Candidate_Name,
        COALESCE(cr.Employee_ID, crs.Employee_ID)                       AS Employee_ID,
        COALESCE(cr.Sex_Code, crs.Sex_Code)                             AS Sex_Code,
        COALESCE(cr.Designation_Code, crs.Designation_Code)             AS Designation_Code,
        CONVERT(VARCHAR(10), COALESCE(cr.DOB, crs.DOB), 103)            AS DOB,
        COALESCE(cr.Email_Id, crs.Email_Id)                             AS Email_Id,
        COALESCE(cr.Mobile_No, crs.Mobile_No)                           AS Mobile_No,
        COALESCE(cr.Nationality_Code, crs.Nationality_Code)             AS Nationality_Code,
        COALESCE(cr.Educational_Qualification_Code, crs.Educational_Qualification_Code) AS Educational_Qualification_Code,
        COALESCE(cr.Aadhaar_Number, crs.Aadhaar_Number)                 AS Aadhaar_Number,
        COALESCE(cr.OrganisationNameOf_Candidate, crs.OrganisationNameOf_Candidate) AS OrganisationNameOf_Candidate,
        COALESCE(cr.Organisation_Code, crs.Organisation_Code)           AS Organisation_Code,
        COALESCE(cr.BillerOrganisation_Code, crs.BillerOrganisation_Code) AS BillerOrganisation_Code,
        COALESCE(cr.PostedAtStateName, crs.PostedAtStateName)           AS PostedAtStateName,
        COALESCE(cr.PostedAtCityName, crs.PostedAtCityName)             AS PostedAtCityName,
        COALESCE(cr.Is_Application_Complete, crs.Is_Application_Complete) AS Is_Application_Complete,
        COALESCE(cr.Is_Relieved, crs.Is_Relieved)                       AS Is_Relieved,
        COALESCE(cr.Relieve_Remarks, crs.Relieve_Remarks)               AS Relieve_Remarks,
        COALESCE(cr.EbasId, crs.EbasId)                                 AS EbasId,
        COALESCE(cr.BcasId, crs.BcasId)                                 AS BcasId,
        COALESCE(cr.Photo_Filename, crs.Photo_Filename)                 AS Photo_Filename,
        COALESCE(cr.Signature_Filename, crs.Signature_Filename)         AS Signature_Filename,
        COALESCE(cr.PhotoIcard_Filename, crs.PhotoIcard_Filename)       AS PhotoIcard_Filename,
        n.Schedule_ID                                                   AS Nomination_Schedule_ID,
        n.Demand_Note_ID,
        n.Nominating_OrgCode,
        n.Nominated_By,
        CONVERT(VARCHAR(10), n.Nominating_Date, 103)                    AS Nominating_Date,
        n.BCAS_Approval_Status,
        n.BCAS_Approval_By,
        CONVERT(VARCHAR(10), n.BCAS_Approval_Date, 103)                 AS BCAS_Approval_Date,
        n.BCAS_Remarks,
        n.Exam_Location_Code,
        n.Whether_Reappearing,
        n.Fee_Amount,
        n.Service_Tax,
        n.Total_Fee_With_Service_Tax,
        n.Fee_Status,
        CONVERT(VARCHAR(10), n.Fee_Payment_Date, 103)                   AS Fee_Payment_Date
    FROM dbo.Online_Transaction ot
    INNER JOIN dbo.Proforma_Invoice pi
            ON ot.ProformaInvoiceID = pi.ID
    INNER JOIN dbo.NOMINATIONS_FOR_COURSES n
            ON n.Demand_Note_ID = pi.DemandNoteId
           AND n.Schedule_ID    = pi.Schedule_ID
    LEFT JOIN dbo.Candidate_Registration cr
           ON CAST(cr.Registration_Number AS VARCHAR(50)) = CAST(n.Registration_No AS VARCHAR(50))
    LEFT JOIN dbo.Candidate_Registration_Screeners crs
           ON CAST(crs.Registration_Number AS VARCHAR(50)) = CAST(n.Registration_No AS VARCHAR(50))
    WHERE ot.Transaction_id = @TransactionId
      AND (@OnlyVerified = 0 OR ot.Exam_Details_Verified = 1)
) cand
LEFT JOIN dbo.Exam_Candidate_Verification v
       ON v.Transaction_id = @TransactionId
      AND v.Candidate_ID   = cand.Candidate_ID
WHERE   @OnlyVerified = 0 OR v.Candidate_ID IS NOT NULL
ORDER BY cand.Candidate_Name, cand.Candidate_ID";

        DataTable candidates = new DataTable();
        using (SqlConnection con = new SqlConnection(GetConnectionString()))
        using (SqlCommand cmd = new SqlCommand(sql, con))
        {
            cmd.CommandType = CommandType.Text;
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

    // Opens the box that lists the candidates.
    // Admin / Payee / Sponsoring: every candidate with a checkbox (to tick + save).
    // Finance: ONLY the ticked candidates, no checkboxes, no Save (look only).
    private void OpenCandidatePanel(DataTable paymentDetails, string transactionId)
    {
        lbl_ErrorMsg.Text = string.Empty;
        lblCandMsg.Text = string.Empty;

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

        bool finance = IsFinanceUser;

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

        // finance only looks: hide the buttons that change things
        btnCandSelectAll.Visible = !finance;
        btnCandClearAll.Visible = !finance;
        btnCandSave.Visible = !finance;

        ViewState["CandidateTxnId"] = transactionId;
        lblCandTitle.Text = (finance ? "Candidates selected for verification - Transaction ID " : "Select candidates for Transaction ID ")
            + transactionId + " (" + GetColumnValue(payment, "Course_Name") + ")";

        if (finance && grdCandidates.Rows.Count == 0)
        {
            lblCandMsg.Text = "No candidates have been selected for verification for this payment yet.";
        }

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

        // finance does not get the tick-box column
        grdCandidates.Columns[0].Visible = !IsFinanceUser;

        grdCandidates.DataSource = candidates;
        grdCandidates.DataBind();

        foreach (GridViewRow row in grdCandidates.Rows)
        {
            CheckBox chk = row.FindControl("chkCand") as CheckBox;
            if (chk != null)
            {
                chk.Checked = ticked.Contains(Convert.ToString(grdCandidates.DataKeys[row.RowIndex].Value));
            }
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
        if (IsFinanceUser)
        {
            return;
        }

        foreach (GridViewRow row in grdCandidates.Rows)
        {
            CheckBox chk = row.FindControl("chkCand") as CheckBox;
            if (chk != null)
            {
                chk.Checked = value;
            }
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

        // Safety lock: finance can never save, even if someone fakes a click
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

        // collect the ticked candidates (each only once)
        System.Collections.Generic.List<string> ticked = new System.Collections.Generic.List<string>();
        System.Collections.Generic.HashSet<string> added = new System.Collections.Generic.HashSet<string>();
        foreach (GridViewRow row in grdCandidates.Rows)
        {
            CheckBox chk = row.FindControl("chkCand") as CheckBox;
            string candidateId = Convert.ToString(grdCandidates.DataKeys[row.RowIndex].Value);
            if (chk != null && chk.Checked && added.Add(candidateId))
            {
                ticked.Add(candidateId);
            }
        }

        string error = SaveTickedCandidates(transactionId, ticked);
        if (error != null)
        {
            lbl_ErrorMsg.Text = error;
            return;
        }

        // update the number shown on the main grid
        payment["Candidates_Selected"] = ticked.Count;
        ViewState[PaymentDetailsViewStateKey] = paymentDetails;
        BindGrid(paymentDetails);

        lblCandMsg.Text = ticked.Count + " candidate(s) saved for verification.";
    }

    // Saves EXACTLY the ticked list for one payment (plain SQL, one SQL transaction).
    // Returns null when all is fine, otherwise the message to show on the page.
    // The same safety checks the old stored procedure did:
    //   1. the payment must be "Verified"
    //   2. every ticked candidate must really belong to this payment
    private string SaveTickedCandidates(string transactionId, System.Collections.Generic.List<string> ticked)
    {
        try
        {
            int txn = Convert.ToInt32(transactionId);
            string user = Convert.ToString(Session["UserId"]);

            using (SqlConnection con = new SqlConnection(GetConnectionString()))
            {
                con.Open();
                using (SqlTransaction tx = con.BeginTransaction())
                {
                    // 1. payment must be Verified
                    using (SqlCommand cmd = new SqlCommand(
                        "SELECT COUNT(*) FROM dbo.Online_Transaction WHERE Transaction_id = @T AND Exam_Details_Verified = 1", con, tx))
                    {
                        cmd.Parameters.Add("@T", SqlDbType.Int).Value = txn;
                        if (Convert.ToInt32(cmd.ExecuteScalar()) == 0)
                        {
                            return "Please verify the exam details first, then select candidates.";
                        }
                    }

                    // 2. candidates that really belong to this payment
                    System.Collections.Generic.HashSet<string> valid = new System.Collections.Generic.HashSet<string>();
                    using (SqlCommand cmd = new SqlCommand(@"SELECT DISTINCT CAST(n.Registration_No AS VARCHAR(50))
FROM dbo.Online_Transaction ot
INNER JOIN dbo.Proforma_Invoice pi ON ot.ProformaInvoiceID = pi.ID
INNER JOIN dbo.NOMINATIONS_FOR_COURSES n ON n.Demand_Note_ID = pi.DemandNoteId AND n.Schedule_ID = pi.Schedule_ID
WHERE ot.Transaction_id = @T", con, tx))
                    {
                        cmd.Parameters.Add("@T", SqlDbType.Int).Value = txn;
                        using (SqlDataReader rd = cmd.ExecuteReader())
                        {
                            while (rd.Read())
                            {
                                valid.Add(Convert.ToString(rd[0]));
                            }
                        }
                    }

                    foreach (string id in ticked)
                    {
                        if (!valid.Contains(id))
                        {
                            return "One or more selected candidates do not belong to this payment.";
                        }
                    }

                    // 3. who is already saved?
                    System.Collections.Generic.HashSet<string> existing = new System.Collections.Generic.HashSet<string>();
                    using (SqlCommand cmd = new SqlCommand(
                        "SELECT Candidate_ID FROM dbo.Exam_Candidate_Verification WHERE Transaction_id = @T", con, tx))
                    {
                        cmd.Parameters.Add("@T", SqlDbType.Int).Value = txn;
                        using (SqlDataReader rd = cmd.ExecuteReader())
                        {
                            while (rd.Read())
                            {
                                existing.Add(Convert.ToString(rd[0]));
                            }
                        }
                    }

                    System.Collections.Generic.HashSet<string> tickedSet = new System.Collections.Generic.HashSet<string>(ticked);

                    // 4. remove the un-ticked ones
                    foreach (string id in existing)
                    {
                        if (!tickedSet.Contains(id))
                        {
                            using (SqlCommand cmd = new SqlCommand(
                                "DELETE FROM dbo.Exam_Candidate_Verification WHERE Transaction_id = @T AND Candidate_ID = @C", con, tx))
                            {
                                cmd.Parameters.Add("@T", SqlDbType.Int).Value = txn;
                                cmd.Parameters.Add("@C", SqlDbType.VarChar, 50).Value = id;
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }

                    // 5. add the newly ticked ones
                    foreach (string id in ticked)
                    {
                        if (!existing.Contains(id))
                        {
                            using (SqlCommand cmd = new SqlCommand(
                                "INSERT INTO dbo.Exam_Candidate_Verification (Transaction_id, Candidate_ID, Verified_By) VALUES (@T, @C, @U)", con, tx))
                            {
                                cmd.Parameters.Add("@T", SqlDbType.Int).Value = txn;
                                cmd.Parameters.Add("@C", SqlDbType.VarChar, 50).Value = id;
                                cmd.Parameters.Add("@U", SqlDbType.VarChar, 100).Value = user;
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }

                    tx.Commit();
                }
            }
        }
        catch (Exception)
        {
            return "Unable to save selected candidates. Please try again.";
        }

        return null;
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

        using (DataTable paymentDetails = LoadPaymentDetails(roleName, organisationCode, scheduleId, fromDate, toDate))
        {
            NormalizePaymentDetailColumns(paymentDetails);
            AddDisplayDateColumns(paymentDetails);
            ViewState[PaymentDetailsViewStateKey] = paymentDetails.Copy();
            BindGrid(paymentDetails);
        }
    }

    // The payments list (plain SQL, same as the old stored procedure + 2 new things):
    //   * returns Exam_Details_Verified and Candidates_Selected
    //   * FINANCE sees ONLY payments that are Verified
    private DataTable LoadPaymentDetails(string roleName, int organisationCode, int scheduleId, DateTime? fromDate, DateTime? toDate)
    {
        const string sql = @"
SELECT
    cm.Course_Name,
    cs.Schedule_ID,
    pi.Payee_OrgCode,
    cm.Course_Name + ' - Exam Date: ' + CONVERT(VARCHAR(10), cs.Date_of_Exam, 103) AS CourseSchedule,
    ot.Transaction_id,
    ot.Reference_No,
    ot.Amount_Recieved,
    ot.Response_Date AS Date_of_Payment,
    CASE WHEN ot.Response_Status_Code = '0300' THEN 'DONE' ELSE 'NOT DONE' END AS Payment_Status,
    ot.Response_Status_Message AS ResponseDescription,
    ot.Response_Parameters AS Response_String,
    CAST(NULL AS DATETIME) AS Settled_On,
    CAST(NULL AS VARCHAR(255)) AS Settled_FileName,
    ot.User_ID AS Payment_By,
    pom.Payee_Organisation_Name,
    pom.GSTIN AS GST_Number,
    pom.Payee_Location_Pincode AS Payee_Pincode,
    pom.Payee_address AS Payee_Address,
    pi.NoOfCandidates AS No_of_candidates,
    pom_sponsor.Organisation_Name AS Sponsoring_Organisation,
    pi.ProformaInvoiceNo,
    pi.NoOfCandidates,
    pi.Fee_Amount,
    pi.Service_Tax,
    pi.Total_Fee_With_Service_Tax,
    upload.InvoiceUploadFile,
    ISNULL(upload.Upload_Status, 'Not Uploaded') AS Upload_Status,
    ISNULL(ot.Exam_Details_Verified, 0) AS Exam_Details_Verified,
    (SELECT COUNT(*) FROM dbo.Exam_Candidate_Verification ecv
      WHERE ecv.Transaction_id = ot.Transaction_id) AS Candidates_Selected
FROM Online_Transaction ot
INNER JOIN Proforma_Invoice pi
    ON ot.ProformaInvoiceID = pi.ID
    AND ot.Sponsoring_Organisation_Code = pi.Payee_OrgCode
INNER JOIN Course_Schedule cs
    ON pi.Schedule_ID = cs.Schedule_ID
INNER JOIN Course_Master cm
    ON cs.Course_Code = cm.Course_Code
JOIN Payee_Organisation_Master pom
    ON pi.Payee_OrgCode = pom.Payee_Organisation_Code
    AND ot.Sponsoring_Organisation_Code = pom.Payee_Organisation_Code
LEFT JOIN Sponsoring_Organisation_Master pom_sponsor
    ON pi.Sponsoring_OrgCode = pom_sponsor.Organisation_Code
LEFT JOIN [dbo].[TaxInvoice_Upload_Tracking] upload
    ON ot.Transaction_id = upload.Transaction_id
WHERE
    ot.Response_Status_Code = '0300'
    AND (@ScheduleID = 0 OR cs.Schedule_ID = @ScheduleID)
    AND (@FromDate IS NULL OR CAST(ot.Response_Date AS DATE) >= @FromDate)
    AND (@ToDate IS NULL OR CAST(ot.Response_Date AS DATE) <= @ToDate)
    AND
    (
        UPPER(ISNULL(@RoleName, '')) LIKE '%ADMIN%'
        OR
        (
            UPPER(ISNULL(@RoleName, '')) LIKE '%PAYEE%'
            AND ISNULL(pi.Payee_OrgCode, 0) = @OrganisationCode
        )
        OR
        (
            (
                UPPER(ISNULL(@RoleName, '')) LIKE '%SPON%'
                OR UPPER(ISNULL(@RoleName, '')) LIKE '%SPONSORING%'
            )
            AND ISNULL(ot.Sponsoring_Organisation_Code, 0) = @OrganisationCode
        )
        OR
        (
            -- finance sees ONLY verified payments
            UPPER(ISNULL(@RoleName, '')) LIKE '%FINANCE%'
            AND ISNULL(ot.Exam_Details_Verified, 0) = 1
        )
    )
ORDER BY
    ot.Response_Date DESC,
    ot.Transaction_id DESC";

        DataTable result = new DataTable();
        using (SqlConnection con = new SqlConnection(GetConnectionString()))
        using (SqlCommand cmd = new SqlCommand(sql, con))
        {
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@RoleName", SqlDbType.VarChar, 50).Value = roleName;
            cmd.Parameters.Add("@OrganisationCode", SqlDbType.Int).Value = organisationCode;
            cmd.Parameters.Add("@ScheduleID", SqlDbType.Int).Value = scheduleId;
            cmd.Parameters.Add("@FromDate", SqlDbType.Date).Value = fromDate.HasValue ? (object)fromDate.Value : DBNull.Value;
            cmd.Parameters.Add("@ToDate", SqlDbType.Date).Value = toDate.HasValue ? (object)toDate.Value : DBNull.Value;
            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
            {
                adapter.Fill(result);
            }
        }
        return result;
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
 Last updated: 19 Nov 2026 (SERVER version - plain SQL inside the page)
=====================================================================

 WHAT THIS FILE IS
 -----------------
 The .aspx file is the "face" (what you see). This file is the "brain"
 (what happens when you click).

 WHAT WE HAD ALREADY DONE
 ------------------------
 1. List of successful payments, View receipt, Download Excel of the list.
 2. "Verify Exam Details" checkbox + button (red <-> green); it saves who
    verified and when.
 3. FINANCE = look only (IsFinanceUser + a lock in SetExamVerification) and
    finance sees only Verified payments.
 4. "Number of Candidates" link = Excel of the candidates of that payment.
 5. "Verify Candidates" box: tick e.g. 5 of 10 candidates and save
    (only after the payment is Verified).
 6. Finance can open the box and SEE only the ticked candidates
    ("View Candidates (2/4)"), with no tick-boxes and no Save.

 WHAT WE DID NOW (SERVER VERSION)
 --------------------------------
 The server does not use new stored procedures, so the page now talks to
 the database with its own plain SQL (always with @parameters):
   * LoadPaymentDetails()   = the payments list (+ Exam_Details_Verified,
                              Candidates_Selected, finance sees only Verified)
                              It replaces the call to the class library.
   * SetExamVerification()  = UPDATE of the 3 verify columns
   * LoadCandidates()       = candidates of a payment (3 tables joined)
   * SaveTickedCandidates() = checks "payment is Verified" and "candidates
                              belong to this payment", then saves the ticks
                              in ONE SQL transaction
 The safety checks that were inside stored procedures now live here.
 The class library (BCASClassLibrary) is not used by this page any more.

 WHAT IS LEFT TO DO
 ------------------
 1. Run the small SQL script (3 columns, FINANCE page key, 1 table) on the
    server database, then upload the .aspx and this .aspx.cs file.
 2. Test with 3 logins: admin, payee/sponsoring, finadmin.
 3. Remember: old payments start as "Not Verified", so finance sees an empty
    list until somebody verifies payments.
 4. Ask Varun sir: if a payment goes back to "Not Verified", should the
    ticked candidates be cleared? (Now they are kept.)
 5. Later idea: show names instead of codes (Sex_Code, Designation_Code ...)
    in the Excel by joining the master tables.
=====================================================================
*/
