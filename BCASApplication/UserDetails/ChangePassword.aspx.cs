using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Security;
using BCASClassLibrary;

public partial class UserDetails_ChangePassword : System.Web.UI.Page
{
    TYP_ApplicantDetails obj = new TYP_ApplicantDetails();

    protected void Page_Load(object sender, EventArgs e)
    {
      //  btn_ChangePassword.Attributes.Add("OnClick", "SpPwd()");

        if ((Convert.ToString(HttpContext.Current.Session["UserId"]).Trim().Length == 0))
        {
            HttpContext.Current.Response.Redirect("~/Home.aspx");
        }       

    }

    protected void btn_ChangePassword_Click(object sender, EventArgs e)
    {
        
        if (Page.IsValid)
        {

           lbl_Message.Text = "";
           if (ValidateUser())
           {
                try
                {
                    if (txt_OldPassword.Text == "")
                    {
                        lbl_msg.Text = "";
                        lbl_msg.Text = "Old Password required !!";
                        lbl_msg.Visible = true;
                        return;
                    }
                    if (txt_Password.Text == "")
                    {
                        lbl_msg.Text = "";
                        lbl_msg.Text = "New Password required !!";
                        lbl_msg.Visible = true;
                        return;
                    }
                    if (txt_ConfirmPassword.Text == "")
                    {
                        lbl_msg.Text = "";
                        lbl_msg.Text = "Confirm Password required !!";
                        lbl_msg.Visible = true;
                        return;
                    }

                    if (txt_Password.Text != txt_ConfirmPassword.Text)
                    {
                        lbl_msg.Text = "";
                        lbl_msg.Text = "New Password and Confirm Password must match !!";
                        lbl_msg.Visible = true;
                        return;
                    }
                    string user = HttpContext.Current.User.Identity.Name;

//                    if (txt_OldPassword.Text == pwd)
                        // SA May-2026: removed 1==1 bypass — old password must be validated
                        if(Membership.ValidateUser(user, txt_OldPassword.Text))
                    {
                        MembershipUser updinfo = Membership.GetUser(HttpContext.Current.Session["UserId"].ToString().Trim());
                        //MembershipUser updinfo = Membership.GetUser(HttpContext.Current.User.Identity.Name);
                        updinfo.UnlockUser();
                        updinfo.ChangePassword(txt_OldPassword.Text, txt_Password.Text);

                        if (updinfo.IsApproved)
                        {
                            //***** Update Password
                            //TYPApplicantDetails obj = new TYPApplicantDetails();
                          // using (DataTable dt = obj.UpdatePassword(HttpContext.Current.Session["UserId"].ToString().Trim(), txt_Password.Text))
                            {
                                //if (dt.Rows.Count > 0)
                                //{
                                //    lbl_Message.Text = "password updated";
                                //} 
                            }
                            //*********
                            Random rd = new Random();
                            Session["New"] = rd.Next();
                            HttpCookie myCookie = new HttpCookie("Session");
                            myCookie.Value = Session["New"].ToString();
                            myCookie.Path = "/";
                            Response.Cookies.Add(myCookie);

                            //**********       
                            lbl_msg.Text = "";
                            lbl_msg.Text = "Password has been changed";
                            lbl_msg.Visible = true;

                           

                        }
                        
                    }
                    else
                    {
                        txt_OldPassword.Text= "";
                        lbl_msg.Text = "";
                        lbl_Message.Text = "Old Password Does Not Match";
                    }

                    

                }
                catch (Exception err)
                {
                    ClearCookiesSessions();                 
                    lbl_Message.Text = err.Message;
                }
            }
            //else
            //{
            //    lbl_msg.Visible = false;
            //    return;
            //}
        }
        
       
    }


    //protected bool ValidateUser()
    //{
    //    bool valid = true;
    //    bullet_ChangePassword.Items.Clear();
    //    //--------------------------------------------------------------------
    //    rfv_OldPassword.Validate();
    //    if (!rfv_OldPassword.IsValid)
    //    {
    //        bullet_ChangePassword.Items.Add("Old Password must be Entered");
    //        valid = false;
    //    }

    //    if (txt_OldPassword.Text.Trim() == txt_Password.Text.Trim())
    //    {
    //        bullet_ChangePassword.Items.Add("Old Password is not Allowed");
    //        valid = false;
    //    }

    //    if (txt_Password.Text != txt_ConfirmPassword.Text)
    //    {
    //        bullet_ChangePassword.Items.Add("Password and Confirm Password must match");
    //        valid = false;
    //    }

       
    //    return valid;
    //}
    public bool ValidateUser()
    {
        bool valid = true;
        if (txt_Password.Text != txt_ConfirmPassword.Text)
        {
           
            string script = "alert('Password and Confirm Password must match');";
            ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", script, true);
            valid = false;
        }
      
        if (ImageVerifier1.Text != txt_ImgVerifyCode.Text)
        {
            string script = "alert('You have entered a wrong verification code');";
            ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", script, true);
            valid = false;
        }
        return valid;
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


}
