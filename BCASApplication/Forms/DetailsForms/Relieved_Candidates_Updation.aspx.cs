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

public partial class Relieved_Candidates_Updation : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if ((Convert.ToString(HttpContext.Current.Session["UserId"]).Trim().Length == 0))
            {
                HttpContext.Current.Response.Redirect("~/Home.aspx");
            }
            else
            {
                enabled_disabled_controls('L');
            }
        }
    }
    protected void btn_insert_Click(object sender, EventArgs e)
    {
        lbl_choice.Text = "I";
        enabled_disabled_controls('I');
        txt_employee_id.Focus();
    }

    protected void btn_update_Click(object sender, EventArgs e)
    {
        lbl_choice.Text = "U";
        enabled_disabled_controls('U');
        txt_employee_id.Focus();
    }

    protected void btn_search_Click(object sender, EventArgs e)
    {
        lbl_choice.Text = "F";
        enabled_disabled_controls('F');
        txt_employee_id.Focus();
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
            TYP_CANDIDATE_REGISTRATION newobj = new TYP_CANDIDATE_REGISTRATION();
            newobj.ORGANISATION_CODE = Convert.ToInt32(ddl_OrgCode.SelectedValue);
            newobj.EMPLOYEE_ID = txt_employee_id.Text.ToUpper();
            newobj.CANDIDATE_NAME = txt_candidate_name.Text.ToUpper();
            newobj.SEX_CODE = ddl_gender_code.SelectedValue;
            newobj.DESIGNATION_CODE = Convert.ToInt32(ddl_Designation.SelectedValue);
            newobj.DOB = DateTime.ParseExact(txt_dob.Text, "dd/MM/yyyy", new System.Globalization.DateTimeFormatInfo());
            newobj.EMAIL_ID = txt_email_id.Text.ToUpper();
            newobj.MOBILE_NO = txt_mobile_no.Text.ToUpper();
            newobj.NATIONALITY_CODE = Convert.ToInt32(ddl_Nationality.SelectedValue);
            newobj.EDUCATIONAL_QUALIFICATION_CODE = Convert.ToInt32(ddl_Qualification .SelectedValue );
            if (lbl_choice.Text == "I")
                newobj.REGISTRATION_NUMBER = 0;
            else
                newobj.REGISTRATION_NUMBER = Convert.ToInt32(txt_registration_number.Text);
            newobj.ENTER_DATE = DateTime.Now;
            newobj.ENTER_BY = HttpContext.Current.User.Identity.Name;
if (newobj.ORGANISATION_CODE==0)
  {
      lbl_error.Text = "Please Select Organisation Code";
      return;
  }
            switch (lbl_choice.Text)
            {
                case "I":
                    txt_registration_number.Text = TYP_CANDIDATE_REGISTRATION.InsertCandidate_Registration(newobj).ToString();
                    lbl_error.Text = " Record Successfully Saved ";
                    break;
                case "U":
                    TYP_CANDIDATE_REGISTRATION.UpdateCandidate_Registration_Relieved(newobj);
                    lbl_error.Text = " Record Successfully Updated ";
                    break;
                case "D":
                    TYP_CANDIDATE_REGISTRATION.DeleteCandidate_Registration(Convert.ToInt32(txt_registration_number.Text));
                    lbl_error.Text = " Record Successfully Deleted ";
                    break;
            }

            GridParameter("-1", "*", "*", "*", "-1", "01/01/1900", "*", "*", "-1", "-1", txt_registration_number.Text);
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
        ddl_NewOrgCode .SelectedValue  = GridView1.SelectedRow.Cells[1].Text;
        txt_employee_id.Text = GridView1.SelectedRow.Cells[2].Text;
        txt_candidate_name.Text = GridView1.SelectedRow.Cells[3].Text;
        ddl_gender_code.SelectedValue = GridView1.SelectedRow.Cells[4].Text;
        ddl_Designation.SelectedValue  = GridView1.SelectedRow.Cells[5].Text;
        txt_dob.Text = Convert.ToDateTime(GridView1.SelectedRow.Cells[6].Text).Date.ToString("dd/MM/yyyy");
        txt_email_id.Text = GridView1.SelectedRow.Cells[7].Text;
        txt_mobile_no.Text = GridView1.SelectedRow.Cells[8].Text;
        ddl_Nationality.SelectedValue  = GridView1.SelectedRow.Cells[9].Text;
        ddl_Qualification.SelectedValue = GridView1.SelectedRow.Cells[10].Text;
        txt_registration_number.Text = GridView1.SelectedRow.Cells[11].Text;
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
                txt_registration_number.Enabled = false;
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
                txt_registration_number.Enabled = false;
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
        ddl_OrgCode.SelectedIndex = 0;
        txt_employee_id.Text = "";
        txt_candidate_name.Text = "";
        ddl_gender_code.SelectedIndex = 0;
        ddl_Designation.SelectedIndex = 0;
        txt_dob.Text = "";
        txt_email_id.Text = "";
        txt_mobile_no.Text = "";
        ddl_Nationality.SelectedIndex = 0;
        ddl_Qualification.SelectedIndex = 0;
        txt_registration_number.Text = "";
    }

    void enabled_disabled_controls(bool flag)
    {
        ddl_OrgCode.Enabled = flag;
        txt_employee_id.Enabled = flag;
        txt_candidate_name.Enabled = flag;
       ddl_gender_code.Enabled = flag;
        ddl_Designation .Enabled = flag;
        txt_dob.Enabled = flag;
        txt_email_id.Enabled = flag;
        txt_mobile_no.Enabled = flag;
        ddl_Nationality.Enabled = flag;
        ddl_Qualification.Enabled = flag;
        txt_registration_number.Enabled = flag;
    }

    void enabled_disabled_validation(bool flag)
    {
        //mev_organisation_code.Enabled = flag;
        mev_employee_id.Enabled = flag;
        mev_candidate_name.Enabled = flag;
        //mev_sex_code.Enabled = flag;
        //mev_designation_code.Enabled = flag;
        mev_dob.Enabled = flag;
        mev_email_id.Enabled = flag;
        mev_mobile_no.Enabled = flag;
        //mev_nationality_code.Enabled = flag;
        //mev_educational_qualification_code.Enabled = flag;
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
            e.Row.Cells[6].Text = Convert.ToDateTime(e.Row.Cells[6].Text).Date.ToString("dd-MMM-yyyy").ToUpper().Trim();
            e.Row.Cells[16].Text = Convert.ToDateTime(e.Row.Cells[16].Text).Date.ToString("dd-MMM-yyyy").ToUpper().Trim();
            }
    }

    protected void btn_display_Click(object sender, EventArgs e)
    {
        lbl_error.Text = "";
        string pOrganisation_Code, pEmployee_ID, pCandidate_Name, pSex_Code, pDesignation_Code, pDOB, pEmail_Id, pMobile_No, pNationality_Code, pEducational_Qualification_Code, pRegistration_Number;

        if (ddl_OrgCode.SelectedIndex == 0)
            pOrganisation_Code = "-1";
        else
            pOrganisation_Code = ddl_OrgCode.SelectedValue;

        if (txt_employee_id.Text == "")
            pEmployee_ID = "*";
        else
            pEmployee_ID = txt_employee_id.Text.ToUpper();

        if (txt_candidate_name.Text == "")
            pCandidate_Name = "*";
        else
            pCandidate_Name = txt_candidate_name.Text.ToUpper();

        if (ddl_gender_code.SelectedIndex  == 0)
            pSex_Code = "*";
        else
            pSex_Code = ddl_gender_code.SelectedValue ;

        if (ddl_Designation.SelectedIndex == 0)
            pDesignation_Code = "-1";
        else
            pDesignation_Code =ddl_Designation.SelectedValue;

        if (txt_dob.Text == "")
            pDOB = "01/01/1900";
        else
            pDOB = txt_dob.Text.ToUpper();

        if (txt_email_id.Text == "")
            pEmail_Id = "*";
        else
            pEmail_Id = txt_email_id.Text.ToUpper();

        if (txt_mobile_no.Text == "")
            pMobile_No = "*";
        else
            pMobile_No = txt_mobile_no.Text.ToUpper();

        if (ddl_Nationality.SelectedIndex == 0)
            pNationality_Code = "-1";
        else
            pNationality_Code = ddl_Nationality.SelectedValue;

        if (ddl_Qualification.SelectedIndex  == 0)
            pEducational_Qualification_Code = "-1";
        else
            pEducational_Qualification_Code = ddl_Qualification.SelectedValue ;

        if (txt_registration_number.Text == "")
            pRegistration_Number = "-1";
        else
            pRegistration_Number = txt_registration_number.Text.ToUpper();

        //-----Setting Grid Parameters-----------------------------------------
        GridParameter(pOrganisation_Code, pEmployee_ID, pCandidate_Name, pSex_Code, pDesignation_Code, pDOB, pEmail_Id, pMobile_No, pNationality_Code, pEducational_Qualification_Code, pRegistration_Number);
    }

    private void GridParameter(string pOrganisation_Code, string pEmployee_ID, string pCandidate_Name, string pSex_Code, string pDesignation_Code, string pDOB, string pEmail_Id, string pMobile_No, string pNationality_Code, string pEducational_Qualification_Code, string pRegistration_Number)
    {
        Parameter objOrganisation_CodeParameter = new Parameter("pOrganisation_Code", TypeCode.String, pOrganisation_Code);
        Parameter objEmployee_IDParameter = new Parameter("pEmployee_ID", TypeCode.String, pEmployee_ID);
        Parameter objCandidate_NameParameter = new Parameter("pCandidate_Name", TypeCode.String, pCandidate_Name);
        Parameter objSex_CodeParameter = new Parameter("pSex_Code", TypeCode.String, pSex_Code);
        Parameter objDesignation_CodeParameter = new Parameter("pDesignation_Code", TypeCode.String, pDesignation_Code);
        Parameter objDOBParameter = new Parameter("pDOB", TypeCode.DateTime , pDOB);
        Parameter objEmail_IdParameter = new Parameter("pEmail_Id", TypeCode.String, pEmail_Id);
        Parameter objMobile_NoParameter = new Parameter("pMobile_No", TypeCode.String, pMobile_No);
        Parameter objNationality_CodeParameter = new Parameter("pNationality_Code", TypeCode.String, pNationality_Code);
        Parameter objEducational_Qualification_CodeParameter = new Parameter("pEducational_Qualification_Code", TypeCode.String, pEducational_Qualification_Code);
        Parameter objRegistration_NumberParameter = new Parameter("pRegistration_Number", TypeCode.String, pRegistration_Number);

        //set up object data source parameters
        ObjectDataSource1.SelectParameters["pOrganisation_Code"] = objOrganisation_CodeParameter;
        ObjectDataSource1.SelectParameters["pEmployee_ID"] = objEmployee_IDParameter;
        ObjectDataSource1.SelectParameters["pCandidate_Name"] = objCandidate_NameParameter;
        ObjectDataSource1.SelectParameters["pSex_Code"] = objSex_CodeParameter;
        ObjectDataSource1.SelectParameters["pDesignation_Code"] = objDesignation_CodeParameter;
        ObjectDataSource1.SelectParameters["pDOB"] = objDOBParameter;
        ObjectDataSource1.SelectParameters["pEmail_Id"] = objEmail_IdParameter;
        ObjectDataSource1.SelectParameters["pMobile_No"] = objMobile_NoParameter;
        ObjectDataSource1.SelectParameters["pNationality_Code"] = objNationality_CodeParameter;
        ObjectDataSource1.SelectParameters["pEducational_Qualification_Code"] = objEducational_Qualification_CodeParameter;
        ObjectDataSource1.SelectParameters["pRegistration_Number"] = objRegistration_NumberParameter;

        //Rebinding Data Grid
        GridView1.DataBind();
    }

   

    protected bool ValidateData()
    {
        bool valid = true;
        bullet_errors.Items.Clear();
        //--------------------------------------------------------------------
       
        mev_employee_id.Validate();
        if (!mev_employee_id.IsValid)
        {
            bullet_errors.Items.Add("Employee ID must be Entered");
            valid = false;
        }

        mev_candidate_name.Validate();
        if (!mev_candidate_name.IsValid)
        {
            bullet_errors.Items.Add("Candidate Name must be Entered");
            valid = false;
        }

        //mev_sex_code.Validate();
        //if (!mev_sex_code.IsValid)
        //{
        //    bullet_errors.Items.Add("Sex Code must be Entered");
        //    valid = false;
        //}

        //mev_designation_code.Validate();
        //if (!mev_designation_code.IsValid)
        //{
        //    bullet_errors.Items.Add("Designation Code must be Entered");
        //    valid = false;
        //}

        mev_dob.Validate();
        if (!mev_dob.IsValid)
        {
            bullet_errors.Items.Add("DOB must be Entered");
            valid = false;
        }

        mev_email_id.Validate();
        if (!mev_email_id.IsValid)
        {
            bullet_errors.Items.Add("Email Id must be Entered");
            valid = false;
        }

        mev_mobile_no.Validate();
        if (!mev_mobile_no.IsValid)
        {
            bullet_errors.Items.Add("Mobile No must be Entered");
            valid = false;
        }

        //mev_nationality_code.Validate();
        //if (!mev_nationality_code.IsValid)
        //{
        //    bullet_errors.Items.Add("Nationality Code must be Entered");
        //    valid = false;
        //}

        //mev_educational_qualification_code.Validate();
        //if (!mev_educational_qualification_code.IsValid)
        //{
        //    bullet_errors.Items.Add("Educational Qualification Code must be Entered");
        //    valid = false;
        //}

        return valid;
    }
}
