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


public partial class UserDetails_SponsoringOrganizationDetails : System.Web.UI.Page
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
                BindSponsoringOrganizationGrid();
            }
        }
    }
    #endregion

    #region Private Methods

    private void BindSponsoringOrganizationGrid()
    {
        try
        {

            lblMessage.Text = string.Empty;


            string userOrgCode = string.Empty;
            if (Session["UserOrg"] != null && !string.IsNullOrEmpty(Session["UserOrg"].ToString()))
            {
                userOrgCode = Session["UserOrg"].ToString();
            }


            SqlCommand cmd = SqlHelper.GetSqlCommand("GetSponsoringOrganizationDetails");
            cmd.CommandType = CommandType.StoredProcedure;


            SqlParameter paramOrgCode = new SqlParameter("@Organisation_Code", SqlDbType.VarChar, 50);
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

                grdSponsoringOrganizationDetails.DataSource = dt;
                grdSponsoringOrganizationDetails.DataBind();
            }
            else
            {

                grdSponsoringOrganizationDetails.DataSource = null;
                grdSponsoringOrganizationDetails.DataBind();
                lblMessage.Text = "No Sponsoring Organization details found for your organization.";
            }
        }
        catch (Exception ex)
        {

            lblMessage.Text = "Error loading data: " + ex.Message;

        }
    }
    #endregion

    #region Grid Events

    /// <summary>
    /// Handles GridView row editing event
    /// </summary>
    /// <param name="sender">Event sender</param>
    /// <param name="e">GridView edit event arguments</param>
    protected void grdSponsoringOrganizationDetails_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            // Set the edit index to enable editing mode for the selected row
            grdSponsoringOrganizationDetails.EditIndex = e.NewEditIndex;

            // Rebind the grid to show the row in edit mode
            BindSponsoringOrganizationGrid();
        }
        catch (Exception ex)
        {
            lblMessage.Text = "Error during editing: " + ex.Message;
        }
    }

    /// <summary>
    /// Handles GridView row update event
    /// </summary>
    /// <param name="sender">Event sender</param>
    /// <param name="e">GridView update event arguments</param>
    protected void grdSponsoringOrganizationDetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            lblMessage.Text = string.Empty;

            // Get the edited row
            GridViewRow row = grdSponsoringOrganizationDetails.Rows[e.RowIndex];

            // Get the primary key (Sponsoring_Organisation_Code) - it's read-only
            string sponsoringOrgCode = grdSponsoringOrganizationDetails.DataKeys[e.RowIndex].Value.ToString();

            // Get the updated values from the text boxes in edit mode
            string sponsoringOrgName = ((TextBox)row.Cells[2].Controls[0]).Text.Trim();
            string sponsoringOrgState = ((TextBox)row.Cells[3].Controls[0]).Text.Trim();
            string sponsoringAddress = ((TextBox)row.Cells[4].Controls[0]).Text.Trim();
            string gstNumber = ((TextBox)row.Cells[5].Controls[0]).Text.Trim();
            string stateCode = ((TextBox)row.Cells[6].Controls[0]).Text.Trim();

            // Create SQL command to update the sponsoring organization details
            SqlCommand cmd = SqlHelper.GetSqlCommand("UpdateSponsoringOrganizationDetails");
            cmd.CommandType = CommandType.StoredProcedure;

            // Add parameters
            cmd.Parameters.Add(new SqlParameter("@Sponsoring_Organisation_Code", SqlDbType.VarChar, 50) { Value = sponsoringOrgCode });
            cmd.Parameters.Add(new SqlParameter("@Sponsoring_Organisation_Name", SqlDbType.VarChar, 200) { Value = sponsoringOrgName });
            cmd.Parameters.Add(new SqlParameter("@Sponsoring_Organisation_State", SqlDbType.VarChar, 100) { Value = sponsoringOrgState });
            cmd.Parameters.Add(new SqlParameter("@Sponsoring_Address", SqlDbType.VarChar, 500) { Value = sponsoringAddress });
            cmd.Parameters.Add(new SqlParameter("@GST_NUMBER", SqlDbType.VarChar, 50) { Value = gstNumber });
            cmd.Parameters.Add(new SqlParameter("@State_Code", SqlDbType.VarChar, 10) { Value = stateCode });

            // Execute the update command
            int result = 0;
            if (cmd.Connection.State != ConnectionState.Open)
            {
                cmd.Connection.Open();
            }
            try
            {
                result = cmd.ExecuteNonQuery();
            }
            finally
            {
                if (cmd.Connection.State == ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
                cmd.Dispose();
            }

            if (result > 0)
            {
                lblMessage.ForeColor = System.Drawing.Color.Green;
                lblMessage.Text = "Sponsoring Organization details updated successfully.";
            }
            else
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "Failed to update Sponsoring Organization details.";
            }

            // Exit edit mode and rebind the grid
            grdSponsoringOrganizationDetails.EditIndex = -1;
            BindSponsoringOrganizationGrid();
        }
        catch (Exception ex)
        {
            lblMessage.ForeColor = System.Drawing.Color.Red;
            lblMessage.Text = "Error during update: " + ex.Message;
        }
    }

    /// <summary>
    /// Handles GridView row cancel edit event
    /// </summary>
    /// <param name="sender">Event sender</param>
    /// <param name="e">GridView cancel edit event arguments</param>
    protected void grdSponsoringOrganizationDetails_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            // Exit edit mode
            grdSponsoringOrganizationDetails.EditIndex = -1;

            // Rebind the grid to show normal view
            BindSponsoringOrganizationGrid();
        }
        catch (Exception ex)
        {
            lblMessage.Text = "Error during cancel: " + ex.Message;
        }
    }

    /// <param name="sender">Event sender</param>
    /// <param name="e">GridView page event arguments</param>
    protected void grdSponsoringOrganizationDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {

            grdSponsoringOrganizationDetails.PageIndex = e.NewPageIndex;


            BindSponsoringOrganizationGrid();
        }
        catch (Exception ex)
        {

            lblMessage.Text = "Error during pagination: " + ex.Message;
        }
    }
    #endregion
}
