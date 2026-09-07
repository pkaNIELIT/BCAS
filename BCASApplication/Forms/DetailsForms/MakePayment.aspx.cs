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

public partial class Forms_DetailsForms_MakePayment : System.Web.UI.Page
{
    CalculateCheckSum objChkSum = new CalculateCheckSum();
    TYP_PayeeOrganisationPayment objPay = new TYP_PayeeOrganisationPayment();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {           

           
            if (Convert.ToString(HttpContext.Current.Session["UserId"]).Trim().Length != 0)
            {
                LoginStatus.Visible = Convert.ToBoolean(Session["View_Login"].ToString());
                //**********  
                using (DataTable dt = objPay.GetDetailsForMakePaymentBySpon(Convert.ToInt32(Session["Transaction_ID"].ToString())))
                {
                    if (dt.Rows.Count != 0)
                    {
                        ddlSponsoredOrg.SelectedValue = dt.Rows[0]["Sponsoring_Organisation_Code"].ToString();
                        txtAmount.Text = dt.Rows[0]["Amount_To_Pay"].ToString();
                        lblPayeeOrgName.Text = dt.Rows[0]["Payee_Organisation_Name"].ToString();
                        //txtAmount.Text = (txtAmount.Text).Trim() + (".00").Trim();
                        txtAmount.Text = (txtAmount.Text).Trim();
                        string strRequest = "";
                        string dateData = System.DateTime.Today.Year.ToString() +(System.DateTime.Today.Month.ToString().Length <2?("0"+ System.DateTime.Today.Month.ToString()): System.DateTime.Today.Month.ToString()) + (System.DateTime.Today.Day.ToString().Length <2?"0"+ System.DateTime.Today.Day.ToString(): System.DateTime.Today.Day.ToString());
                        strRequest = "NIELIT" + "|" + dateData +"-"+dt.Rows[0]["Transaction_id"].ToString() + "|" + "NA" + "|" + txtAmount.Text + "|NA|NA|NA|INR|NA|R|nielit|NA|NA|F|" + dt.Rows[0]["Sponsoring_Organisation_Code"].ToString() + "|BCAS01|" + dt.Rows[0]["Sponsoring_Organisation_Code"].ToString() + "|NA|NA|NA|NA|" + "https://bcas.nielit.gov.in/Forms/DetailsForms/PaymentResponse.aspx";
                        //strRequest = "NIELIT" + "|" +dateData +"-"+ dt.Rows[0]["Transaction_id"].ToString() + "|" + "NA" + "|" + txtAmount.Text + "|NA|NA|NA|INR|NA|R|nielit|NA|NA|F|" + dt.Rows[0]["Sponsoring_Organisation_Code"].ToString() + "|BCAS01|" + dt.Rows[0]["Sponsoring_Organisation_Code"].ToString() + "|NA|NA|NA|NA|" + "http://localhost:2323/Forms/DetailsForms/PaymentResponse.aspx";
                        //  strRequest = "NIELIT" + "|" + dt.Rows[0]["Transaction_id"].ToString() + "|" + "NA" + "|" + txtAmount.Text + "|NA|NA|NA|INR|NA|R|nielit|NA|NA|F|" + dt.Rows[0]["Sponsoring_Organisation_Code"].ToString() + "|BCAS01|" + dt.Rows[0]["Organisation_Code"].ToString() + "|NA|NA|NA|NA|" + "https://14.139.53.83:8000/BCASApplication/Forms/DetailsForms/PaymentResponse.aspx";


                        // strRequest = "NIELIT" + "|"+"1234567"+ "|" + "NA" + "|" + txtAmount.Text + "|NA|NA|NA|INR|NA|R|nielit|NA|NA|F|" + dt.Rows[0]["Sponsoring_Organisation_Code"].ToString() + "|BCAS01|" + dt.Rows[0]["Sponsoring_Organisation_Code"].ToString() + "|NA|NA|NA|NA|" + "http://localhost:6072/BCASApplication/Forms/DetailsForms/PaymentResponse.aspx";

                        string strReqWithChkKey = "";
                        strReqWithChkKey = strRequest + "|WYZZHkyZuU9K";

                        SHASample dataprg = new SHASample();
                        String hash = String.Empty;
                        hash = dataprg.GetSHA256(strReqWithChkKey);
                        hash = hash.ToUpper();
                        msg.Value = strRequest + "|" + hash;

                        //lblMsgValue.Text = msg.Value;
                        //lblMsgValue.Visible = false;


                    }

                }
                //using (DataTable dt = objPay.GetDetailsForMakePayment(Convert.ToInt32(Session["Transaction_ID"].ToString())))
                //{
                //    if (dt.Rows.Count != 0)
                //    {
                //        ddlSponsoredOrg.SelectedValue = dt.Rows[0]["Sponsoring_Organisation_Code"].ToString();
                //        txtAmount.Text = dt.Rows[0]["Amount_To_Pay"].ToString();                       
                //        lblPayeeOrgName.Text = dt.Rows[0]["Payee_Organisation_Name"].ToString();
                //        txtAmount.Text = (txtAmount.Text).Trim() + (".00").Trim();
                //        string strRequest = "";
                //        strRequest = "NIELIT" + "|" + dt.Rows[0]["Transaction_id"].ToString() + "|" + "NA" + "|" + txtAmount.Text + "|NA|NA|NA|INR|NA|R|nielit|NA|NA|F|" + dt.Rows[0]["Payee_Organisation_Code"].ToString() + "|BCAS01|" + dt.Rows[0]["Sponsoring_Organisation_Code"].ToString() + "|NA|NA|NA|NA|" + "http://bcas.nielit.in/bcas/Forms/DetailsForms/PaymentResponse.aspx";
                //        string strReqWithChkKey = "";
                //        strReqWithChkKey = strRequest + "|WYZZHkyZuU9K";
                        
                //        SHASample dataprg = new SHASample();
                //        String hash = String.Empty;
                //        hash = dataprg.GetSHA256(strReqWithChkKey);
                //        hash = hash.ToUpper();
                //        msg.Value = strRequest + "|" + hash;

                //        //lblMsgValue.Text = msg.Value;
                //        //lblMsgValue.Visible = false;


                //    }

                //}
                //###########            

            }
            else
            {
                Response.Redirect("~/Home.aspx");

            }
            

        }

    }
   
    protected void LoginStatus_LoggedOut(object sender, EventArgs e)
    {
        Session["login"] = false;
        Session.Abandon();
        Session.Clear();
        Response.Redirect("~/Home.aspx");
    }


}
