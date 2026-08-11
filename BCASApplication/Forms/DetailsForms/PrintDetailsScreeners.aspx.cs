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
using BCASClassLibrary;

public partial class Forms_DetailsForms_PrintDetailsScreeners : System.Web.UI.Page
{

    TYP_ScreenersApplicantDetails objApplicant = new TYP_ScreenersApplicantDetails();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Convert.ToString(HttpContext.Current.Session["UserId"]).Trim().Length == 0))
        {
            HttpContext.Current.Response.Redirect("~/Home.aspx");
        }
        else
        {
            //*******Update Flag for Completion of application....14.10.2013

            objApplicant.i_Organisation_Code =Convert.ToInt32(Session["UserOrg"]);
            objApplicant.b_Organisation_Code = true;
            objApplicant.i_Registration_Number = Convert.ToInt32(Session["lbl_Apl_no_Screeners"].ToString());
            objApplicant.i_course_code = Convert.ToInt32(Session["Screeners_course"].ToString());
            objApplicant.c_Is_Application_Complete = "Y";


            using (DataTable dt_new = objApplicant.Update_ApplicationStatus ())
            {
                if (Convert.ToInt32(dt_new.Rows[0][0]) != 0)
                {
                    lbl_ErrorMsg.Text = "Error Occured During Application Complete";
                }
               

            }


            //*******Till Here


            //DisableForwordButton();
            //**********
            HttpContext.Current.Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
            HttpContext.Current.Response.Cache.SetValidUntilExpires(false);
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            HttpContext.Current.Response.Cache.SetNoStore();
            //*******

            //Session["lbl_Apl_no_Screeners"] = 1000012;
            Img_Photo.Controls.Clear();

            ImgSign.Controls.Clear();
            
            if (Convert.ToString(HttpContext.Current.Session["lbl_Apl_no_Screeners"]).Trim().Length != 0)
            {

                //**********      

                //objApplicant.i_Registration_Number = Convert.ToInt32(Session["lbl_Apl_no_Screeners"].ToString());


                using (DataTable dt = objApplicant.getCandidateDetails_PrintApplicationScreeners(Convert.ToInt32(Session["lbl_Apl_no_Screeners"].ToString()), Convert.ToInt32(Session["UserOrg"]), Convert.ToInt32(Session["Screeners_course"].ToString())))
                {
                    if (dt.Rows.Count != 0)
                    {
                        lbl_Apl_no.Text = dt.Rows[0]["Registration_Number"].ToString();
                        lbl_Name.Text = dt.Rows[0]["Candidate_Name"].ToString();
                        lbl_billerOrganisation.Text = dt.Rows[0]["Payee_Organisation_Name"].ToString();
                        //deep
                        lbl_OrganisationName.Text = dt.Rows[0]["OrganisationNameOf_Candidate"].ToString();
                        //deep
                        lblCourse.Text = dt.Rows[0]["Course_Name"].ToString();
                        lbl_Sex.Text = dt.Rows[0]["Sex_Code"].ToString();
                        lbl_valDesignation.Text = dt.Rows[0]["Designation"].ToString();
                        lbl_dob.Text = dt.Rows[0]["DOB"].ToString();
                        lbl_Qualification.Text = dt.Rows[0]["Qualification"].ToString();
                        lbl_Mobile.Text = dt.Rows[0]["Mobile_No"].ToString();
                        lbl_Email.Text = dt.Rows[0]["Email_Id"].ToString();
                        lbl_Country.Text = dt.Rows[0]["Country_Name"].ToString();
                        lbl_valOrg.Text = dt.Rows[0]["Organisation_Name"].ToString();
                        //******** 
                        if (dt.Rows[0]["Photo_Filename"].ToString() == "*")
                        {
                            Img_Photo.Visible = false;

                        }
                        else
                        {
                            Img_Photo.Visible = true;
                            Img_Photo.ImageUrl = "~//Upload//Screeners//Photograph//" + dt.Rows[0]["Photo_Filename"].ToString();
                            Img_Photo.DataBind();
                        }

                        if (dt.Rows[0]["Signature_Filename"].ToString() == "*")
                        {
                            ImgSign.Visible = false;

                        }
                        else
                        {
                            ImgSign.Visible = true;
                            ImgSign.ImageUrl = "~//Upload//Screeners//Signature//" + dt.Rows[0]["Signature_Filename"].ToString();
                            ImgSign.DataBind();
                        }
                       

                    }

                }
                //###########            

            }
            else
            {
                Response.Redirect("~/Forms/DetailsForms/ScreenersApplicantDetails.aspx");

            }

        }

    }
}
