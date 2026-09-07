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
using System.Data.SqlClient;

public partial class Forms_DetailsForms_Manual_Entry_NEFT_RTGS_Details : System.Web.UI.Page
{
    TYP_NEFT_RTGS_File_Upload objNEFT = new TYP_NEFT_RTGS_File_Upload();

    protected void Page_Load(object sender, EventArgs e)
    {
        DisableForwordButton();
        HttpContext.Current.Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
        HttpContext.Current.Response.Cache.SetValidUntilExpires(false);
        HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        HttpContext.Current.Response.Cache.SetNoStore();

        if (!Page.IsPostBack)
        {
            if (Convert.ToString(HttpContext.Current.Session["UserId"]).Trim().Length == 0)
            {
                ClearCookiesSessions();
                Response.Redirect("~/Home.aspx");
            }
            else
            {
                txtSerial_No.Focus();
            }
           


        }  

    }
   
     

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        
        CompareValidator1.ValueToCompare = "0";
        
        if (Convert.ToString(HttpContext.Current.Session["UserId"]).Trim().Length != 0)
        {
           
            objNEFT.i_Serial_No = Convert.ToInt32(txtSerial_No.Text.Trim());            
            objNEFT.v_UTR_Number = txtUTR_No.Text.Trim().ToUpper ();
            objNEFT.v_Sender_IFSC = txtSenderIFSC.Text.Trim().ToUpper();
            objNEFT.v_Sender_Name = txtSenderName.Text.Trim().ToUpper();
          
            objNEFT.d_Transaction_date = DateTime.ParseExact(txtTXN_Date.Text.Trim(), "dd/MM/yyyy", new System.Globalization.DateTimeFormatInfo());
            objNEFT.n_Amount = Convert.ToInt32(txtAmount.Text.Trim());
            objNEFT.v_Enter_By = Session["user_id"].ToString();

            using (DataTable dt = objNEFT.Manual_Insert_Rtgs_Neft_Details ())
            {

                if (Convert.ToInt32(dt.Rows[0][0]) == -786)
                {
                    lbl_ErrorMsg.Text = "UTR Number is already Exist!!!";
                    lbl_Success.Visible = false;
                   

                }
                else if ((Convert.ToInt32(dt.Rows[0][0]) == 0))
                {
                    lbl_Success.Text = "Record Successfully Inserted!!!";
                    lbl_Success.Visible = true;
                    lbl_ErrorMsg.Visible = false;

                    //SqlDataSource1.SelectParameters.Clear();
                    //SqlDataSource1.SelectCommand = "SELECT [Serial_No], [UTR_Number], [Sender_IFSC], [Sender_Name], [Transaction_date], [Amount] FROM [Rtgs_Neft_Details] WHERE ([UTR_Number] = @UTR_Number)";
                    //SqlDataSource1.SelectParameters.Add("@UTR_Number", objNEFT.v_UTR_Number);
                    //SqlDataSource1.DataBind();
                    //grdDisplay.DataSource = SqlDataSource1;
                    //grdDisplay.DataBind();
                    //grdDisplay.Visible = true;

                    ClearTextFields();

                }
                else
                {
                    lbl_ErrorMsg.Text = "Error Occured!!!";
                    lbl_Success.Visible = false;

                   

                }
               
            }

        }
        else
        {
            ClearCookiesSessions();
            Response.Redirect("~/Home.aspx");

        }

    }


    private void ClearTextFields()
    {
        txtSerial_No.Focus();
        txtSerial_No.Text = "";
        txtUTR_No.Text = "";
        txtSenderIFSC.Text = "";
        txtSenderName.Text = "";
        txtTXN_Date.Text = "";
        txtAmount.Text = "";

    }


    public void DisableForwordButton()
    {
        Response.Expires = 0;
        Response.Cache.SetNoStore();
        Response.AppendHeader("Pragma", "no-cache");
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

}
