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

public partial class Qualification_Master : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            enabled_disabled_controls('L');
        }
    }
    protected void btn_insert_Click(object sender, EventArgs e)
    {
        lbl_choice.Text = "I";
        enabled_disabled_controls('I');
        txt_description.Focus();
    }

    protected void btn_update_Click(object sender, EventArgs e)
    {
        lbl_choice.Text = "U";
        enabled_disabled_controls('U');
        txt_description.Focus();
    }

    protected void btn_search_Click(object sender, EventArgs e)
    {
        lbl_choice.Text = "F";
        enabled_disabled_controls('F');
        txt_qualification_code.Focus();
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
            TYP_QUALIFICATION_MASTER newobj = new TYP_QUALIFICATION_MASTER();
            if (lbl_choice.Text == "I")
                newobj.QUALIFICATION_CODE = 0;
            else
                newobj.QUALIFICATION_CODE = Convert.ToInt32(txt_qualification_code.Text);
            newobj.DESCRIPTION = txt_description.Text.ToUpper();
            newobj.ENTER_DATE = DateTime.Now;
            newobj.ENTER_BY = HttpContext.Current.User.Identity.Name;

            switch (lbl_choice.Text)
            {
                case "I":
                    txt_qualification_code.Text = TYP_QUALIFICATION_MASTER.InsertQualification_Master(newobj).ToString();
                    lbl_error.Text = " Record Successfully Saved ";
                    break;
                case "U":
                    TYP_QUALIFICATION_MASTER.UpdateQualification_Master(newobj);
                    lbl_error.Text = " Record Successfully Updated ";
                    break;
                case "D":
                    TYP_QUALIFICATION_MASTER.DeleteQualification_Master(Convert.ToInt32(txt_qualification_code.Text));
                    lbl_error.Text = " Record Successfully Deleted ";
                    break;
            }

            GridParameter(txt_qualification_code.Text, "*");
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
        txt_qualification_code.Text = GridView1.SelectedRow.Cells[1].Text;
        txt_description.Text = GridView1.SelectedRow.Cells[2].Text;
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
                txt_qualification_code.Enabled = false;
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
                txt_qualification_code.Enabled = false;
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
        txt_qualification_code.Text = "";
        txt_description.Text = "";
    }

    void enabled_disabled_controls(bool flag)
    {
        txt_qualification_code.Enabled = flag;
        txt_description.Enabled = flag;
    }

    void enabled_disabled_validation(bool flag)
    {
        mev_description.Enabled = flag;
    }
    protected void btn_quit_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/HomePage.aspx");
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
            e.Row.Cells[4].Text = Convert.ToDateTime(e.Row.Cells[4].Text).Date.ToString("dd-MMM-yyyy").ToUpper().Trim();
            }
    }
    protected void btn_display_Click(object sender, EventArgs e)
    {
        lbl_error.Text = "";
        string pQualification_Code, pDescription;

        if (txt_qualification_code.Text == "")
            pQualification_Code = "-1";
        else
            pQualification_Code = txt_qualification_code.Text.ToUpper();

        if (txt_description.Text == "")
            pDescription = "*";
        else
            pDescription = txt_description.Text.ToUpper();

        //-----Setting Grid Parameters-----------------------------------------
        GridParameter(pQualification_Code, pDescription);
    }

    private void GridParameter(string pQualification_Code, string pDescription)
    {
        Parameter objQualification_CodeParameter = new Parameter("pQualification_Code", TypeCode.String, pQualification_Code);
        Parameter objDescriptionParameter = new Parameter("pDescription", TypeCode.String, pDescription);

        //set up object data source parameters
        ObjectDataSource1.SelectParameters["pQualification_Code"] = objQualification_CodeParameter;
        ObjectDataSource1.SelectParameters["pDescription"] = objDescriptionParameter;

        //Rebinding Data Grid
        GridView1.DataBind();
    }
    protected void btn_generate_Click(object sender, EventArgs e)
    {
        string pQualification_Code, pDescription;

        if (txt_qualification_code.Text == "")
            pQualification_Code = "-1";
        else
            pQualification_Code = txt_qualification_code.Text.ToUpper();

        if (txt_description.Text == "")
            pDescription = "*";
        else
            pDescription = txt_description.Text.ToUpper();


        string url = "~/Reports/MasterReports/RepQualification_Master.aspx?";
        url += "QUALIFICATION_CODE=" + pQualification_Code + "&";
        url += "DESCRIPTION=" + pDescription + "&";
        Response.Redirect(url);
    }

    protected bool ValidateData()
    {
        bool valid = true;
        bullet_errors.Items.Clear();
        //--------------------------------------------------------------------
        mev_description.Validate();
        if (!mev_description.IsValid)
        {
            bullet_errors.Items.Add("Description must be Entered");
            valid = false;
        }

        return valid;
    }
}
