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

public partial class Previous_Courses_attended : System.Web.UI.Page
{
    TYP_ApplicantDetails objApplicant = new TYP_ApplicantDetails();

    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Convert.ToString(HttpContext.Current.Session["UserId"]).Trim().Length == 0))
        {
            HttpContext.Current.Response.Redirect("~/Home.aspx");
        }
        else
        {

            R1.Visible = false;

            //DisableForwordButton();
            //**********
            HttpContext.Current.Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
            HttpContext.Current.Response.Cache.SetValidUntilExpires(false);
            //HttpContext.Current.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            HttpContext.Current.Response.Cache.SetNoStore();
            //*******

            //Session["lbl_Apl_no"] = 1000012;

            if (Convert.ToString(HttpContext.Current.Session["lbl_Apl_no"]).Trim().Length != 0)
            {
                //############

                ////*********
                //Random rd = new Random();
                //Session["New"] = rd.Next();
                //HttpCookie myCookie = new HttpCookie("Session");
                //myCookie.Value = Session["New"].ToString();
                //myCookie.Path = "/";
                //Response.Cookies.Add(myCookie);

                //**********      

                objApplicant.i_Registration_Number = Convert.ToInt32(Session["lbl_Apl_no"].ToString());

                using (DataTable dt = objApplicant.getCandidateDetails_By_RegNo(Convert.ToInt32(Session["lbl_Apl_no"].ToString())))
                {
                    if (dt.Rows.Count != 0)
                    {
                        txt_registration_number.Text = dt.Rows[0]["Registration_Number"].ToString();

                        txt_CandidateName.Text = dt.Rows[0]["Candidate_Name"].ToString();


                    }

                }
                //###########            

            }
            else
            {
                Response.Redirect("~/Forms/DetailsForms/ApplicantDetails.aspx");

            }

            //**************

            if (!Page.IsPostBack)
            {

                Com_v_date_from.ValueToCompare = DateTime.Now.ToShortDateString();
                enabled_disabled_controls('L');

                //enabled_disabled_controls('I');
                grd_panel.Visible = false;
                //ddlCourseCode.Focus();

            }
        }

        //if (!Page.IsPostBack)
        //{
            
        //}


    }
    protected void btn_insert_Click(object sender, EventArgs e)
    {
        lbl_choice.Text = "I";
        enabled_disabled_controls('I');
        ddlCourseCode.Focus();
        //txt_registration_number.Focus();
    }

    protected void btn_update_Click(object sender, EventArgs e)
    {
        lbl_choice.Text = "U";
        enabled_disabled_controls('U');
        //txt_registration_number.Focus();
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
            if (!ValidateData())
                return;
            lbl_errors.Visible = false;
        }

        try
        {
            TYP_PREVIOUS_COURSES_ATTENDED newobj = new TYP_PREVIOUS_COURSES_ATTENDED();
            if (lbl_choice.Text == "I")
                newobj.SR_NO = 0;
            else
                newobj.SR_NO = Convert.ToInt32(txt_sr_no.Text);
            newobj.REGISTRATION_NUMBER = Convert.ToInt32(txt_registration_number.Text);
            newobj.COURSE_CODE = Convert.ToInt32(ddlCourseCode.SelectedValue.ToString());
            newobj.DATE_FROM = DateTime.ParseExact(txt_date_from.Text, "dd/MM/yyyy", new System.Globalization.DateTimeFormatInfo());
            newobj.DATE_TO = DateTime.ParseExact(txt_date_to.Text, "dd/MM/yyyy", new System.Globalization.DateTimeFormatInfo());
            newobj.MARKS = Convert.ToInt32(txt_marks.Text);
            newobj.RESULT = ddlResult.SelectedValue;
            if (txt_remarks.Text == "")
                newobj.REMARKSIsNull = true;
            else
            {
                newobj.REMARKSIsNull = false;
                newobj.REMARKS = txt_remarks.Text.ToUpper();
            }

            newobj.ENTER_DATE = DateTime.Now;
            newobj.ENTER_BY = HttpContext.Current.User.Identity.Name;

            switch (lbl_choice.Text)
            {
                case "I":
                    txt_sr_no.Text = TYP_PREVIOUS_COURSES_ATTENDED.InsertPrevious_Courses_attended(newobj).ToString();
                    lbl_error.Text = " Record Successfully Saved ";
                    break;
                case "U":
                    TYP_PREVIOUS_COURSES_ATTENDED.UpdatePrevious_Courses_attended(newobj);
                    lbl_error.Text = " Record Successfully Updated ";
                    break;
                case "D":
                    TYP_PREVIOUS_COURSES_ATTENDED.DeletePrevious_Courses_attended(Convert.ToInt32(txt_sr_no.Text));
                    lbl_error.Text = " Record Successfully Deleted ";
                    break;
            }
            GridView1.DataSource = ObjectDataSource1;
            grd_panel.Visible = true;

            hlinkWorkingExp.Visible = true;
            GridParameter(txt_sr_no.Text, "-1", "-1", "01/01/1900", "01/01/1900", "-1", "*", "*");
            enabled_disabled_controls('S');
            hlinkWorkingExp.Visible = true;
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
        txt_registration_number.Text = GridView1.SelectedRow.Cells[2].Text;
        ddlCourseCode.SelectedValue = GridView1.SelectedRow.Cells[3].Text;
        txt_date_from.Text = Convert.ToDateTime(GridView1.SelectedRow.Cells[4].Text).Date.ToString("dd/MM/yyyy");
        txt_date_to.Text = Convert.ToDateTime(GridView1.SelectedRow.Cells[5].Text).Date.ToString("dd/MM/yyyy");
        txt_marks.Text = GridView1.SelectedRow.Cells[6].Text;
        ddlResult.SelectedValue  = GridView1.SelectedRow.Cells[7].Text;
        if (GridView1.SelectedRow.Cells[8].Text == "&nbsp;" || GridView1.SelectedRow.Cells[8].Text == "")
            txt_remarks.Text = "";
        else
            txt_remarks.Text = GridView1.SelectedRow.Cells[8].Text;

        //enabled_disabled_controls('G');
        lbl_choice.Text = "U";
        enabled_disabled_controls('U');
    }

    void enabled_disabled_controls(char choice)
    {
        switch (choice)
        {
            case 'L': //Login
                //lbl_error.Text = "";
                //btn_insert.Enabled = true;
                //btn_search.Enabled = true;
                //btn_update.Enabled = true ;
                //btn_save.Enabled = true ;
                //btn_cancel.Enabled = true ;
                //btn_quit.Enabled = true;
                //enabled_disabled_validation(false);
                ////frm_panel.Enabled = false;
                //frm_panel.Enabled = true;


                lbl_error.Text = "";
                btn_insert.Enabled = true;
                btn_search.Enabled = true;
                btn_update.Enabled = false;
                btn_save.Enabled = false;
                btn_cancel.Enabled = false;
                btn_quit.Enabled = true;
                enabled_disabled_validation(false);
                //frm_panel.Enabled = false;

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
               // frm_panel.Enabled = false;
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
                //frm_panel.Enabled = false;
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
                //frm_panel.Enabled = false;
                break;
        };
    }

    void clear_contents()
    {
        lbl_choice.Text = "";
        lbl_error.Text = "";
        txt_sr_no.Text = "";
        //txt_registration_number.Text = "";
        ddlCourseCode.SelectedIndex = 0;
        txt_date_from.Text = "";
        txt_date_to.Text = "";
        txt_marks.Text = "";
        ddlResult.SelectedIndex  = 0;
        txt_remarks.Text = "";
    }

    void enabled_disabled_controls(bool flag)
    {
        txt_sr_no.Enabled = flag;
        //txt_registration_number.Enabled = flag;
        ddlCourseCode.Enabled = flag;
        txt_date_from.Enabled = flag;
        txt_date_to.Enabled = flag;
        txt_marks.Enabled = flag;
        ddlResult.Enabled  = flag;
        txt_remarks.Enabled = flag;
    }

    void enabled_disabled_validation(bool flag)
    {
        mev_registration_number.Enabled = flag;
        //mev_course_code.Enabled = flag;
        mev_date_from.Enabled = flag;
        mev_date_to.Enabled = flag;
        mev_marks.Enabled = flag;
        //mev_result.Enabled = flag;
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
            e.Row.Cells[5].Text = Convert.ToDateTime(e.Row.Cells[5].Text).Date.ToString("dd-MMM-yyyy").ToUpper().Trim();
            e.Row.Cells[10].Text = Convert.ToDateTime(e.Row.Cells[10].Text).Date.ToString("dd-MMM-yyyy").ToUpper().Trim();
            }
    }
    protected void btn_display_Click(object sender, EventArgs e)
    {
        GridView1.DataSource = ObjectDataSource1;
        grd_panel.Visible = true;

        lbl_error.Text = "";
        string pSr_No, pRegistration_Number, pCourse_Code, pDate_from, pDate_To, pMarks, pResult, pRemarks;

        if (txt_sr_no.Text == "")
            pSr_No = "-1";
        else
            pSr_No = txt_sr_no.Text.ToUpper();

        if (txt_registration_number.Text == "")
            pRegistration_Number = "-1";
        else
            pRegistration_Number = txt_registration_number.Text.ToUpper();

        if (ddlCourseCode.SelectedIndex == 0)
            pCourse_Code = "-1";
        else
            pCourse_Code = ddlCourseCode.SelectedValue;

        if (txt_date_from.Text == "")
            pDate_from = "01/01/1900";
        else
            pDate_from = txt_date_from.Text.ToUpper();

        if (txt_date_to.Text == "")
            pDate_To = "01/01/1900";
        else
            pDate_To = txt_date_to.Text.ToUpper();

        if (txt_marks.Text == "")
            pMarks = "-1";
        else
            pMarks = txt_marks.Text.ToUpper();

        if (ddlResult.SelectedIndex  == 0)
            pResult = "*";
        else
            pResult = ddlResult.SelectedValue;

        if (txt_remarks.Text == "")
            pRemarks = "*";
        else
            pRemarks = txt_remarks.Text.ToUpper();

        //-----Setting Grid Parameters-----------------------------------------
        GridParameter(pSr_No, pRegistration_Number, pCourse_Code, pDate_from, pDate_To, pMarks, pResult, pRemarks);
    }

    private void GridParameter(string pSr_No, string pRegistration_Number, string pCourse_Code, string pDate_from, string pDate_To, string pMarks, string pResult, string pRemarks)
    {
        Parameter objSr_NoParameter = new Parameter("pSr_No", TypeCode.String, pSr_No);
        Parameter objRegistration_NumberParameter = new Parameter("pRegistration_Number", TypeCode.String, pRegistration_Number);
        Parameter objCourse_CodeParameter = new Parameter("pCourse_Code", TypeCode.String, pCourse_Code);
        Parameter objDate_fromParameter = new Parameter("pDate_from", TypeCode.DateTime , pDate_from);
        Parameter objDate_ToParameter = new Parameter("pDate_To", TypeCode.DateTime , pDate_To);
        Parameter objMarksParameter = new Parameter("pMarks", TypeCode.String, pMarks);
        Parameter objResultParameter = new Parameter("pResult", TypeCode.String, pResult);
        Parameter objRemarksParameter = new Parameter("pRemarks", TypeCode.String, pRemarks);

        //set up object data source parameters
        ObjectDataSource1.SelectParameters["pSr_No"] = objSr_NoParameter;
        ObjectDataSource1.SelectParameters["pRegistration_Number"] = objRegistration_NumberParameter;
        ObjectDataSource1.SelectParameters["pCourse_Code"] = objCourse_CodeParameter;
        ObjectDataSource1.SelectParameters["pDate_from"] = objDate_fromParameter;
        ObjectDataSource1.SelectParameters["pDate_To"] = objDate_ToParameter;
        ObjectDataSource1.SelectParameters["pMarks"] = objMarksParameter;
        ObjectDataSource1.SelectParameters["pResult"] = objResultParameter;
        ObjectDataSource1.SelectParameters["pRemarks"] = objRemarksParameter;

        //Rebinding Data Grid
        GridView1.DataBind();
    }
   

    protected bool ValidateData()
    {
        bool valid = true;
        bullet_errors.Items.Clear();
        //--------------------------------------------------------------------
        mev_registration_number.Validate();
        if (!mev_registration_number.IsValid)
        {
            bullet_errors.Items.Add("Registration Number must be Entered");
            valid = false;
        }

        //mev_course_code.Validate();
        //if (!mev_course_code.IsValid)
        //{
        //    bullet_errors.Items.Add("Course Code must be Entered");
        //    valid = false;
        //}

        mev_date_from.Validate();
        if (!mev_date_from.IsValid)
        {
            bullet_errors.Items.Add("Date from must be Entered");
            valid = false;
        }

        mev_date_to.Validate();
        if (!mev_date_to.IsValid)
        {
            bullet_errors.Items.Add("Date To must be Entered");
            valid = false;
        }

        mev_marks.Validate();
        if (!mev_marks.IsValid)
        {
            bullet_errors.Items.Add("Marks must be Entered");
            valid = false;
        }

        //mev_result.Validate();
        //if (!mev_result.IsValid)
        //{
        //    bullet_errors.Items.Add("Result must be Entered");
        //    valid = false;
        //}

        return valid;
    }

    protected void btnWorkingExp_Click(object sender, EventArgs e)
    {
        if ((Session["lbl_Apl_no"].ToString()).Length > 0)
        {

            Response.Redirect("~/Forms/DetailsForms/Working_Experience.aspx");

        }
        else
        {
            Response.Redirect("~/Forms/DetailsForms/ApplicantDetails.aspx");
        }

        
    }

    protected void ddlWhetherPrevious_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlWhetherPrevious.SelectedValue == "0")
        {
            UpdatePanel1.Visible = false;
           // hlinkWorkingExp.Visible = true;
        }
        if (ddlWhetherPrevious.SelectedValue == "1")
        {
            UpdatePanel1.Visible = true;
          /*  if (GridView1.Rows.Count == 0)
                hlinkWorkingExp.Visible = false;
            else
                hlinkWorkingExp.Visible = true;*/
        }

    }


}
