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

public partial class Payee_Sponsored_Organisation : System.Web.UI.Page
{
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
                enabled_disabled_controls('L');
            }
            R1.Visible = false;
        }
    }
    protected void btn_insert_Click(object sender, EventArgs e)
    {
        lbl_choice.Text = "I";
        enabled_disabled_controls('I');
        ddlSponsoringOrganisation .Focus();
    }

    protected void btn_update_Click(object sender, EventArgs e)
    {
        lbl_choice.Text = "U";
        enabled_disabled_controls('U');
        ddlSponsoringOrganisation.Focus();
    }

    protected void btn_search_Click(object sender, EventArgs e)
    {
        lbl_choice.Text = "F";
        enabled_disabled_controls('F');
        txt_s_no.Focus();
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
            TYP_PAYEE_SPONSORED_ORGANISATION newobj = new TYP_PAYEE_SPONSORED_ORGANISATION();
            if (lbl_choice.Text == "I")
                newobj.S_NO = 0;
            else
                newobj.S_NO = Convert.ToInt32(txt_s_no.Text);
            newobj.SPONSORING_ORGANISATION_CODE = Convert.ToInt32(ddlSponsoringOrganisation.SelectedValue );
            newobj.PAYEE_ORGANISATION_CODE = Convert.ToInt32(ddlPayeeOrg.SelectedValue);
            newobj.ENTER_DATE = DateTime.Now;
            newobj.ENTER_BY = HttpContext.Current.User.Identity.Name;

            switch (lbl_choice.Text)
            {
                case "I":
                    txt_s_no.Text = TYP_PAYEE_SPONSORED_ORGANISATION.InsertPayee_Sponsored_Organisation(newobj).ToString();
                    lbl_error.Text = " Record Successfully Saved ";
                    break;
                case "U":
                    TYP_PAYEE_SPONSORED_ORGANISATION.UpdatePayee_Sponsored_Organisation(newobj);
                    lbl_error.Text = " Record Successfully Updated ";
                    break;
                case "D":
                    TYP_PAYEE_SPONSORED_ORGANISATION.DeletePayee_Sponsored_Organisation(Convert.ToInt32(txt_s_no.Text));
                    lbl_error.Text = " Record Successfully Deleted ";
                    break;
            }

            GridParameter(txt_s_no.Text, "-1", "-1");
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
        txt_s_no.Text = GridView1.SelectedRow.Cells[1].Text;
        ddlSponsoringOrganisation.SelectedValue  = GridView1.SelectedRow.Cells[2].Text;
        ddlPayeeOrg.SelectedValue = GridView1.SelectedRow.Cells[3].Text;
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
                
                enabled_disabled_validation(false);
                frm_panel.Enabled = false;
                break;

            case 'N':  //Exception
                btn_insert.Enabled = false;
                btn_search.Enabled = false;
                btn_update.Enabled = false;
                btn_save.Enabled = true;
                btn_cancel.Enabled = true;
                
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
               
                btn_save.Enabled = true;
                btn_cancel.Enabled = true;
                frm_panel.Enabled = true;
                enabled_disabled_validation(true);
                enabled_disabled_controls(true);
                txt_s_no.Enabled = false;
                break;
            case 'F':
                btn_insert.Enabled = false;
                btn_search.Enabled = false;
                btn_update.Enabled = false;
                
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
                
                btn_save.Enabled = true;
                btn_cancel.Enabled = true;
                frm_panel.Enabled = true;
                enabled_disabled_validation(true);
                enabled_disabled_controls(true);
                txt_s_no.Enabled = false;
                break;
            case 'G':
                lbl_error.Text = "";
                btn_insert.Enabled = false;
                btn_search.Enabled = false;
                btn_update.Enabled = true;
                
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
        txt_s_no.Text = "";
        ddlSponsoringOrganisation.SelectedIndex = 0;
        ddlPayeeOrg.SelectedIndex   = 0;
    }

    void enabled_disabled_controls(bool flag)
    {
        txt_s_no.Enabled = flag;
        ddlSponsoringOrganisation.Enabled = flag;
        ddlPayeeOrg.Enabled = flag;
    }

    void enabled_disabled_validation(bool flag)
    {
        //mev_sponsoring_organisation_code.Enabled = flag;
        //mev_payee_organisation_code.Enabled = flag;
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
            e.Row.Cells[5].Text = Convert.ToDateTime(e.Row.Cells[5].Text).Date.ToString("dd-MMM-yyyy").ToUpper().Trim();
            }
    }

    protected void btn_display_Click(object sender, EventArgs e)
    {
        lbl_error.Text = "";
        string pS_No, pSponsoring_Organisation_Code, pPayee_Organisation_Code;

        if (txt_s_no.Text == "")
            pS_No = "-1";
        else
            pS_No = txt_s_no.Text.ToUpper();

        if (ddlSponsoringOrganisation.SelectedIndex == 0)
            pSponsoring_Organisation_Code = "-1";
        else
            pSponsoring_Organisation_Code = ddlSponsoringOrganisation.SelectedValue;

        if (ddlPayeeOrg.SelectedIndex  == 0)
            pPayee_Organisation_Code = "-1";
        else
            pPayee_Organisation_Code = ddlPayeeOrg.SelectedValue;

        //-----Setting Grid Parameters-----------------------------------------
        GridParameter(pS_No, pSponsoring_Organisation_Code, pPayee_Organisation_Code);
    }

    private void GridParameter(string pS_No, string pSponsoring_Organisation_Code, string pPayee_Organisation_Code)
    {
        Parameter objS_NoParameter = new Parameter("pS_No", TypeCode.String, pS_No);
        Parameter objSponsoring_Organisation_CodeParameter = new Parameter("pSponsoring_Organisation_Code", TypeCode.String, pSponsoring_Organisation_Code);
        Parameter objPayee_Organisation_CodeParameter = new Parameter("pPayee_Organisation_Code", TypeCode.String, pPayee_Organisation_Code);

        //set up object data source parameters
        ObjectDataSource1.SelectParameters["pS_No"] = objS_NoParameter;
        ObjectDataSource1.SelectParameters["pSponsoring_Organisation_Code"] = objSponsoring_Organisation_CodeParameter;
        ObjectDataSource1.SelectParameters["pPayee_Organisation_Code"] = objPayee_Organisation_CodeParameter;

        //Rebinding Data Grid
        GridView1.DataBind();
    }
   

    protected bool ValidateData()
    {
        bool valid = true;
        bullet_errors.Items.Clear();
        //--------------------------------------------------------------------
        //mev_sponsoring_organisation_code.Validate();
        //if (!mev_sponsoring_organisation_code.IsValid)
        //{
        //    bullet_errors.Items.Add("Sponsoring Organisation Code must be Entered");
        //    valid = false;
        //}

        //mev_payee_organisation_code.Validate();
        //if (!mev_payee_organisation_code.IsValid)
        //{
        //    bullet_errors.Items.Add("Payee Organisation Code must be Entered");
        //    valid = false;
        //}

        return valid;
    }
}
