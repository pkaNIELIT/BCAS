using System;

using System.Configuration;
using System.Data;
using System.IO;
using System.Web;

using System.Web.UI;

using System.Web.UI.WebControls;

using BCASClassLibrary;
using System.Data.SqlClient;
using System.Text;

using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;

using System.Globalization;
using System.IO.Compression;
using System.Drawing;
using System.Reflection.Emit;
using System.Web.Services.Description;

public partial class Forms_DetailsForms_BCAS_Signed_TaxInvoice_Download_Payee : System.Web.UI.Page
{
    TYP_BCAS_Approval objBCAS = new TYP_BCAS_Approval();
    Int64 DemandNoteId = 0;
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
              
                //Added for biller Organisation
                using (DataSet dtOrg = TYP_PAYEE_ORGANISATION_MASTER.GetAllPayee_Organisation_Master())
                {
                    if (dtOrg.Tables[0].Rows.Count > 0)
                    {
                        ddlBillerOrganisation.DataSource = dtOrg;
                        ddlBillerOrganisation.DataValueField = "Payee_Organisation_Code";
                        ddlBillerOrganisation.DataTextField = "Payee_Organisation_Name";
                        ddlBillerOrganisation.DataBind();
                        ddlBillerOrganisation.Enabled = true;

                        //Session["OrgName"] == null
                        if (Session["UserOrg"] != null)
                        {
                            ddlBillerOrganisation.Enabled = false;
                            ddlBillerOrganisation.SelectedValue = Session["UserOrg"].ToString();
                        }

                        string constr = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
                        using (SqlConnection Conn = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand("CheckPayee_Organisation_GSTIN_For_E_TaxInvoice", Conn))
                            {
                                Conn.Open();
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@pOrg", ddlBillerOrganisation.SelectedItem.Value);
                                object code =cmd.ExecuteScalar();
                                if (code == null)
                                    btn_BCAS_Signed_E_TaxInvoiceDownload.Visible = false;
                            }
                        }
                    }

                }

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

            }

        }
        

    }    
    
    private void ShowMessage(int count)
    {

        StringBuilder sb = new StringBuilder();

        sb.Append("<script type = 'text/javascript'>");

        sb.Append("alert('");

        sb.Append(count.ToString());

        sb.Append(" records Updated.');");

        sb.Append("</script>");

        ClientScript.RegisterStartupScript(this.GetType(),

                        "script", sb.ToString());

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

    public void GenerateDemandNote(int Schedule_ID, int countRecord, string allRegNo, string ServiceID)
    {
        try
        {

            string constr = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            using (SqlConnection Conn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("InsertDemandNote", Conn))
                {
                    Conn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Schedule_ID", Schedule_ID);
                    cmd.Parameters.AddWithValue("@countRecord", countRecord);
                    //cmd.Parameters.AddWithValue("@allRegNo", allRegNo);
                    cmd.Parameters.AddWithValue("@ServiceID", ServiceID);
                    cmd.Parameters.AddWithValue("@Created_By", Session["user_id"].ToString());
                    cmd.Parameters.Add("@DemandNoteid", SqlDbType.Int).Direction = ParameterDirection.Output; 
                    cmd.ExecuteNonQuery();

                    string id = cmd.Parameters["@DemandNoteid"].Value.ToString();

                    DemandNoteId = Convert.ToInt64(id);
                }
            }
        }
        catch (Exception ex)
        {           
        }
    }

    //protected void btn_BCAS_SignedTaxInvoiceDownload_Click(object sender, EventArgs e)
    //{
    //    //File to be downloaded.
    //    string connectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;

    //    using (SqlConnection objConn = new SqlConnection(connectionString))
    //    {

    //        SqlCommand objCmd1 = new SqlCommand("[getSignedBCASTaxInvoiceFileName]", objConn);
    //        objCmd1.CommandType = CommandType.StoredProcedure;
    //        //objCmd1.Parameters.AddWithValue("@invoiceNo", txt_Invoice_No.Text.ToString());
    //        objCmd1.Parameters.AddWithValue("@pOrg", ddlBillerOrganisation.SelectedItem.Value);
    //        objCmd1.Parameters.AddWithValue("@scheduleId", ddlSchedule.SelectedItem.Value);
    //        try
    //        {

    //            objConn.Open();
    //            Object nm = objCmd1.ExecuteScalar();
    //            //Path of the File to be downloaded.
    //            //string filePath = Server.MapPath(string.Format("~/Files/{0}", fileName));
    //            objConn.Close();

    //            if (Convert.IsDBNull(nm))
    //            {
    //                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "alert", "alert('Empty Tax Invoice For Specific Schedule of Organisation')", true);
    //                //lbl_Message.Text = "Empty Record For Specific Invoice No";
    //                //lbl_Message.Visible = true;
    //                //lbl_Message.ForeColor = Color.Red;
    //            }
    //            else
    //            {
    //                lbl_Message.Text = "";
    //                lbl_Message.Visible = false;
    //                string Name = (String)nm;
    //                string filePath = Server.MapPath(Name);

    //                //Content Type and Header.
    //                Response.ContentType = "application/pdf";
    //                Response.AppendHeader("Content-Disposition", "attachment; filename=SignedTaxInvoiceOf_" + ddlBillerOrganisation.SelectedItem.Text);

    //                //Writing the File to Response Stream.
    //                Response.WriteFile(filePath);

    //                //Flushing the Response.
    //                Response.Flush();
    //                Response.End();
    //                // Throws exception due to foreign key constraint
    //                //objTrans.Commit();
    //            }
    //        }
    //        catch (Exception)
    //        {


    //        }
    //    }
    //}

    protected void btn_BCAS_SignedTaxInvoiceDownload_Click(object sender, EventArgs e)
    {
        //File to be downloaded.
        string connectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;

        using (SqlConnection objConn = new SqlConnection(connectionString))
        {
            SqlCommand objCmd1 = new SqlCommand("[getSignedBCASTaxInvoiceFileName]", objConn);
            objCmd1.CommandType = CommandType.StoredProcedure;
            objCmd1.Parameters.AddWithValue("@pOrg", ddlBillerOrganisation.SelectedItem.Value);
            objCmd1.Parameters.AddWithValue("@scheduleId", ddlSchedule.SelectedItem.Value);

            try
            {
                objConn.Open();
                Object nm = objCmd1.ExecuteScalar();
                objConn.Close();

                // Check if file name is null or empty
                if (Convert.IsDBNull(nm) || nm == null)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "alert",
                        "alert('Empty Tax Invoice For Specific Schedule of Organisation');", true);
                }
                else
                {
                    lbl_Message.Text = "";
                    lbl_Message.Visible = false;

                    string fileName = (string)nm;
                    string filePath = Server.MapPath(fileName);

                    // Check if file exists
                    if (System.IO.File.Exists(filePath))
                    {
                        // Content Type and Header.
                        Response.ContentType = "application/pdf";
                        Response.AppendHeader("Content-Disposition", "attachment; filename=SignedTaxInvoiceOf_"
                            + ddlBillerOrganisation.SelectedItem.Text + ".pdf");

                        // Writing the File to Response Stream.
                        Response.WriteFile(filePath);

                        // Flushing the Response.
                        Response.Flush();

                        // Use CompleteRequest instead of Response.End to avoid ThreadAbortException.
                        HttpContext.Current.ApplicationInstance.CompleteRequest();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "alert",
                            "alert('File not found on the server');", true);
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception or display an error message for debugging purposes.
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "alert",
                    "alert('An error occurred: " + ex.Message + "');", true);
            }
        }
    }

    protected void btn_BCAS_Signed_E_TaxInvoiceDownload_Click(object sender, EventArgs e)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;

        using (SqlConnection objConn = new SqlConnection(connectionString))
        {

            SqlCommand objCmd1 = new SqlCommand("[getSignedBCAS_E_TaxInvoiceFileName]", objConn);
            objCmd1.CommandType = CommandType.StoredProcedure;
            objCmd1.Parameters.AddWithValue("@pOrg", ddlBillerOrganisation.SelectedItem.Value);
            objCmd1.Parameters.AddWithValue("@scheduleId", ddlSchedule.SelectedItem.Value);
            //objCmd1.Parameters.AddWithValue("@invoiceNo", txt_Invoice_No.Text.ToString());
            try
            {

                objConn.Open();
                Object nm = objCmd1.ExecuteScalar();
                //Path of the File to be downloaded.
                //string filePath = Server.MapPath(string.Format("~/Files/{0}", fileName));
                objConn.Close();
                if (Convert.IsDBNull(nm))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "alert", "alert('Empty E-Tax Invoice For Specific Schedule of Organisation')", true);
                    //lbl_Message.Text = "Empty Record For Specific E-Invoice No";
                    //lbl_Message.Visible = true;
                    //lbl_Message.ForeColor = Color.Red;
                }
                else
                {
                    lbl_Message.Text = "";
                    string Name = (String)nm;
                    string filePath = Server.MapPath(Name);

                    //Content Type and Header.
                    Response.ContentType = "application/pdf";
                    Response.AppendHeader("Content-Disposition", "attachment; filename=Signed_E_TaxInvoiceOf_" + ddlBillerOrganisation.SelectedItem.Text);

                    //Writing the File to Response Stream.
                    Response.WriteFile(filePath);

                    //Flushing the Response.
                    Response.Flush();
                    Response.End();
                    // Throws exception due to foreign key constraint
                    //objTrans.Commit();
                }
            }
            catch (Exception)
            {


            }
        }
    }
}
