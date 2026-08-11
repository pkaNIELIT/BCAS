using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Forms_DetailsForms_UpdateCandidatePayeeOrg : System.Web.UI.Page
{

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
                //string conStr;
                //SqlConnection con;
                //SqlCommand cmd;
                //SqlDataAdapter adr;
                //DataTable dt;


                //conStr = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
                //using (con = new SqlConnection(conStr))
                //{
                //    using (cmd = new SqlCommand("getCourseWithSchedule", con))
                //    {
                //        cmd.CommandType = CommandType.StoredProcedure;
                //        // or cmd.Parameters.AddWithValue("@fname", ddlEmployees.SelectedText.ToString()); 
                //        // according to your query at the backend
                //        adr = new SqlDataAdapter(cmd);
                //        dt = new DataTable();
                //        adr.Fill(dt);

                //    }
                //}
                //ddlSchedule.DataSource = dt;
                //ddlSchedule.DataTextField = "Course_Schedule";
                //ddlSchedule.DataValueField = "Schedule_ID";
                //ddlSchedule.DataBind();


                ddlSchedule.DataBind();

            }
        }

        GridView1.Visible = false;

        GridViewBtnEdit.Visible = false;
        divBilling.Visible = false;
    } 

    protected void  btnDisplay_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            lblError.Visible = false;
            GridView1.DataBind();
            GridView1.Visible = true;
        }


    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            GridViewBtnEdit.Visible = true;
        }
    }


    protected void GridViewBtnEdit_Click(object sender, EventArgs e)
    {
        GridView1.Visible = true;
        divBilling.Visible = true;

        //string conStr;
        //SqlConnection con;
        //SqlCommand cmd;
        //SqlDataAdapter adr;
        //DataTable dt;
        //string conStr;
        //conStr = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
        //using (con = new SqlConnection(conStr))
        //{
        //    using (cmd = new SqlCommand("[GetPayeeOrganisation]", con))
        //    {
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        // or cmd.Parameters.AddWithValue("@fname", ddlEmployees.SelectedText.ToString()); 
        //        // according to your query at the backend
        //        adr = new SqlDataAdapter(cmd);
        //        dt = new DataTable();
        //        adr.Fill(dt);
        //    }
        //}

        //ddl_BillerOrgCode.DataSource = dt;
        //ddl_BillerOrgCode.DataTextField = "Payee_Organisation_Name";
        //ddl_BillerOrgCode.DataValueField = "Payee_Organisation_Code";
        //ddl_BillerOrgCode.DataBind();
        ddl_BillerOrgCode.SelectedIndex = 0;

    }

    protected void btnUpdatePayee_Click(object sender, EventArgs e)
    {

        string strConnString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString; // get it from Web.config file
        using (SqlConnection objConn = new SqlConnection(strConnString))
        {
            objConn.Open();
          
            SqlCommand objCmd2 = new SqlCommand("UpdateCandidatePayeeOrg", objConn);

            objCmd2.CommandType = CommandType.StoredProcedure;

            try
            {
                     
                        objCmd2.Parameters.Clear();
                        objCmd2.CommandType = CommandType.StoredProcedure;
                //objCmd2.Parameters.AddWithValue("@BillerOrgCode", ddl_BillerOrgCode.SelectedValue.ToString());
                //objCmd2.Parameters.AddWithValue("@ScheduleID", ddlSchedule.SelectedValue.ToString());
                //objCmd2.Parameters.AddWithValue("@RegNo", txtRegNo.Text);
                objCmd2.Parameters.Add(new SqlParameter("@BillerOrgCode", SqlDbType.Int, 10));
                objCmd2.Parameters["@BillerOrgCode"].Value = Convert.ToInt32(ddl_BillerOrgCode.SelectedValue.ToString());
                objCmd2.Parameters.Add(new SqlParameter("@ScheduleID", SqlDbType.Int, 10));
                objCmd2.Parameters["@ScheduleID"].Value = Convert.ToInt32(ddlSchedule.SelectedValue.ToString());
                objCmd2.Parameters.Add(new SqlParameter("@RegNo", SqlDbType.Int, 10));
                objCmd2.Parameters["@RegNo"].Value = Convert.ToInt32(txtRegNo.Text);
                objCmd2.ExecuteNonQuery();

                ddlSchedule.SelectedIndex = 0;
                txtRegNo.Text = "";
                lblError.Text = "Updation Done";
                lblError.Visible = true;
            }
            catch (Exception)
            {
                Session["Msg"] = "Some Error Occured";
            }
            finally
            {
                objConn.Close();
               
            }
        }

    }


}
