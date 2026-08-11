using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
//using System.Web.Mail;
using System.Net.Mail;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.OleDb;
using BCASClassLibrary;

using System.IO;
using System.Text;
using System.Globalization;                 
using AjaxControlToolkit;

public partial class UserDetails_ChangePassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Convert.ToString(HttpContext.Current.Session["UserId"]).Trim().Length == 0))
        {
            HttpContext.Current.Response.Redirect("~/Home.aspx");
        }

    }
    protected void btn_ResetPassword_Click(object sender, EventArgs e)
    {
        string newpassword="";

        lbl_Message.Text = "";
        if (ValidateUser())
        {
            try
            {
                MembershipUser updinfo = Membership.GetUser(txt_UserName.Text);
                updinfo.UnlockUser();
                //Match the secret Question and Answer with the stored details----------------
                //if (updinfo.PasswordQuestion == cdd_PasswordQuestion.SelectedValue)
                {
                    //Reset the password-------------------------------------------------
                    //newpassword = updinfo.ResetPassword(txt_PasswordAnswer.Text);
                    newpassword = updinfo.ResetPassword();
                    Membership.UpdateUser(updinfo);
                    lbl_Message.Text = newpassword;
                }

                //try
                //{
                //    string Body = "<b>Username: </b>" + txt_UserName.Text + "<br/>" + "<b>New-Password: </b>" + newpassword + "<br/>";
                //    MyLibrary.SendMail(updinfo.Email, "New Password", Body);

                //    lbl_Message.Text = "eMail Send OK... ";
                //}
                //catch (Exception ex)
                //{
                //    lbl_Message.Text = "Error Sending eMail: " + ex.Message.ToString();
                //}

               // Response.Redirect("~/UserLogin.aspx");
            }
            catch (Exception err)
            {
                lbl_Message.Text = err.Message;
            }
        }
        else
            return;
    }
    protected bool ValidateUser()
    {
        bool valid = true;
        bullet_ResetPassword.Items.Clear();
        //--------------------------------------------------------------------
        mev_UserName.Validate();
        if (!mev_UserName.IsValid)
        {
            bullet_ResetPassword.Items.Add("User Name must be Entered");
            valid = false;
        }

        //rfv_PasswordQuestion.Validate();
        //if (!rfv_PasswordQuestion.IsValid)
        //{
        //    bullet_ResetPassword.Items.Add("Password Question must be Entered");
        //    valid = false;
        //}

        //mev_PasswordAnswer.Validate();
        //if (!mev_PasswordAnswer.IsValid)
        //{
        //    bullet_ResetPassword.Items.Add("Password Answer must be Entered");
        //    valid = false;
        //}


        mev_ImgVerifyCode.Validate();
        if (!mev_ImgVerifyCode.IsValid)
        {
            bullet_ResetPassword.Items.Add("Verification Code must be Entered");
            valid = false;
        }

        if (ImageVerifier1.Text != txt_ImgVerifyCode.Text)
        {
            bullet_ResetPassword.Items.Add("You have entered a wrong verification code.");
            valid = false;
        }
        return valid;

    }
}
