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

public partial class Forms_DetailsForms_BCAS_Signed_TaxInvoice_Upload : System.Web.UI.Page
{
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
                            ddlBillerOrganisation.SelectedValue = Session["UserOrg"].ToString();
                        }
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


    //protected void ddlSchedule_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    //ddlDemandNote.Items.Clear();
    //    if (ddlBillerOrganisation.SelectedValue != "0")
    //    {

    //        //ddlDemandNote.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
    //        Int64 OrgID = Convert.ToInt64(ddlBillerOrganisation.SelectedValue);
    //        Int64 ScheduleID = Convert.ToInt64(ddlSchedule.SelectedValue);
    //        string constr = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
    //        using (SqlConnection con = new SqlConnection(constr))
    //        {
    //            using (SqlCommand cmd = new SqlCommand())
    //            {
    //                //cmd.CommandText = "SELECT ID, 'DemandNoteId - '+ cast ( ID as varchar) + '; ( Date - ' +  CONVERT(varchar,date,105) +'; #Candidates - '+ CAST(NoOfCandidates as varchar)+'; FeeAmount - '+ CAST(Amount as varchar) +' )' as IDDateCountAmt   FROM Demand_Note where Schedule_ID=@ScheduleID and Online_Transaction_ID is null and ID in (select Demand_Note_ID from NOMINATIONS_FOR_COURSES where Biller_OrgCode =@OrgID)";
    //                cmd.CommandText ="SELECT ID, 'DemandNoteId - '+ cast ( ID as varchar) + '; ( Date - ' +  CONVERT(varchar,date,105) +'; #Candidates - '+  ";
    //                cmd.CommandText +="CAST(NoOfCandidates as varchar)+'; FeeAmount - '+ CAST(Amount as varchar) +' )' as IDDateCountAmt   FROM Demand_Note ";
    //                cmd.CommandText += "where Schedule_ID=@ScheduleID and Online_Transaction_ID is null and ID in (select Demand_Note_ID from NOMINATIONS_FOR_COURSES n, ";
    //                cmd.CommandText += "Candidate_Registration_Screeners c, Course_Schedule cs where c.BillerOrganisation_Code =@OrgID ";
    //                cmd.CommandText +="and n.Registration_No =c.Registration_Number and cs.Course_Code =c.course_code  and cs.Schedule_ID =n.Schedule_ID) ";
    //                cmd.CommandText += " union SELECT ID, 'DemandNoteId - '+ cast ( ID as varchar) + '; ( Date - ' +  CONVERT(varchar,date,105) +'; #Candidates - '+  ";
    //                cmd.CommandText += "CAST(NoOfCandidates as varchar)+'; FeeAmount - '+ CAST(Amount as varchar) +' )' as IDDateCountAmt   FROM Demand_Note ";
    //                cmd.CommandText += "where Schedule_ID=@ScheduleID and Online_Transaction_ID is null and ID in (select Demand_Note_ID from NOMINATIONS_FOR_COURSES n, ";
    //                cmd.CommandText += "Candidate_Registration c, Course_Schedule cs where c.BillerOrganisation_Code =@OrgID ";
    //                cmd.CommandText += "and n.Registration_No =c.Registration_Number  and cs.Schedule_ID =n.Schedule_ID) ";

    //                cmd.Parameters.AddWithValue("@OrgID", OrgID);
    //                cmd.Parameters.AddWithValue("@ScheduleID", ScheduleID);
    //                cmd.Connection = con;
    //                con.Open();
    //                SqlDataAdapter adpt = new SqlDataAdapter(cmd);
    //                DataTable dt = new DataTable();
    //                adpt.Fill(dt);
    //                //ddlDemandNote.DataSource = dt;                
    //                //ddlDemandNote.DataTextField = "IDDateCountAmt";
    //                //ddlDemandNote.DataValueField = "ID";

    //                //ddlDemandNote.DataBind();
    //                con.Close();
    //            }
    //        }
    //    }
    //}


    //protected void ddlBillerOrganisation_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    //ddlDemandNote.Items.Clear();
    //    if (ddlBillerOrganisation.SelectedValue != "0")
    //    {

    //        //ddlDemandNote.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
    //        Int64 OrgID = Convert.ToInt64(ddlBillerOrganisation.SelectedValue);
    //        Int64 ScheduleID = Convert.ToInt64(ddlSchedule.SelectedValue);
    //        string constr = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
    //        using (SqlConnection con = new SqlConnection(constr))
    //        {
    //            using (SqlCommand cmd = new SqlCommand())
    //            {
    //               // cmd.CommandText = "SELECT ID   FROM Demand_Note where Schedule_ID=@ScheduleID and ID in (select Demand_Note_ID from NOMINATIONS_FOR_COURSES where Nominating_OrgCode =@OrgID)";
    //                cmd.CommandText = "SELECT ID   FROM Demand_Note where Schedule_ID=@ScheduleID and ID in ";
    //                cmd.CommandText +="(select Demand_Note_ID from NOMINATIONS_FOR_COURSES n,Candidate_Registration_Screeners c,Course_Schedule s ";
    //                cmd.CommandText+="where n.Registration_No =c.Registration_Number and n.Schedule_ID =s.Schedule_ID and c.course_code =s.Course_Code ";
    //                cmd.CommandText+=" and c.BillerOrganisation_Code =@BillerOrgID)";


    //                cmd.Parameters.AddWithValue("@BillerOrgID", OrgID);
    //                cmd.Parameters.AddWithValue("@ScheduleID", ScheduleID);
    //                cmd.Connection = con;
    //                con.Open();
    //                SqlDataAdapter adpt = new SqlDataAdapter(cmd);
    //                DataTable dt = new DataTable();
    //                adpt.Fill(dt);
    //                //ddlDemandNote.DataSource = dt;
    //                //ddlDemandNote.DataTextField = "ID";
    //                //ddlDemandNote.DataValueField = "ID";

    //                //ddlDemandNote.DataBind();
    //                con.Close();
    //            }
    //        }
    //    }
    //}




    protected void btn_BCAS_SignedTaxInvoice_Click(object sender, EventArgs e)
    {

        int  PayeeOrg;
        string InvoiceNo;
        PayeeOrg = Convert.ToInt32(ddlBillerOrganisation.SelectedValue);

        DateTime date_of_Sign;



        date_of_Sign = DateTime.ParseExact(txt_date_of_Signing.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        

        string sqlCompatible_Date_Of_Sign = date_of_Sign.ToString("yyyy-MM-dd");
        if (date_of_Sign <= DateTime.Today)
        {
            InvoiceNo = txt_Invoice_No.Text;

            string filePath = FileUpload1.PostedFile.FileName;
            // getting the file path of uploaded file  
            string filename1 = Path.GetFileName(filePath);
            // getting the file name of uploaded file  
            string ext = Path.GetExtension(filename1);
            // getting the file extension of uploaded file  
            string type = String.Empty;
            if (!FileUpload1.HasFile)
            {
                lbl_Message.Text = "Please Select File"; //if file uploader has no file selected  
                lbl_Message.Visible = true;
            }
            else
            {
                if (FileUpload1.HasFile)
                {
                    try
                    {
                        switch (ext) // this switch code validate the files which allow to upload only PDF file   
                        {
                            case ".pdf":
                                type = "application/pdf";
                                break;
                        }
                        if (type != String.Empty)
                        {
                            string connectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
                            DataTable dataTable = new DataTable();
                            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                            {
                                //For Uploading PDF Data in Bytes

                                //Stream fs = FileUpload1.PostedFile.InputStream;

                                //BinaryReader br = new BinaryReader(fs); //reads the binary files  
                                //Byte[] bytes = br.ReadBytes((Int32)fs.Length); //counting the file length into bytes  


                                string folderPath = Server.MapPath("~/Upload/TaxInvoice");
                                if (!Directory.Exists(folderPath))
                                {
                                    Directory.CreateDirectory(folderPath);
                                }



                                string fileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
                                //FileUpload1.PostedFile.SaveAs(Server.MapPath("~/Uploads/") + fileName);
                                FileUpload1.PostedFile.SaveAs(folderPath + "/" + fileName);
                                //Response.Redirect(Request.Url.AbsoluteUri);

                                string query = "spUpdateTaxInvoiceDetails";

                                SqlCommand com = new SqlCommand(query, sqlConnection);
                                com.Parameters.Add("@PayeeOrg", SqlDbType.VarChar).Value = PayeeOrg;
                                com.Parameters.Add("@SigningDate", SqlDbType.VarChar).Value = sqlCompatible_Date_Of_Sign;


                                com.Parameters.Add("@InvoiceNo", SqlDbType.VarChar).Value = InvoiceNo;
                                com.Parameters.Add("@path", SqlDbType.VarChar).Value = "~/Upload/TaxInvoice/" + fileName;

                                //For Uploading PDF Data in Bytes
                                //com.Parameters.Add("@Data", SqlDbType.Binary).Value = bytes;


                                com.CommandType = CommandType.StoredProcedure;

                                sqlConnection.Open();
                                int rowAffected = com.ExecuteNonQuery();
                                sqlConnection.Close();
                                if (rowAffected > 0)
                                {
                                    lbl_Message.ForeColor = System.Drawing.Color.Green;
                                    lbl_Message.Text = "File Uploaded Successfully";
                                    lbl_Message.Visible = true;
                                }
                                else
                                {
                                    lbl_Message.ForeColor = System.Drawing.Color.Red;
                                    lbl_Message.Text = "No Record for the Specified InvoiceNo";
                                    lbl_Message.Visible = true;
                                }

                                GridView1.DataBind();
                                ddlBillerOrganisation.SelectedValue = "0";
                                txt_Invoice_No.Text = "";
                                txt_date_of_Signing.Text = "";

                            }
                        }
                        else
                        {
                            lbl_Message.Visible = true;
                            lbl_Message.ForeColor = System.Drawing.Color.Red;
                            lbl_Message.Text = "Select Only PDF Files "; // if file is other than speified extension   

                        }
                    }
                    catch (Exception ex)
                    {
                        lbl_Message.Visible = true;
                        lbl_Message.Text = "Error: " + ex.Message.ToString();

                    }

                }
            }
        }
        else
        {
            lbl_Message.ForeColor = System.Drawing.Color.Red;
            lbl_Message.Text = "Date of Sign Can't Be After Today";
            lbl_Message.Visible = true;
        }
    }

    //protected void btn_View_BCAS_SignedTaxInvoice_Click(object sender, EventArgs e)
    //{

    //    string connectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
    //    DataTable dataTable = new DataTable();
    //    using (SqlConnection sqlConnection = new SqlConnection(connectionString))
    //    {
    //        Stream fs = FileUpload1.PostedFile.InputStream;

    //        string query = "select * from  BCAS_Signed_TaxInvoice_Uploads where Invoice_No=@InvoiceNo";


    //        SqlCommand com = new SqlCommand(query, sqlConnection);
    //        com.Parameters.Add("@InvoiceNo", SqlDbType.VarChar).Value = txt_Invoice_No.Text; ;

    //        //For Data Read
    //        sqlConnection.Open();
    //        SqlDataReader dr = com.ExecuteReader();
    //        if (dr.Read())
    //        {
    //            Response.Clear();
    //            Response.Buffer = true;
    //            Response.ContentType = "application/pdf";
    //            Response.AddHeader("content-disposition", "attachment;filename=Invoice.pdf"); // to open file prompt Box open or Save file  
    //            Response.Charset = "";
    //            Response.Cache.SetCacheability(HttpCacheability.NoCache);
    //            Response.BinaryWrite((byte[])dr["data"]);
    //            Response.End();
    //        }
    //        sqlConnection.Close();
    //    }
    //}

    protected void SqlDataSource_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
    {

    }
}
