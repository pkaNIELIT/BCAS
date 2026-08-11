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

public partial class Working_Experience : System.Web.UI.Page
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

            //Session["lbl_Apl_no"] = 1000007;

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

                        txt_Candidate_Name.Text = dt.Rows[0]["Candidate_Name"].ToString();


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

                Comp_Val_DateTo.ValueToCompare = DateTime.Now.ToShortDateString();

                enabled_disabled_controls('L');
                grd_panel.Visible = false;
                //txt_organisation.Focus();

            }
        }

    }
    protected void btn_insert_Click(object sender, EventArgs e)
    {
        lbl_choice.Text = "I";
        enabled_disabled_controls('I');
        txt_organisation.Focus();
    }

    protected void btn_update_Click(object sender, EventArgs e)
    {
        lbl_choice.Text = "U";
        enabled_disabled_controls('U');
        txt_organisation.Focus();
    }

    protected void btn_search_Click(object sender, EventArgs e)
    {
        lbl_choice.Text = "F";
        enabled_disabled_controls('F');
        txt_organisation.Focus();
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
            TYP_WORKING_EXPERIENCE newobj = new TYP_WORKING_EXPERIENCE();
            if (lbl_choice.Text == "I")
                newobj.SR_NO = 0;
            else
                newobj.SR_NO = Convert.ToInt32(txt_sr_no.Text);
            newobj.REGISTRATION_NUMBER = Convert.ToInt32(txt_registration_number.Text);
            newobj.ORGANISATION = txt_organisation.Text.ToUpper();
            newobj.DESIGNATION = txt_designation.Text.ToUpper();
            newobj.DATE_FROM = DateTime.ParseExact(txt_date_from.Text, "dd/MM/yyyy", new System.Globalization.DateTimeFormatInfo());
            newobj.DATE_TO = DateTime.ParseExact(txt_date_to.Text, "dd/MM/yyyy", new System.Globalization.DateTimeFormatInfo());
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
                    txt_sr_no.Text = TYP_WORKING_EXPERIENCE.InsertWorking_Experience(newobj).ToString();
                    lbl_error.Text = " Record Successfully Saved ";
                    break;
                case "U":
                    TYP_WORKING_EXPERIENCE.UpdateWorking_Experience(newobj);
                    lbl_error.Text = " Record Successfully Updated ";
                    break;
                case "D":
                    TYP_WORKING_EXPERIENCE.DeleteWorking_Experience(Convert.ToInt32(txt_sr_no.Text));
                    lbl_error.Text = " Record Successfully Deleted ";
                    break;
            }

            GridView1.DataSource = ObjectDataSource1;
            grd_panel.Visible = true;


            GridParameter(txt_sr_no.Text, "-1", "*", "*", "01/01/1900", "01/01/1900", "*");
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
        txt_registration_number.Text = GridView1.SelectedRow.Cells[2].Text;
        txt_organisation.Text = GridView1.SelectedRow.Cells[3].Text;
        txt_designation.Text = GridView1.SelectedRow.Cells[4].Text;
        txt_date_from.Text = Convert.ToDateTime(GridView1.SelectedRow.Cells[5].Text).Date.ToString("dd/MM/yyyy");
        txt_date_to.Text = Convert.ToDateTime(GridView1.SelectedRow.Cells[6].Text).Date.ToString("dd/MM/yyyy");
        if (GridView1.SelectedRow.Cells[7].Text == "&nbsp;" || GridView1.SelectedRow.Cells[7].Text == "")
            txt_remarks.Text = "";
        else
            txt_remarks.Text = GridView1.SelectedRow.Cells[7].Text;

       // enabled_disabled_controls('G');
        lbl_choice.Text = "U";
        enabled_disabled_controls('U');
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
                //frm_panel.Enabled = false;
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
                //frm_panel.Enabled = false;
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
                //frm_panel.Enabled = false;
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
                txt_sr_no.Enabled = false;
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
                txt_sr_no.Enabled = false;
                break;
            case 'G':
                lbl_error.Text = "";
                btn_insert.Enabled = false;
                btn_search.Enabled = false;
                btn_update.Enabled = true;
                
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
        txt_organisation.Text = "";
        txt_designation.Text = "";
        txt_date_from.Text = "";
        txt_date_to.Text = "";
        txt_remarks.Text = "";
    }

    void enabled_disabled_controls(bool flag)
    {
        txt_sr_no.Enabled = flag;
        //txt_registration_number.Enabled = flag;
        txt_organisation.Enabled = flag;
        txt_designation.Enabled = flag;
        txt_date_from.Enabled = flag;
        txt_date_to.Enabled = flag;
        txt_remarks.Enabled = flag;
    }

    void enabled_disabled_validation(bool flag)
    {
       
        mev_organisation.Enabled = flag;
        mev_designation.Enabled = flag;
        mev_date_from.Enabled = flag;
        mev_date_to.Enabled = flag;
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
            e.Row.Cells[6].Text = Convert.ToDateTime(e.Row.Cells[6].Text).Date.ToString("dd-MMM-yyyy").ToUpper().Trim();
            e.Row.Cells[9].Text = Convert.ToDateTime(e.Row.Cells[9].Text).Date.ToString("dd-MMM-yyyy").ToUpper().Trim();
            }
    }
    protected void btn_display_Click(object sender, EventArgs e)
    {
        GridView1.DataSource = ObjectDataSource1;
        grd_panel.Visible = true;


        lbl_error.Text = "";
        string pSr_no, pRegistration_Number, pOrganisation, pDesignation, pDate_From, pDate_To, pRemarks;

        if (txt_sr_no.Text == "")
            pSr_no = "-1";
        else
            pSr_no = txt_sr_no.Text.ToUpper();

        if (txt_registration_number.Text == "")
            pRegistration_Number = "-1";
        else
            pRegistration_Number = txt_registration_number.Text.ToUpper();

        if (txt_organisation.Text == "")
            pOrganisation = "*";
        else
            pOrganisation = txt_organisation.Text.ToUpper();

        if (txt_designation.Text == "")
            pDesignation = "*";
        else
            pDesignation = txt_designation.Text.ToUpper();

        if (txt_date_from.Text == "")
            pDate_From = "01/01/1900";
        else
            pDate_From = txt_date_from.Text.ToUpper();

        if (txt_date_to.Text == "")
            pDate_To = "01/01/1900";
        else
            pDate_To = txt_date_to.Text.ToUpper();

        if (txt_remarks.Text == "")
            pRemarks = "*";
        else
            pRemarks = txt_remarks.Text.ToUpper();

        //-----Setting Grid Parameters-----------------------------------------
        GridParameter(pSr_no, pRegistration_Number, pOrganisation, pDesignation, pDate_From, pDate_To, pRemarks);
    }

    private void GridParameter(string pSr_no, string pRegistration_Number, string pOrganisation, string pDesignation, string pDate_From, string pDate_To, string pRemarks)
    {
        Parameter objSr_noParameter = new Parameter("pSr_no", TypeCode.String, pSr_no);
        Parameter objRegistration_NumberParameter = new Parameter("pRegistration_Number", TypeCode.String, pRegistration_Number);
        Parameter objOrganisationParameter = new Parameter("pOrganisation", TypeCode.String, pOrganisation);
        Parameter objDesignationParameter = new Parameter("pDesignation", TypeCode.String, pDesignation);
        Parameter objDate_FromParameter = new Parameter("pDate_From", TypeCode.DateTime , pDate_From);
        Parameter objDate_ToParameter = new Parameter("pDate_To", TypeCode.DateTime , pDate_To);
        Parameter objRemarksParameter = new Parameter("pRemarks", TypeCode.String, pRemarks);

        //set up object data source parameters
        ObjectDataSource1.SelectParameters["pSr_no"] = objSr_noParameter;
        ObjectDataSource1.SelectParameters["pRegistration_Number"] = objRegistration_NumberParameter;
        ObjectDataSource1.SelectParameters["pOrganisation"] = objOrganisationParameter;
        ObjectDataSource1.SelectParameters["pDesignation"] = objDesignationParameter;
        ObjectDataSource1.SelectParameters["pDate_From"] = objDate_FromParameter;
        ObjectDataSource1.SelectParameters["pDate_To"] = objDate_ToParameter;
        ObjectDataSource1.SelectParameters["pRemarks"] = objRemarksParameter;

        //Rebinding Data Grid
        GridView1.DataBind();
    }
   
    

    protected bool ValidateData()
    {
        bool valid = true;
        bullet_errors.Items.Clear();
        //--------------------------------------------------------------------
        //mev_registration_number.Validate();
        //if (!mev_registration_number.IsValid)
        //{
        //    bullet_errors.Items.Add("Registration Number must be Entered");
        //    valid = false;
        //}

        mev_organisation.Validate();
        if (!mev_organisation.IsValid)
        {
            bullet_errors.Items.Add("Organisation must be Entered");
            valid = false;
        }

        mev_designation.Validate();
        if (!mev_designation.IsValid)
        {
            bullet_errors.Items.Add("Designation must be Entered");
            valid = false;
        }

        mev_date_from.Validate();
        if (!mev_date_from.IsValid)
        {
            bullet_errors.Items.Add("Date From must be Entered");
            valid = false;
        }

        mev_date_to.Validate();
        if (!mev_date_to.IsValid)
        {
            bullet_errors.Items.Add("Date To must be Entered");
            valid = false;
        }

        return valid;
    }
}
