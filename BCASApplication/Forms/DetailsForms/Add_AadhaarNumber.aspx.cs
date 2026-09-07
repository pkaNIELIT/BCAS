using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using BCASClassLibrary;
using System.Collections.Generic;
using System.Text;




public partial class Forms_DetailsForms_Add_AadhaarNumber : System.Web.UI.Page
{

    TYP_ApplicantDetails objApplicant = new TYP_ApplicantDetails();

    protected void Page_Load(object sender, EventArgs e)
    {

        if ((Convert.ToString(HttpContext.Current.Session["UserId"]).Trim().Length == 0))
        {
            HttpContext.Current.Response.Redirect("~/Home.aspx");
        }
            
    }

    protected void LoginButton_Click(object sender, EventArgs e)
    {
        int UserOrgCode;
        UserOrgCode = Convert.ToInt32(Session["UserOrg"]);
        if (Convert.ToInt32(txtRegNo.ToString().Length) != 0)
        {
            using (DataTable dt2 = objApplicant.CheckAadhaarNumberExists(Convert.ToInt32(txtRegNo.Text), Convert.ToInt32(Session["UserOrg"])))
            {
                if (dt2.Rows.Count == 0 && UserOrgCode!=0)
                {
                    Panel1.Visible = false;
                    Panel2.Visible = false;                    
                    objApplicant.i_Registration_Number = Convert.ToInt32(txtRegNo.Text);

                    using (DataTable dt = objApplicant.getCandidateDetails_forAddAadhaarNumber(Convert.ToInt32(txtRegNo.Text), Convert.ToInt32(Session["UserOrg"])))
                    {
                        if (dt.Rows.Count != 0)
                        {
                            Panel1.Visible = false;
                            Panel2.Visible = true;
                            txt_organisation_Name2.Text = dt.Rows[0]["Organisation_Name"].ToString();
                            txt_employee_id2.Text = dt.Rows[0]["Employee_ID"].ToString();
                            txt_candidate_name2.Text = dt.Rows[0]["Candidate_Name"].ToString();
                            txt_sex2.Text = dt.Rows[0]["Sex_Code"].ToString();
                            txt_designation2.Text = dt.Rows[0]["Designation"].ToString();
                            txt_dob2.Text = dt.Rows[0]["DOB"].ToString();
                            txt_email_id2.Text = dt.Rows[0]["Email_Id"].ToString();
                            txt_mobile_no2.Text = dt.Rows[0]["Mobile_No"].ToString();
                            txt_nationality2.Text = dt.Rows[0]["Country_Name"].ToString();
                            txt_educational_qualification2.Text = dt.Rows[0]["Qualification"].ToString();
                            txt_registration_number2.Text = dt.Rows[0]["Registration_Number"].ToString();
                            txt_AadhaarNumber2.Text = dt.Rows[0]["Aadhaar_Number"].ToString();
                            lblError.Visible = false;
                        }
                        else
                        {
                            lblError.Text = "No Record Found";
                            lblError.Visible = true;
                            Panel1.Visible = false;
                            Panel2.Visible = false;
                        }
                    }
                }
                else
                {


                    lblmessage.Text = "";
                    objApplicant.i_Registration_Number = Convert.ToInt32(txtRegNo.Text);

                    using (DataTable dt = objApplicant.getCandidateDetails_forAddAadhaarNumber(Convert.ToInt32(txtRegNo.Text), Convert.ToInt32(Session["UserOrg"])))
                    {
                        if (dt.Rows.Count != 0)
                        {
                            Panel2.Visible = false;
                            Panel1.Visible = true;
                            txt_organisation_Name.Text = dt.Rows[0]["Organisation_Name"].ToString();
                            txt_employee_id.Text = dt.Rows[0]["Employee_ID"].ToString();
                            txt_candidate_name.Text = dt.Rows[0]["Candidate_Name"].ToString();
                            txt_sex.Text = dt.Rows[0]["Sex_Code"].ToString();
                            txt_designation.Text = dt.Rows[0]["Designation"].ToString();
                            txt_dob.Text = dt.Rows[0]["DOB"].ToString();
                            txt_email_id.Text = dt.Rows[0]["Email_Id"].ToString();
                            txt_mobile_no.Text = dt.Rows[0]["Mobile_No"].ToString();
                            txt_nationality.Text = dt.Rows[0]["Country_Name"].ToString();
                            txt_educational_qualification.Text = dt.Rows[0]["Qualification"].ToString();
                            txt_registration_number.Text = dt.Rows[0]["Registration_Number"].ToString();
                            txt_AadhaarNumber.Text = dt.Rows[0]["Aadhaar_Number"].ToString();
                            lblError.Visible = false;                           
                        }
                  
                    else
                        {
                            lblError.Text = "No Record Found";
                            lblError.Visible = true;
                            Panel1.Visible = false;
                            Panel2.Visible = false;
                        }


                    }
                }
            }
                

           
        }
        }
    protected void btn_Update_AadhaarNumber_Click(object sender, EventArgs e)
    {
        int UserOrgCode;
            UserOrgCode = Convert.ToInt32(Session["UserOrg"]);
        if (UserOrgCode !=0)
        {
            CheckAadhaarNumberExists();
        }
        else 
        {
           Update_AadhaarNumber();
        }        
    }
    protected void Update_AadhaarNumber()
    {
        if (txt_AadhaarNumber.Text != "")
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Update_AadhaarNumber";
            cmd.Parameters.Add("@Organisation_Code", SqlDbType.Int).Value = Convert.ToInt32(Session["UserOrg"]);
            cmd.Parameters.Add("@Registration_Number", SqlDbType.Int).Value = txt_registration_number.Text;
            cmd.Parameters.Add("@AadhaarNumber", SqlDbType.VarChar).Value = txt_AadhaarNumber.Text;
            cmd.Connection = con;
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                lblmessage.ForeColor = System.Drawing.Color.Green;
                lblmessage.Font.Bold = true;
                lblmessage.Text = "Aadhaar Number updated successfully.";
            }


            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
        }
        else
        {
            lblmessage.ForeColor = System.Drawing.Color.Red;
            lblmessage.Font.Bold = true;
            lblmessage.Text = "Please enter Aadhaar Number.";
        }
    }
    protected void CheckAadhaarNumberExists()
    {       
            lblmessage.Text = "";
            objApplicant.i_Registration_Number = Convert.ToInt32(txtRegNo.Text);

            using (DataTable dt = objApplicant.CheckAadhaarNumberExists(Convert.ToInt32(txtRegNo.Text), Convert.ToInt32(Session["UserOrg"])))
            {
                if (dt.Rows.Count != 0)
                {
                   Update_AadhaarNumber();
                   
                }
                
            }          
        }  
        
}
