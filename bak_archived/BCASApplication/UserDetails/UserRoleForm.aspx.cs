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

public partial class UserRoleForm : System.Web.UI.Page
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
                //--Selecting All the Forms--------------------------
                DataSet ds = TYP_MENUMASTER.GetFormNames();
                foreach (DataRow rw in ds.Tables[0].Rows)
                {
                    string pFormName = rw["FORMNAME"].ToString();
                    lb_Forms.Items.Add(pFormName);
                }
                ds.Clear();
                ds.Dispose();
                lb_Forms.DataBind();

                //--Selecting All the Roles--------------------------
                Roles.ApplicationName = Membership.ApplicationName;
                lb_AllRoles.DataSource = Roles.GetAllRoles();
                lb_AllRoles.DataBind();
            }
        }
    }

    protected void lb_AllRoles_SelectedIndexChanged(object sender, EventArgs e)
    {
        //--Display all the roles of the selected user----------------------
        string RoleName = lb_AllRoles.SelectedValue;

        //Remove existing items if any-----------------------------
        AddItemsToList(RoleName);
    }

    protected void btn_AddRole_Click(object sender, EventArgs e)
    {
        //--Get Selected Role------------------------------------
        Roles.ApplicationName = Membership.ApplicationName;
        string RoleName = lb_AllRoles.SelectedValue;

        if (RoleName.Length > 0)
        {
            //--Now all the selected forms---------------------------
            foreach (ListItem item in lb_Forms.Items)
            {
                if (item.Selected)
                {
                    //*********** Added on: 08.01.2014

                    if (item.Text == "ApplicantDetails.aspx")
                    {
                        string[] arr = { "ApplicantDetails.aspx", "ApplicantDetails_Preview.aspx", "frmPhotoSignUpload.aspx", "Previous_Courses_attended.aspx", "Working_Experience.aspx", "ProceedToRelieve.aspx", "RelieveFromOrganisation.aspx", "Relieved_Candidates_Updation.aspx", "CompleteStep2.aspx", "PrintDetails.aspx" };
                        foreach (string page in arr)
                        {
                            DataSet ds1 = TYP_USERROLEFORM.GetRoleForm(RoleName, page);
                            Int32 count = ds1.Tables[0].Rows.Count;
                            ds1.Clear();
                            ds1.Dispose();


                            if (count == 0)    //if role not already added for user
                                TYP_USERROLEFORM.Insert(page, RoleName, HttpContext.Current.User.Identity.Name);
                        }

                    }
                    else if (item.Text == "BCAS_Approval.aspx")
                    {
                        string[] arr = { "BCAS_Approval.aspx", "BCAS_ViewDetails.aspx" };
                        foreach (string page in arr)
                        {
                            DataSet ds1 = TYP_USERROLEFORM.GetRoleForm(RoleName, page);
                            Int32 count = ds1.Tables[0].Rows.Count;
                            ds1.Clear();
                            ds1.Dispose();


                            if (count == 0)    //if role not already added for user
                                TYP_USERROLEFORM.Insert(page, RoleName, HttpContext.Current.User.Identity.Name);
                        }

                    }
                    else if (item.Text == "PayeeOrganisationPayment.aspx")
                    {
                        string[] arr = { "PayeeOrganisationPayment.aspx", "MakePayment.aspx","PaymentResponse.aspx" };
                        foreach (string page in arr)
                        {
                            DataSet ds1 = TYP_USERROLEFORM.GetRoleForm(RoleName, page);
                            Int32 count = ds1.Tables[0].Rows.Count;
                            ds1.Clear();
                            ds1.Dispose();


                            if (count == 0)    //if role not already added for user
                                TYP_USERROLEFORM.Insert(page, RoleName, HttpContext.Current.User.Identity.Name);
                        }

                    }
                    else
                    {
                        //*********till here

                        //--check whether role already exist or not---
                        DataSet ds = TYP_USERROLEFORM.GetRoleForm(RoleName, item.Text);
                        Int32 count = ds.Tables[0].Rows.Count;
                        ds.Clear();
                        ds.Dispose();

                        if (count == 0)    //if role not already added for user
                            TYP_USERROLEFORM.Insert(item.Text, RoleName, HttpContext.Current.User.Identity.Name);
                    }
                }
                AddItemsToList(RoleName);
            }
        }
        else
            lbl_error.Text = "Role Name must be selected";
    }

    protected void btn_AddAll_Click(object sender, EventArgs e)
    {
        //--Get Selected Role------------------------------------
        Roles.ApplicationName = Membership.ApplicationName;
        string RoleName = lb_AllRoles.SelectedValue;

        if (RoleName.Length > 0)
        {
            //--Get all Forms ----------------------------------------
            DataSet ds = TYP_MENUMASTER.GetFormNames();
            if (ds.Tables[0].Rows.Count > 0)
            {
                //--Delete all forms of this role-----------------------
                TYP_USERROLEFORM.Delete(RoleName, "*");

                foreach (DataRow rw in ds.Tables[0].Rows)
                {
                    string FormName=rw["FORMNAME"].ToString();
                    TYP_USERROLEFORM.Insert(FormName, RoleName, HttpContext.Current.User.Identity.Name);
                }
            }

            ds.Clear();
            ds.Dispose();
            AddItemsToList(RoleName);
        }
        else
            lbl_error.Text = "Role Name must be selected";
    }

 
    protected void btn_RemoveAll_Click(object sender, EventArgs e)
    {
        //--Get Selected user---------------------------------------------
        string RoleName = lb_AllRoles.SelectedValue;
        if (RoleName.Length > 0)
        {
            TYP_USERROLEFORM.Delete(RoleName,"*" );
            lb_FormRoles.Items.Clear();
        }
        else
            lbl_error.Text = "Role Name must be selected";
    }

    protected void btn_RemoveRole_Click(object sender, EventArgs e)
    {
        string RoleName = lb_AllRoles.SelectedValue;
        if (RoleName.Length > 0)
        {
            for (int i = 0; i < lb_FormRoles.Items.Count; i++)
            {
                ListItem item = lb_FormRoles.Items[i];
                if (item.Selected)
                    TYP_USERROLEFORM.Delete(RoleName, item.Text);
            }
            AddItemsToList(RoleName);
        }
        else
            lbl_error.Text = "Role Name must be selected";
    }

    protected void AddItemsToList(string RoleName)
    {
        //Remove existing items if any-----------------------------
        lb_FormRoles.Items.Clear();

        DataSet ds = TYP_USERROLEFORM.GetFormsForRole(RoleName);
        foreach (DataRow rw in ds.Tables[0].Rows)
        {
            string pFormName = rw["FORMNAME"].ToString();
            lb_FormRoles.Items.Add(pFormName);
        }
        ds.Clear();
        ds.Dispose();
    }
}
