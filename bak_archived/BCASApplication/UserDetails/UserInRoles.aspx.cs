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
using System.Data.OleDb;
using BCASClassLibrary;
using System.IO;

public partial class UserDetails_UserInRoles : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if ((Convert.ToString(HttpContext.Current.Session["UserId"]).Trim().Length == 0))
        {
            HttpContext.Current.Response.Redirect("~/Home.aspx");
        }
        else
        {
            if (!Page.IsPostBack)
            {
                lb_Users.DataSource = Membership.GetAllUsers();
                lb_Users.DataBind();

                //--Selecting All the Roles--------------------------
                Roles.ApplicationName = Membership.ApplicationName;
                lb_AllRoles.DataSource = Roles.GetAllRoles();
                lb_AllRoles.DataBind();
            }
        }

    }

    protected void lb_Users_SelectedIndexChanged(object sender, EventArgs e)
    {
        //--Display all the roles of the selected user----------------------
        string UserName = lb_Users.SelectedValue;

        Roles.ApplicationName = Membership.ApplicationName;
        lb_UserRoles.DataSource = Roles.GetRolesForUser(UserName);
        lb_UserRoles.DataBind();
    }

    protected void btn_AddRole_Click(object sender, EventArgs e)
    {
        string UserName = lb_Users.SelectedValue;
        if (UserName.Length > 0)
        {
            Roles.ApplicationName = Membership.ApplicationName;
            foreach (ListItem item in lb_AllRoles.Items)
            {
                if (item.Selected)
                {
                   //--check whether role already exist or not---
                    if(!Roles.IsUserInRole(UserName, item.Text))    //if role not already added for user
                       Roles.AddUserToRole(UserName, item.Text);
                }
            }
            lb_UserRoles.DataSource = Roles.GetRolesForUser(UserName);
            lb_UserRoles.DataBind();
        }
        else
            lbl_error.Text = "User Name must be selected";
    }

    protected void btn_AddAll_Click(object sender, EventArgs e)
    {
        //--Get Selected user------------------------------------
        string UserName = lb_Users.SelectedValue;

        if (UserName.Length > 0)
        {
            Roles.ApplicationName = Membership.ApplicationName;
            string[] roles = Roles.GetAllRoles();
            if (roles.Length > 0)
            {
                //--Check whether roles exists or not-----------------------
                string[] UserRoles = Roles.GetRolesForUser(UserName);
                if (UserRoles.Length > 0)
                {
                    //--Some user roles already exists remove them
                    Roles.RemoveUserFromRoles(UserName, UserRoles);
                }

                Roles.AddUserToRoles(UserName, roles);
                lb_UserRoles.DataSource = Roles.GetRolesForUser(UserName);
                lb_UserRoles.DataBind();
            }
            else
                lbl_error.Text = "No Role exists create roles first";

            lb_UserRoles.DataSource = Roles.GetRolesForUser(UserName);
            lb_UserRoles.DataBind();
        }
        else
            lbl_error.Text = "User Name must be selected";
    }

 

    protected void btn_RemoveAll_Click(object sender, EventArgs e)
    {
        //--Get Selected user---------------------------------------------
        string UserName = lb_Users.SelectedValue;
        if (UserName.Length > 0)
        {
            Roles.ApplicationName = Membership.ApplicationName;
            string[] roles = Roles.GetRolesForUser(UserName);
            if (roles.Length > 0)
            {
                Roles.RemoveUserFromRoles(UserName, roles);
                lb_UserRoles.DataSource = Roles.GetRolesForUser(UserName);
                lb_UserRoles.DataBind();
            }
            else
                lbl_error.Text = "No Role exist for the user " + UserName;
        }
        else
            lbl_error.Text = "User Name must be selected";
    }

    protected void btn_RemoveRole_Click(object sender, EventArgs e)
    {
        string UserName = lb_Users.SelectedValue;
        if (UserName.Length > 0)
        {
            Roles.ApplicationName = Membership.ApplicationName;
            foreach (ListItem item in lb_UserRoles.Items)
            {
                if (item.Selected)
                    Roles.RemoveUserFromRole(UserName, item.Text);
            }
            lb_UserRoles.DataSource = Roles.GetRolesForUser(UserName);
            lb_UserRoles.DataBind();
        }
        else
            lbl_error.Text = "User Name must be selected";
    }

  
}
