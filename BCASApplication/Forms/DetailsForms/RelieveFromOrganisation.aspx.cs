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

public partial class Forms_DetailsForms_RelieveFromOrganisation : System.Web.UI.Page
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

            //DisableForwordButton();
            //**********
            HttpContext.Current.Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
            HttpContext.Current.Response.Cache.SetValidUntilExpires(false);
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            HttpContext.Current.Response.Cache.SetNoStore();
            //*******

            //Session["lbl_Apl_no"] = 10000001;
            //Session["UserOrg"] = 1;


            Img_Photo.Controls.Clear();

            ImgSign.Controls.Clear();

            if (Convert.ToString(HttpContext.Current.Session["lbl_Apl_no"]).Trim().Length != 0)
            {

                //**********      

                //objApplicant.i_Registration_Number = Convert.ToInt32(Session["lbl_Apl_no"].ToString());

                using (DataTable dt = objApplicant.getCandidateDetails_PrintApplication(Convert.ToInt32(Session["lbl_Apl_no"].ToString()), Convert.ToInt32(Session["UserOrg"])))
                {
                    if (dt.Rows.Count != 0)
                    {
                        txtRemarks.Focus();

                        lbl_Apl_no.Text = dt.Rows[0]["Registration_Number"].ToString();
                        lbl_Name.Text = dt.Rows[0]["Candidate_Name"].ToString();
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
                            Img_Photo.ImageUrl = "~//Upload//Photograph//" + dt.Rows[0]["Photo_Filename"].ToString();
                            Img_Photo.DataBind();
                        }

                        if (dt.Rows[0]["Signature_Filename"].ToString() == "*")
                        {
                            ImgSign.Visible = false;

                        }
                        else
                        {
                            ImgSign.Visible = true;
                            ImgSign.ImageUrl = "~//Upload//Signature//" + dt.Rows[0]["Signature_Filename"].ToString();
                            ImgSign.DataBind();
                        }


                    }

                }
                //###########            

            }
            else
            {
                Response.Redirect("~/Forms/DetailsForms/ApplicantDetails.aspx");

            }

        }

    }

    protected void btnRelieve_Click(object sender, EventArgs e)
    {
        objApplicant.i_Registration_Number = Convert.ToInt32(Session["lbl_Apl_no"].ToString());
        objApplicant.i_Organisation_Code = Convert.ToInt32(Session["UserOrg"]);
        objApplicant.c_Is_Relieved = "Y";
        objApplicant.v_Relieve_Remarks = txtRemarks.Text.Trim();
        objApplicant.v_Relieved_By = Session["user_id"].ToString();
        using (DataTable dt = objApplicant.RelieveFromOrganisation())
        {
            //if (Convert.ToInt32(dt.Rows[0][0].ToString()) != 0 && Convert.ToInt32(dt.Rows[0][1].ToString()) != 0)
            if(Convert.ToInt32(dt.Rows[0][0].ToString()) == -786)
            {
                lbl_ErrorMsg.Text = "";
                lbl_Success.Text = "Candidate Already Relieved";
                

            }
            else if (Convert.ToInt32(dt.Rows[0][0].ToString()) != 0 && Convert.ToInt32(dt.Rows[0][1].ToString()) != 0)
            {
               lbl_Success.Text = "";
               lbl_ErrorMsg.Text = "Relieving Failed!!!";
            }
            else
            {
                lbl_ErrorMsg.Text = "";
                lbl_Success.Text = "Candidate Relieved Successfully";

            }

        }

    }


}
