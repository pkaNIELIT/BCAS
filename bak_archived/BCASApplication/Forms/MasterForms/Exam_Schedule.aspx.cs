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

public partial class Exam_Schedule: System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Convert.ToString(HttpContext.Current.Session["UserId"]).Trim().Length == 0))
        {
            HttpContext.Current.Response.Redirect("~/Home.aspx");
        }
        else
        {
            RowSchedule.Visible = false;

            if (!Page.IsPostBack)
            {
                enabled_disabled_controls('L');

                Com_v_start_date.ValueToCompare = DateTime.Now.ToShortDateString();
            }

        }
    }
    protected void btn_insert_Click(object sender, EventArgs e)
    {
        lbl_choice.Text = "I";
        enabled_disabled_controls('I');
        ddlCourseCode.Focus();
    }

    protected void btn_update_Click(object sender, EventArgs e)
    {
        lbl_choice.Text = "U";
        enabled_disabled_controls('U');
        ddlCourseCode.Focus();
        if (ddlCourseCode.SelectedValue.ToString() == "3" || ddlCourseCode.SelectedValue.ToString() == "4")
        {
            //txt_end_date.Enabled = false;
            txt_last_date_for_nomination_reappearing.Enabled = false;
            txt_last_date_for_reappearing_fee_payments.Enabled = false;
            
            mev_last_date_for_nomination_reappearing.EnableClientScript = false;
            mev_last_date_for_reappearing_fee_payments.EnableClientScript = false;
            mev_end_date.EnableClientScript = false;
            Com_v_End_date.EnableClientScript = false;
            
        }
    }

    protected void btn_search_Click(object sender, EventArgs e)
    {
        lbl_choice.Text = "F";
        enabled_disabled_controls('F');
        txt_schedule_id.Focus();
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
            //Added 12 Oct 2023
            if (ddlCourseCode.SelectedValue.ToString() == "3" || ddlCourseCode.SelectedValue.ToString() == "4")
            {
               // txt_end_date.Text = txt_start_date.Text;
                txt_last_date_for_nomination_reappearing.Text = txt_last_date_for_nomination.Text;
                txt_last_date_for_reappearing_fee_payments.Text = txt_last_date_for_payments.Text;
             }
            //
            TYP_EXAM_SCHEDULE newobj = new TYP_EXAM_SCHEDULE();
            if (lbl_choice.Text == "I")
                newobj.SCHEDULE_ID = 0;
            else
                newobj.SCHEDULE_ID = Convert.ToInt32(txt_schedule_id.Text);
            newobj.COURSE_CODE = Convert.ToInt32(ddlCourseCode.SelectedValue );
            newobj.START_DATE = DateTime.ParseExact(txt_start_date.Text, "dd/MM/yyyy", new System.Globalization.DateTimeFormatInfo());
            newobj.END_DATE = DateTime.ParseExact(txt_end_date.Text, "dd/MM/yyyy", new System.Globalization.DateTimeFormatInfo());
            newobj.DATE_OF_EXAM = DateTime.ParseExact(txt_date_of_exam.Text, "dd/MM/yyyy", new System.Globalization.DateTimeFormatInfo());
            newobj.LAST_DATE_FOR_NOMINATION = DateTime.ParseExact(txt_last_date_for_nomination.Text, "dd/MM/yyyy", new System.Globalization.DateTimeFormatInfo());
            newobj.LAST_DATE_FOR_BCAS_ACCEPTANCE = DateTime.ParseExact(txt_last_date_for_bcas_acceptance.Text, "dd/MM/yyyy", new System.Globalization.DateTimeFormatInfo());
            newobj.LAST_DATE_FOR_PAYMENTS = DateTime.ParseExact(txt_last_date_for_payments.Text, "dd/MM/yyyy", new System.Globalization.DateTimeFormatInfo());
            newobj.LAST_DATE_FOR_NOMINATION_REAPPEARING = DateTime.ParseExact(txt_last_date_for_nomination_reappearing.Text, "dd/MM/yyyy", new System.Globalization.DateTimeFormatInfo());
            newobj.LAST_DATE_FOR_REAPPEARING_FEE_PAYMENTS = DateTime.ParseExact(txt_last_date_for_reappearing_fee_payments.Text, "dd/MM/yyyy", new System.Globalization.DateTimeFormatInfo());
            newobj.ENTER_DATE = DateTime.Now;
            newobj.ENTER_BY = HttpContext.Current.User.Identity.Name;

            switch (lbl_choice.Text)
            {
                case "I":
                    txt_schedule_id.Text = TYP_EXAM_SCHEDULE.InsertCourse_Schedule(newobj).ToString();
                    lbl_error.Text = " Record Successfully Saved ";
                    break;
                case "U":
                    TYP_EXAM_SCHEDULE.UpdateCourse_Schedule(newobj);
                    lbl_error.Text = " Record Successfully Updated ";
                    break;
                case "D":
                    TYP_EXAM_SCHEDULE.DeleteCourse_Schedule(Convert.ToInt32(txt_schedule_id.Text));
                    lbl_error.Text = " Record Successfully Deleted ";
                    break;
            }

            GridParameter(txt_schedule_id.Text, "-1", "01/01/1900", "01/01/1900", "01/01/1900", "01/01/1900", "01/01/1900", "01/01/1900", "01/01/1900", "01/01/1900");
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
        txt_schedule_id.Text = GridView1.SelectedRow.Cells[1].Text;
        ddlCourseCode.SelectedValue  = GridView1.SelectedRow.Cells[2].Text;
        txt_start_date.Text = Convert.ToDateTime(GridView1.SelectedRow.Cells[3].Text).Date.ToString("dd/MM/yyyy");
        txt_end_date.Text = Convert.ToDateTime(GridView1.SelectedRow.Cells[4].Text).Date.ToString("dd/MM/yyyy");
        txt_date_of_exam.Text = Convert.ToDateTime(GridView1.SelectedRow.Cells[5].Text).Date.ToString("dd/MM/yyyy");
        txt_last_date_for_nomination.Text = Convert.ToDateTime(GridView1.SelectedRow.Cells[6].Text).Date.ToString("dd/MM/yyyy");
        txt_last_date_for_bcas_acceptance.Text = Convert.ToDateTime(GridView1.SelectedRow.Cells[7].Text).Date.ToString("dd/MM/yyyy");
        txt_last_date_for_payments.Text = Convert.ToDateTime(GridView1.SelectedRow.Cells[8].Text).Date.ToString("dd/MM/yyyy");
        txt_last_date_for_nomination_reappearing.Text = Convert.ToDateTime(GridView1.SelectedRow.Cells[9].Text).Date.ToString("dd/MM/yyyy");
        txt_last_date_for_reappearing_fee_payments.Text = Convert.ToDateTime(GridView1.SelectedRow.Cells[10].Text).Date.ToString("dd/MM/yyyy");
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
                txt_schedule_id.Enabled = false;
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
                txt_schedule_id.Enabled = false;
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
        txt_last_date_for_nomination_reappearing.Enabled = false;
        txt_last_date_for_reappearing_fee_payments.Enabled = false;

        mev_last_date_for_nomination_reappearing.EnableClientScript = false;
        mev_last_date_for_reappearing_fee_payments.EnableClientScript = false;
        mev_end_date.EnableClientScript = false;
        Com_v_End_date.EnableClientScript = false;
    }

    void clear_contents()
    {
        lbl_choice.Text = "";
        lbl_error.Text = "";
        txt_schedule_id.Text = "";
        ddlCourseCode .SelectedIndex = 0;
        txt_start_date.Text = "";
        txt_end_date.Text = "";
        txt_date_of_exam.Text = "";
        txt_last_date_for_nomination.Text = "";
        txt_last_date_for_bcas_acceptance.Text = "";
        txt_last_date_for_payments.Text = "";
        txt_last_date_for_nomination_reappearing.Text = "";
        txt_last_date_for_reappearing_fee_payments.Text = "";
    }

    void enabled_disabled_controls(bool flag)
    {
        txt_schedule_id.Enabled = flag;
        ddlCourseCode.Enabled = flag;
        txt_start_date.Enabled = flag;
        txt_end_date.Enabled = flag;
        txt_date_of_exam.Enabled = flag;
        txt_last_date_for_nomination.Enabled = flag;
        txt_last_date_for_bcas_acceptance.Enabled = flag;
        txt_last_date_for_payments.Enabled = flag;
        txt_last_date_for_nomination_reappearing.Enabled = flag;
        txt_last_date_for_reappearing_fee_payments.Enabled = flag;
    }

    void enabled_disabled_validation(bool flag)
    {
        //mev_course_code.Enabled = flag;
        mev_start_date.Enabled = flag;
        mev_end_date.Enabled = flag;
        mev_date_of_exam.Enabled = flag;
        mev_last_date_for_nomination.Enabled = flag;
        mev_last_date_for_bcas_acceptance.Enabled = flag;
        mev_last_date_for_payments.Enabled = flag;
     //  mev_last_date_for_nomination_reappearing.Enabled = flag;
     //   mev_last_date_for_reappearing_fee_payments.Enabled = flag;
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
            e.Row.Cells[3].Text = Convert.ToDateTime(e.Row.Cells[3].Text).Date.ToString("dd-MMM-yyyy").ToUpper().Trim();
            e.Row.Cells[4].Text = Convert.ToDateTime(e.Row.Cells[4].Text).Date.ToString("dd-MMM-yyyy").ToUpper().Trim();
            e.Row.Cells[5].Text = Convert.ToDateTime(e.Row.Cells[5].Text).Date.ToString("dd-MMM-yyyy").ToUpper().Trim();
            e.Row.Cells[6].Text = Convert.ToDateTime(e.Row.Cells[6].Text).Date.ToString("dd-MMM-yyyy").ToUpper().Trim();
            e.Row.Cells[7].Text = Convert.ToDateTime(e.Row.Cells[7].Text).Date.ToString("dd-MMM-yyyy").ToUpper().Trim();
            e.Row.Cells[8].Text = Convert.ToDateTime(e.Row.Cells[8].Text).Date.ToString("dd-MMM-yyyy").ToUpper().Trim();
            e.Row.Cells[9].Text = Convert.ToDateTime(e.Row.Cells[9].Text).Date.ToString("dd-MMM-yyyy").ToUpper().Trim();
            e.Row.Cells[10].Text = Convert.ToDateTime(e.Row.Cells[10].Text).Date.ToString("dd-MMM-yyyy").ToUpper().Trim();
            e.Row.Cells[12].Text = Convert.ToDateTime(e.Row.Cells[12].Text).Date.ToString("dd-MMM-yyyy").ToUpper().Trim();
            }
    }
    protected void btn_display_Click(object sender, EventArgs e)
    {
        lbl_error.Text = "";
        string pSchedule_ID, pCourse_Code, pStart_Date, pEnd_Date, pDate_of_Exam, pLast_date_for_nomination, pLast_date_for_bcas_acceptance, pLast_date_for_payments, pLast_date_for_nomination_reappearing, pLast_date_for_reappearing_fee_payments;

        if (txt_schedule_id.Text == "")
            pSchedule_ID = "-1";
        else
            pSchedule_ID = txt_schedule_id.Text.ToUpper();

        if (ddlCourseCode.SelectedIndex  == 0)
            pCourse_Code = "-1";
        else
            pCourse_Code = ddlCourseCode.SelectedValue ;

        if (txt_start_date.Text == "")
            pStart_Date = "01/01/1900";
        else
            pStart_Date = txt_start_date.Text.ToUpper();

        if (txt_end_date.Text == "")
            pEnd_Date = "01/01/1900";
        else
            pEnd_Date = txt_end_date.Text.ToUpper();

        if (txt_date_of_exam.Text == "")
            pDate_of_Exam = "01/01/1900";
        else
            pDate_of_Exam = txt_date_of_exam.Text.ToUpper();

        if (txt_last_date_for_nomination.Text == "")
            pLast_date_for_nomination = "01/01/1900";
        else
            pLast_date_for_nomination = txt_last_date_for_nomination.Text.ToUpper();

        if (txt_last_date_for_bcas_acceptance.Text == "")
            pLast_date_for_bcas_acceptance = "01/01/1900";
        else
            pLast_date_for_bcas_acceptance = txt_last_date_for_bcas_acceptance.Text.ToUpper();

        if (txt_last_date_for_payments.Text == "")
            pLast_date_for_payments = "01/01/1900";
        else
            pLast_date_for_payments = txt_last_date_for_payments.Text.ToUpper();

        if (txt_last_date_for_nomination_reappearing.Text == "")
            pLast_date_for_nomination_reappearing = "01/01/1900";
        else
            pLast_date_for_nomination_reappearing = txt_last_date_for_nomination_reappearing.Text.ToUpper();

        if (txt_last_date_for_reappearing_fee_payments.Text == "")
            pLast_date_for_reappearing_fee_payments = "01/01/1900";
        else
            pLast_date_for_reappearing_fee_payments = txt_last_date_for_reappearing_fee_payments.Text.ToUpper();

        //-----Setting Grid Parameters-----------------------------------------
        GridParameter(pSchedule_ID, pCourse_Code, pStart_Date, pEnd_Date, pDate_of_Exam, pLast_date_for_nomination, pLast_date_for_bcas_acceptance, pLast_date_for_payments, pLast_date_for_nomination_reappearing, pLast_date_for_reappearing_fee_payments);
    }

    private void GridParameter(string pSchedule_ID, string pCourse_Code, string pStart_Date, string pEnd_Date, string pDate_of_Exam, string pLast_date_for_nomination, string pLast_date_for_bcas_acceptance, string pLast_date_for_payments, string pLast_date_for_nomination_reappearing, string pLast_date_for_reappearing_fee_payments)
    {
        Parameter objSchedule_IDParameter = new Parameter("pSchedule_ID", TypeCode.String, pSchedule_ID);
        Parameter objCourse_CodeParameter = new Parameter("pCourse_Code", TypeCode.String, pCourse_Code);
        Parameter objStart_DateParameter = new Parameter("pStart_Date", TypeCode.DateTime, pStart_Date);
        Parameter objEnd_DateParameter = new Parameter("pEnd_Date", TypeCode.DateTime, pEnd_Date);
        Parameter objDate_of_ExamParameter = new Parameter("pDate_of_Exam", TypeCode.DateTime, pDate_of_Exam);
        Parameter objLast_date_for_nominationParameter = new Parameter("pLast_date_for_nomination", TypeCode.DateTime, pLast_date_for_nomination);
        Parameter objLast_date_for_bcas_acceptanceParameter = new Parameter("pLast_date_for_bcas_acceptance", TypeCode.DateTime, pLast_date_for_bcas_acceptance);
        Parameter objLast_date_for_paymentsParameter = new Parameter("pLast_date_for_payments", TypeCode.DateTime, pLast_date_for_payments);
        Parameter objLast_date_for_nomination_reappearingParameter = new Parameter("pLast_date_for_nomination_reappearing", TypeCode.DateTime, pLast_date_for_nomination_reappearing);
        Parameter objLast_date_for_reappearing_fee_paymentsParameter = new Parameter("pLast_date_for_reappearing_fee_payments", TypeCode.DateTime , pLast_date_for_reappearing_fee_payments);

        //set up object data source parameters
        ObjectDataSource1.SelectParameters["pSchedule_ID"] = objSchedule_IDParameter;
        ObjectDataSource1.SelectParameters["pCourse_Code"] = objCourse_CodeParameter;
        ObjectDataSource1.SelectParameters["pStart_Date"] = objStart_DateParameter;
        ObjectDataSource1.SelectParameters["pEnd_Date"] = objEnd_DateParameter;
        ObjectDataSource1.SelectParameters["pDate_of_Exam"] = objDate_of_ExamParameter;
        ObjectDataSource1.SelectParameters["pLast_date_for_nomination"] = objLast_date_for_nominationParameter;
        ObjectDataSource1.SelectParameters["pLast_date_for_bcas_acceptance"] = objLast_date_for_bcas_acceptanceParameter;
        ObjectDataSource1.SelectParameters["pLast_date_for_payments"] = objLast_date_for_paymentsParameter;
        ObjectDataSource1.SelectParameters["pLast_date_for_nomination_reappearing"] = objLast_date_for_nomination_reappearingParameter;
        ObjectDataSource1.SelectParameters["pLast_date_for_reappearing_fee_payments"] = objLast_date_for_reappearing_fee_paymentsParameter;

        //Rebinding Data Grid
        GridView1.DataBind();
    }

    

    protected bool ValidateData()
    {
        bool valid = true;
        bullet_errors.Items.Clear();
        //--------------------------------------------------------------------
        //mev_course_code.Validate();
        //if (!mev_course_code.IsValid)
        //{
        //    bullet_errors.Items.Add("Course Code must be Entered");
        //    valid = false;
        //}

        mev_start_date.Validate();
        if (!mev_start_date.IsValid)
        {
            bullet_errors.Items.Add("Start Date must be Entered");
            valid = false;
        }

        //Added 12 Oct 2023
        if (!(ddlCourseCode.SelectedValue.ToString() == "3" || ddlCourseCode.SelectedValue.ToString() == "4"))
        {
            
            mev_end_date.Validate();
            if (!mev_end_date.IsValid)
            {
                bullet_errors.Items.Add("End Date must be Entered");
                valid = false;
            }
        }
       
           

        mev_date_of_exam.Validate();
        if (!mev_date_of_exam.IsValid)
        {
            bullet_errors.Items.Add("Date of Exam must be Entered");
            valid = false;
        }

        mev_last_date_for_nomination.Validate();
        if (!mev_last_date_for_nomination.IsValid)
        {
            bullet_errors.Items.Add("Last date for nomination must be Entered");
            valid = false;
        }

        mev_last_date_for_bcas_acceptance.Validate();
        if (!mev_last_date_for_bcas_acceptance.IsValid)
        {
            bullet_errors.Items.Add("Last date for bcas acceptance must be Entered");
            valid = false;
        }

        mev_last_date_for_payments.Validate();
        if (!mev_last_date_for_payments.IsValid)
        {
            bullet_errors.Items.Add("Last date for payments must be Entered");
            valid = false;
        }
        //Added 202 Oct 2023
        if (!(ddlCourseCode.SelectedValue.ToString() == "3" || ddlCourseCode.SelectedValue.ToString() == "4"))
        {
            
            mev_last_date_for_nomination_reappearing.Validate();
            if (!mev_last_date_for_nomination_reappearing.IsValid)
            {
                bullet_errors.Items.Add("Last date for nomination reappearing must be Entered");
                valid = false;
            }
        }
       
        //Added 12 Oct 2023
        if (!(ddlCourseCode.SelectedValue.ToString() == "3" || ddlCourseCode.SelectedValue.ToString() == "4"))
        {
          
            mev_last_date_for_reappearing_fee_payments.Validate();
            if (!mev_last_date_for_reappearing_fee_payments.IsValid)
            {
                bullet_errors.Items.Add("Last date for reappearing fee payments must be Entered");
                valid = false;
            }
        }
        
        return valid;
    }

    protected void ddlCourseCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (lbl_choice.Text == "I")
        {
            if (ddlCourseCode.SelectedValue.ToString() == "3" || ddlCourseCode.SelectedValue.ToString() == "4")
            {
                txt_end_date.Enabled = false;
                txt_last_date_for_nomination_reappearing.Enabled = false;
                txt_last_date_for_reappearing_fee_payments.Enabled = false;
                txt_end_date.Text = txt_start_date.Text;
                txt_last_date_for_nomination_reappearing.Text = txt_last_date_for_nomination.Text;
                txt_last_date_for_reappearing_fee_payments.Text = txt_last_date_for_payments.Text;
                mev_last_date_for_nomination_reappearing.EnableClientScript = false;
                mev_last_date_for_reappearing_fee_payments.EnableClientScript = false;
                mev_end_date.EnableClientScript = false;

            }
            else
            {
                txt_end_date.Enabled = true;
                txt_last_date_for_nomination_reappearing.Enabled = true;
                txt_last_date_for_reappearing_fee_payments.Enabled = true;
                mev_last_date_for_nomination_reappearing.Enabled = true;
                mev_last_date_for_reappearing_fee_payments.Enabled = true;
                mev_last_date_for_reappearing_fee_payments.EnableClientScript = true;
                mev_last_date_for_nomination_reappearing.EnableClientScript = true;
                mev_end_date.EnableClientScript = true;
            }
        }
    }
}
