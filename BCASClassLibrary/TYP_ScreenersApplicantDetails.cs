using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using BCASClassLibrary;

namespace BCASClassLibrary
{
    public class TYP_ScreenersApplicantDetails:TYP_ScreenersApplicantDetailsAttrib 
    {
        public DataTable getScreenersCourse()
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            DataTable myDt = new DataTable();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetScreenersCourseName", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(myDt);
                    }
                }
            }
            return myDt;
        }

        public DataTable getExistingCourse()
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            DataTable myDt = new DataTable();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetExistingCourseName", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(myDt);
                    }
                }
            }
            return myDt;
        }
        public DataTable getExistingApplicantData(Int32 vRegistrationNo,Int32 vCourseCode)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            DataTable myDt = new DataTable();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("getExistingApplicantData", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@vCourse_Code", SqlDbType.Int);
                    cmd.Parameters["@vCourse_Code"].Value = vCourseCode ;
                    cmd.Parameters.Add("@vRegistration_No", SqlDbType.Int);
                    cmd.Parameters["@vRegistration_No"].Value = vRegistrationNo;
                    con.Open();
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(myDt);
                    }
                }
            }
            return myDt;
        }

        public DataTable InsertCandidateDetailsScreeners()
        {
            using (SqlCommand cmd = SqlHelper.GetSqlCommand("InsertCandidateDetailsScreeners"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter param = new SqlParameter();
                 
                param = cmd.Parameters.Add("@ExistingRegistrationNo", SqlDbType.Int); param.Direction = ParameterDirection.Input; if (base.b_Registration_Number ) param.Value = base.i_Registration_Number; else param.Value = DBNull.Value;
                param = cmd.Parameters.Add("@course_code", SqlDbType.Int); param.Direction = ParameterDirection.Input; if (base.b_course_code) param.Value = base.i_course_code; else param.Value = DBNull.Value;
                param = cmd.Parameters.Add("@i_Organisation_Code", SqlDbType.Int); param.Direction = ParameterDirection.Input; if (base.b_Organisation_Code) param.Value = base.i_Organisation_Code; else param.Value = DBNull.Value;
                param = cmd.Parameters.Add("@BillerOrganisation_Code", SqlDbType.Int); param.Direction = ParameterDirection.Input; if (base.b_bOrganisation_Code ) param.Value = base.i_bOrganisation_Code; else param.Value = DBNull.Value;
                param = cmd.Parameters.Add("@v_Employee_ID", SqlDbType.VarChar, 12); param.Direction = ParameterDirection.Input; if (base.b_Employee_ID) param.Value = base.v_Employee_ID; else param.Value = DBNull.Value;
                param = cmd.Parameters.Add("@v_Candidate_Name", SqlDbType.VarChar, 100); param.Direction = ParameterDirection.Input; if (base.b_Candidate_Name) param.Value = base.v_Candidate_Name; else param.Value = DBNull.Value;
                //deep
                param = cmd.Parameters.Add("@v_OrganisationNameOf_Candidate", SqlDbType.VarChar, 100); param.Direction = ParameterDirection.Input; if (base.b_OrganisationNameOf_Candidate) param.Value = base.v_OrganisationNameOf_Candidate; else param.Value = DBNull.Value;
                param = cmd.Parameters.Add("@v_Aadhaar_Number", SqlDbType.VarChar, 12); param.Direction = ParameterDirection.Input; if (base.b_Aadhaar_Number) param.Value = base.v_Aadhaar_Number; else param.Value = DBNull.Value;
                param = cmd.Parameters.Add("@v_PostedAtStateName", SqlDbType.VarChar, 100); param.Direction = ParameterDirection.Input; if (base.b_PostedAtStateId) param.Value = base.v_PostedAtStateId; else param.Value = DBNull.Value;
                param = cmd.Parameters.Add("@v_PostedAtCityName", SqlDbType.VarChar, 100); param.Direction = ParameterDirection.Input; if (base.b_PostedAtCityId) param.Value = base.v_PostedAtCityId; else param.Value = DBNull.Value;
                param = cmd.Parameters.Add("@v_EbasId", SqlDbType.VarChar, 100); param.Direction = ParameterDirection.Input; if (base.b_EbasId) param.Value = base.v_EbasId; else param.Value = DBNull.Value;
                param = cmd.Parameters.Add("@v_BcasId", SqlDbType.VarChar, 100); param.Direction = ParameterDirection.Input; if (base.b_EbasId) param.Value = base.v_EbasId; else param.Value = DBNull.Value;
      
                //deep
                param = cmd.Parameters.Add("@c_Sex_Code", SqlDbType.VarChar, 1); param.Direction = ParameterDirection.Input; if (base.b_Sex_Code) param.Value = base.c_Sex_Code; else param.Value = DBNull.Value;
                param = cmd.Parameters.Add("@i_Designation_Code", SqlDbType.Int); param.Direction = ParameterDirection.Input; if (base.b_Designation_Code) param.Value = base.i_Designation_Code; else param.Value = DBNull.Value;
                param = cmd.Parameters.Add("@d_DOB", SqlDbType.DateTime); param.Direction = ParameterDirection.Input; if (base.b_DOB) param.Value = base.d_DOB; else param.Value = DBNull.Value;
                param = cmd.Parameters.Add("@v_Email_Id", SqlDbType.VarChar, 50); param.Direction = ParameterDirection.Input; if (base.b_Email_Id) param.Value = base.v_Email_Id; else param.Value = DBNull.Value;
                param = cmd.Parameters.Add("@v_Mobile_No", SqlDbType.VarChar, 10); param.Direction = ParameterDirection.Input; if (base.b_Mobile_No) param.Value = base.v_Mobile_No; else param.Value = DBNull.Value;
                param = cmd.Parameters.Add("@i_Nationality_Code", SqlDbType.Int); param.Direction = ParameterDirection.Input; if (base.b_Nationality_Code) param.Value = base.i_Nationality_Code; else param.Value = DBNull.Value;
                param = cmd.Parameters.Add("@i_Educational_Qualification_Code", SqlDbType.Int); param.Direction = ParameterDirection.Input; if (base.b_Educational_Qualification_Code) param.Value = base.i_Educational_Qualification_Code; else param.Value = DBNull.Value;                
                param = cmd.Parameters.Add("@v_Enter_by", SqlDbType.VarChar, 50); param.Direction = ParameterDirection.Input; if (base.b_Enter_by) param.Value = base.v_Enter_by; else param.Value = DBNull.Value;
                //param = cmd.Parameters.Add("@d_Enter_Date", SqlDbType.DateTime); param.Direction = ParameterDirection.Input; if (base.b_Enter_Date) param.Value = base.d_Enter_Date; else param.Value = DBNull.Value;

                return SqlHelper.ExecuteProcedure(cmd);

            }


        }
        
        public DataTable getCandidateDetails_forScreenersCompleteStep2(int RegNo, int OrgCode,int courseCode)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            DataTable myDt = new DataTable();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetCandidateDetails__forScreenersCompleteStep2", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@i_Registration_Number", SqlDbType.Int));
                    cmd.Parameters["@i_Registration_Number"].Value = RegNo;

                    cmd.Parameters.Add(new SqlParameter("@OrgCode", SqlDbType.Int));
                    cmd.Parameters["@OrgCode"].Value = OrgCode;

                    cmd.Parameters.Add(new SqlParameter("@CourseCode", SqlDbType.Int));
                    cmd.Parameters["@CourseCode"].Value = courseCode ;

                    con.Open();
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(myDt);
                    }
                }
            }
            return myDt;
        }
        //deep create this method for aadhaar details updation in candidate registration table
        public DataTable getCandidateDetails_forAddAadhaarNumber(int RegNo, int OrgCode)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            DataTable myDt = new DataTable();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("getCandidateDetails_forAddAadhaarNumber", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@i_Registration_Number", SqlDbType.Int));
                    cmd.Parameters["@i_Registration_Number"].Value = RegNo;
                    cmd.Parameters.Add(new SqlParameter("@OrgCode", SqlDbType.Int));
                    cmd.Parameters["@OrgCode"].Value = OrgCode;                
                    con.Open();
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(myDt);
                    }
                }
            }
            return myDt;
        }
        public DataTable CheckAadhaarNumberExists(int RegNo, int OrgCode)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            DataTable myDt = new DataTable();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("CheckAadhaarNumberExists", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@i_Registration_Number", SqlDbType.Int));
                    cmd.Parameters["@i_Registration_Number"].Value = RegNo;

                    cmd.Parameters.Add(new SqlParameter("@OrgCode", SqlDbType.Int));
                    cmd.Parameters["@OrgCode"].Value = OrgCode;

                    con.Open();
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(myDt);
                    }
                }
            }
            return myDt;
        }

        
        //deep 
        
        public DataTable GetCandidateDetails_for_Relieving(int RegNo, int OrgCode)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            DataTable myDt = new DataTable();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetCandidateDetails_for_Relieving", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@i_Registration_Number", SqlDbType.Int));
                    cmd.Parameters["@i_Registration_Number"].Value = RegNo;

                    cmd.Parameters.Add(new SqlParameter("@OrgCode", SqlDbType.Int));
                    cmd.Parameters["@OrgCode"].Value = OrgCode;

                    con.Open();
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(myDt);
                    }
                }
            }
            return myDt;
        }
                
        public DataTable getCandidateDetailsScreeners_By_RegNo(int RegNo)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            DataTable myDt = new DataTable();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetCandidateDetailsScreeners_RegNo", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@i_Registration_Number", SqlDbType.Int));
                    cmd.Parameters["@i_Registration_Number"].Value = RegNo;
                   
                    con.Open();
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(myDt);
                    }
                }
            }
            return myDt;
        }

        public DataTable getCandidateDetailsScreeners_By_RegNoCourse(int RegNo,int courseCode)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            DataTable myDt = new DataTable();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetCandidateDetailsScreeners_RegNoCourse", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@i_Registration_Number", SqlDbType.Int));
                    cmd.Parameters["@i_Registration_Number"].Value = RegNo;

                    cmd.Parameters.Add(new SqlParameter("@vCourseCode", SqlDbType.Int));
                    cmd.Parameters["@vCourseCode"].Value = courseCode ;
                    
                    con.Open();
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(myDt);
                    }
                }
            }
            return myDt;
        }

        public DataTable UploadImages()
        {
            using (SqlCommand cmd = SqlHelper.GetSqlCommand("UploadPhotoSignScreeners"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter param = new SqlParameter();                
                param = cmd.Parameters.Add("@i_Registration_Number", SqlDbType.Int); param.Direction = ParameterDirection.Input; if (base.b_Registration_Number) param.Value = base.i_Registration_Number; else param.Value = DBNull.Value;
                param = cmd.Parameters.Add("@v_Photo_Filename", SqlDbType.VarChar, 50); param.Direction = ParameterDirection.Input; if (base.b_Photo_Filename) param.Value = base.v_Photo_Filename; else param.Value = DBNull.Value;
                param = cmd.Parameters.Add("@v_Signature_Filename", SqlDbType.VarChar, 50); param.Direction = ParameterDirection.Input; if (base.b_Signature_Filename) param.Value = base.v_Signature_Filename; else param.Value = DBNull.Value;

                param = cmd.Parameters.Add("@v_PhotoIcard_Filename", SqlDbType.VarChar, 50); param.Direction = ParameterDirection.Input; if (base.b_PhotoIcard_Filename) param.Value = base.v_PhotoIcard_Filename; else param.Value = DBNull.Value;                

                param = cmd.Parameters.Add("@i_option", SqlDbType.Int); param.Direction = ParameterDirection.Input; if (base.b_option) param.Value = base.i_option; else param.Value = DBNull.Value;
                return SqlHelper.ExecuteProcedure(cmd);
            }

        }

        public DataTable UpdateExistingImages()
        {
            using (SqlCommand cmd = SqlHelper.GetSqlCommand("UpdateExistingImagesScreeners"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter param = new SqlParameter();
                param = cmd.Parameters.Add("@i_Registration_Number", SqlDbType.Int); param.Direction = ParameterDirection.Input; if (base.b_Registration_Number) param.Value = base.i_Registration_Number; else param.Value = DBNull.Value;
               
                return SqlHelper.ExecuteProcedure(cmd);
            }

        }

        public DataTable getUserOrganisation(string UID)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            DataTable myDt = new DataTable();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetUserOrganisation", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@User_Name", SqlDbType.NVarChar));
                    cmd.Parameters["@User_Name"].Value = UID;
                    con.Open();
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(myDt);
                    }
                }
            }
            return myDt;
        }

        public DataTable getCandidateDetails_PrintApplicationScreeners(int RegNo, int OrgCode,int CourseCode)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            DataTable myDt = new DataTable();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetCandidateDetails_PrintApplicationScreeners", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@i_Registration_Number", SqlDbType.Int));
                    cmd.Parameters["@i_Registration_Number"].Value = RegNo;

                    cmd.Parameters.Add(new SqlParameter("@OrgCode", SqlDbType.Int));
                    cmd.Parameters["@OrgCode"].Value = OrgCode;

                    cmd.Parameters.Add(new SqlParameter("@courseCode", SqlDbType.Int));
                    cmd.Parameters["@courseCode"].Value = CourseCode ;

                    con.Open();
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(myDt);
                    }
                }
            }
            return myDt;
        }

        public DataTable CheckDuplicateRegdScreeners()
        {
            using (SqlCommand cmd = SqlHelper.GetSqlCommand("Check_Duplicate_Screeners"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter param = new SqlParameter();
                                
                param = cmd.Parameters.Add("@v_Candidate_Name", SqlDbType.VarChar, 100); param.Direction = ParameterDirection.Input; if (base.b_Candidate_Name) param.Value = base.v_Candidate_Name; else param.Value = DBNull.Value;                
                param = cmd.Parameters.Add("@d_DOB", SqlDbType.DateTime); param.Direction = ParameterDirection.Input; if (base.b_DOB) param.Value = base.d_DOB; else param.Value = DBNull.Value;
                param = cmd.Parameters.Add("@vCourseCode", SqlDbType.Int ); param.Direction = ParameterDirection.Input; if (base.b_course_code ) param.Value = base.b_course_code ; else param.Value = DBNull.Value;                
                param = cmd.Parameters.Add("@v_Enter_by", SqlDbType.VarChar, 50); param.Direction = ParameterDirection.Input; if (base.b_Enter_by) param.Value = base.v_Enter_by; else param.Value = DBNull.Value;
                
                return SqlHelper.ExecuteProcedure(cmd);

            }


        }
        public int validateEBCASNo()
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = SqlHelper.GetSqlCommand("validateEBCASNo"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter param = new SqlParameter();

                    param = cmd.Parameters.Add("@pEBCASID", SqlDbType.VarChar, 100); param.Direction = ParameterDirection.Input; if (base.b_EbasId) param.Value = base.v_EbasId; else param.Value = DBNull.Value;
                    param = cmd.Parameters.Add("@pName", SqlDbType.VarChar, 100); param.Direction = ParameterDirection.Input; if (base.b_Candidate_Name) param.Value = base.v_Candidate_Name; else param.Value = DBNull.Value;
                    param = cmd.Parameters.Add("@pDOB", SqlDbType.DateTime); param.Direction = ParameterDirection.Input; if (base.b_DOB) param.Value = base.d_DOB; else param.Value = DBNull.Value;
                    param = cmd.Parameters.Add("@pSuccess", SqlDbType.Int); param.Direction = ParameterDirection.Output;

                    con.Open();
                    cmd.Connection = con;

                    cmd.ExecuteNonQuery();

                    int pSuccess = Convert.ToInt32(cmd.Parameters["@pSuccess"].Value);
                    con.Close();
                    return (pSuccess);
                }
            }


        }

        public DataTable RelieveFromOrganisation()
        {
            using (SqlCommand cmd = SqlHelper.GetSqlCommand("Update_RelievingStatus"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter param = new SqlParameter();
                param = cmd.Parameters.Add("@i_Organisation_Code", SqlDbType.Int); param.Direction = ParameterDirection.Input; if (base.b_Organisation_Code) param.Value = base.i_Organisation_Code; else param.Value = DBNull.Value;
                param = cmd.Parameters.Add("@i_Registration_Number", SqlDbType.Int); param.Direction = ParameterDirection.Input; if (base.b_Registration_Number) param.Value = base.i_Registration_Number; else param.Value = DBNull.Value;
                param = cmd.Parameters.Add("@c_Is_Relieved", SqlDbType.VarChar, 1); param.Direction = ParameterDirection.Input; if (base.b_Is_Relieved) param.Value = base.c_Is_Relieved; else param.Value = DBNull.Value;
                param = cmd.Parameters.Add("@v_Relieve_Remarks", SqlDbType.VarChar, 250); param.Direction = ParameterDirection.Input; if (base.b_Relieve_Remarks) param.Value = base.v_Relieve_Remarks; else param.Value = DBNull.Value;
                param = cmd.Parameters.Add("@v_Relieved_By", SqlDbType.VarChar, 50); param.Direction = ParameterDirection.Input; if (base.b_Relieved_By) param.Value = base.v_Relieved_By; else param.Value = DBNull.Value;
                //param = cmd.Parameters.Add("@d_Relieving_Entry_Date", SqlDbType.DateTime); param.Direction = ParameterDirection.Input; if (base.b_Relieving_Entry_Date) param.Value = base.d_Relieving_Entry_Date; else param.Value = DBNull.Value;

                return SqlHelper.ExecuteProcedure(cmd);

            }

        }

        public DataTable Update_ApplicationStatus()
        {
            using (SqlCommand cmd = SqlHelper.GetSqlCommand("Update_ApplicationStatusScreeners"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter param = new SqlParameter();
                param = cmd.Parameters.Add("@i_Organisation_Code", SqlDbType.Int); param.Direction = ParameterDirection.Input; if (base.b_Organisation_Code) param.Value = base.i_Organisation_Code; else param.Value = DBNull.Value;
                param = cmd.Parameters.Add("@vCourseCode", SqlDbType.Int); param.Direction = ParameterDirection.Input; if (base.b_course_code) param.Value = base.i_course_code; else param.Value = DBNull.Value;                
                param = cmd.Parameters.Add("@i_Registration_Number", SqlDbType.Int); param.Direction = ParameterDirection.Input; if (base.b_Registration_Number) param.Value = base.i_Registration_Number; else param.Value = DBNull.Value;
                param = cmd.Parameters.Add("@c_Is_Application_Complete", SqlDbType.VarChar, 1); param.Direction = ParameterDirection.Input; if (base.b_Is_Application_Complete) param.Value = base.c_Is_Application_Complete; else param.Value = DBNull.Value;

                return SqlHelper.ExecuteProcedure(cmd);

            }

        }


        public DataTable ResetApplicationStatus()
        {
            using (SqlCommand cmd = SqlHelper.GetSqlCommand("ResetApplicationStatus"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter param = new SqlParameter();                
                param = cmd.Parameters.Add("@i_Registration_Number", SqlDbType.Int); param.Direction = ParameterDirection.Input; if (base.b_Registration_Number) param.Value = base.i_Registration_Number; else param.Value = DBNull.Value;                

                return SqlHelper.ExecuteProcedure(cmd);

            }

        }

        //Start added on 01 August 2023 for Login Process by Deep 

        public DataTable GetPassword(string UserName)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            
            DataTable DT = new DataTable();

            using (SqlConnection Conn = new SqlConnection(ConnectionString))
            {
                using (SqlCommand Cmm = new SqlCommand("GetPassword", Conn))
                {
                    Cmm.CommandType = CommandType.StoredProcedure;
                    Cmm.Parameters.Add(new SqlParameter("@UserName", SqlDbType.NVarChar, 256));
                    Cmm.Parameters["@UserName"].Value = UserName;                    
                    Conn.Open();
                    SqlDataAdapter Sda = new SqlDataAdapter(Cmm);
                    Sda.Fill(DT);
                }
            }

            return DT;
        }

        // reset the user account
        public DataTable ResetAccount(string UserName)
        {

            string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            DataTable myDt = new DataTable();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("AccountReset", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@UserName", SqlDbType.NVarChar, 128));
                    cmd.Parameters["@UserName"].Value = UserName;
                    con.Open();
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(myDt);
                    }
                }
            }
            return myDt;
        }

        // for user lock attempt count and updates user account
        public DataTable UpdateUserLockOutAttempt(string UserName)
        {

            string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            DataTable myDt = new DataTable();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("UpdateUserLockOutAttempt", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@UserName", SqlDbType.NVarChar, 128));
                    cmd.Parameters["@UserName"].Value = UserName;
                    con.Open();
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(myDt);
                    }
                }
            }
            return myDt;
        }
        // for update password
        public DataTable UpdatePassword(string UserName, string Password)
        {

            string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            DataTable myDt = new DataTable();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("UpdatePassword", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@UserName", SqlDbType.NVarChar, 128));
                    cmd.Parameters["@UserName"].Value = UserName;
                    cmd.Parameters.Add(new SqlParameter("@Password", SqlDbType.NVarChar, 128));
                    cmd.Parameters["@Password"].Value = Password;
                    con.Open();
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(myDt);
                    }
                }
            }
            return myDt;
        }
        // end on code for Login process by deep
     

      
    }
}
