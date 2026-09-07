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
using BCASClassLibrary;

public partial class CreateUser : System.Web.UI.Page
{
    TYP_CREATE_USER objCreateUser = new TYP_CREATE_USER();
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Convert.ToString(HttpContext.Current.Session["UserId"]).Trim().Length == 0))
        {
            HttpContext.Current.Response.Redirect("~/Home.aspx");
        }
       

    }

    protected void btn_CreateUser_Click(object sender, EventArgs e)
    {
        lbl_Message.Text = "";       
        if (ValidateUser())
        {
            try
            {
                //Membership.CreateUser(txt_UserName.Text, txt_Password.Text);
                //Roles.ApplicationName = Membership.ApplicationName;
                
                //******* Insert into UserDetails Table******                               
                objCreateUser.v_UserName = txt_UserName.Text;
                objCreateUser.v_Email = txt_email_id.Text;
                objCreateUser.c_Organisation_Type = ddlOrganisationType.SelectedValue;
                objCreateUser.i_OrganisationCode = Convert.ToInt32(ddlOrgName.SelectedValue);
                objCreateUser.v_Name = txtNameofUser.Text;
                objCreateUser.v_Created_By = Session["user_id"].ToString();
                objCreateUser.v_Mobile_No = txt_mobile_no.Text;

                using (DataTable new_dt = objCreateUser.InsertUserDetails ())
                {
                    if (Convert.ToInt32 (new_dt.Rows[0][0])==0)
                    {
                        //Response.Redirect("~/Home.aspx");
                        Membership.CreateUser(txt_UserName.Text, txt_Password.Text);
                        Roles.ApplicationName = Membership.ApplicationName;

                        lbl_Message.Text = "User Creation Successful.";
                        clearText();
                    }
                    else
                    {
                        lbl_Message.Text = "Error occured during user creation.Either user ID or emial already exists";
                        clearText();
                        
                    }

                }


                //Roles.AddUserToRole(txt_UserName.Text, "DataEntry");
                //Response.Redirect("~/UserLogin.aspx");
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
        bullet_NewUser.Items.Clear();
        //--------------------------------------------------------------------
        mev_UserName.Validate();
        if (!mev_UserName.IsValid)
        {
            bullet_NewUser.Items.Add("User ID must be Entered");
            valid = false;
        }

        mev_Password.Validate();
        if (!mev_Password.IsValid)
        {
            bullet_NewUser.Items.Add("Password must be Entered");
            valid = false;
        }

        mev_ConfirmPassword.Validate();
        if (!mev_ConfirmPassword.IsValid)
        {
            bullet_NewUser.Items.Add("Confirm Password must be Entered");
            valid = false;
        }

        if (txt_Password.Text != txt_ConfirmPassword.Text)
        {
            bullet_NewUser.Items.Add("Password and Confirm Password must match");
            valid = false;
        }

        mev_ImgVerifyCode.Validate();
        if (!mev_ImgVerifyCode.IsValid)
        {
            bullet_NewUser.Items.Add("Verification Code must be Entered");
            valid = false;
        }

        if (ImageVerifier1.Text != txt_ImgVerifyCode.Text)
        {
            bullet_NewUser.Items.Add("You have entered a wrong verification code.");
            valid = false;
        }

        mev_NameofUser.Validate();
        if (!mev_NameofUser.IsValid)
        {
            bullet_NewUser.Items.Add("User Name must be Entered");
            valid = false;
        }
        return valid;
    }

    protected void ddlOrganisationType_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlOrgName.Items.Clear();
        ddlOrgName.AppendDataBoundItems = true;
        ddlOrgName.Items.Insert(0, new ListItem("Select", string.Empty));       
        ddlOrgName.DataBind();

        //if (ddlOrganisationType.SelectedValue == "S")
        //{

        //}
        //else if (ddlOrganisationType.SelectedValue=="P")
        //{

        //}
    }

    private void clearText()
    {
        ddlOrganisationType.SelectedIndex  = 0;
        ddlOrgName.SelectedIndex = 0;
        txt_UserName.Text = "";
        txt_Password.Text = "";
        txt_ConfirmPassword.Text = "";
        txtNameofUser.Text = "";
        txt_email_id.Text = "";
        txt_mobile_no.Text = "";
        txt_ImgVerifyCode.Text = "";

    }

}
