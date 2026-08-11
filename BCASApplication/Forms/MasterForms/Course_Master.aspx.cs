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

public partial class Course_Master : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Convert.ToString(HttpContext.Current.Session["UserId"]).Trim().Length == 0))
        {
            HttpContext.Current.Response.Redirect("~/Home.aspx");
        }
        else
        {
            R1.Visible = false;
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
        txt_course_name.Focus();
    }

    protected void btn_update_Click(object sender, EventArgs e)
    {
        lbl_choice.Text = "U";
        enabled_disabled_controls('U');
        txt_course_name.Focus();
    }

    protected void btn_search_Click(object sender, EventArgs e)
    {
        lbl_choice.Text = "F";
        enabled_disabled_controls('F');
        txt_course_code.Focus();
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
            TYP_COURSE_MASTER newobj = new TYP_COURSE_MASTER();
            if (lbl_choice.Text == "I")
                newobj.COURSE_CODE = 0;
            else
                newobj.COURSE_CODE = Convert.ToInt32(txt_course_code.Text);
            newobj.COURSE_NAME = txt_course_name.Text.ToUpper();
            newobj.ENTER_DATE = DateTime.Now;
            newobj.ENTER_BY = HttpContext.Current.User.Identity.Name;

            switch (lbl_choice.Text)
            {
                case "I":
                    txt_course_code.Text = TYP_COURSE_MASTER.InsertCourse_Master(newobj).ToString();
                    lbl_error.Text = " Record Successfully Saved ";
                    break;
                case "U":
                    TYP_COURSE_MASTER.UpdateCourse_Master(newobj);
                    lbl_error.Text = " Record Successfully Updated ";
                    break;
                case "D":
                    TYP_COURSE_MASTER.DeleteCourse_Master(Convert.ToInt32(txt_course_code.Text));
                    lbl_error.Text = " Record Successfully Deleted ";
                    break;
            }

            GridParameter(txt_course_code.Text, "*");
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
        txt_course_code.Text = GridView1.SelectedRow.Cells[1].Text;
        txt_course_name.Text = GridView1.SelectedRow.Cells[2].Text;
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
                txt_course_code.Enabled = false;
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
                txt_course_code.Enabled = false;
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
        txt_course_code.Text = "";
        txt_course_name.Text = "";
    }

    void enabled_disabled_controls(bool flag)
    {
        txt_course_code.Enabled = flag;
        txt_course_name.Enabled = flag;
    }

    void enabled_disabled_validation(bool flag)
    {
        mev_course_name.Enabled = flag;
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
        string pCourse_Code, pCourse_Name;

        if (txt_course_code.Text == "")
            pCourse_Code = "-1";
        else
            pCourse_Code = txt_course_code.Text.ToUpper();

        if (txt_course_name.Text == "")
            pCourse_Name = "*";
        else
            pCourse_Name = txt_course_name.Text.ToUpper();

        //-----Setting Grid Parameters-----------------------------------------
        GridParameter(pCourse_Code, pCourse_Name);
    }

    private void GridParameter(string pCourse_Code, string pCourse_Name)
    {
        Parameter objCourse_CodeParameter = new Parameter("pCourse_Code", TypeCode.String, pCourse_Code);
        Parameter objCourse_NameParameter = new Parameter("pCourse_Name", TypeCode.String, pCourse_Name);

        //set up object data source parameters
        ObjectDataSource1.SelectParameters["pCourse_Code"] = objCourse_CodeParameter;
        ObjectDataSource1.SelectParameters["pCourse_Name"] = objCourse_NameParameter;

        //Rebinding Data Grid
        GridView1.DataBind();
    }
   

    protected bool ValidateData()
    {
        bool valid = true;
        bullet_errors.Items.Clear();
        //--------------------------------------------------------------------
        mev_course_name.Validate();
        if (!mev_course_name.IsValid)
        {
            bullet_errors.Items.Add("Course Name must be Entered");
            valid = false;
        }

        return valid;
    }
}
