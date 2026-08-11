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
using System.Xml.Linq;
using BCASClassLibrary;
using System.IO;

public partial class Forms_MasterForms_rptSuccessfulTransaction : System.Web.UI.Page
{
    
    string path="";
    static string filename="";

    protected override void OnPreRender(EventArgs e)
    {
        // Diable Back Button **//
        base.OnPreRender(e);
        string strDisAbleBackButton;
        strDisAbleBackButton = "<SCRIPT language=javascript>\n";
        strDisAbleBackButton += "window.history.forward(1);\n";
        strDisAbleBackButton += "\n</SCRIPT>";
        Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "clientScript", strDisAbleBackButton);
    }


    protected void Page_Init(object sender, EventArgs e)
    {

        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
        Response.Cache.SetNoStore();

    }

    public void DisableForwordButton()
    {
        Response.Expires = 0;
        Response.Cache.SetNoStore();
        Response.AppendHeader("Pragma", "no-cache");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        DisableForwordButton();

        grdRow.Visible = false;


        if (Convert.ToString(HttpContext.Current.Session["UserId"]).Trim().Length != 0)
        {

            //if (Session["New"].ToString() != Request.Cookies["Session"].Value.ToString())
            //{
            //    ClearCookiesSessions();
            //    HttpContext.Current.Response.Redirect("~/Forms/DetailForms/ErrorPage.aspx", false);
            //}
            //else
            //{
            //    Random ds = new Random();
            //    int cks = ds.Next();
            //    Session["New"] = cks;
            //    HttpCookie mycookie = new HttpCookie("Session");
            //    mycookie.Path = "/";
            //    mycookie.Value = Session["New"].ToString();
            //    Response.Cookies.Add(mycookie);

            //}

        }
        else
        {
            ClearCookiesSessions();
            //HttpContext.Current.Response.Redirect("~/Forms/DetailForms/ErrorPage.aspx", false);

            HttpContext.Current.Server.Transfer("~/Home.aspx", false);

        }      

        //*********
        
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
            
    }

    
    protected void btn_generate_Click(object sender, EventArgs e)
    {
        Page.Validate();

        if (Page.IsValid)
        {
            if ((txtFomDate.Text.Length != 0) && (txtToDate.Text.Length != 0))
            {
                DeleteAllPrevBackups();

                lbl_error.Text = "";             
                TYP_rptSuccessfulTransaction objSuccess = new TYP_rptSuccessfulTransaction();
               
                DateTime fromDate_of_Payment, toDate_of_Payment;
                //fromDate_of_Payment = Convert.ToDateTime(txtFomDate.Text);
                //toDate_of_Payment = Convert.ToDateTime(txtToDate.Text);

                fromDate_of_Payment = DateTime.ParseExact(txtFomDate.Text.Trim (), "dd/MM/yyyy", new System.Globalization.DateTimeFormatInfo());
                toDate_of_Payment = DateTime.ParseExact(txtToDate.Text.Trim (), "dd/MM/yyyy", new System.Globalization.DateTimeFormatInfo());

                if ((fromDate_of_Payment >= System.DateTime.Today .Date) || (toDate_of_Payment >=System.DateTime.Today .Date ))
                {

                    lbl_error.Text = "Date of Payment > Current Date";
                    grdRow.Visible = false;
                    
                }
                else
                {

                    using (DataTable dt = objSuccess.getSuccessfulTransaction(fromDate_of_Payment, toDate_of_Payment))
                    {
                        if (dt.Rows.Count > 0)
                        {
                            grdRow.Visible = true;
                            GridView1.DataSource = dt;
                            GridView1.DataBind();

                            //*******
                            //CreateCSVFile(dt, "f:\\csvData.csv");
                            filename = "Success_Settlement_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".csv";
                            path = Server.MapPath("~\\Upload\\SuccessfulTransaction\\" + filename);
                            CreateCSVFile(dt, path);

                        }
                        else
                        {
                            lbl_error.Text = "Information Not Available";
                            grdRow.Visible = false;

                        }

                    } 


                }
                 

            }
            else
            {
                lbl_error.Text = "Please Enter From Date And To Date";
            }

        }

    }

    protected void btn_cancel_Click(object sender, EventArgs e)
    {
        txtFomDate.Text = "";
        txtToDate.Text = "";
    }
    
   
    protected void LoginStatus1_LoggedOut(object sender, EventArgs e)
    {
        Session.Abandon();
        Response.Redirect("~/UserDetails/UserLogin.aspx", false);
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {

       
            //**********************
        // Get the physical Path of the file(test.doc)    


        string filepath = Server.MapPath("~\\Upload\\SuccessfulTransaction\\" + filename);

        // Create New instance of FileInfo class to get the properties of the file being downloaded
        FileInfo file = new FileInfo(filepath);

        // Checking if file exists
        if (file.Exists)
        {
            // Clear the content of the response
            Response.ClearContent();

            // LINE1: Add the file name and attachment, which will force the open/cance/save dialog to show, to the header
            Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);

            // Add the file size into the response header
            Response.AddHeader("Content-Length", file.Length.ToString());

            // Set the ContentType
            Response.ContentType = "application/csv";

            // Write the file into the response (TransmitFile is for ASP.NET 2.0. In ASP.NET 1.1 you have to use WriteFile instead)
            Response.TransmitFile(file.FullName);

            // End the response
            Response.End();

        }
       
    }

   

    //**************
    public void CreateCSVFile(DataTable dt, string strFilePath)
    {
        #region Export Grid to CSV

        // Create the CSV file to which grid data will be exported.
        StreamWriter sw = new StreamWriter(strFilePath, false);
        // First we will write the headers.
        //DataTable dt = m_dsProducts.Tables[0];


        int iColCount = dt.Columns.Count;
        //for (int i = 0; i < iColCount; i++)
        //{
        //    sw.Write(dt.Columns[i]);
        //    if (i < iColCount - 1)
        //    {
        //        sw.Write(",");
        //    }
        //}
        //sw.Write(sw.NewLine);

        // Now write all the rows.
        foreach (DataRow dr in dt.Rows)
        {
            for (int i = 0; i < iColCount; i++)
            {
                if (!Convert.IsDBNull(dr[i]))
                {
                    sw.Write(dr[i].ToString());
                }
                if (i < iColCount - 1)
                {
                    sw.Write(",");
                }
            }
            sw.Write(sw.NewLine);
        }
        sw.Close();

        #endregion
    }


    public void DeleteAllPrevBackups()
    {
        foreach (var f in System.IO.Directory.GetFiles(Server.MapPath("~//Upload//SuccessfulTransaction")))
            System.IO.File.Delete(f);
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

    protected void lnkLogout_Click(object sender, EventArgs e)
    {
        //////**** Logout Time Update *** 
        //TYP_AUDIT_TRAIL objLogout = new TYP_AUDIT_TRAIL();
        //objLogout.i_ID = Convert.ToInt32(HttpContext.Current.Session["ID"].ToString().Trim());
        //using (DataTable dt_Pay = objLogout.UpdateLogOut())
        //{
        //    //if (Convert.ToInt32(dt_Pay.Rows[0][0]) != 0)
        //    //{
        //    //    //lblError.Text = "Insertion Failed";
        //    //}
        //}
        //ClearCookiesSessions();
        //Response.Redirect("~/frmMain.aspx", false);
        //////*******Till Here 
    }
}
