using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Threading;
using BCASClassLibrary;
using System.Data.SqlClient;
using System.Text;
using System.Web.Services.Description;

public partial class Forms_DetailsForms_BCAS_ProformaInvoice : System.Web.UI.Page
{
    TYP_BCAS_Approval objBCAS = new TYP_BCAS_Approval();
  
    Int64 DemandNoteId = 0;
    DateTime LastDate = DateTime.Today.AddDays(-2);
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            if ((Convert.ToString(HttpContext.Current.Session["UserId"]).Trim().Length == 0))
            {
                HttpContext.Current.Response.Redirect("~/Home.aspx");
            }
            else
            {

                using (DataTable dt = objBCAS.getBCASCourseSchedule())
                {
                    if (dt.Rows.Count > 0)
                    {
                        ddlSchedule.DataSource = dt;
                        ddlSchedule.DataTextField = "Course_Schedule"; // This is text displayed
                        ddlSchedule.DataValueField = "Schedule_ID"; // This is the value returned
                        ddlSchedule.DataBind();
                    }

                }
                //Added for biller Organisation
                using (DataSet  dtOrg = TYP_PAYEE_ORGANISATION_MASTER.GetAllPayee_Organisation_Master())
                {
                    if (dtOrg.Tables [0].Rows .Count > 0)
                    {
                        ddlBillerOrganisation.DataSource = dtOrg;
                        ddlBillerOrganisation.DataValueField = "Payee_Organisation_Code";
                        ddlBillerOrganisation.DataTextField = "Payee_Organisation_Name";
                        ddlBillerOrganisation.DataBind();


                        ddlBillerOrganisation.SelectedValue = Session["UserOrg"].ToString();
                    }

                }

                using (DataTable dtOrg = objBCAS.getGetAllOrganisation ())
                {
                    if (dtOrg.Rows.Count > 0)
                    {
                        ddlOrganisation.DataSource = dtOrg;
                        ddlOrganisation.DataValueField = "Organisation_Code";
                        ddlOrganisation.DataTextField = "Organisation_Name";
                        ddlOrganisation.DataBind();


                        ddlBillerOrganisation.SelectedValue = Session["UserOrg"].ToString();
                    }

                }
               
            }

        }       

    }    
    
    private void ShowMessage(int count)
    {

        StringBuilder sb = new StringBuilder();

        sb.Append("<script type = 'text/javascript'>");

        sb.Append("alert('");

        sb.Append(count.ToString());

        sb.Append(" records demand note Generated !');");

        sb.Append("</script>");

        ClientScript.RegisterStartupScript(this.GetType(),

                        "script", sb.ToString());

    }    
    
    private void BindGrid()
    {
        int scheduleID = Convert.ToInt32(ddlSchedule.SelectedValue);
        int organisation = Convert.ToInt32(ddlBillerOrganisation.SelectedValue);
        gvAll.Visible = true;

        // DLL call unchanged — returns AVSEC (and possibly screeners) candidates
        DataTable dt;
        using (DataTable basic = objBCAS.getBCASApporvedCandidates(scheduleID, organisation))
        {
            dt = basic.Copy();
        }

        // Build a set of registration numbers already in the DLL result
        var existing = new HashSet<int>();
        foreach (DataRow row in dt.Rows)
            existing.Add(Convert.ToInt32(row["Registration_Number"]));

        // Add screeners candidates that the DLL did not already return
        string conStr = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(conStr))
        using (SqlCommand cmd = new SqlCommand(@"
            SELECT
                nfc.Registration_No                AS Registration_Number,
                crs.Candidate_Name,
                crs.Sex_Code,
                ISNULL(dm.Description,  '')        AS Designation,
                ISNULL(qm.Description,  '')        AS Qualification,
                CONVERT(varchar(20), crs.DOB, 103) AS DOB,
                crs.Mobile_No,
                nfc.BCAS_Approval_Status,
                ISNULL(som.Organisation_Name, '')  AS Organisation_Name,
                nfc.Schedule_ID,
                nfc.Demand_Note_ID
            FROM NOMINATIONS_FOR_COURSES nfc
            INNER JOIN Candidate_Registration_Screeners crs
                   ON crs.Registration_Number = nfc.Registration_No
            LEFT  JOIN Designation_Master dm
                   ON dm.Designation_Code = crs.Designation_Code
            LEFT  JOIN Qualification_Master qm
                   ON qm.Qualification_Code = crs.Educational_Qualification_Code
            LEFT  JOIN Sponsoring_Organisation_Master som
                   ON som.Organisation_Code = crs.Organisation_Code
            WHERE nfc.Schedule_ID = @Schedule_ID
              AND (@Organisation_Code = 0 OR crs.BillerOrganisation_Code = @Organisation_Code)
              AND nfc.BCAS_Approval_Status = 'Y'", con))
        {
            cmd.Parameters.AddWithValue("@Schedule_ID", scheduleID);
            cmd.Parameters.AddWithValue("@Organisation_Code", organisation);
            con.Open();
            DataTable screeners = new DataTable();
            new SqlDataAdapter(cmd).Fill(screeners);
            foreach (DataRow row in screeners.Rows)
            {
                if (!existing.Contains(Convert.ToInt32(row["Registration_Number"])))
                    dt.ImportRow(row);
            }
        }

        // Remove any duplicate Registration_Numbers (keeps first occurrence)
        var seen = new HashSet<int>();
        var dupes = new List<DataRow>();
        foreach (DataRow row in dt.Rows)
        {
            int regNo = Convert.ToInt32(row["Registration_Number"]);
            if (!seen.Add(regNo))
                dupes.Add(row);
        }
        foreach (DataRow row in dupes)
            dt.Rows.Remove(row);

        gvAll.DataSource = dt;
        gvAll.DataBind();
    }
   
    protected void OnPaging(object sender, GridViewPageEventArgs e)
    {

        gvAll.PageIndex = e.NewPageIndex;        
        BindGrid(); 
    }


    protected void btnUpdateStatus_Click(object sender, EventArgs e)
    {
        if (ddlSchedule.SelectedValue == "0")
        {
            ShowAlertMessage("Please select Course Schedule !!");
            lblerror.Text = "Please select Course Schedule !!";
            return;
        } 
        if (ddlBillerOrganisation.SelectedValue == "0")
        {
            ShowAlertMessage("Please select Biller Organisation !!");
            lblerror.Text = "Please select Biller Organisation !!";
            return;
        }
         
        var a = gvAll.SelectedRow;
        int count = 0;
        int countRecord = 0;
        string  allRegNO = "NA";
        
        string ServiceID="BCAS01";
        foreach (GridViewRow item in gvAll.Rows)
        {
            CheckBox chk = (CheckBox)item.Cells[0].FindControl("chk");
            if (chk != null && chk.Checked)
            {
                countRecord++;            
                if (allRegNO == "NA")
                {

                    allRegNO = "'" + ((HyperLink)item.FindControl("Label1")).Text + "'";
                }
                else
                {
                    allRegNO = allRegNO + ", " + "'" + ((HyperLink)item.FindControl("Label1")).Text + "'";
                }
            }
        }
        
        int schId = Convert.ToInt32(ddlSchedule.SelectedValue);
       
          string constr = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            using (SqlConnection Conn = new SqlConnection(constr))
            {
                Conn.Open();
                var trans=Conn.BeginTransaction();  
                 try
                {

                PaymentLastDate(schId,  trans, Conn);

                if (LastDate >= DateTime.Today)
                {
                    GenerateDemandNote(schId, countRecord, ServiceID, trans, Conn);

                    foreach (GridViewRow item in gvAll.Rows)
                    {
                        CheckBox chk = (CheckBox)item.Cells[0].FindControl("chk");
                        if (chk != null && chk.Checked)
                        {
                            count++;
                            int reg = Convert.ToInt32(((HyperLink)item.FindControl("Label1")).Text);
                            int schid = Convert.ToInt32(item.Cells[11].Text);

                            UpdateCanData(reg, schid, DemandNoteId, trans, Conn);
                        }
                    }
                    trans.Commit();
                    ShowMessage(count);
                    //DataBind();
                    BindGrid();
                    if (Conn.State != ConnectionState.Closed)
                        Conn.Close();
                    ShowMessage(countRecord);
        


                }
                else 
                {
                    ShowAlertMessage("Last Date is Over !!");
                    lblerror.Text = "Last Date is Over !!";
                }

            }

                //GenerateDemandNote(schId, countRecord, ServiceID,trans,Conn);

                //foreach (GridViewRow item in gvAll.Rows)
                //{
                //    CheckBox chk = (CheckBox)item.Cells[0].FindControl("chk");
                //    if (chk != null && chk.Checked)
                //    {
                //        count++;
                //        UpdateCanData(Convert.ToInt32(((HyperLink)item.FindControl("Label1")).Text), Convert.ToInt32(item.Cells[10].Text), DemandNoteId,trans,Conn);
                //    }
                //}
                //trans.Commit();
                //ShowMessage(count);
                ////DataBind();
                //BindGrid();
                //if (Conn.State != ConnectionState.Closed )
                //           Conn.Close();
                //}
                 catch (Exception ex)
                 {
                     trans.Rollback();
                     trans.Dispose();
                     
                 }
            if (Conn.State != ConnectionState.Closed)
                Conn.Close();


        }
       
    }

    public void PaymentLastDate(int Schedule_ID,  SqlTransaction trans, SqlConnection Conn)
    {
        try
        {


            using (SqlCommand cmd = new SqlCommand("PaymentLastDate", Conn))
            {
                if (Conn.State != ConnectionState.Open)
                    Conn.Open();
                cmd.Transaction = trans;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Schedule_ID", Schedule_ID);
                
                cmd.Parameters.Add("@LastDate", SqlDbType.Date).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();

                string id = cmd.Parameters["@LastDate"].Value.ToString();

                LastDate = Convert.ToDateTime(id);

            }
        }

        catch (Exception ex)
        {
            trans.Rollback();
        }

    }

    public void GenerateDemandNote(int Schedule_ID, int countRecord, string ServiceID,SqlTransaction trans,SqlConnection Conn)
    {
        try
        {
            
           
                using (SqlCommand cmd = new SqlCommand("InsertDemandNote", Conn))
                {
                    if(Conn.State !=ConnectionState .Open )
                            Conn.Open();
                    cmd.Transaction = trans;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Schedule_ID", Schedule_ID);
                    cmd.Parameters.AddWithValue("@countRecord", countRecord);                  
                    cmd.Parameters.AddWithValue("@ServiceID", ServiceID);
                    string s = HttpContext.Current.Session["UserId"].ToString();
                    cmd.Parameters.AddWithValue("@Created_By", HttpContext.Current.Session["UserId"].ToString());
                    cmd.Parameters.Add("@DemandNoteid", SqlDbType.Int).Direction = ParameterDirection.Output; 
                    cmd.ExecuteNonQuery();

                    string id = cmd.Parameters["@DemandNoteid"].Value.ToString();

                    DemandNoteId = Convert.ToInt64(id);
                   
                }
            }
        
        catch (Exception ex)
        {
            trans.Rollback();
        }
    }
    private void UpdateCanData(int Registration_Number, int Schedule_ID, Int64 demandNoteId,SqlTransaction trans,SqlConnection Conn)
    {
       
            using (SqlCommand cmd = new SqlCommand("UpdateCandDemandNote", Conn))
            {

                if (Conn.State != ConnectionState.Open)
                    Conn.Open();
                cmd.Transaction = trans;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Schedule_ID", Schedule_ID);
                cmd.Parameters.AddWithValue("@Registration_Number", Registration_Number);
                cmd.Parameters.AddWithValue("@DemandNoteid", demandNoteId);
                cmd.ExecuteNonQuery();
               
            }
       

    }
    private void ShowAlertMessage(string msg)
    {

        StringBuilder sb = new StringBuilder();

        sb.Append("<script type = 'text/javascript'>");

        sb.Append("alert('");

        sb.Append(msg);

        sb.Append("</script>");

        ClientScript.RegisterStartupScript(this.GetType(),

                        "script", sb.ToString());

    }    

    protected void ddlSchedule_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGrid();
        lblerror.Text = "";
    }

    protected void ddlOrganisation_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblerror.Text = "";
        BindGrid();
    }
}

// using System;
// using System.Collections;
// using System.Configuration;
// using System.Data;
// using System.IO;
// using System.Web;
// using System.Web.Security;
// using System.Web.UI;
// using System.Web.UI.HtmlControls;
// using System.Web.UI.WebControls;
// using System.Web.UI.WebControls.WebParts;
// using System.Threading;
// using BCASClassLibrary;
// using System.Data.SqlClient;
// using System.Text;
// using System.Web.Services.Description;

// public partial class Forms_DetailsForms_BCAS_ProformaInvoice : System.Web.UI.Page
// {
//     TYP_BCAS_Approval objBCAS = new TYP_BCAS_Approval();
  
//     Int64 DemandNoteId = 0;
//     DateTime LastDate = DateTime.Today.AddDays(-2);
//     protected void Page_Load(object sender, EventArgs e)
//     {

//         if (!IsPostBack)
//         {
//             if ((Convert.ToString(HttpContext.Current.Session["UserId"]).Trim().Length == 0))
//             {
//                 HttpContext.Current.Response.Redirect("~/Home.aspx");
//             }
//             else
//             {

//                 using (DataTable dt = objBCAS.getBCASCourseSchedule())
//                 {
//                     if (dt.Rows.Count > 0)
//                     {
//                         ddlSchedule.DataSource = dt;
//                         ddlSchedule.DataTextField = "Course_Schedule"; // This is text displayed
//                         ddlSchedule.DataValueField = "Schedule_ID"; // This is the value returned
//                         ddlSchedule.DataBind();
//                     }

//                 }
//                 //Added for biller Organisation
//                 using (DataSet  dtOrg = TYP_PAYEE_ORGANISATION_MASTER.GetAllPayee_Organisation_Master())
//                 {
//                     if (dtOrg.Tables [0].Rows .Count > 0)
//                     {
//                         ddlBillerOrganisation.DataSource = dtOrg;
//                         ddlBillerOrganisation.DataValueField = "Payee_Organisation_Code";
//                         ddlBillerOrganisation.DataTextField = "Payee_Organisation_Name";
//                         ddlBillerOrganisation.DataBind();


//                         ddlBillerOrganisation.SelectedValue = Session["UserOrg"].ToString();
//                     }

//                 }

//                 using (DataTable dtOrg = objBCAS.getGetAllOrganisation ())
//                 {
//                     if (dtOrg.Rows.Count > 0)
//                     {
//                         ddlOrganisation.DataSource = dtOrg;
//                         ddlOrganisation.DataValueField = "Organisation_Code";
//                         ddlOrganisation.DataTextField = "Organisation_Name";
//                         ddlOrganisation.DataBind();


//                         ddlBillerOrganisation.SelectedValue = Session["UserOrg"].ToString();
//                     }

//                 }
               
//             }

//         }       

//     }    
    
//     private void ShowMessage(int count)
//     {

//         StringBuilder sb = new StringBuilder();

//         sb.Append("<script type = 'text/javascript'>");

//         sb.Append("alert('");

//         sb.Append(count.ToString());

//         sb.Append(" records demand note Generated !');");

//         sb.Append("</script>");

//         ClientScript.RegisterStartupScript(this.GetType(),

//                         "script", sb.ToString());

//     }    
    
//     private void BindGrid()
//     {
//         int scheduleID = Convert.ToInt32(ddlSchedule.SelectedValue);
//         int organisation = Convert.ToInt32(ddlBillerOrganisation.SelectedValue);
//         gvAll.Visible = true;

//         // Basic AVSEC candidates via DLL (joins Candidate_Registration only)
//         DataTable dt;
//         using (DataTable basic = objBCAS.getBCASApporvedCandidates(scheduleID, organisation))
//         {
//             dt = basic.Copy();
//         }

//         // Screeners candidates: Candidate_Registration_Screeners is not joined by the DLL SP,
//         // so query directly and merge the rows in.
//         string conStr = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
//         using (SqlConnection con = new SqlConnection(conStr))
//         using (SqlCommand cmd = new SqlCommand(@"
//             SELECT
//                 nfc.Registration_No          AS Registration_Number,
//                 crs.Candidate_Name,
//                 crs.Sex_Code,
//                 ISNULL(dm.Description,  '')  AS Designation,
//                 ISNULL(qm.Description,  '')  AS Qualification,
//                 CONVERT(varchar(20), crs.DOB, 103) AS DOB,
//                 crs.Mobile_No,
//                 nfc.BCAS_Approval_Status,
//                 ISNULL(som.Organisation_Name, '') AS Organisation_Name,
//                 nfc.Schedule_ID,
//                 nfc.Demand_Note_ID
//             FROM NOMINATIONS_FOR_COURSES nfc
//             INNER JOIN Candidate_Registration_Screeners crs
//                    ON crs.Registration_Number = nfc.Registration_No
//             LEFT  JOIN Designation_Master dm
//                    ON dm.Designation_Code = crs.Designation_Code
//             LEFT  JOIN Qualification_Master qm
//                    ON qm.Qualification_Code = crs.Educational_Qualification_Code
//             LEFT  JOIN Sponsoring_Organisation_Master som
//                    ON som.Organisation_Code = crs.Organisation_Code
//             WHERE nfc.Schedule_ID = @Schedule_ID
//               AND (@Organisation_Code = 0 OR crs.BillerOrganisation_Code = @Organisation_Code)
//               AND nfc.BCAS_Approval_Status = 'Y'", con))
//         {
//             cmd.Parameters.AddWithValue("@Schedule_ID", scheduleID);
//             cmd.Parameters.AddWithValue("@Organisation_Code", organisation);
//             con.Open();
//             DataTable screeners = new DataTable();
//             new SqlDataAdapter(cmd).Fill(screeners);
//             if (screeners.Rows.Count > 0)
//                 dt.Merge(screeners);
//         }

//         gvAll.DataSource = dt;
//         gvAll.DataBind();
//     }
   
//     protected void OnPaging(object sender, GridViewPageEventArgs e)
//     {

//         gvAll.PageIndex = e.NewPageIndex;        
//         BindGrid(); 
//     }


//     protected void btnUpdateStatus_Click(object sender, EventArgs e)
//     {
//         if (ddlSchedule.SelectedValue == "0")
//         {
//             ShowAlertMessage("Please select Course Schedule !!");
//             lblerror.Text = "Please select Course Schedule !!";
//             return;
//         } 
//         if (ddlBillerOrganisation.SelectedValue == "0")
//         {
//             ShowAlertMessage("Please select Biller Organisation !!");
//             lblerror.Text = "Please select Biller Organisation !!";
//             return;
//         }
         
//         var a = gvAll.SelectedRow;
//         int count = 0;
//         int countRecord = 0;
//         string  allRegNO = "NA";
        
//         string ServiceID="BCAS01";
//         foreach (GridViewRow item in gvAll.Rows)
//         {
//             CheckBox chk = (CheckBox)item.Cells[0].FindControl("chk");
//             if (chk != null && chk.Checked)
//             {
//                 countRecord++;            
//                 if (allRegNO == "NA")
//                 {

//                     allRegNO = "'" + ((HyperLink)item.FindControl("Label1")).Text + "'";
//                 }
//                 else
//                 {
//                     allRegNO = allRegNO + ", " + "'" + ((HyperLink)item.FindControl("Label1")).Text + "'";
//                 }
//             }
//         }
        
//         int schId = Convert.ToInt32(ddlSchedule.SelectedValue);
       
//           string constr = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
//             using (SqlConnection Conn = new SqlConnection(constr))
//             {
//                 Conn.Open();
//                 var trans=Conn.BeginTransaction();  
//                  try
//                 {

//                 PaymentLastDate(schId,  trans, Conn);

//                 if (LastDate >= DateTime.Today)
//                 {
//                     GenerateDemandNote(schId, countRecord, ServiceID, trans, Conn);

//                     foreach (GridViewRow item in gvAll.Rows)
//                     {
//                         CheckBox chk = (CheckBox)item.Cells[0].FindControl("chk");
//                         if (chk != null && chk.Checked)
//                         {
//                             count++;
//                             int reg = Convert.ToInt32(((HyperLink)item.FindControl("Label1")).Text);
//                             int schid = Convert.ToInt32(item.Cells[11].Text);

//                             UpdateCanData(reg, schid, DemandNoteId, trans, Conn);
//                         }
//                     }
//                     trans.Commit();
//                     ShowMessage(count);
//                     //DataBind();
//                     BindGrid();
//                     if (Conn.State != ConnectionState.Closed)
//                         Conn.Close();
//                     ShowMessage(countRecord);
        


//                 }
//                 else 
//                 {
//                     ShowAlertMessage("Last Date is Over !!");
//                     lblerror.Text = "Last Date is Over !!";
//                 }

//             }

//                 //GenerateDemandNote(schId, countRecord, ServiceID,trans,Conn);

//                 //foreach (GridViewRow item in gvAll.Rows)
//                 //{
//                 //    CheckBox chk = (CheckBox)item.Cells[0].FindControl("chk");
//                 //    if (chk != null && chk.Checked)
//                 //    {
//                 //        count++;
//                 //        UpdateCanData(Convert.ToInt32(((HyperLink)item.FindControl("Label1")).Text), Convert.ToInt32(item.Cells[10].Text), DemandNoteId,trans,Conn);
//                 //    }
//                 //}
//                 //trans.Commit();
//                 //ShowMessage(count);
//                 ////DataBind();
//                 //BindGrid();
//                 //if (Conn.State != ConnectionState.Closed )
//                 //           Conn.Close();
//                 //}
//                  catch (Exception ex)
//                  {
//                      trans.Rollback();
//                      trans.Dispose();
                     
//                  }
//             if (Conn.State != ConnectionState.Closed)
//                 Conn.Close();


//         }
       
//     }

//     public void PaymentLastDate(int Schedule_ID,  SqlTransaction trans, SqlConnection Conn)
//     {
//         try
//         {


//             using (SqlCommand cmd = new SqlCommand("PaymentLastDate", Conn))
//             {
//                 if (Conn.State != ConnectionState.Open)
//                     Conn.Open();
//                 cmd.Transaction = trans;
//                 cmd.CommandType = CommandType.StoredProcedure;
//                 cmd.Parameters.AddWithValue("@Schedule_ID", Schedule_ID);
                
//                 cmd.Parameters.Add("@LastDate", SqlDbType.Date).Direction = ParameterDirection.Output;
//                 cmd.ExecuteNonQuery();

//                 string id = cmd.Parameters["@LastDate"].Value.ToString();

//                 LastDate = Convert.ToDateTime(id);

//             }
//         }

//         catch (Exception ex)
//         {
//             trans.Rollback();
//         }

//     }

//     public void GenerateDemandNote(int Schedule_ID, int countRecord, string ServiceID,SqlTransaction trans,SqlConnection Conn)
//     {
//         try
//         {
            
           
//                 using (SqlCommand cmd = new SqlCommand("InsertDemandNote", Conn))
//                 {
//                     if(Conn.State !=ConnectionState .Open )
//                             Conn.Open();
//                     cmd.Transaction = trans;
//                     cmd.CommandType = CommandType.StoredProcedure;
//                     cmd.Parameters.AddWithValue("@Schedule_ID", Schedule_ID);
//                     cmd.Parameters.AddWithValue("@countRecord", countRecord);                  
//                     cmd.Parameters.AddWithValue("@ServiceID", ServiceID);
//                     string s = HttpContext.Current.Session["UserId"].ToString();
//                     cmd.Parameters.AddWithValue("@Created_By", HttpContext.Current.Session["UserId"].ToString());
//                     cmd.Parameters.Add("@DemandNoteid", SqlDbType.Int).Direction = ParameterDirection.Output; 
//                     cmd.ExecuteNonQuery();

//                     string id = cmd.Parameters["@DemandNoteid"].Value.ToString();

//                     DemandNoteId = Convert.ToInt64(id);
                   
//                 }
//             }
        
//         catch (Exception ex)
//         {
//             trans.Rollback();
//         }
//     }
//     private void UpdateCanData(int Registration_Number, int Schedule_ID, Int64 demandNoteId,SqlTransaction trans,SqlConnection Conn)
//     {
       
//             using (SqlCommand cmd = new SqlCommand("UpdateCandDemandNote", Conn))
//             {

//                 if (Conn.State != ConnectionState.Open)
//                     Conn.Open();
//                 cmd.Transaction = trans;
//                 cmd.CommandType = CommandType.StoredProcedure;
//                 cmd.Parameters.AddWithValue("@Schedule_ID", Schedule_ID);
//                 cmd.Parameters.AddWithValue("@Registration_Number", Registration_Number);
//                 cmd.Parameters.AddWithValue("@DemandNoteid", demandNoteId);
//                 cmd.ExecuteNonQuery();
               
//             }
       

//     }
//     private void ShowAlertMessage(string msg)
//     {

//         StringBuilder sb = new StringBuilder();

//         sb.Append("<script type = 'text/javascript'>");

//         sb.Append("alert('");

//         sb.Append(msg);

//         sb.Append("</script>");

//         ClientScript.RegisterStartupScript(this.GetType(),

//                         "script", sb.ToString());

//     }    

//     protected void ddlSchedule_SelectedIndexChanged(object sender, EventArgs e)
//     {
//         BindGrid();
//         lblerror.Text = "";
//     }

//     protected void ddlOrganisation_SelectedIndexChanged(object sender, EventArgs e)
//     {
//         lblerror.Text = "";
//         BindGrid();
//     }
// }
