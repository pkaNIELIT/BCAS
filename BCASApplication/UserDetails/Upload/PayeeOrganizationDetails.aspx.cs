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
using System.Data.SqlClient;
using BCASClassLibrary;

public partial class UserDetails_PayeeOrganizationDetails : System.Web.UI.Page
{
    #region Page Load Event

    /// <param name="sender">Event sender</param>
    /// <param name="e">Event arguments</param>
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
                BindPayeeOrganizationGrid();
            }
        }
    }
    #endregion

    #region Private Methods


    private void BindPayeeOrganizationGrid()
    {
        try
        {

            lblMessage.Text = string.Empty;


            string userOrgCode = string.Empty;
            if (Session["UserOrg"] != null && !string.IsNullOrEmpty(Session["UserOrg"].ToString()))
            {
                userOrgCode = Session["UserOrg"].ToString();
            }


            SqlCommand cmd = SqlHelper.GetSqlCommand("GetPayeeOrganizationDetails");
            cmd.CommandType = CommandType.StoredProcedure;


            SqlParameter paramOrgCode = new SqlParameter("@Payee_Organisation_Code", SqlDbType.VarChar, 50);
            if (!string.IsNullOrEmpty(userOrgCode))
            {
                paramOrgCode.Value = userOrgCode;
            }
            else
            {
                paramOrgCode.Value = DBNull.Value;
            }
            paramOrgCode.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(paramOrgCode);


            DataTable dt = SqlHelper.ExecuteProcedure(cmd);


            if (dt != null && dt.Rows.Count > 0)
            {

                grdPayeeOrganizationDetails.DataSource = dt;
                grdPayeeOrganizationDetails.DataBind();
            }
            else
            {

                grdPayeeOrganizationDetails.DataSource = null;
                grdPayeeOrganizationDetails.DataBind();
                lblMessage.Text = "No Payee Organization details found for your organization.";
            }
        }
        catch (Exception ex)
        {

            lblMessage.Text = "Error loading data: " + ex.Message;

        }
    }
    #endregion

    #region Grid Events

    /// <param name="sender">Event sender</param>
    /// <param name="e">GridView page event arguments</param>
    protected void grdPayeeOrganizationDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {

            grdPayeeOrganizationDetails.PageIndex = e.NewPageIndex;


            BindPayeeOrganizationGrid();
        }
        catch (Exception ex)
        {

            lblMessage.Text = "Error during pagination: " + ex.Message;
        }
    }
    #endregion
}
