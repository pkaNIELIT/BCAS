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

public partial class Forms_DetailsForms_NEFT_RTGS_PayeePayments : System.Web.UI.Page
{

    //TYP_PayeeOrganisationPayment objPay = new TYP_PayeeOrganisationPayment();
    TYP_NEFT_RTGS_PayeePayments objNEFT = new TYP_NEFT_RTGS_PayeePayments();

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

            using (DataTable dt = objFee.getAmountBalance(Convert.ToInt32(ddlSponsoredOrg.SelectedValue), Convert.ToInt32(Session["UserOrg"])))
            {
                if (dt.Rows.Count == 0)
                {
                    //******** Insert Into Payment Balance Table

                    objRes.i_Sponsoring_Organisation_Code = Convert.ToInt32(ddlSponsoredOrg.SelectedValue);
                    objRes.i_Payee_Organisation_Code = Convert.ToInt32(Session["UserOrg"].ToString());
                    using (DataTable dt_new = objRes.Insert_PaymentBalance())
                    {
                        if (Convert.ToInt32(dt_new.Rows[0][0]) != 0)
                        {
                            //******** Insert Into Payment Balance Table
                            lbl_ErrorMsg.Text = "Error Occured";
                        }
                    }

                }

            }

            //************

            objNEFT.i_Payee_Organisation_Code = Convert.ToInt32(Session["UserOrg"].ToString());
            objNEFT.i_Sponsoring_Organisation_Code = Convert.ToInt32(ddlSponsoredOrg.SelectedValue);
            objNEFT.v_UTR_Number = txtUTR_No.Text.Trim();
            //objNEFT.d_Transaction_date = Convert.ToDateTime(txtTXN_Date.Text.Trim());
            objNEFT.d_Transaction_date = DateTime.ParseExact(txtTXN_Date.Text.Trim(), "dd/MM/yyyy", new System.Globalization.DateTimeFormatInfo());
            objNEFT.n_Amount = Convert.ToInt32(txtAmount.Text.Trim());
            objNEFT.v_Verified_By = Session["user_id"].ToString();

            using (DataTable dt_Pay = objNEFT.Update_NEFT_RTGS_Pymt () )
            {

                  if (Convert.ToInt32(dt_Pay.Rows[0][0]) == -786)
                  {
                      lbl_ErrorMsg.Text = "Either UTR Number is already verified or Record Does Not Exist!!!";

                  }
                  else if ((Convert.ToInt32(dt_Pay.Rows[0][0]) == 0) & (Convert.ToInt32(dt_Pay.Rows[0][1]) == 0))
                  {
                      lbl_ErrorMsg.Text = "Payment Verified!!!";

                  }
                  else
                  {
                      lbl_ErrorMsg.Text = "Error Occured!!!";

                  }
               
                //if ((Convert.ToInt32(dt_Pay.Rows[0][0]) == 0) & (Convert.ToInt32(dt_Pay.Rows[0][1]) == 0))
                //{
                //    lbl_ErrorMsg.Text = "Payment Verified!!!";
                //}
                //else if (Convert.ToInt32(dt_Pay.Rows[0][0]) == -786)
                //{
                //    lbl_ErrorMsg.Text = "Record Does Not Exist!!!";
                    
                //}
                //else
                //{
                //    lbl_ErrorMsg.Text = "Error Occured!!!";
                    
                //}
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





}
