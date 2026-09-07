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
using System.Reflection.Emit;
using System.Data.OleDb;

public partial class Forms_DetailsForms_ExamDateExtension : System.Web.UI.Page
{
    string strConnString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString; // get it from Web.config file

    TYP_ScreenersApplicantDetails objApplicant = new TYP_ScreenersApplicantDetails();

    protected void Page_Load(object sender, EventArgs e)
    {

        if ((Convert.ToString(HttpContext.Current.Session["UserId"]).Trim().Length == 0))
        {
            HttpContext.Current.Response.Redirect("~/Home.aspx");
        }

        if (!Page.IsPostBack)
        {

            string constr = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("GetCourseNameForLastDateExtension", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    //cmd.Parameters.AddWithValue("@Course_Code", Course_Code);
                    cmd.Connection = con;
                    con.Open();
                  
                    SqlDataAdapter adpt = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adpt.Fill(dt);
                    ddlExistingCourse.DataSource = dt;
                    ddlExistingCourse.DataTextField = "Course_Name";
                    ddlExistingCourse.DataValueField = "Course_Code";
                    ddlExistingCourse.DataBind();
                    con.Close();
                }
            }
            
            string UserName = Session["user_id"].ToString();
        }
    }
     
    protected void SubmitButton_Click(object sender, EventArgs e)
    {
        string courecode = ddlExistingCourse.SelectedValue;
       
        DateTime P_start_date;
        bool isValidDate = DateTime.TryParseExact(ddlSchedule.SelectedItem.Text.ToString(), "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out P_start_date);
       
        //DateTime P_start_date = DateTime.ParseExact(ddlSchedule.SelectedItem.Text.ToString(), "yyyy/MM/dd", null);
       
        DateTime newDate = Calendar1.SelectedDate;

        string P_start_date_tmp = P_start_date.ToString("yyyy-MM-dd");
        string newDate_tmp = newDate.ToString("yyyy-MM-dd");

        try
        {
            string connectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("USP_last_date_extention", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Assuming you have a stored procedure to update the schedule
                    cmd.Parameters.AddWithValue("@P_course_code", courecode);
                    cmd.Parameters.AddWithValue("@P_start_date", P_start_date_tmp);
                    cmd.Parameters.AddWithValue("@PExtention_date_for_last_date", newDate_tmp);
                    cmd.Parameters.Add("@Pmsg", SqlDbType.VarChar, 100);
                    cmd.Parameters["@Pmsg"].Direction = ParameterDirection.Output;
                    // Execute the command
                    cmd.ExecuteNonQuery();
                    string mdg = cmd.Parameters["@Pmsg"].Value.ToString();

                    Label3.Text = mdg.ToString();
                    

                }
            }
        }
        catch (Exception ex)
        {
            // Handle any errors
            Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
            Label3.Text = ex.Message.ToString();
        }

    }
     
    protected void ddlExistingCourse_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlExistingCourse.SelectedValue != "0")
        {
            ddlSchedule.Items.Clear();
            ddlSchedule.Items.Insert(0, new ListItem("--Select--", "0"));
             Int64 Course_Code = Convert.ToInt64(ddlExistingCourse.SelectedValue);

            string constr = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("GetStartDateByCourseCode", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    
                    cmd.Parameters.AddWithValue("@Course_Code", Course_Code);
                    cmd.Connection = con;
                    con.Open();
                    SqlDataAdapter adpt = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adpt.Fill(dt);
                    ddlSchedule.DataSource = dt;
                    ddlSchedule.DataTextField = "start_date"; 
                    ddlSchedule.DataValueField = "start_date";
                    ddlSchedule.DataBind();
                    con.Close();
                }
            }
        }
    }

   
}
