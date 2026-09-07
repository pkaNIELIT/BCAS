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

public partial class Forms_DetailsForms_BCAS_Approval : System.Web.UI.Page
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
                        ddlOrganisation.DataValueField = "Organisation_Code";
                        ddlOrganisation.DataTextField = "Organisation_Name";
                        ddlOrganisation.DataBind();
                    }

                }
               
            }

        }
        

    }    
    
    private void ShowMessage(int count)
    {

        StringBuilder sb = new StringBuilder();

        sb.Append("<script type = 'text/javascript'>");

        sb.Append("alert('");

        sb.Append(count.ToString());

        sb.Append(" records Updated.');");

        sb.Append("</script>");

        ClientScript.RegisterStartupScript(this.GetType(),

                        "script", sb.ToString());

    }    
    
    private void BindGrid()
    {
                
        int scheduleID, Organisation;

        scheduleID = Convert.ToInt32(ddlSchedule.SelectedValue);

        Organisation = Convert.ToInt32(ddlOrganisation.SelectedValue);

        gvAll.Visible = true;

        using (DataTable dt = objBCAS.getNomination(scheduleID, Organisation))
        {
            
            gvAll.DataSource = dt;
            gvAll.DataBind();
            
        }


    }
   
    protected void OnPaging(object sender, GridViewPageEventArgs e)
    {

        gvAll.PageIndex = e.NewPageIndex;        
        BindGrid();       
      
    }


    protected void btnUpdateStatus_Click(object sender, EventArgs e)
    {
        var a = gvAll.SelectedRow;
        int count = 0;
        foreach (GridViewRow item in gvAll.Rows)
        {
            CheckBox chk = (CheckBox)item.Cells[0].FindControl("chk");
            if (chk != null && chk.Checked)
            {
                count++;
                UpdateRecord(Convert.ToInt32(((HyperLink)item.FindControl("Label1")).Text), Convert.ToInt32(item.Cells[11].Text));

            }
        }
        ShowMessage(count);
        //DataBind();
        BindGrid();
    }

    private void UpdateRecord(int Registration_Number, int Schedule_ID)
    {

        objBCAS.i_Schedule_ID = Schedule_ID;
        objBCAS.i_Registration_No = Registration_Number;
        objBCAS.v_BCAS_Approval_By = Session["user_id"].ToString();

        using (DataTable dt_Update = objBCAS.UpdateNominationApproval())
        {
            if (Convert.ToInt32(dt_Update.Rows[0][0]) != 0)
            {
                lbl_error.Text = "Transaction Failed";
                lbl_error.Visible = true;

            }
            else
            {
                //*******Display Payment Reciept
                lbl_error.Visible = false;
            }
        }     
        
    }


    protected void ddlSchedule_SelectedIndexChanged(object sender, EventArgs e)
    {
        //BindGrid();
    }

    protected void ddlOrganisation_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGrid();
    }
}
