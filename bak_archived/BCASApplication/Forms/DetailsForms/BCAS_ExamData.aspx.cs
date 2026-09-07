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

public partial class Forms_DetailsForms_BCAS_ExamData : System.Web.UI.Page
{
    TYP_BCAS_Approval objBCAS = new TYP_BCAS_Approval();
    Int64 DemandNoteId = 0;
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
            }
        } 
    }    
    
    private void ShowMessage(int count)
    {

        StringBuilder sb = new StringBuilder();

        sb.Append("<script type = 'text/javascript'>");

        sb.Append("alert('");

        sb.Append(count.ToString());

        sb.Append(" records demand note Generated !');");

        sb.Append("</script>");

        ClientScript.RegisterStartupScript(this.GetType(),

                        "script", sb.ToString());

    }    
    
    private void BindGrid()
    {                
        int scheduleID;
        scheduleID = Convert.ToInt32(ddlSchedule.SelectedValue);
        gvAll.Visible = true;
        string vENTER_BY = HttpContext.Current.User.Identity.Name;
        using (DataTable dt = objBCAS.GetExamDataNew(scheduleID,vENTER_BY ))
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


    protected void btnExamData_Click(object sender, EventArgs e)
    {
        if (ddlSchedule.SelectedValue == "0")
        {
            ShowAlertMessage("Please select Course Schedule !!");
            lblerror.Text = "Please select Course Schedule !!";
            return;
        }
        int scheduleID;

        scheduleID = Convert.ToInt32(ddlSchedule.SelectedValue);
        Response.ClearContent();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "BCASExamData.xls"));
        Response.ContentType = "application/ms-excel";
        DataTable dt = objBCAS.GetExamData(scheduleID);
        string str = string.Empty;
        foreach (DataColumn dtcol in dt.Columns)
        {
            Response.Write(str + dtcol.ColumnName);
            str = "\t";
        }
        Response.Write("\n");
        foreach (DataRow dr in dt.Rows)
        {
            str = "";
            for (int j = 0; j < dt.Columns.Count; j++)
            {
                Response.Write(str + Convert.ToString(dr[j]));
                str = "\t";
            }
            Response.Write("\n");
        }
        Response.End();
    }


    public void GenerateDemandNote(int Schedule_ID, int countRecord, string ServiceID)
    {
        try
        {

            string constr = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            using (SqlConnection Conn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("InsertDemandNote", Conn))
                {
                    Conn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Schedule_ID", Schedule_ID);
                    cmd.Parameters.AddWithValue("@countRecord", countRecord);                  
                    cmd.Parameters.AddWithValue("@ServiceID", ServiceID);
                    cmd.Parameters.AddWithValue("@Created_By", Session["user_id"].ToString());
                    cmd.Parameters.Add("@DemandNoteid", SqlDbType.Int).Direction = ParameterDirection.Output; 
                    cmd.ExecuteNonQuery();

                    string id = cmd.Parameters["@DemandNoteid"].Value.ToString();

                    DemandNoteId = Convert.ToInt64(id);
                }
            }
        }
        catch (Exception ex)
        {           
        }
    }
    private void UpdateCanData(int Registration_Number, int Schedule_ID, Int64 demandNoteId)
    {
        string constr = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
        using (SqlConnection Conn = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("UpdateCandDemandNote", Conn))
            {
                Conn.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Schedule_ID", Schedule_ID);
                cmd.Parameters.AddWithValue("@Registration_Number", Registration_Number);
                cmd.Parameters.AddWithValue("@DemandNoteid", demandNoteId);
                cmd.ExecuteNonQuery();              
            }
        }

    }
    private void ShowAlertMessage(string msg)
    {

        StringBuilder sb = new StringBuilder();

        sb.Append("<script type = 'text/javascript'>");

        sb.Append("alert('");

        sb.Append(msg);

        sb.Append("</script>");

        ClientScript.RegisterStartupScript(this.GetType(),

                        "script", sb.ToString());

    }    

    protected void ddlSchedule_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGrid();
        lblerror.Text = "";
    }
     
}
