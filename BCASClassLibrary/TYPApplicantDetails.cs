using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace CrisExamLibrary
{
    public class TYPApplicantDetails : ApplicantDetailsAttrib
    {
        public DataTable RegisterApplicant()
        {
            using (SqlCommand cmd = SqlHelper.GetSqlCommand("RegisterApplicantDetails"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter param = new SqlParameter();

                //param = cmd.Parameters.Add("@i_Application_no", SqlDbType.Int); param.Direction = ParameterDirection.Input; if (base.b_Application_no) param.Value = base.i_Application_no; else param.Value = DBNull.Value;
                param = cmd.Parameters.Add("@n_Post_Applied", SqlDbType.Int); param.Direction = ParameterDirection.Input; if (base.b_Post_Applied) param.Value = base.n_Post_Applied; else param.Value = DBNull.Value;
                param = cmd.Parameters.Add("@v_Candidate_Name", SqlDbType.VarChar, 50); param.Direction = ParameterDirection.Input; if (base.b_Candidate_Name) param.Value = base.v_Candidate_Name; else param.Value = DBNull.Value;
                param = cmd.Parameters.Add("@v_Father_Name", SqlDbType.VarChar, 50); param.Direction = ParameterDirection.Input; if (base.b_Father_Name) param.Value = base.v_Father_Name; else param.Value = DBNull.Value;
                param = cmd.Parameters.Add("@c_Gender_Code", SqlDbType.VarChar, 1); param.Direction = ParameterDirection.Input; if (base.b_Gender_Code) param.Value = base.c_Gender_Code; else param.Value = DBNull.Value;
                param = cmd.Parameters.Add("@d_Date_of_Birth", SqlDbType.DateTime); param.Direction = ParameterDirection.Input; if (base.b_Date_of_Birth) param.Value = base.d_Date_of_Birth; else param.Value = DBNull.Value;
                param = cmd.Parameters.Add("@i_Age", SqlDbType.Int); param.Direction = ParameterDirection.Input; if (base.b_Age) param.Value = base.i_Age; else param.Value = DBNull.Value;
                param = cmd.Parameters.Add("@n_Category_Code", SqlDbType.Int); param.Direction = ParameterDirection.Input; if (base.b_Category_Code) param.Value = base.n_Category_Code; else param.Value = DBNull.Value;
                param = cmd.Parameters.Add("@c_Handicapped_Code", SqlDbType.VarChar, 1); param.Direction = ParameterDirection.Input; if (base.b_Handicapped_Code) param.Value = base.c_Handicapped_Code; else param.Value = DBNull.Value;
                param = cmd.Parameters.Add("@v_Correspondence_Address1", SqlDbType.VarChar, 200); param.Direction = ParameterDirection.Input; if (base.b_Correspondence_Address1) param.Value = base.v_Correspondence_Address1; else param.Value = DBNull.Value;
                //  param = cmd.Parameters.Add("@v_Correspondence_Address2", SqlDbType.VarChar, 100); param.Direction = ParameterDirection.Input; if (base.b_Correspondence_Address2) param.Value = base.v_Correspondence_Address2; else param.Value = DBNull.Value;
                param = cmd.Parameters.Add("@v_Correspondence_City", SqlDbType.VarChar, 30); param.Direction = ParameterDirection.Input; if (base.b_Correspondence_City) param.Value = base.v_Correspondence_City; else param.Value = DBNull.Value;
                param = cmd.Parameters.Add("@n_Correspondence_State", SqlDbType.Int); param.Direction = ParameterDirection.Input; if (base.b_Correspondence_State) param.Value = base.n_Correspondence_State; else param.Value = DBNull.Value;
                param = cmd.Parameters.Add("@i_Correspondence_Pin", SqlDbType.Int); param.Direction = ParameterDirection.Input; if (base.b_Correspondence_Pin) param.Value = base.i_Correspondence_Pin; else param.Value = DBNull.Value;
                param = cmd.Parameters.Add("@v_Permanent_Address1", SqlDbType.VarChar, 200); param.Direction = ParameterDirection.Input; if (base.b_Permanent_Address1) param.Value = base.v_Permanent_Address1; else param.Value = DBNull.Value;
                //  param = cmd.Parameters.Add("@v_Permanent_Address2", SqlDbType.VarChar, 100); param.Direction = ParameterDirection.Input; if (base.b_Permanent_Address2) param.Value = base.v_Permanent_Address2; else param.Value = DBNull.Value;
                param = cmd.Parameters.Add("@v_Permanent_City", SqlDbType.VarChar, 30); param.Direction = ParameterDirection.Input; if (base.b_Permanent_City) param.Value = base.v_Permanent_City; else param.Value = DBNull.Value;
                param = cmd.Parameters.Add("@n_Permanent_State", SqlDbType.Int); param.Direction = ParameterDirection.Input; if (base.b_Permanent_State) param.Value = base.n_Permanent_State; else param.Value = DBNull.Value;
                param = cmd.Parameters.Add("@i_Permanent_Pin", SqlDbType.Int); param.Direction = ParameterDirection.Input; if (base.b_Permanent_Pin) param.Value = base.i_Permanent_Pin; else param.Value = DBNull.Value;
                param = cmd.Parameters.Add("@v_Mobile_No", SqlDbType.VarChar, 10); param.Direction = ParameterDirection.Input; if (base.b_Mobile_No) param.Value = base.v_Mobile_No; else param.Value = DBNull.Value;
                param = cmd.Parameters.Add("@v_Phone_No", SqlDbType.VarChar, 10); param.Direction = ParameterDirection.Input; if (base.b_Phone_No) param.Value = base.v_Phone_No; else param.Value = DBNull.Value;
                param = cmd.Parameters.Add("@v_Email_Id", SqlDbType.VarChar, 50); param.Direction = ParameterDirection.Input; if (base.b_Email_Id) param.Value = base.v_Email_Id; else param.Value = DBNull.Value;
                param = cmd.Parameters.Add("@v_Qualification", SqlDbType.VarChar, 100); param.Direction = ParameterDirection.Input; if (base.b_Qualification) param.Value = base.v_Qualification; else param.Value = DBNull.Value;
                //param = cmd.Parameters.Add("@v_Qualification_Subject", SqlDbType.VarChar, 100); param.Direction = ParameterDirection.Input; if (base.b_Qualification_Subject) param.Value = base.v_Qualification_Subject; else param.Value = DBNull.Value;
                param = cmd.Parameters.Add("@i_Qualification_Pass_Year", SqlDbType.Int); param.Direction = ParameterDirection.Input; if (base.b_Qualification_Pass_Year) param.Value = base.i_Qualification_Pass_Year; else param.Value = DBNull.Value;
                param = cmd.Parameters.Add("@v_Qualification_University", SqlDbType.VarChar, 100); param.Direction = ParameterDirection.Input; if (base.b_Qualification_University) param.Value = base.v_Qualification_University; else param.Value = DBNull.Value;
                param = cmd.Parameters.Add("@n_Qualification_Percentage", SqlDbType.Float); param.Direction = ParameterDirection.Input; if (base.b_Qualification_Percentage) param.Value = base.n_Qualification_Percentage; else param.Value = DBNull.Value;
                param = cmd.Parameters.Add("@n_Experience", SqlDbType.Float); param.Direction = ParameterDirection.Input; if (base.b_Experience) param.Value = base.n_Experience; else param.Value = DBNull.Value;
                param = cmd.Parameters.Add("@v_Agreed_Declaration", SqlDbType.VarChar, 1); param.Direction = ParameterDirection.Input; if (base.b_Agreed_Declaration) param.Value = base.v_Agreed_Declaration; else param.Value = DBNull.Value;
                param = cmd.Parameters.Add("@i_Application_Fee", SqlDbType.Int); param.Direction = ParameterDirection.Input; if (base.b_Application_Fee) param.Value = base.i_Application_Fee; else param.Value = DBNull.Value;
                //param = cmd.Parameters.Add("@v_Photo_Filename", SqlDbType.VarChar, 50); param.Direction = ParameterDirection.Input; if (base.b_Photo_Filename) param.Value = base.v_Photo_Filename; else param.Value = DBNull.Value;
                //param = cmd.Parameters.Add("@v_Signature_Filename", SqlDbType.VarChar, 50); param.Direction = ParameterDirection.Input; if (base.b_Signature_Filename) param.Value = base.v_Signature_Filename; else param.Value = DBNull.Value;
                //param = cmd.Parameters.Add("@i_Fee_Reference_Number", SqlDbType.Int); param.Direction = ParameterDirection.Input; if (base.b_Fee_Reference_Number) param.Value = base.i_Fee_Reference_Number; else param.Value = DBNull.Value;
                //param = cmd.Parameters.Add("@d_Date_of_Payment", SqlDbType.DateTime); param.Direction = ParameterDirection.Input; if (base.b_Date_of_Payment) param.Value = base.d_Date_of_Payment; else param.Value = DBNull.Value;
                //param = cmd.Parameters.Add("@i_Fee_Deducted", SqlDbType.Int); param.Direction = ParameterDirection.Input; if (base.b_Fee_Deducted) param.Value = base.i_Fee_Deducted; else param.Value = DBNull.Value;
                //param = cmd.Parameters.Add("@i_Payment_Id", SqlDbType.Int); param.Direction = ParameterDirection.Input; if (base.b_Payment_Id) param.Value = base.i_Payment_Id; else param.Value = DBNull.Value;
                //param = cmd.Parameters.Add("@c_Payment_Status", SqlDbType.VarChar, 10); param.Direction = ParameterDirection.Input; if (base.b_Payment_Status) param.Value = base.c_Payment_Status; else param.Value = DBNull.Value;
                param = cmd.Parameters.Add("@v_Enter_By", SqlDbType.VarChar, 50); param.Direction = ParameterDirection.Input; if (base.b_Enter_By) param.Value = base.v_Enter_By; else param.Value = DBNull.Value;
                //param = cmd.Parameters.Add("@d_Enter_Date", SqlDbType.DateTime); param.Direction = ParameterDirection.Input; if (base.b_Enter_Date) param.Value = base.d_Enter_Date; else param.Value = DBNull.Value; 
                param = cmd.Parameters.Add("@i_option", SqlDbType.Int); param.Direction = ParameterDirection.Input; if (base.b_option) param.Value = base.i_option; else param.Value = DBNull.Value;
                return SqlHelper.ExecuteProcedure(cmd);
            }
        }

        public DataTable GetApplicantDetails()
        {
            using (SqlCommand cmd = SqlHelper.GetSqlCommand("GetApplicantDetails"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter param = new SqlParameter();

                param = cmd.Parameters.Add("@i_Application_no", SqlDbType.Int); param.Direction = ParameterDirection.Input; if (base.b_Application_no) param.Value = base.i_Application_no; else param.Value = DBNull.Value;
                return SqlHelper.ExecuteProcedure(cmd);
            }
        }

        public DataTable UploadImages()
        {
            using (SqlCommand cmd = SqlHelper.GetSqlCommand("UploadPhotoSign"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter param = new SqlParameter();
                param = cmd.Parameters.Add("@i_Application_no", SqlDbType.Int); param.Direction = ParameterDirection.Input; if (base.b_Application_no) param.Value = base.i_Application_no; else param.Value = DBNull.Value;
                param = cmd.Parameters.Add("@v_Photo_Filename", SqlDbType.VarChar, 50); param.Direction = ParameterDirection.Input; if (base.b_Photo_Filename) param.Value = base.v_Photo_Filename; else param.Value = DBNull.Value;
                param = cmd.Parameters.Add("@v_Signature_Filename", SqlDbType.VarChar, 50); param.Direction = ParameterDirection.Input; if (base.b_Signature_Filename) param.Value = base.v_Signature_Filename; else param.Value = DBNull.Value;
                param = cmd.Parameters.Add("@i_option", SqlDbType.Int); param.Direction = ParameterDirection.Input; if (base.b_option) param.Value = base.i_option; else param.Value = DBNull.Value;
                return SqlHelper.ExecuteProcedure(cmd);
            }

        }
        
        public DataTable GetDetailsForPayments()
        {
            using (SqlCommand cmd = SqlHelper.GetSqlCommand("GetDetailsForPayments"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter param = new SqlParameter();
                param = cmd.Parameters.Add("@i_Application_no", SqlDbType.Int); param.Direction = ParameterDirection.Input; if (base.b_Application_no) param.Value = base.i_Application_no; else param.Value = DBNull.Value;
                param = cmd.Parameters.Add("@d_Date_of_Birth", SqlDbType.DateTime); param.Direction = ParameterDirection.Input; if (base.b_Date_of_Birth) param.Value = base.d_Date_of_Birth; else param.Value = DBNull.Value;

                return SqlHelper.ExecuteProcedure(cmd);
            }

        }
               
        public DataTable GetPassword(string UserName)
        {
             
            string ConnectionString = ConfigurationManager.ConnectionStrings["CrisExamConnectionString"].ConnectionString;
            DataTable myDt = new DataTable();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetPassword", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@UserName", SqlDbType.NVarChar , 256));
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
        
        public DataTable UpdatePassword(string UserName, string Password)
        {

            string ConnectionString = ConfigurationManager.ConnectionStrings["CrisExamConnectionString"].ConnectionString;
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

        public DataTable ResetAccount(string UserName)
        {

            string ConnectionString = ConfigurationManager.ConnectionStrings["CrisExamConnectionString"].ConnectionString;
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

        public DataTable UpdateUserLockOutAttempt(string UserName)
        {

            string ConnectionString = ConfigurationManager.ConnectionStrings["CrisExamConnectionString"].ConnectionString;
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

    }

}
