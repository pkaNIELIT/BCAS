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
using System.IO;

public partial class Sponsoring_Organisation_Master : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Convert.ToString(HttpContext.Current.Session["UserId"]).Trim().Length == 0))
        {
            HttpContext.Current.Response.Redirect("~/Home.aspx");
        }
        else
        {

            row1.Visible = false;
            if (!Page.IsPostBack)
            {
                enabled_disabled_controls('L');
            }
        }

    }
    protected void btn_insert_Click(object sender, EventArgs e)
    {
        lbl_choice.Text = "I";
        enabled_disabled_controls('I');
        txt_organisation_name.Focus();
    }

    protected void btn_update_Click(object sender, EventArgs e)
    {
        lbl_choice.Text = "U";
        enabled_disabled_controls('U');
        txt_organisation_name.Focus();
    }

    protected void btn_search_Click(object sender, EventArgs e)
    {
        lbl_choice.Text = "F";
        enabled_disabled_controls('F');
        txt_organisation_code.Focus();
    }

    protected void btn_save_Click(object sender, EventArgs e)
    {
        if (lbl_choice.Text == "I" || lbl_choice.Text == "U")
        {
            lbl_errors.Visible = true;
            if (!ValidateData())
                return;
            lbl_errors.Visible = false;
        }

        try
        {
            TYP_SPONSORING_ORGANISATION_MASTER newobj = new TYP_SPONSORING_ORGANISATION_MASTER();
            if (lbl_choice.Text == "I")
                newobj.ORGANISATION_CODE = 0;
            else
                newobj.ORGANISATION_CODE = Convert.ToInt32(txt_organisation_code.Text);
            newobj.ORGANISATION_NAME = txt_organisation_name.Text.ToUpper();
            newobj.ORGANISATION_ADDRESS = txt_organisation_address.Text.ToUpper();
            newobj.ENTER_DATE = DateTime.Now;
            newobj.ENTER_BY = HttpContext.Current.User.Identity.Name;

            switch (lbl_choice.Text)
            {
                case "I":
                    txt_organisation_code.Text = TYP_SPONSORING_ORGANISATION_MASTER.InsertSponsoring_Organisation_Master(newobj).ToString();
                    lbl_error.Text = " Record Successfully Saved ";
                    break;
                case "U":
                    TYP_SPONSORING_ORGANISATION_MASTER.UpdateSponsoring_Organisation_Master(newobj);
                    lbl_error.Text = " Record Successfully Updated ";
                    break;
                case "D":
                    TYP_SPONSORING_ORGANISATION_MASTER.DeleteSponsoring_Organisation_Master(Convert.ToInt32(txt_organisation_code.Text));
                    lbl_error.Text = " Record Successfully Deleted ";
                    break;
            }

            GridParameter(txt_organisation_code.Text, "*", "*");
            enabled_disabled_controls('S');
        }
        catch (SqlException ex)
        {
            Int32 ErrorCode = ex.Number;

            switch (ErrorCode)
            {
                case 2627:
                    lbl_error.Text = "Primary key Already Exists, unique constraint violated";
                    break;
                default:
                    lbl_error.Text = ex.Message;
                    break;
            }
            enabled_disabled_controls('N');
        }

        finally
        {
            btn_insert.Focus();
        }
    }

    protected void btn_cancel_Click(object sender, EventArgs e)
    {
        enabled_disabled_controls('C');
        btn_insert.Focus();
    }

    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        txt_organisation_code.Text = GridView1.SelectedRow.Cells[1].Text;
        txt_organisation_name.Text = GridView1.SelectedRow.Cells[2].Text;
        txt_organisation_address.Text = GridView1.SelectedRow.Cells[3].Text;
        enabled_disabled_controls('G');
    }

    void enabled_disabled_controls(char choice)
    {
        switch (choice)
        {
            case 'L': //Login
                lbl_error.Text = "";
                btn_insert.Enabled = true;
                btn_search.Enabled = true;
                btn_update.Enabled = false;
                btn_save.Enabled = false;
                btn_cancel.Enabled = false;
                btn_quit.Enabled = true;
                enabled_disabled_validation(false);
                frm_panel.Enabled = false;
                break;

            case 'N':  //Exception
                btn_insert.Enabled = false;
                btn_search.Enabled = false;
                btn_update.Enabled = false;
                btn_save.Enabled = true;
                btn_cancel.Enabled = true;
                btn_quit.Enabled = true;
                frm_panel.Enabled = true;
                enabled_disabled_validation(true);
                enabled_disabled_controls(true);
                break;
            case 'S':
                // on save
                btn_insert.Enabled = true;
                btn_search.Enabled = true;
                btn_update.Enabled = false;
                btn_save.Enabled = false;
                btn_cancel.Enabled = false;
                btn_quit.Enabled = true;

                enabled_disabled_validation(false);
                frm_panel.Enabled = false;
                enabled_disabled_controls(false);
                clear_contents();
                break;
            case 'C':
                //on  cancel
                btn_insert.Enabled = true;
                btn_search.Enabled = true;
                btn_update.Enabled = false;
                btn_save.Enabled = false;
                btn_cancel.Enabled = false;
                btn_quit.Enabled = true;

                enabled_disabled_validation(false);
                frm_panel.Enabled = false;
                enabled_disabled_controls(false);
                clear_contents();
                break;
            case 'I':
                lbl_error.Text = "";
                btn_insert.Enabled = false;
                btn_update.Enabled = false;
                btn_search.Enabled = false;
                btn_quit.Enabled = true;
                btn_save.Enabled = true;
                btn_cancel.Enabled = true;
                frm_panel.Enabled = true;
                enabled_disabled_validation(true);
                enabled_disabled_controls(true);
                txt_organisation_code.Enabled = false;
                break;
            case 'F':
                btn_insert.Enabled = false;
                btn_search.Enabled = false;
                btn_update.Enabled = false;
                btn_quit.Enabled = false;
                btn_save.Enabled = false;
                btn_cancel.Enabled = true;
                frm_panel.Enabled = true;
                enabled_disabled_validation(false);
                enabled_disabled_controls(true);
                break;
            case 'U':
                btn_insert.Enabled = false;
                btn_update.Enabled = false;
                btn_search.Enabled = false;
                btn_quit.Enabled = false;
                btn_save.Enabled = true;
                btn_cancel.Enabled = true;
                frm_panel.Enabled = true;
                enabled_disabled_validation(true);
                enabled_disabled_controls(true);
                txt_organisation_code.Enabled = false;
                break;
            case 'G':
                lbl_error.Text = "";
                btn_insert.Enabled = false;
                btn_search.Enabled = false;
                btn_update.Enabled = true;
                btn_quit.Enabled = true;
                btn_save.Enabled = false;
                btn_cancel.Enabled = true;

                enabled_disabled_validation(false);
                frm_panel.Enabled = false;
                break;
        };
    }

    void clear_contents()
    {
        lbl_choice.Text = "";
        lbl_error.Text = "";
        txt_organisation_code.Text = "";
        txt_organisation_name.Text = "";
        txt_organisation_address.Text = "";
    }

    void enabled_disabled_controls(bool flag)
    {
        txt_organisation_code.Enabled = flag;
        txt_organisation_name.Enabled = flag;
        txt_organisation_address.Enabled = flag;
    }

    void enabled_disabled_validation(bool flag)
    {
        mev_organisation_name.Enabled = flag;
        mev_organisation_address.Enabled = flag;
    }
    protected void btn_quit_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Home.aspx");
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            foreach (DataControlFieldCell cell in e.Row.Cells)
            {
                cell.Width = cell.Text.Trim().Length;
            }
        }
        else if (e.Row.RowType == DataControlRowType.DataRow)
            {
            if (e.Row.Cells[5].Text == "&nbsp;")
                e.Row.Cells[5].Text = "";
            else
               // e.Row.Cells[5].Text = Convert.ToDateTime(e.Row.Cells[5].Text).Date.ToString("dd-MMM-yyyy").ToUpper().Trim();
                e.Row.Cells[5].Text = Convert.ToInt32(e.Row.Cells[5].Text).ToString ();

            }
    }
    protected void btn_display_Click(object sender, EventArgs e)
    {
        lbl_error.Text = "";
        string pOrganisation_Code, pOrganisation_Name, pOrganisation_Address;

        if (txt_organisation_code.Text == "")
            pOrganisation_Code = "-1";
        else
            pOrganisation_Code = txt_organisation_code.Text.ToUpper();

        if (txt_organisation_name.Text == "")
            pOrganisation_Name = "*";
        else
            pOrganisation_Name = txt_organisation_name.Text.ToUpper();

        if (txt_organisation_address.Text == "")
            pOrganisation_Address = "*";
        else
            pOrganisation_Address = txt_organisation_address.Text.ToUpper();

        //-----Setting Grid Parameters-----------------------------------------
        GridParameter(pOrganisation_Code, pOrganisation_Name, pOrganisation_Address);
    }

    private void GridParameter(string pOrganisation_Code, string pOrganisation_Name, string pOrganisation_Address)
    {
        Parameter objOrganisation_CodeParameter = new Parameter("pOrganisation_Code", TypeCode.String, pOrganisation_Code);
        Parameter objOrganisation_NameParameter = new Parameter("pOrganisation_Name", TypeCode.String, pOrganisation_Name);
        Parameter objOrganisation_AddressParameter = new Parameter("pOrganisation_Address", TypeCode.String, pOrganisation_Address);

        //set up object data source parameters
        ObjectDataSource1.SelectParameters["pOrganisation_Code"] = objOrganisation_CodeParameter;
        ObjectDataSource1.SelectParameters["pOrganisation_Name"] = objOrganisation_NameParameter;
        ObjectDataSource1.SelectParameters["pOrganisation_Address"] = objOrganisation_AddressParameter;

        //Rebinding Data Grid
        GridView1.DataBind();
    }
    protected void btn_generate_Click(object sender, EventArgs e)
    {
        string pOrganisation_Code, pOrganisation_Name, pOrganisation_Address;

        if (txt_organisation_code.Text == "")
            pOrganisation_Code = "-1";
        else
            pOrganisation_Code = txt_organisation_code.Text.ToUpper();

        if (txt_organisation_name.Text == "")
            pOrganisation_Name = "*";
        else
            pOrganisation_Name = txt_organisation_name.Text.ToUpper();

        if (txt_organisation_address.Text == "")
            pOrganisation_Address = "*";
        else
            pOrganisation_Address = txt_organisation_address.Text.ToUpper();


        string url = "~/Reports/MasterReports/RepSponsoring_Organisation_Master.aspx?";
        url += "ORGANISATION_CODE=" + pOrganisation_Code + "&";
        url += "ORGANISATION_NAME=" + pOrganisation_Name + "&";
        url += "ORGANISATION_ADDRESS=" + pOrganisation_Address + "&";
        Response.Redirect(url);
    }

    protected bool ValidateData()
    {
        bool valid = true;
        bullet_errors.Items.Clear();
        //--------------------------------------------------------------------
        mev_organisation_name.Validate();
        if (!mev_organisation_name.IsValid)
        {
            bullet_errors.Items.Add("Organisation Name must be Entered");
            valid = false;
        }

        mev_organisation_address.Validate();
        if (!mev_organisation_address.IsValid)
        {
            bullet_errors.Items.Add("Organisation Address must be Entered");
            valid = false;
        }

        return valid;
    }
}
