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
using System.IO;


public partial class Forms_DetailsForms_frmPhotoSignUpload : System.Web.UI.Page
{
    TYP_ApplicantDetails objApplicant = new TYP_ApplicantDetails();

    protected void Page_Load(object sender, EventArgs e)
    {
        HttpContext.Current.Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
        HttpContext.Current.Response.Cache.SetValidUntilExpires(false);
        HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        HttpContext.Current.Response.Cache.SetNoStore();

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

            //Session["lbl_Apl_no"] = 1000012;
            Img_Photo.Controls.Clear();

            ImgSign.Controls.Clear();
            Img_PhotoICard.Controls.Clear();

            if (Convert.ToString(HttpContext.Current.Session["lbl_Apl_no"]).Trim().Length != 0)
            {
                
                //**********      

                objApplicant.i_Registration_Number = Convert.ToInt32(Session["lbl_Apl_no"].ToString());

                using (DataTable dt = objApplicant.getCandidateDetails_By_RegNo(Convert.ToInt32(Session["lbl_Apl_no"].ToString())))
                {
                    if (dt.Rows.Count != 0)
                    {
                        //********Disable Upload Controls
                        if (dt.Rows[0]["Is_Application_Complete"].ToString() == "Y")
                        {
                            R_Img.Visible = false;
                            R_Sign.Visible = false;
                            R_Icard.Visible = false;                           
                                                                                   
                        }
                        else
                        {
                            R_Img.Visible = true;
                            R_Sign.Visible = true;
                            R_Icard.Visible = true;                                            
                        }
                        //********

                        lbl_Apl_no.Text = dt.Rows[0]["Registration_Number"].ToString();
                        lbl_Name.Text = dt.Rows[0]["Candidate_Name"].ToString();
                        //deep
                        lbl_OrganisationName.Text = dt.Rows[0]["OrganisationNameOf_Candidate"].ToString();
                        //deep
                        lbl_valDesignation.Text = dt.Rows[0]["Designation"].ToString();
                        lbl_dob.Text = dt.Rows[0]["DOB"].ToString();
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

                        if (dt.Rows[0]["PhotoIcard_Filename"].ToString() == "*")
                        {
                            Img_PhotoICard.Visible = false;

                        }
                        else
                        {
                            Img_PhotoICard.Visible = true;
                            Img_PhotoICard.ImageUrl = "~//Upload//PhotoIDCard//" + dt.Rows[0]["PhotoIcard_Filename"].ToString();
                            Img_PhotoICard.DataBind();
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

    protected void btn_photo_upload_Click(object sender, EventArgs e)
    {
        Img_Photo.Controls.Clear();
        Img_Photo.DataBind();
        fupload_image.Controls.Clear();

        Int32 app_No;

        if ((Convert.ToString(HttpContext.Current.Session["lbl_Apl_no"]).Trim().Length > 0))
        {
            //*******
            //applicant.i_Application_no = Convert.ToInt32(Session["lbl_Apl_no"].ToString());

            using (DataTable dt_exist = objApplicant.getCandidateDetails_By_RegNo (Convert.ToInt32(Session["lbl_Apl_no"].ToString())))
            {
                if (Convert.ToInt32(dt_exist.Rows.Count) != 0)
                {
                    app_No = Convert.ToInt32(Session["lbl_Apl_no"].ToString());
                    string FileName, FileExtension;

                    string ftype;
                    ftype = "";
                    ftype = fupload_image.PostedFile.ContentType;

                    if (fupload_image.HasFile)
                    {

                        lbl_ErrorMsg.Text = "";
                        FileName = fupload_image.FileName;
                        FileExtension = System.IO.Path.GetExtension(fupload_image.FileName.ToLower ());

                        if (ftype == "image/jpeg" && checkRealFile(fupload_image, fupload_image))
                        {
                            if ((FileExtension == ".jpeg" || FileExtension == ".jpg"))
                            {
                                FileName = app_No + ".jpeg";
                                lbl_ErrorMsg.Text = "";

                                if (fupload_image.PostedFile.ContentLength > 0 && fupload_image.PostedFile.ContentLength <= 30000)
                                {
                                    lbl_ErrorMsg.Text = "";
                                    fupload_image.SaveAs(Server.MapPath("~//Upload//Photograph//") + FileName);

                                    //*********** Update in Database
                                    objApplicant.i_Registration_Number = Convert.ToInt32(Session["lbl_Apl_no"].ToString());
                                    objApplicant.v_Photo_Filename = FileName;
                                    objApplicant.i_option = 1;
                                    using (DataTable dt = objApplicant.UploadImages())
                                    {
                                        if (Convert.ToInt32(dt.Rows[0][0].ToString()) != 0)
                                        {
                                            lbl_ErrorMsg.Text = "Upload failed.";
                                        }
                                        else
                                        {
                                            Img_Photo.ImageUrl = "~//Upload//Photograph//" + dt.Rows[0]["Photo_Filename"].ToString();
                                            Img_Photo.DataBind();
                                            lbl_ErrorMsg.Text = "";
                                            lbl_fMsg.Text = "Upload Successful.";
                                            Response.AppendHeader("Refresh", "0.1");
                                            Img_Photo.Visible = true;

                                        }
                                    }

                                }
                                else
                                {

                                    lbl_ErrorMsg.Text = "File not uploaded. File size exceed.";
                                    fupload_image.Controls.Clear();
                                }
                            }
                            else
                            {
                                lbl_ErrorMsg.Text = "File not uploaded. File format is not accepted.";
                            }


                        }
                        else
                        {
                            lbl_ErrorMsg.Text = "File not uploaded. File format is not accepted.";
                        }

                        //****

                    }
                    else
                    {
                        lbl_ErrorMsg.Text = "Select Photo for Upload";
                    }
                    Img_Photo.DataBind();

                }
                else
                {
                    lbl_ErrorMsg.Text = "";
                    lbl_fMsg.Text = "Application No does not exist.";
                }
            }
            //*******
        }
        else
        {
            lbl_ErrorMsg.Text = "You are not authorized to access this page.";
        }


    }

    protected void btn_sign_upload_Click(object sender, EventArgs e)
    {
        lbl_SMsg.Visible = false;
        ImgSign.Controls.Clear();
        ImgSign.DataBind();
        fupload_signature.Controls.Clear();
        Int32 app_No;

        if ((Session["lbl_Apl_no"].ToString()).Length > 0)
        {

            //******
            objApplicant.i_Registration_Number = Convert.ToInt32(Session["lbl_Apl_no"].ToString());
            using (DataTable dt_exist = objApplicant.getCandidateDetails_By_RegNo(Convert.ToInt32(Session["lbl_Apl_no"].ToString())))
            {
                if (Convert.ToInt32(dt_exist.Rows.Count) != 0)
                {
                    app_No = Convert.ToInt32(Session["lbl_Apl_no"].ToString());
                    string FileName, FileExtension;

                    string ftype;
                    ftype = fupload_signature.PostedFile.ContentType;
                    if (fupload_signature.HasFile)
                    {
                        //******
                        //if (ftype == "image/jpeg" && checkRealFile(fupload_image ,fupload_image) )
                        if (ftype == "image/jpeg" && checkRealFile(fupload_signature, fupload_signature))
                        {
                            lbl_ErrorMsg.Text = "";
                            FileExtension = System.IO.Path.GetExtension(fupload_signature.FileName.ToLower ());
                            if ((FileExtension == ".jpeg" || FileExtension == ".jpg") && (ftype == "image/jpeg"))
                            {
                                lbl_ErrorMsg.Text = "";
                                FileName = app_No + ".jpeg";
                                if (fupload_signature.PostedFile.ContentLength > 0 && fupload_signature.PostedFile.ContentLength <= 30000)
                                {
                                    lbl_ErrorMsg.Text = "";
                                    fupload_signature.SaveAs(Server.MapPath("~//Upload//Signature//") + FileName);

                                    //****** Update in Database
                                    objApplicant.i_Registration_Number = Convert.ToInt32(Session["lbl_Apl_no"].ToString());
                                    objApplicant.v_Signature_Filename = FileName;
                                    objApplicant.i_option = 2;
                                    using (DataTable dt = objApplicant.UploadImages())
                                    {
                                        if (Convert.ToInt32(dt.Rows[0][0].ToString()) != 0)
                                        {
                                            lbl_ErrorMsg.Text = "Upload Failed.";
                                        }
                                        else
                                        {
                                            ImgSign.ImageUrl = "~//Upload//Signature//" + dt.Rows[0]["Signature_Filename"].ToString();
                                            ImgSign.DataBind();
                                            lbl_ErrorMsg.Text = "";
                                            lbl_SMsg.Text = "Upload Successful.";
                                            lbl_SMsg.Visible = true;
                                            Response.AppendHeader("Refresh", "0.1");
                                            ImgSign.Visible = true;
                                        }

                                    }

                                }
                                else
                                {

                                    lbl_ErrorMsg.Text = "File not uploaded. File size exceed.";
                                    fupload_signature.Controls.Clear();
                                }
                            }
                            else
                            {
                                lbl_ErrorMsg.Text = "File not uploaded. File format is not accepted.";
                            }

                        }
                        else
                        {
                            lbl_ErrorMsg.Text = "File not uploaded. File format is not accepted.";

                        }
                        //******                        


                    }
                    else
                    {
                        lbl_ErrorMsg.Text = "Select Signature for Upload";
                    }


                }
                else
                {
                    lbl_ErrorMsg.Text = "";
                    lbl_ErrorMsg.Text = "Application No does not exist.";
                }
            }
            //********         

        }   


    }

    protected void btn_save_continue_Click(object sender, EventArgs e)
    {
        if ((Session["lbl_Apl_no"].ToString()).Length > 0)
        {
            objApplicant.i_Registration_Number = Convert.ToInt32(Session["lbl_Apl_no"].ToString());
            using (DataTable dt = objApplicant.getCandidateDetails_By_RegNo(Convert.ToInt32(Session["lbl_Apl_no"].ToString())))
            {
                if (Convert.ToInt32(dt.Rows[0]["Registration_Number"].ToString()) == 0 || dt.Rows[0]["Photo_Filename"].ToString() == "*" || dt.Rows[0]["Photo_Filename"].ToString().Trim() == "" || dt.Rows[0]["Signature_Filename"].ToString() == "*" || dt.Rows[0]["Signature_Filename"].ToString().Trim() == "" || dt.Rows[0]["PhotoIcard_Filename"].ToString() == "*" || dt.Rows[0]["PhotoIcard_Filename"].ToString().Trim() == "")
                {
                    lbl_ErrorMsg.Text = "Either you are not registered or you have not uploaded photo or signature or Photo ID Card!!!";
                }
                else
                {
                    Response.Redirect("~/Forms/DetailsForms/Previous_Courses_attended.aspx");
                }

            }

        }

    }


    Boolean checkRealFile(FileUpload FileUpload1, FileUpload passfile)
    {
        byte[] imgfile;
        Stream fs = default(Stream);
        fs = passfile.PostedFile.InputStream;
        BinaryReader br1 = new BinaryReader(fs);
        imgfile = br1.ReadBytes(FileUpload1.PostedFile.ContentLength);

        //Image file Starting Bytes
        byte[] chkByte = { 255, 216, 255, 224 };

        //if you want check doc format Content the Use below suitable one for your requirement

        //doc files start like this value
        //byte[] chkByte = { 208, 207, 17, 224 };   //2003 MS word starting bytes (.doc format)
        // byte[] chkByte = {80,75,3,4,20};         //2007 MS word starting bytes (.docx format)

        int j = 0;

        //Check bytes are equal to real file bytes
        for (Int32 i = 0; i <= 2; i++)
        {
            if (imgfile[i] == chkByte[i])
            {
                j = j + 1;
                if (j == 3)
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
        return false;
    }

    protected void btn_Icard_upload_Click(object sender, EventArgs e)
    {

        Img_PhotoICard.Controls.Clear();
        Img_PhotoICard.DataBind();
        fupload_Icard.Controls.Clear();

        Int32 app_No;

        if ((Convert.ToString(HttpContext.Current.Session["lbl_Apl_no"]).Trim().Length > 0))
        {
            //*******
            //applicant.i_Application_no = Convert.ToInt32(Session["lbl_Apl_no"].ToString());

            using (DataTable dt_exist = objApplicant.getCandidateDetails_By_RegNo(Convert.ToInt32(Session["lbl_Apl_no"].ToString())))
            {
                if (Convert.ToInt32(dt_exist.Rows.Count) != 0)
                {
                    app_No = Convert.ToInt32(Session["lbl_Apl_no"].ToString());
                    string FileName, FileExtension;

                    string ftype;
                    ftype = "";
                    ftype = fupload_Icard.PostedFile.ContentType;

                    if (fupload_Icard.HasFile)
                    {

                        lbl_ErrorMsg.Text = "";
                        FileName = fupload_Icard.FileName;
                        FileExtension = System.IO.Path.GetExtension(fupload_Icard.FileName.ToLower ());

                        if (ftype == "image/jpeg" && checkRealFile(fupload_Icard, fupload_Icard))
                        {
                            if ((FileExtension == ".jpeg" || FileExtension == ".jpg"))
                            {
                                FileName = app_No + ".jpeg";
                                lbl_ErrorMsg.Text = "";

                                if (fupload_Icard.PostedFile.ContentLength > 0 && fupload_Icard.PostedFile.ContentLength <= 30000)
                                {
                                    lbl_ErrorMsg.Text = "";
                                    fupload_Icard.SaveAs(Server.MapPath("~//Upload//PhotoIDCard//") + FileName);

                                    //*********** Update in Database
                                    objApplicant.i_Registration_Number = Convert.ToInt32(Session["lbl_Apl_no"].ToString());                                    
                                    objApplicant.v_PhotoIcard_Filename = FileName;
                                    objApplicant.i_option = 3;
                                    using (DataTable dt = objApplicant.UploadImages())
                                    {
                                        if (Convert.ToInt32(dt.Rows[0][0].ToString()) != 0)
                                        {
                                            lbl_ErrorMsg.Text = "Upload failed.";
                                        }
                                        else
                                        {
                                            Img_PhotoICard.ImageUrl = "~//Upload//PhotoIDCard//" + dt.Rows[0]["PhotoIcard_Filename"].ToString();
                                            Img_PhotoICard.DataBind();
                                            lbl_ErrorMsg.Text = "";
                                            lbl_IcardMsg.Text = "Upload Successful.";
                                            Response.AppendHeader("Refresh", "0.1");
                                            Img_PhotoICard.Visible = true;

                                        }
                                    }

                                }
                                else
                                {

                                    lbl_ErrorMsg.Text = "File not uploaded. File size exceed.";
                                    fupload_Icard.Controls.Clear();
                                }
                            }
                            else
                            {
                                lbl_ErrorMsg.Text = "File not uploaded. File format is not accepted.";
                            }


                        }
                        else
                        {
                            lbl_ErrorMsg.Text = "File not uploaded. File format is not accepted.";
                        }

                        //****

                    }
                    else
                    {
                        lbl_ErrorMsg.Text = "Select Photo ID Card for Upload";
                    }
                    Img_PhotoICard.DataBind();

                }
                else
                {
                    lbl_ErrorMsg.Text = "";
                    lbl_IcardMsg.Text = "Application No does not exist.";
                }
            }
            //*******
        }
        else
        {
            lbl_ErrorMsg.Text = "You are not authorized to access this page.";
        }
    }


}
