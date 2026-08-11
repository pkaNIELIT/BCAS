using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using System.IO;
using BCASClassLibrary;

public partial class Forms_DetailsForms_NEFT_RTGS_File_Upload : System.Web.UI.Page
{
    TYP_NEFT_RTGS_File_Upload obj = new TYP_NEFT_RTGS_File_Upload();

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
                DuplicateRecord.Visible = false;
                PortedRecord.Visible = false;
                //DeleteAllPrevBackups();
            }
        }

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string ftype;
        ftype = FileUploadCsv.PostedFile.ContentType;
        if (FileUploadCsv.HasFile && ftype == "application/vnd.ms-excel")
        {

            DeleteAllPrevBackups(); //*****Delete previous records from Rtgs_Neft_File table and server files
            UploadFile(); //********Port data into Rtgs_Neft_File table 
            //InsertRtgs_Neft_Details(); //******Validate and Insert data into Rtgs_Neft_Details Table
            //grdDisplay.DataBind();

        }
        else
        {
            lblError.Text = "Please select a valid file...";
            lblError.Visible = true;
            DuplicateRecord.Visible = false;
            PortedRecord.Visible = false;

        }

    }


    public void MessageBox(string msg)
    {
        string MSG = String.Format("alert('" + msg + "')");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "MSG", MSG, true);
    }

    public void UploadFile()
    {
        if (FileUploadCsv.HasFile)
        {
            try
            {

                string userID = Session["UserId"].ToString();
                DateTime enterDate = DateTime.Now;
                string filename;
                filename = Path.GetFileName(FileUploadCsv.FileName);
                string filePath;

                filePath = Server.MapPath("~\\Rtgs_Neft_File\\");

                if (filename.Contains(".csv"))
                {

                    if (FileUploadCsv.PostedFile.ContentLength > 0)
                    {

                        FileUploadCsv.SaveAs(Server.MapPath("~\\Rtgs_Neft_File\\" + filename));

                        //TYP_NEFT_RTGS_File_Upload obj = new TYP_NEFT_RTGS_File_Upload();

                        Int32 CheckStatus = obj.PortDataToServer(filename, filePath, userID);

                        if (CheckStatus == 1)
                        {

                            InsertRtgs_Neft_Details();

                            //lblError.Text = "Upload status: File uploaded sucessfully...";
                            //lblError.Visible = true;
                        }
                        else
                        {
                            lblError.Text = "Upload status:Data porting Failed";
                            lblError.Visible = true;
                        }

                    }
                    else
                    {
                        lblError.Text ="File upload Failed. The file must have data.";
                        lblError.Visible = true;
                    }


                }
                else
                {
                    lblError.Text ="File upload Failed. Only CSV files are accepted!";
                    lblError.Visible = true;
                }
            }
            catch (Exception e)
            {
                ClearCookiesSessions();

            }
        }
    }

    public void ClearCookiesSessions()
    {
        Session.Abandon();
        Session.Clear();
        Session.RemoveAll();

        string[] myCookies = Request.Cookies.AllKeys;
        foreach (string cookie in myCookies)
        {
            Response.Cookies[cookie].Expires = DateTime.Now.AddDays(-1);
        }

    }

    public void DeleteAllPrevBackups()
    {
        foreach (var f in System.IO.Directory.GetFiles(Server.MapPath("~/Rtgs_Neft_File")))
            System.IO.File.Delete(f);

        using (DataTable dt = obj.DeleteNEFT_RTGS_File ())
        { 
        }

    }


    public void InsertRtgs_Neft_Details()
    {
        //******** Check for duplicate records
        using (DataTable dt = obj.GetDuplicateRtgs_Neft_Details ())
        {
            if (dt.Rows.Count == 0)
            {
                //********Insert Into Rtgs_Neft_Details Table*****
                using (DataTable dt1 = obj.InsertRtgs_Neft_Details() )
                {
                    if (Convert.ToInt32(dt1.Rows[0][0].ToString ()) == 0)
                    {
                        //*****Display Ported Records
                        lblError.Text = "Upload status: File uploaded sucessfully...";
                        lblError.Visible = true;
                        grdDisplay.DataBind();
                        PortedRecord.Visible = true;
                        grdDuplicateRecords.Visible = false;
                    }
                    else
                    {
                        lblError.Text = "Upload status:Data porting Failed due to some error !!!";
                        lblError.Visible = true;                    

                    }

                }   

             
            }
            else
            {
                //*******Display duplicate records
                lblError.Text = "Upload status:Data porting Failed due to some duplicate records !!!";
                lblError.Visible = true; 
                grdDuplicateRecords.DataSource = dt;
                grdDuplicateRecords.DataBind();
                PortedRecord.Visible = false;
                DuplicateRecord.Visible = true;

            }

        }

    }


    protected void btnUpload_Click(object sender, EventArgs e)
    {
        DeleteAllPrevBackups();
        Bulk_UploadFile();
    }


    public void Bulk_UploadFile()
    {
        if (FileUploadCsv.HasFile)
        {
            try
            {

                string userID = Session["UserId"].ToString();
                DateTime enterDate = DateTime.Now;
                string filename;
                filename = Path.GetFileName(FileUploadCsv.FileName);
                string filePath;

                filePath = Server.MapPath("~\\Rtgs_Neft_File\\");

                if (filename.Contains(".csv"))
                {

                    if (FileUploadCsv.PostedFile.ContentLength > 0)
                    {

                        FileUploadCsv.SaveAs(Server.MapPath("~\\Rtgs_Neft_File\\" + filename));

                        //TYP_NEFT_RTGS_File_Upload obj = new TYP_NEFT_RTGS_File_Upload();
                        
                        //obj.v_filePath = filePath + "\\" + filename;

                        obj.v_filePath = filePath + filename;

                        using (DataTable dt1 = obj.CSV_BulkInsert_NEFT_RTGS_File  ())
                        {
                            if (dt1.Rows.Count > 0)
                            {
                                //
                                //InsertRtgs_Neft_Details();


                                Bulk_InsertRtgs_Neft_Details();
                                
                                //grdTemp.DataSource = dt1;
                                //grdTemp.DataBind();
                            }
                            else
                            {
                                lblError.Text = "Data porting Failed";
                                lblError.Visible = true;

                            }

                        }

                        //Int32 CheckStatus = obj.PortDataToServer(filename, filePath, userID);

                        //if (CheckStatus == 1)
                        //{

                        //    InsertRtgs_Neft_Details();

                            
                        //}
                        //else
                        //{
                        //    lblError.Text = "Upload status:Data porting Failed";
                        //    lblError.Visible = true;
                        //}

                    }
                    else
                    {
                        lblError.Text = "File upload Failed. The file must have data.";
                        lblError.Visible = true;
                    }


                }
                else
                {
                    lblError.Text = "File upload Failed. Only CSV files are accepted!";
                    lblError.Visible = true;
                }
            }
            catch (Exception e)
            {
                //ClearCookiesSessions();

                lblError.Text = "Error in CSV File Format!!!";
                lblError.Visible = true;

            }
        }
    }


    public void Bulk_InsertRtgs_Neft_Details()
    {
        //******** Check for duplicate records
        using (DataTable dt = obj.GetDuplicateRtgs_Neft_Details())
        {
            if (dt.Rows.Count == 0)
            {

                obj.v_Enter_By = Session["user_id"].ToString();
                //********Insert Into Rtgs_Neft_Details Table*****
                using (DataTable dt1 = obj.BULK_InsertRtgs_Neft_Details ())
                {
                    if (Convert.ToInt32(dt1.Rows[0][0].ToString()) == 0)
                    {
                        //*****Display Ported Records
                        lblError.Text = "Upload status: File uploaded sucessfully...";
                        lblError.Visible = true;
                        grdDisplay.DataBind();
                        PortedRecord.Visible = true;
                        grdDuplicateRecords.Visible = false;
                    }
                    else
                    {
                        lblError.Text = "Upload status:Data porting Failed due to some error !!!";
                        lblError.Visible = true;

                    }

                }


            }
            else
            {
                //*******Display duplicate records
                lblError.Text = "Upload status:Data porting Failed due to some duplicate records !!!";
                lblError.Visible = true;
                //grdDuplicateRecords.DataSource = null;
                //grdDuplicateRecords.DataBind();

                grdDuplicateRecords.DataSource = dt;
                grdDuplicateRecords.DataBind();
                PortedRecord.Visible = false;
                DuplicateRecord.Visible = true;

            }

        }

    }

}
