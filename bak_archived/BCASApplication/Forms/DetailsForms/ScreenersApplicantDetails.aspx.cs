using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Threading;
using BCASClassLibrary;
using System.Data.SqlClient;

public partial class Forms_DetailsForms_ScreenersApplicantDetails : System.Web.UI.Page
{
    public RadioButtonList pb_rdoOld
    {
        get
        {
            return rdOld;
        }
    }

    public String CurrentCity
    {
        get
        {
            return txt_EmpID.Text;
        }
    }

    
    public DropDownList pbddl_OrgCode
    {
        get
        {
            return ddl_OrgCode;
        }

    }

    public TextBox pbTxt_EmpID
    {
        get
        {
            return txt_EmpID;
        }
    }

    public TextBox pbTxt_EbasId
    {
        get
        {
            return Txt_EbasId;
        }
    }
    public TextBox pbTxt_BcasId
    {
        get
        {
            return Txt_BcasId;
        }
    }

    public DropDownList pbddl_Designation
    {
        get
        {
            return ddl_Designation;
        }
    }

    public DropDownList pbddl_drpStates
    {
        get
        {
            return drpStates;
        }
    }

    public DropDownList pbddl_drpCity
    {
        get
        {
            return drpCity;
        }
    }


    public TextBox pbTxt_candidate_name
    {
        get
        {
            return txt_candidate_name;
        }
    }
    //deep
    public TextBox pbTxt_organisation_name_of_candidate
    {
        get
        {
            return txt_organisation_name_of_candidate;
        }
    }

    public TextBox pbTxt_ApplicationNo
    {
        get
        {
            return txtApplicationNo ;
        }
    }

    public DropDownList pbddl_course_code
    {
        get
        {
            return ddlExistingCourse ;
        }
    }

    public DropDownList pbddl_billerOrgCode
    {
        get
        {
            return ddl_BillerOrgCode ;
        }
    }
    //public TextBox pbTxt_Aadhaar_Number
    //{
    //    get
    //    {
    //        if (txt_Aadhaar_Number.Text.Length ==12)
    //        {
    //            string aadhar = txt_Aadhaar_Number.Text;
    //            string EncAdh = EncryptDecrypt.EncryptString(aadhar);
    //            txt_Aadhaar_Number.Text = EncAdh;
    //            Session["EEA"] = "";
    //            Session["EEA"] = EncAdh;
    //            return txt_Aadhaar_Number;
    //        }
    //        ////if (txt_Aadhaar_Number.Text.Length > 12)
    //        ////{
    //        ////    txt_Aadhaar_Number.Text = Session["EEA"].ToString();
    //        ////    return txt_Aadhaar_Number;
    //        ////}
    //        ////else
    //        ////{
    //        ////    string aadhar = txt_Aadhaar_Number.Text;
    //        ////    string EncAdh = EncryptDecrypt.EncryptString(aadhar);
    //        ////    txt_Aadhaar_Number.Text = EncAdh;
    //        ////    Session["EEA"] = "";
    //        ////    Session["EEA"] = EncAdh;
    //        ////    return txt_Aadhaar_Number;
    //        //////}
    //        //string aadhar = txt_Aadhaar_Number.Text;
    //        //string EncAdh = EncryptDecrypt.EncryptString(aadhar);
    //        if (Session["EEA"].ToString() != "")
    //        {
    //            txt_Aadhaar_Number.Text = Session["EEA"].ToString();
    //        }
    //        //Session["EEA"] = EncAdh;
    //        return txt_Aadhaar_Number ;

    //    }
    //}

    public HiddenField pbTxt_Aadhaar_No
    {
        get
        {            
            return HAdh;
        }
    }
    //deep

    public DropDownList pbDdl_gender_code
    {
        get
        {
            return ddl_gender_code;
        }
    }

    public TextBox pbTxt_date_of_birth
    {
        get
        {
            return txt_date_of_birth;
        }
    }

    public DropDownList pbddl_Nationality
    {
        get
        {
            return ddl_Nationality;
        }
    }

    public TextBox pbTxt_mobile_no
    {
        get
        {
            return txt_mobile_no;
        }
    }

    public TextBox pbTxt_email_id
    {
        get
        {
            return txt_email_id;
        }
    }

    public DropDownList pbddl_Qualification
    {
        get
        {
            return ddl_Qualification;
        }
    }
    public DropDownList pbddl_Course
    {
        get
        {
            return ddl_Course;
        }
    }


    public CheckBox pbChk_declaration
    {
        get
        {
            return chk_declaration;
        }
    }

    protected void rdOld_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdOld.SelectedValue.ToString() == "1")
        {
            lnkRegister.Visible = false;
            trOldRegn.Visible = true;
            trOldAppln.Visible = true;
            trOldShow.Visible = true;
        }
        if (rdOld.SelectedValue.ToString() == "0")
        {
            lnkRegister.Visible = true;
            trOldRegn.Visible = false;
            trOldAppln.Visible = false;
            trOldShow.Visible = false;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            TYP_ScreenersApplicantDetails objScreener = new TYP_ScreenersApplicantDetails();
            ddlExistingCourse.Items.Clear();
            ListItem li=new ListItem ();
            li.Text ="Select";
            li.Value ="0";
            ddlExistingCourse.Items.Add(li);
            using (DataTable dt = objScreener.getExistingCourse())
            {
                if (dt.Rows.Count > 0)
                {

                    ddlExistingCourse.DataSource = dt;
                    ddlExistingCourse.DataTextField = "Course_Name"; // This is text displayed
                    ddlExistingCourse.DataValueField = "Course_Code"; // This is the value returned
                    ddlExistingCourse.DataBind();
                }

            }
            using (DataTable dt = objScreener.getScreenersCourse())
            {
                if (dt.Rows.Count > 0)
                {

                    ddl_Course.DataSource = dt;
                    ddl_Course.DataTextField = "Course_Name"; // This is text displayed
                    ddl_Course.DataValueField = "Course_Code"; // This is the value returned
                    ddl_Course.DataBind();
                }

            }
        }

        if (txt_email_id.Text != "")
        {
            chk_declaration.Checked = true;
            //if (chk_declaration.Checked == false)
            //{
            //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Check the Declaration!!')", true);
            //    return;
            //}
        }
        btn_confirm.Attributes.Add("OnClick", "ConfirmRecord()");
        if (!Page.IsPostBack)
        {
            if ((Convert.ToString(HttpContext.Current.Session["UserId"]).Trim().Length == 0))
            {
                HttpContext.Current.Response.Redirect("~/Home.aspx");
            }
          /*  else
            {
                Com_v_date_of_birth.ValueToCompare = DateTime.Now.ToShortDateString();
            }*/

        }

    }
   
    //protected void chk_declaration_CheckedChanged(object sender, EventArgs e)
    //{        
    //    //string EncAdh1 = EncryptDecrypt.DecryptString("vznwrprvRRsBxPx3ucpZ1A==");
    //    //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Script", "ConfirmRecord()", true);
    //    //if (chk_declaration.Checked == true)
    //    //{
    //    //    txt_Aadhaar_Number.Enabled = false;
    //    //    lblA.Text = txt_Aadhaar_Number.Text;
    //    //    lblA.Visible = true;
    //    //   // txt_Aadhaar_Number.Visible = false;
    //    //}
    //    //else
    //    //{
    //    //    txt_Aadhaar_Number.Enabled = true;
    //    //    lblA.Visible = false;
    //    //    txt_Aadhaar_Number.Visible = true;
    //    //}
    //    //if (txt_Aadhaar_Number.Text.Length==12)
    //    //{
    //    //  //txt_Aadhaar_Number.Text = "785";
    //    //string aadhar = txt_Aadhaar_Number.Text;
    //    //string EncAdh = EncryptDecrypt.EncryptString(aadhar);
    //    //txt_Aadhaar_Number.Text = EncAdh;
    //    //Session["EEA"] = EncAdh;
    //    //}
    //    btn_confirm.Enabled = true;
    //}
   
    protected void cmdShow_Click(object sender, EventArgs e)
    {
        if (ddlExistingCourse.SelectedValue.ToString () == "0")
        {
            lbl_error.Text = "Select Course";
            return;

        }
        if (txtApplicationNo.Text.Length == 0)
        {
            lbl_error.Text = "Enter Registration No.";
            return;

        }
        Int32 vRegistrationNo = Convert.ToInt32(txtApplicationNo .Text );
        Int32 vCourseCode = Convert.ToInt32(ddlExistingCourse.SelectedValue);

        TYP_ScreenersApplicantDetails sApplicantDetails = new TYP_ScreenersApplicantDetails();
        DataTable dt = sApplicantDetails.getExistingApplicantData(vRegistrationNo, vCourseCode );
        if (dt.Rows .Count ==0)
        {
            lblmsg.Text = "No such registration no. exists for the selected course";
            lblmsg.Visible = true;
            btn_confirm.Enabled = false;
            ddl_Course.Enabled = false;

            return;

        }
        else
        {
           
            lblmsg.Text = "";
            lblmsg.Visible = true;
            ddl_Course.Enabled = true;
           // ddl_OrgCode.SelectedValue = dt.Rows[0]["Nominating_OrgCode"].ToString();
            txt_candidate_name.Text = dt.Rows[0]["Candidate_Name"].ToString();
            txt_organisation_name_of_candidate.Text = ddl_OrgCode.SelectedItem.Text;
            var empId = dt.Rows[0]["Employee_Id"];
            txt_EmpID .Text = dt.Rows[0]["Employee_Id"].ToString();
            ddl_Designation .SelectedValue = dt.Rows[0]["Designation_Code"].ToString();
            drpStates.SelectedItem .Text  = dt.Rows[0]["PostedAtStatename"].ToString();
            drpCity .SelectedItem.Text = dt.Rows[0]["PostedAtCityName"].ToString();

            //Txt_EbasId .Text = dt.Rows[0]["EbasId"].ToString().PadLeft (Convert.ToInt32(dt.Rows[0]["ebasidlen"]),'0');
            //Changed_12_06_2024

            if (dt.Rows[0]["EbasId"] != DBNull.Value)
            {
                Txt_EbasId.Text = dt.Rows[0]["EbasId"].ToString().PadLeft(Convert.ToInt32(dt.Rows[0]["ebasidlen"]), '0');
            }
            else
            {
                //Txt_EbasId.Text = "0";
                Txt_EbasId.Enabled = true;
            }

            //Changed_12_06_2024

            Txt_BcasId.Text= dt.Rows[0]["BcasId"].ToString();
            txt_Aadhaar_Number .Text= dt.Rows[0]["Aadhaar_Number"].ToString(); ;
            ddl_gender_code .SelectedValue = dt.Rows[0]["Sex_Code"].ToString(); 
            txt_date_of_birth .Text = dt.Rows[0]["DOB"].ToString();
            ddl_Nationality .SelectedValue = dt.Rows[0]["Nationality_Code"].ToString();
            txt_mobile_no .Text = dt.Rows[0]["Mobile_No"].ToString();
            txt_email_id .Text = dt.Rows[0]["Email_Id"].ToString();
            ddl_Qualification.SelectedValue = dt.Rows[0]["Educational_Qualification_Code"].ToString();

            ddl_OrgCode.Enabled = false;
            txt_candidate_name.Enabled = false;
           // txt_organisation_name_of_candidate.Enabled = true;
            txt_EmpID.Enabled = false;
            ddl_Designation.Enabled = true;
            drpStates.Enabled = true; 
             drpCity.Enabled = true;
            Txt_EbasId.Enabled = true;
            Txt_BcasId.Enabled = true;
            txt_Aadhaar_Number.Enabled = false;
            ddl_gender_code.Enabled = false;
            txt_date_of_birth.Enabled = false;
            ddl_Nationality.Enabled = false;
            txt_mobile_no.Enabled = true;
            txt_email_id.Enabled = true;
            ddl_Qualification.Enabled = true;

            btn_confirm.Enabled = true;
        }


    }
    protected void drpStates_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (drpStates.SelectedValue != "0")
        {
            drpCity.Items.Clear();
            drpCity.Items.Insert(0, new ListItem("--Select--", "0"));
            Int64 StateID = Convert.ToInt64(drpStates.SelectedValue);
            string constr = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "Select ID , Name  FROM Location where Parent_ID=@StateID order by name";

                    cmd.Parameters.AddWithValue("@StateID", StateID);
                    cmd.Connection = con;
                    con.Open();
                    SqlDataAdapter adpt = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adpt.Fill(dt);
                    drpCity.DataSource = dt;
                   // drpCity.DataBind();
                    drpCity.DataTextField = "Name";
                    drpCity.DataValueField = "ID";
                    drpCity.DataBind(); 
                    con.Close();
                }
            }
        }
    }
    protected void txt_EmpID_TextChanged(object sender, EventArgs e)
    {
        Txt_BcasId.Text = txt_EmpID.Text;
    }
   
    protected void lnkRegister_Click(object sender, EventArgs e)
    {
        if (rdOld.SelectedValue.ToString() == "0")
        {
            Session["old"] = "Y";
            Response.Redirect("ApplicantDetails.aspx");
        }
        else
        {
            Session["old"] = "";
        }
    }
    //Added_10_06_2024
    protected void btnViewCandidateDetailsScreenersFrmEBCASNo_Click(object sender, EventArgs e)
    {
        try
        {
            TYP_ApplicantDetails obj = new TYP_ApplicantDetails();
            string encasNo = Txt_EbasId.Text.ToString();
            DataTable dt = obj.getCandidateDetailsFrmEBCASNo(encasNo);
            if (dt.Rows.Count > 0)
            {
                // object columnValue = dataTable.Rows[0]["ColumnName"];
                txt_candidate_name.Text = dt.Rows[0]["name"].ToString();
                txt_date_of_birth.Text = dt.Rows[0]["Dateofbirth"].ToString();
                btn_confirm.Visible = true;
                //txt_candidate_name.Text = dataTable.Rows[0]["Dateofbirth"].ToString();
            }
            else
            {
                txt_candidate_name.Text = "";
                txt_date_of_birth.Text = "";
                btn_confirm.Visible = false;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('No Candidate Details available With this EBCASNo');", true);

            }
        }
        catch
        {
            btn_confirm.Visible = false;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Exception", "alert('Exception Occured : Please Enter EBCAS No');", true);
        }
    }

    //Added_10_06_2024
}
