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

public partial class Course_Schedule_Exam_location : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        R1.Visible = false;

        if (!Page.IsPostBack)
        {
            enabled_disabled_controls('L');
        }
    }
    protected void btn_insert_Click(object sender, EventArgs e)
    {
        lbl_choice.Text = "I";
        enabled_disabled_controls('I');
        ddlSchedule.Focus();
    }

    protected void btn_update_Click(object sender, EventArgs e)
    {
        lbl_choice.Text = "U";
        enabled_disabled_controls('U');
        ddlSchedule.Focus();
    }

    protected void btn_search_Click(object sender, EventArgs e)
    {
        lbl_choice.Text = "F";
        enabled_disabled_controls('F');
        txt_sr_no.Focus();
    }

    protected void btn_save_Click(object sender, EventArgs e)
    {
        if (lbl_choice.Text == "I" || lbl_choice.Text == "U")
        {
            lbl_errors.Visible = true;
            //if (!ValidateData())
            //    return;
            lbl_errors.Visible = false;
        }

        try
        {
            TYP_COURSE_SCHEDULE_EXAM_LOCATION newobj = new TYP_COURSE_SCHEDULE_EXAM_LOCATION();
            if (lbl_choice.Text == "I")
                newobj.SR_NO = 0;
            else
                newobj.SR_NO = Convert.ToInt32(txt_sr_no.Text);
            newobj.COURSE_SCHEDULE_ID = Convert.ToInt32(ddlSchedule.SelectedValue );
            newobj.EXAM_LOCATION_CODE = Convert.ToInt32(ddlExamLocation.SelectedValue );
            newobj.ENTER_DATE = DateTime.Now;
            newobj.ENTER_BY = HttpContext.Current.User.Identity.Name;

            switch (lbl_choice.Text)
            {
                case "I":
                    txt_sr_no.Text = TYP_COURSE_SCHEDULE_EXAM_LOCATION.InsertCourse_Schedule_Exam_location(newobj).ToString();
                    lbl_error.Text = " Record Successfully Saved ";
                    break;
                case "U":
                    TYP_COURSE_SCHEDULE_EXAM_LOCATION.UpdateCourse_Schedule_Exam_location(newobj);
                    lbl_error.Text = " Record Successfully Updated ";
                    break;
                case "D":
                    TYP_COURSE_SCHEDULE_EXAM_LOCATION.DeleteCourse_Schedule_Exam_location(Convert.ToInt32(txt_sr_no.Text));
                    lbl_error.Text = " Record Successfully Deleted ";
                    break;
            }

            GridParameter(txt_sr_no.Text, "-1", "-1");
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
        txt_sr_no.Text = GridView1.SelectedRow.Cells[1].Text;
        ddlSchedule.SelectedValue = GridView1.SelectedRow.Cells[2].Text;
        ddlExamLocation.SelectedValue = GridView1.SelectedRow.Cells[3].Text;
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
                txt_sr_no.Enabled = false;
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
                txt_sr_no.Enabled = false;
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
        txt_sr_no.Text = "";
        ddlSchedule.SelectedIndex = 0;
        ddlExamLocation.SelectedIndex =0;
    }

    void enabled_disabled_controls(bool flag)
    {
        txt_sr_no.Enabled = flag;
        ddlSchedule.Enabled = flag;
        ddlExamLocation.Enabled = flag;
    }

    void enabled_disabled_validation(bool flag)
    {
        //mev_course_schedule_id.Enabled = flag;
        //mev_exam_location_code.Enabled = flag;
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
            if (e.Row.Cells[5].Text == "&nbsp;")
                e.Row.Cells[5].Text = "";
            else
                e.Row.Cells[5].Text = Convert.ToDateTime(e.Row.Cells[5].Text).Date.ToString("dd-MMM-yyyy").ToUpper().Trim();

            }
    }
    protected void btn_display_Click(object sender, EventArgs e)
    {
        lbl_error.Text = "";
        string pSr_No, pCourse_Schedule_ID, pExam_Location_Code;

        if (txt_sr_no.Text == "")
            pSr_No = "-1";
        else
            pSr_No = txt_sr_no.Text.ToUpper();

        if (ddlSchedule.SelectedIndex  == 0)
            pCourse_Schedule_ID = "-1";
        else
            pCourse_Schedule_ID = ddlSchedule.SelectedValue;

        if (ddlExamLocation.SelectedIndex == 0)
            pExam_Location_Code = "-1";
        else
            pExam_Location_Code = ddlExamLocation.SelectedValue;

        //-----Setting Grid Parameters-----------------------------------------
        GridParameter(pSr_No, pCourse_Schedule_ID, pExam_Location_Code);
    }

    private void GridParameter(string pSr_No, string pCourse_Schedule_ID, string pExam_Location_Code)
    {
        Parameter objSr_NoParameter = new Parameter("pSr_No", TypeCode.String, pSr_No);
        Parameter objCourse_Schedule_IDParameter = new Parameter("pCourse_Schedule_ID", TypeCode.String, pCourse_Schedule_ID);
        Parameter objExam_Location_CodeParameter = new Parameter("pExam_Location_Code", TypeCode.String, pExam_Location_Code);

        //set up object data source parameters
        ObjectDataSource1.SelectParameters["pSr_No"] = objSr_NoParameter;
        ObjectDataSource1.SelectParameters["pCourse_Schedule_ID"] = objCourse_Schedule_IDParameter;
        ObjectDataSource1.SelectParameters["pExam_Location_Code"] = objExam_Location_CodeParameter;

        //Rebinding Data Grid
        GridView1.DataBind();
    }
    

    protected bool ValidateData()
    {
        bool valid = true;
        bullet_errors.Items.Clear();
        //--------------------------------------------------------------------
        //mev_course_schedule_id.Validate();
        //if (!mev_course_schedule_id.IsValid)
        //{
        //    bullet_errors.Items.Add("Course Schedule ID must be Entered");
        //    valid = false;
        //}

        //mev_exam_location_code.Validate();
        //if (!mev_exam_location_code.IsValid)
        //{
        //    bullet_errors.Items.Add("Exam Location Code must be Entered");
        //    valid = false;
        //}

        return valid;
    }
}
