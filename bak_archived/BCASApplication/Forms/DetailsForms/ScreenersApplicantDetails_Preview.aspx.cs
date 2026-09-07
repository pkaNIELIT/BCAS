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
using System.IO;
using System.Text.RegularExpressions;
using BCASClassLibrary;


public partial class Forms_DetailsForms_ScreenersApplicantDetails_Preview : System.Web.UI.Page
{
    TYP_ScreenersApplicantDetails objApplicant = new TYP_ScreenersApplicantDetails();

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

               if (!PreviousPage.IsValid)
               {
                   string msg="";
                   
                   foreach (IValidator ovalidator in PreviousPage.Validators )
                   {
                       if(!ovalidator .IsValid )
                           msg=msg+ovalidator .ErrorMessage ;
                   }

        


                    Response.Redirect("~/Forms/DetailsForms/ScreenersApplicantDetails.aspx");
                }
                else
                {
                    if (PreviousPage != null && PreviousPage.IsCrossPagePostBack)
                    {
                        if(PreviousPage .pbddl_Course .SelectedItem .Text =="-Select Course--")
                            Response.Redirect("~/Forms/DetailsForms/ScreenersApplicantDetails.aspx");



                        Session["EEA"] = "";
                        TYP_ScreenersApplicantDetails objScreener = new TYP_ScreenersApplicantDetails();

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
                        ddl_Course.SelectedValue = PreviousPage.pbddl_Course.SelectedValue;
                        ddlBillingOrganisation.DataBind();
                        ddlBillingOrganisation.SelectedValue = PreviousPage.pbddl_billerOrgCode.SelectedValue.ToString();
                        ddl_OrgCode.DataBind();
                        ddl_OrgCode.SelectedValue = PreviousPage.pbddl_OrgCode.SelectedValue.ToString();
                        lbl_prevEmpID.Text = PreviousPage.pbTxt_EmpID.Text.ToUpper();
                        ddl_Designation.DataBind();
                        ddl_Designation.SelectedValue = PreviousPage.pbddl_Designation.SelectedValue.ToString();
                        lbl_prevCandidateName.Text = PreviousPage.pbTxt_candidate_name.Text.ToUpper();
                        //deep
                        drpStates.DataBind();
                        drpStates.SelectedItem.Text = PreviousPage.pbddl_drpStates.SelectedItem.Text;
                        drpCity.DataBind();
                        drpCity.SelectedItem.Text = PreviousPage.pbddl_drpCity.SelectedItem.Text;

                        lbl_prevEbasId.Text = PreviousPage.pbTxt_EbasId.Text.ToUpper();
                        lbl_prevBcasId.Text = PreviousPage.pbTxt_BcasId.Text.ToUpper();

                        lbl_prevOrganisationNameofCandidate.Text = PreviousPage.pbTxt_organisation_name_of_candidate.Text.ToUpper();
                        string test1 = "";
                        if (PreviousPage.pbTxt_Aadhaar_No.Value != "")
                        {
                            lbl_prevAadhaarNumber.Text = EncryptDecrypt.DecryptString(PreviousPage.pbTxt_Aadhaar_No.Value.ToString()); //PreviousPage.pbTxt_Aadhaar_Number.Text;

                            //lbl_prevAadhaarNumber.Text = EncryptDecrypt.DecryptString(PreviousPage.pbTxt_Aadhaar_Number.Text); //PreviousPage.pbTxt_Aadhaar_Number.Text;

                            //string test = EncryptDecrypt.DecryptString(PreviousPage.pbTxt_Aadhaar_Number.Text);
                             test1 = EncryptDecrypt.DecryptString(PreviousPage.pbTxt_Aadhaar_No.Value.ToString());
                        }
                        else 
                            lbl_prevAadhaarNumber.Text = "";

                        //deep
                        ddl_gender_code.DataBind();
                        ddl_gender_code.SelectedValue = PreviousPage.pbDdl_gender_code.SelectedValue.ToString().ToUpper();
                        lbl_prevDOB.Text =Convert.ToDateTime ( PreviousPage.pbTxt_date_of_birth.Text).ToShortDateString();
                        ddl_Nationality.DataBind();
                        ddl_Nationality.SelectedValue = PreviousPage.pbddl_Nationality.SelectedValue.ToString();
                        lbl_prevMobileNo.Text = PreviousPage.pbTxt_mobile_no.Text;
                        lbl_prevEmail.Text = PreviousPage.pbTxt_email_id.Text.ToUpper();
                        ddl_Qualification.DataBind();
                        ddl_Qualification.SelectedValue = PreviousPage.pbddl_Qualification.SelectedValue.ToString();
                        lblRegnNo.Text = PreviousPage.pbTxt_ApplicationNo.Text;
                        chk_declaration.Checked = PreviousPage.pbChk_declaration.Checked;

                        //************Check for Duplicate Registration Added on: 2013-09-30
                        TYP_ScreenersApplicantDetails obj = new TYP_ScreenersApplicantDetails();
                        
                        lbl_prevCandidateName.Text = Regex.Replace(lbl_prevCandidateName.Text, @"\s+", " ");
                        lbl_prevCandidateName.Text = lbl_prevCandidateName.Text.Trim();

                        obj.v_Candidate_Name = lbl_prevCandidateName.Text;
                        obj.d_DOB = Convert.ToDateTime(lbl_prevDOB.Text);
                        obj.v_Enter_by = Session["user_id"].ToString();
                        obj.v_EbasId = lbl_prevEbasId.Text;
                        obj.b_EbasId = true;
                        using (DataTable dt_exist = obj.CheckDuplicateRegdScreeners())
                        {
                            if (Convert.ToInt32(dt_exist.Rows[0][1].ToString()) != 0)
                            {
                                //lbl_error.Text = "Already Exists with";
                                //WebMsgApp.WebMsgBox.Show("Record with same Name and Date of Birth already exists having Registration No:= " + dt_exist.Rows[0][2].ToString ()+ " Kindly verify it before submitting.");     
                                WebMsgApp.WebMsgBox.Show("Record with same Name and Date of Birth already exists, Kindly verify it before submitting.");     
                          
                            }

                        }
                        //int Success = Convert.ToInt32(obj.validateEBCASNo());

                        //if (Success == 0)
                        //{
                        //    //lbl_error.Text = "Already Exists with";
                        //    //WebMsgApp.WebMsgBox.Show("Record with same Name and Date of Birth already exists having Registration No:= " + dt_exist.Rows[0][2].ToString ()+ " Kindly verify it before submitting.");     
                        //    WebMsgApp.WebMsgBox.Show("Record with EBCAS ID, Name and Date of Birth Doesnot exist, Kindly verify from BCAS portal.");
                        //    btn_confirm.Visible = false;
                        //}
                        //************

                    }
                    else
                    {
                        Response.Redirect("~/Forms/DetailForms/ScreenersApplicantDetails.aspx");
                    }

                }
            }
        }

    }

    protected void chk_declaration_CheckedChanged(object sender, EventArgs e)
    {

    }
    protected void btn_correction_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Forms/DetailsForms/ScreenersApplicantDetails.aspx");
    }
    protected void btn_confirm_Click(object sender, EventArgs e)
    {
        Page.Validate();
        if (Page.IsValid)
        {
            objApplicant.i_course_code = Convert.ToInt32(ddl_Course.SelectedValue);           
            objApplicant.i_Organisation_Code = Convert.ToInt32(ddl_OrgCode.SelectedValue.ToString ());
            objApplicant.i_bOrganisation_Code = Convert.ToInt32(ddlBillingOrganisation.SelectedValue.ToString());
            objApplicant.v_Employee_ID = lbl_prevEmpID.Text;
            objApplicant.i_Designation_Code = Convert.ToInt32(ddl_Designation.SelectedValue.ToString ());
            objApplicant.v_Candidate_Name = lbl_prevCandidateName.Text;
            //deep
            objApplicant.v_OrganisationNameOf_Candidate = lbl_prevOrganisationNameofCandidate.Text; //lbl_prevEbasId.Text
            objApplicant.v_Aadhaar_Number = lbl_prevAadhaarNumber.Text;

            objApplicant.v_PostedAtStateId = drpStates.SelectedItem.Text;
            objApplicant.v_PostedAtCityId = drpCity.SelectedItem.Text;

            objApplicant.v_EbasId = lbl_prevEbasId.Text;
            objApplicant.v_BcasId = lbl_prevBcasId.Text;
           //deep
            objApplicant.c_Sex_Code = ddl_gender_code.SelectedValue.ToString();
            objApplicant.d_DOB = Convert.ToDateTime(lbl_prevDOB.Text);
            objApplicant.i_Nationality_Code = Convert.ToInt32(ddl_Nationality .SelectedValue.ToString());
            objApplicant.v_Mobile_No = lbl_prevMobileNo.Text;
            objApplicant.v_Email_Id = lbl_prevEmail.Text;
            objApplicant.i_Registration_Number = Convert.ToInt32(lblRegnNo.Text);
            objApplicant.i_Educational_Qualification_Code = Convert.ToInt32(ddl_Qualification.SelectedValue.ToString());
            objApplicant.v_Enter_by = Session["user_id"].ToString();

            
            ////*******Check from the database whether this record is exists or not*** 27.09.2013
            //using (DataTable dt_exist = objApplicant.CheckDuplicateRegd ())
            //{
            //    if (Convert.ToInt32(dt_exist.Rows[0][0].ToString()) != 0)
            //    {
            //        //lbl_error.Text = "Already Exists with";
            //       //btn_confirm_ConfirmButtonExtender.
            //    }
            //    else
            //    {
            //        using (DataTable dt = objApplicant.InsertCandidateDetails())
            //        {
            //            if (Convert.ToInt32(dt.Rows[0][0].ToString()) != 0)
            //            {
            //                lbl_error.Text = "Your registeration failed.";
            //            }
            //            else
            //            {
            //                lbl_error.Text = "You have Registered Sucessfully. Your Training Registration No is: " + dt.Rows[0][1].ToString();

            //                Session["lbl_Apl_no"] = dt.Rows[0][1].ToString();


            //                //code for sending email to candidate*******************
            //                //string detail = "Dear Candidate " + lbl_candidate_name2.Text + "<br><br>" + "You have successfully submitted  your application for Assistannt Software/Netwrok Engineer for CRIS" + "<br><br>" + "Your Application Number is " + dt.Rows[0][1].ToString() + "<br><br>" + " Please note down Application No. given above, which will be required for further interaction with CRIS " + " <br><br>" + "Application will be treated as incomplete/rejected unless accompanied by Payment of Application Fee.";
            //                //MyLibrary.SendMail(lbl_email_id2.Text, "Submission of Online form for CRIS ", detail + "<br><br> Thanks <br>CRIS <br><br> This is a system generated mail. Please do not reply to this email ID.");
            //                //    WebMsgApp.WebMsgBox.Show("Your Application Details have been sent at your email address ..........");
            //                ///********************************
            //                Response.Redirect("~/Forms/DetailsForms/frmPhotoSignUpload.aspx");

            //            }

            //        }
 
            //    }

            //}
            ////********Till Here

            using (DataTable dt = objApplicant.InsertCandidateDetailsScreeners())
            {
                string x = dt.Rows[0][0].ToString();
                if (Convert.ToInt32(dt.Rows[0][0].ToString()) !=0)
                {
                    lbl_error.Text = "Your registration failed.";
                }
                else
                {
                    lbl_error.Text = "You have Registered Sucessfully. Your Training Registration No is: " + dt.Rows[0][1].ToString();

                    Session["lbl_Apl_no_Screeners"] = dt.Rows[0][1].ToString();
                    Session["Screeners_course"] = Convert.ToInt32(ddl_Course.SelectedValue);

                    //code for sending email to candidate*******************
                    //string detail = "Dear Candidate " + lbl_candidate_name2.Text + "<br><br>" + "You have successfully submitted  your application for Assistannt Software/Netwrok Engineer for CRIS" + "<br><br>" + "Your Application Number is " + dt.Rows[0][1].ToString() + "<br><br>" + " Please note down Application No. given above, which will be required for further interaction with CRIS " + " <br><br>" + "Application will be treated as incomplete/rejected unless accompanied by Payment of Application Fee.";
                    //MyLibrary.SendMail(lbl_email_id2.Text, "Submission of Online form for CRIS ", detail + "<br><br> Thanks <br>CRIS <br><br> This is a system generated mail. Please do not reply to this email ID.");
                    //    WebMsgApp.WebMsgBox.Show("Your Application Details have been sent at your email address ..........");
                    ///********************************
                    Response.Redirect("~/Forms/DetailsForms/frmPhotoSignUploadScreeners.aspx");

                }

            }

 
        }
    }
}
