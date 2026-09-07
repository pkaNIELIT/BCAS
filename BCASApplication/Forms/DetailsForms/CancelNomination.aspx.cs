using BCASClassLibrary;
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Profile;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class Forms_DetailsForms_CancelNomination : System.Web.UI.Page
{
    private TYP_BCAS_Approval objBCAS = new TYP_BCAS_Approval();
    protected void Page_Load(object sender, EventArgs e)
    {
        DisableForwordButton();
        HttpContext.Current.Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
        HttpContext.Current.Response.Cache.SetValidUntilExpires(false);
        HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        HttpContext.Current.Response.Cache.SetNoStore();


        if (!Page.IsPostBack)
        { 
            if (Convert.ToString(HttpContext.Current.Session["UserId"]).Trim().Length == 0)
            {
                ClearCookiesSessions();
                Response.Redirect("~/Home.aspx");
            } 
            else
            {
                using (DataTable bcasCourseSchedule = objBCAS.getQryBCASCourseSchedule())
                {
                    if (bcasCourseSchedule.Rows.Count <= 0)
                        return;
                    ddlSchedule.DataSource = (object)bcasCourseSchedule;
                    ddlSchedule.DataBind();
                }
            }

        }

    }

    public void DisableForwordButton()
    {
        Response.Expires = 0;
        Response.Cache.SetNoStore();
        Response.AppendHeader("Pragma", "no-cache");
    }
    public void ClearCookiesSessions()
    {
        Session.Abandon();
        Session.Clear();
        Session.RemoveAll();

        string[] myCookies = Request.Cookies.AllKeys;
        foreach (string cookie in myCookies)
        {
            Response.Cookies[cookie].Expires = DateTime.Now.AddDays(-1);
        }
    }

    protected void btnDetails_Click(object sender, EventArgs e)
    {
        txtRemarks.Text = "";
        lblRemarksCancel.Text = "";
    }
  
    protected void BtnRemarksCancel_Click(object sender, EventArgs e)
    {
        lblRemarksCancel.Text = "";
        if (this.TxtRegNo1.Text != "")
        {
            using (DataTable dataTable = this.objBCAS.CheckQryBCASNominatedCandidate(Convert.ToInt32(this.TxtRegNo1.Text), Convert.ToInt32(this.ddlSchedule.SelectedValue)))
            {
                if (dataTable.Rows.Count != 0)
                {
                    lblRemarksCancel.ForeColor = Color.Red;
                    lblRemarksCancel.Font.Bold = true;
                    lblRemarksCancel.Text = "Nominated Candidate Already Canceled by Admin";
                }
                else
                    CancelNominatedCandidate();
            }
        }
        else
        {
            lblRemarksCancel.ForeColor = Color.Red;
            lblRemarksCancel.Font.Bold = true;
            lblRemarksCancel.Text = "Enter the Registration Number";
        }
    }

    protected void CancelNominatedCandidate()
    {
        if (txtRemarks.Text != "")
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand("Cancel_of_NominatedCandidateAsPerRequest", connection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    SqlParameter sqlParameter = new SqlParameter();
                    sqlCommand.Parameters.AddWithValue("@scheduleID", (object)SqlDbType.Int).Value = (object)ddlSchedule.SelectedValue;
                    sqlCommand.Parameters.AddWithValue("@Registration_Number", (object)SqlDbType.Int).Value = (object)TxtRegNo1.Text;
                    sqlCommand.Parameters.AddWithValue("@username", (object)SqlDbType.VarChar).Value = (object)Session["UserID"].ToString();
                    sqlCommand.Parameters.AddWithValue("@CancelNominationRemarks", (object)SqlDbType.VarChar).Value = (object)txtRemarks.Text;
                    sqlCommand.ExecuteNonQuery();
                    lblRemarksCancel.ForeColor = Color.Green;
                    lblRemarksCancel.Font.Bold = true;
                    lblRemarksCancel.Text = "Nominated Candidate Successfully Canceled";
                }
            }
            this.grdcancelofnominatedcandidate.DataBind();
        }
        else
        {
            this.lblRemarksCancel.ForeColor = Color.Red;
            this.lblRemarksCancel.Font.Bold = true;
            this.lblRemarksCancel.Text = "Required Cancellation Remarks";
        }
    }

    protected void SqlDataSource1_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
        if (e.AffectedRows == 0)
            Label1.Text = "No Record Found";
        else
            Label1.Text = "";
    }

    protected void SqlDataSource2_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
        if (e.AffectedRows == 0)
            lblheadingnominatedcandidate.Text = "";
        else
            lblheadingnominatedcandidate.Text = "Details Of Nominated Cancel Candidate";
    }
}
