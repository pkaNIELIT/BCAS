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

public partial class Forms_DetailsForms_BCAS_Signed_eTaxInvoice_Upload : System.Web.UI.Page
{
    Int64 DemandNoteId = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
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
        catch (Exception ex)
        {
            Console.Write(ex.Message);
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

    protected void btn_BCAS_Signed_E_TaxInvoice_Click(object sender, EventArgs e)
    {
        int PayeeOrg;
        string InvoiceNo;
        PayeeOrg = Convert.ToInt32(ddlBillerOrganisation.SelectedValue);

        DateTime date_of_Sign;



        date_of_Sign = DateTime.ParseExact(txt_date_of_Signing.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);


        string sqlCompatible_Date_Of_Sign = date_of_Sign.ToString("yyyy-MM-dd");

        InvoiceNo = txt_Invoice_No.Text;

        if (date_of_Sign <= DateTime.Today)
        {
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

                                //For Date in Bytes 

                                //Stream fs = FileUpload1.PostedFile.InputStream;

                                //BinaryReader br = new BinaryReader(fs); //reads the binary files  
                                //Byte[] bytes = br.ReadBytes((Int32)fs.Length); //counting the file length into bytes  


                                string folderPath = Server.MapPath("~/Upload/eTaxInvoice");
                                if (!Directory.Exists(folderPath))
                                {
                                    Directory.CreateDirectory(folderPath);
                                }



                                string fileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
                                //FileUpload1.PostedFile.SaveAs(Server.MapPath("~/Uploads/") + fileName);
                                FileUpload1.PostedFile.SaveAs(folderPath + "/" + fileName);
                                //Response.Redirect(Request.Url.AbsoluteUri);



                                //string query = "update Proforma_Invoice set Payee_OrgCode=@PayeeOrg," +
                                //                " eTaxInvoiceSigningDate=@SigningDate,eTaxInvoiceUploadDate=GETDATE()," +
                                //                " eTaxInvoiceUploadFile=@path where ProformaInvoiceNo= @InvoiceNo"; //insert query  

                                string query = "spUpdate_E_TaxInvoiceDetails";

                                SqlCommand com = new SqlCommand(query, sqlConnection);
                                com.Parameters.Add("@PayeeOrg", SqlDbType.VarChar).Value = PayeeOrg;
                                com.Parameters.Add("@SigningDate", SqlDbType.VarChar).Value = sqlCompatible_Date_Of_Sign;


                                com.Parameters.Add("@InvoiceNo", SqlDbType.VarChar).Value = InvoiceNo;
                                com.Parameters.Add("@path", SqlDbType.VarChar).Value = "~/Upload/eTaxInvoice/" + fileName;

                                //For Date in Bytes 

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
                            lbl_Message.ForeColor = System.Drawing.Color.Red;
                            lbl_Message.Text = "Select Only PDF Files "; // if file is other than speified extension   
                            lbl_Message.Visible = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        lbl_Message.Text = "Error: " + ex.Message.ToString();
                        lbl_Message.Visible = true;
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

    //protected void btn_View_E_BCAS_SignedTaxInvoice_Click(object sender, EventArgs e)
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
}
