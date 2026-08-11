using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Threading;
using BCASClassLibrary;
using System.Data.SqlClient;
using System.Text;


public partial class Forms_DetailsForms_BCAS_Approval_Rejection : System.Web.UI.Page
{
    TYP_BCAS_Approval objBCAS = new TYP_BCAS_Approval();

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

                using (DataTable dt = objBCAS.getBCASCourseSchedule())
                {
                    if (dt.Rows.Count > 0)
                    {
                        ddlSchedule.DataSource = dt;
                        ddlSchedule.DataTextField = "Course_Schedule"; // This is text displayed
                        ddlSchedule.DataValueField = "Schedule_ID"; // This is the value returned
                        ddlSchedule.DataBind();
                    }

                }

                using (DataTable dtOrg = objBCAS.getGetAllOrganisation())
                {
                    if (dtOrg.Rows.Count > 0)
                    {
                        ddlOrganisation.DataSource = dtOrg;                       
                        ddlOrganisation.DataTextField = "Organisation_Name";
                        ddlOrganisation.DataValueField = "Organisation_Code";
                        ddlOrganisation.DataBind();
                    }

                }

            }

        }

    }






    protected void ddlSchedule_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridView1.DataBind();

    }
    protected void ddlOrganisation_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridView1.DataBind();
    }

    protected void btnUpdateStatus_Click(object sender, EventArgs e)
    {

    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList d = (DropDownList)sender;
        GridViewRow r = (GridViewRow)d.NamingContainer;
        if (((DropDownList)r.FindControl("ddl_Approval_Status")).SelectedValue == "Y")
        {
            ((RequiredFieldValidator)(GridView1.Rows[GridView1.EditIndex].FindControl("rfvRemarks"))).Enabled = false;
        }
        else
        {
            ((RequiredFieldValidator)(GridView1.Rows[GridView1.EditIndex].FindControl("rfvRemarks"))).Enabled = true;
        }
       
        
    }


    protected void SqlDataSource1_Updating(object sender, SqlDataSourceCommandEventArgs e)
    {
        e.Command.Parameters["@BCAS_Approval_Status"].Value = ((DropDownList)(GridView1.Rows[GridView1.EditIndex].FindControl("ddl_Approval_Status"))).SelectedValue;
        e.Command.Parameters["@BCAS_Remarks"].Value = ((TextBox)(GridView1.Rows[GridView1.EditIndex].FindControl("txtRemarks"))).Text;
        e.Command.Parameters["@i_Schedule_ID"].Value = Convert.ToInt32((GridView1.Rows[GridView1.EditIndex].Cells[12].Text));
        //e.Command.Parameters["@i_Registration_No"].Value = Convert.ToInt32(((HyperLink)GridView1.Rows[GridView1.EditIndex].FindControl("Label1")).Text);
        e.Command.Parameters["@i_Registration_No"].Value = Convert.ToInt32((GridView1.Rows[GridView1.EditIndex].Cells[3].Text));
        e.Command.Parameters["@BCAS_Approval_Date"].Value = DateTime.Now;

    }

    protected void OnPaging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        GridView1.DataBind();
       
    }

    
}
