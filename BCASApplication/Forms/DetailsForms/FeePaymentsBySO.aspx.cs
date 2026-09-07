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


public partial class Forms_DetailsForms_FeePaymentsBySO : System.Web.UI.Page
{

    TYP_FeePaymentsBySO objFee = new TYP_FeePaymentsBySO();

    TYP_USER_Details objUserDetails = new TYP_USER_Details();
    
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

                //lblOrgName.Text = "Sponsoring Organisation :  " + Session["OrgName"].ToString();
                ddlSchedule.DataBind();
                
                if (Convert.ToString(HttpContext.Current.Session["OrgName"]).Trim().Length == 0)
                {
                    lblHeading.Visible = false;
                }
                else
                {
                    lblHeading.Visible = true;
                    lblOrgName.Text = "Sponsoring Organisation: " + Session["OrgName"].ToString();
                }

            }

        }

        GridView1.Visible = false;
        grdPrevCourses.Visible = false;
        grdWorkingExp.Visible = false;
        GridView2.Visible = false;

    }


    protected void btnSaveNomination_Click(object sender, EventArgs e)
    {

        if (Page.IsValid)
        {
                if (Convert.ToInt32(ddlSponsoredOrg.SelectedIndex) == 0 || txtRegdNo.Text.Length == 0 || Convert.ToInt32(ddlSchedule.SelectedIndex) == 0)
            {
                lblError.Text = "Please Select All Values!!!!!";
                lblError.Visible = true;
            }
            else
            {
                lblMsg.Visible = false;
                lblError.Visible = false;
                
                        objFee.i_Schedule_ID = Convert.ToInt32(ddlSchedule.SelectedValue);
                        objFee.i_Registration_No = Convert.ToInt32(txtRegdNo.Text);
                        objFee.v_Fee_Payment_By = Session["user_id"].ToString();
                        objFee.i_Sponsoring_Organisation_Code = Convert.ToInt32(Session["UserOrg"]);
                        objFee.i_Payee_Organisation_Code = Convert.ToInt32(ddlSponsoredOrg.SelectedValue);
                        if (RadioButtonList1.SelectedValue == "N")
                        {
                        //**** Check for Last Payment Date                          
                            using (DataTable dt_LastFeePymtDate = objFee.Get_LastFeePaymentDate(Convert.ToInt32(ddlSchedule.SelectedValue)))
                            {
                                if (Convert.ToInt32(dt_LastFeePymtDate.Rows[0][0]) == -786)
                                {
                                    lblError.Text = "Fee Payments Date is Over for the Selected Schedule.";
                                    lblError.Visible = true;
                                }
                                else
                                {                                   
                                    using (DataTable dt = objFee.Update_Fee_PaymentBalance())
                                    {
                                        if ((Convert.ToInt32(dt.Rows[0][0]) == 0) & (Convert.ToInt32(dt.Rows[0][1]) == 0))
                                        {
                                            lblMsg.Text = "Fee Payment Successful";
                                            lblMsg.Visible = true;
                                            lblError.Visible = false;
                                        }
                                        else if (Convert.ToInt32(dt.Rows[0][0]) == -111)
                                        {
                                            lblError.Text = "Insufficent Balance!!!";
                                            lblError.Visible = true;
                                        }
                                        else if (Convert.ToInt32(dt.Rows[0][0]) == -786)
                                        {
                                            lblError.Text = "Either Fee Already Paid or Record Does Not Exist!!!";
                                            lblError.Visible = true;
                                        }
                                        else
                                        {
                                            lblError.Text = "Fee Payment Failed!!!";
                                            lblError.Visible = true;
                                        }

                                    }
                                
                                }
                           }

                }

                        if (RadioButtonList1.SelectedValue == "Y")
                            {
                                //**** Check for Last Payment Date
                                using (DataTable dt_LastFeePymtDate = objFee.Get_LastFeePaymentDate_Reappearing(Convert.ToInt32(ddlSchedule.SelectedValue)))
                                {
                                    if (Convert.ToInt32(dt_LastFeePymtDate.Rows[0][0]) == -786)
                                    {
                                        lblError.Text = "Reappearing Fee Payments Date is Over for the Selected Schedule.";
                                        lblError.Visible = true;
                                    }
                                    else
                                    {

                                        using (DataTable dt = objFee.Update_Fee_PaymentBalance())
                                        {
                                            if ((Convert.ToInt32(dt.Rows[0][0]) == 0) & (Convert.ToInt32(dt.Rows[0][1]) == 0))
                                            {
                                                lblMsg.Text = "Fee Payment Successful";
                                                lblMsg.Visible = true;
                                                lblError.Visible = false;
                                            }
                                            else if (Convert.ToInt32(dt.Rows[0][0]) == -111)
                                            {
                                                lblError.Text = "Insufficent Balance!!!";
                                                lblError.Visible = true;
                                            }
                                            else if (Convert.ToInt32(dt.Rows[0][0]) == -786)
                                            {
                                                lblError.Text = "Either Fee Already Paid or Record Does Not Exist!!!";
                                                lblError.Visible = true;
                                            }
                                            else
                                            {
                                                lblError.Text = "Fee Payment Failed!!!";
                                                lblError.Visible = true;
                                            }

                                        }

                                    }
                                }

                            }

                                                      
                    }
            

        }             
         
    }

    protected void  btnDisplay_Click(object sender, EventArgs e)
    {
        lblMsg.Visible = false;
        lblError.Visible = false;
        GridView1.DataBind();
        GridView1.Visible = true;

        grdPrevCourses.DataBind();
        grdPrevCourses.Visible = true;
        grdWorkingExp.DataBind();
        grdWorkingExp.Visible = true;
    }

    protected void btnGetDetails_Click(object sender, EventArgs e)
    {
        lblMsg.Visible = false;
        lblError.Visible = false;
        GridView2.DataBind();
        GridView2.Visible = true;
    }
}