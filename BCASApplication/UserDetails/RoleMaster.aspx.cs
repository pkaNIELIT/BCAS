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

public partial class RoleMaster : System.Web.UI.Page
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
                GridView1.DataSource = Roles.GetAllRoles();
                GridView1.DataBind();
            }
        }

    }

    protected void btn_CreateRole_Click(object sender, EventArgs e)
    {
        lbl_error.Text="";
        if (!Roles.RoleExists(txt_role.Text.ToUpper()))
        {

            Roles.ApplicationName = Membership.ApplicationName;
            Roles.CreateRole(txt_role.Text.ToUpper());
            txt_role.Text = "";
            GridView1.DataSource = Roles.GetAllRoles();
            GridView1.DataBind();
        }
        else
            lbl_error.Text = "Role already exists";
    }

    protected void btn_DeleteRole_Click(object sender, EventArgs e)
    {
        lbl_error.Text = "";
        Roles.ApplicationName = Membership.ApplicationName;
        bool exists = Roles.RoleExists(txt_role.Text.ToUpper());
        if (exists)
        {
            string[] userList=Roles.GetUsersInRole(txt_role.Text.ToUpper());
            if(userList.Length>0)
              Roles.RemoveUsersFromRole(userList, txt_role.Text.ToUpper());

            Roles.DeleteRole(txt_role.Text.ToUpper());
            txt_role.Text = "";
            GridView1.DataSource = Roles.GetAllRoles();
            GridView1.DataBind();
        }
        else
            lbl_error.Text = "Role does not exist";
    }

    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        txt_role.Text = GridView1.SelectedRow.Cells[1].Text;
    }

    protected void btn_quit_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/HomePage.aspx");
    }
}


