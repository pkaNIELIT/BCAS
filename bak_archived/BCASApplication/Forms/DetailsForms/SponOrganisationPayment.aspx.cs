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

public partial class Forms_DetailsForms_SponOrganisationPayment : System.Web.UI.Page
{
    
    TYP_PayeeOrganisationPayment objPay = new TYP_PayeeOrganisationPayment();
    
    TYP_FeePaymentsBySO objFee = new TYP_FeePaymentsBySO();

    TYP_PaymentResponse objRes = new TYP_PaymentResponse();

    protected void Page_Load(object sender, EventArgs e)
    {
        DisableForwordButton();
        HttpContext.Current.Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
        HttpContext.Current.Response.Cache.SetValidUntilExpires(false);
        HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        HttpContext.Current.Response.Cache.SetNoStore();

        if (!Page.IsPostBack)
        {
            
            //lblPayeeOrgName.Text = Session["OrgName"].ToString();

            if (Convert.ToString(HttpContext.Current.Session["OrgName"]).Trim().Length == 0)
            {
                lblPayeeOrgName.Visible = false;
            }
            else
            {
                lblPayeeOrgName.Text = Session["OrgName"].ToString();
                lblPayeeOrgName.Visible = true;                
            }
          ddlSponsoredOrg.SelectedValue=  Session["UserOrg"].ToString();

            if (Convert.ToString(HttpContext.Current.Session["UserId"]).Trim().Length == 0)
            {
                ClearCookiesSessions();
                Response.Redirect("~/Home.aspx");               
            }            

        }  


    }


    protected void btnPayNow_Click(object sender, EventArgs e)
    {
        CompareValidator1.ValueToCompare = "0";
        if (Convert.ToString(HttpContext.Current.Session["UserId"]).Trim().Length != 0)
        {
            //&&&&&&&&&&& Check in the Payment Balance Table for the record

            ////using (DataTable dt = objFee.getAmountBalance(Convert.ToInt32(ddlSponsoredOrg.SelectedValue), Convert.ToInt32(Session["UserOrg"])))
            ////{
            ////    if (dt.Rows.Count == 0)
            ////    {
            ////        //******** Insert Into Payment Balance Table

            ////        objRes.i_Sponsoring_Organisation_Code = Convert.ToInt32(ddlSponsoredOrg.SelectedValue);
            ////        objRes.i_Payee_Organisation_Code = Convert.ToInt32(Session["UserOrg"].ToString());
            ////        using (DataTable dt_new = objRes.Insert_PaymentBalance())
            ////        {
            ////            if (Convert.ToInt32(dt_new.Rows[0][0]) != 0)
            ////            {
            ////                //******** Insert Into Payment Balance Table
            ////                lbl_ErrorMsg.Text = "Error Occured";
            ////            }
            ////        }

            ////    }

            ////}

            //************
            //objPay.i_Payee_Organisation_Code = Convert.ToInt32(Session["UserOrg"].ToString());
            //objPay.i_Sponsoring_Organisation_Code = Convert.ToInt32(ddlSponsoredOrg.SelectedValue);
            //objPay.n_Amount_To_Pay = Convert.ToInt32(txtAmount.Text.Trim());
            //objPay.v_User_ID = Session["user_id"].ToString();
            Int32 OrgID = Convert.ToInt32(ddlSponsoredOrg.SelectedValue);
            Int64 ProfromaInvoiceID = Convert.ToInt64(ddlProformaInvoice.SelectedValue);
            string ProformaInvoiceNo = ddlProformaInvoice.SelectedItem.Text;
            Double TotalAmount = Convert.ToDouble(txtAmount.Text);
            
            string User_ID = Session["user_id"].ToString();

            using (DataTable dt_Pay = objPay.InsertOnlineTransaction(OrgID, ProfromaInvoiceID, ProformaInvoiceNo, TotalAmount, User_ID))
            {
                if (Convert.ToInt32(dt_Pay.Rows[0][0]) != 0)
                {
                    lbl_ErrorMsg.Text = "Error Occured";
                }
                else
                {
                    Session["Transaction_ID"]=(dt_Pay.Rows[0][1].ToString ());
                    Response.Redirect("~/Forms/DetailsForms/MakePayment.aspx");
                                      
                }

            }

        }
        else
        {
            ClearCookiesSessions();
            Response.Redirect("~/Home.aspx");

        }
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



    protected void ddlProformaInvoice_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtAmount.Text="";
        if (ddlProformaInvoice.SelectedValue != "0")
        {


            Int64 OrgID = Convert.ToInt64(ddlSponsoredOrg.SelectedValue);
            Int64 ProfromaInvoiceID = Convert.ToInt64(ddlProformaInvoice.SelectedValue);
            string constr = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select Total_Fee_With_Service_Tax from Proforma_Invoice where  ID=@ProfromaInvoiceID and Nominating_OrgCode=@OrgID";

                    cmd.Parameters.AddWithValue("@OrgID", OrgID);
                    cmd.Parameters.AddWithValue("@ProfromaInvoiceID", ProfromaInvoiceID);
                    cmd.Connection = con;
                    con.Open();
                    //txtAmount.Text = cmd.ExecuteScalar().ToString();
                    Double  amount = Convert.ToDouble(cmd.ExecuteScalar());
                    txtAmount.Text = amount.ToString();
                    txtAmount.Text = string.Format("{0:0.00}", amount);
                    con.Close();
                }
            }
        }
    }
}
