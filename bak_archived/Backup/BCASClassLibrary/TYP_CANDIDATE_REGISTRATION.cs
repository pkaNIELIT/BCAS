using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Configuration;
using System.Collections.Generic;

using System.IO;
using System.Web;

namespace BCASClassLibrary
{
    public class TYP_CANDIDATE_REGISTRATION
    {
        /// <summary>
        /// private bool m_IsNull; 
        /// To Determine Whether Current object is null or not
        /// </summary>
        private bool m_IsNull;

        /// <summary>
        /// Class Fields...same as in Table
        /// </summary>
        private int m_ORGANISATION_CODE;
        private string m_EMPLOYEE_ID;
        private string m_CANDIDATE_NAME;
        private string m_SEX_CODE;
        private int m_DESIGNATION_CODE;
        private System.DateTime m_DOB;
        private string m_EMAIL_ID;
        private string m_MOBILE_NO;
        private int m_NATIONALITY_CODE;
        private int m_EDUCATIONAL_QUALIFICATION_CODE;
        private int m_REGISTRATION_NUMBER;
        private string m_ENTER_BY;
        private System.DateTime m_ENTER_DATE;
        /// <summary>
        /// Overloaded Constructors of current class
        /// </summary>
        public TYP_CANDIDATE_REGISTRATION()
        {
          // TODO : Add code to initialise the object
        }

        public TYP_CANDIDATE_REGISTRATION(string str)
        {
          // TODO : Add code to initialise the object based on the given string 
        }

        /// <summary>
        /// Implementation of Null Interface
        /// </summary>
        public virtual bool IsNull
        {
           get {return this.m_IsNull;}
        }

        public static TYP_CANDIDATE_REGISTRATION Null
        {
           get
           {
              TYP_CANDIDATE_REGISTRATION obj = new TYP_CANDIDATE_REGISTRATION();
              obj.m_IsNull = true;
              return obj;
           }
        }

        /// <summary>
        /// Code of various properties
        /// </summary>
        public int ORGANISATION_CODE
        {
           get {return this.m_ORGANISATION_CODE;}
           set {this.m_ORGANISATION_CODE = value;}
        }
        public string EMPLOYEE_ID
        {
           get {return this.m_EMPLOYEE_ID;}
           set {this.m_EMPLOYEE_ID = value;}
        }
        public string CANDIDATE_NAME
        {
           get {return this.m_CANDIDATE_NAME;}
           set {this.m_CANDIDATE_NAME = value;}
        }
        public string SEX_CODE
        {
           get {return this.m_SEX_CODE;}
           set {this.m_SEX_CODE = value;}
        }
        public int DESIGNATION_CODE
        {
           get {return this.m_DESIGNATION_CODE;}
           set {this.m_DESIGNATION_CODE = value;}
        }
        public System.DateTime DOB
        {
           get {return this.m_DOB;}
           set {this.m_DOB = value;}
        }
        public string EMAIL_ID
        {
           get {return this.m_EMAIL_ID;}
           set {this.m_EMAIL_ID = value;}
        }
        public string MOBILE_NO
        {
           get {return this.m_MOBILE_NO;}
           set {this.m_MOBILE_NO = value;}
        }
        public int NATIONALITY_CODE
        {
           get {return this.m_NATIONALITY_CODE;}
           set {this.m_NATIONALITY_CODE = value;}
        }
        public int EDUCATIONAL_QUALIFICATION_CODE
        {
           get {return this.m_EDUCATIONAL_QUALIFICATION_CODE;}
           set {this.m_EDUCATIONAL_QUALIFICATION_CODE = value;}
        }
        public int REGISTRATION_NUMBER
        {
           get {return this.m_REGISTRATION_NUMBER;}
           set {this.m_REGISTRATION_NUMBER = value;}
        }
        public string ENTER_BY
        {
           get {return this.m_ENTER_BY;}
           set {this.m_ENTER_BY = value;}
        }
        public System.DateTime ENTER_DATE
        {
           get {return this.m_ENTER_DATE;}
           set {this.m_ENTER_DATE = value;}
        }
        public static int InsertCandidate_Registration(TYP_CANDIDATE_REGISTRATION newRecord)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("InsertCandidate_Registration", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Organisation_Code", SqlDbType.Int,10));
                    cmd.Parameters["@Organisation_Code"].Value = newRecord.ORGANISATION_CODE;
                    cmd.Parameters.Add(new SqlParameter("@Employee_ID", SqlDbType.VarChar,12));
                    cmd.Parameters["@Employee_ID"].Value = newRecord.EMPLOYEE_ID;
                    cmd.Parameters.Add(new SqlParameter("@Candidate_Name", SqlDbType.VarChar,100));
                    cmd.Parameters["@Candidate_Name"].Value = newRecord.CANDIDATE_NAME;
                    cmd.Parameters.Add(new SqlParameter("@Sex_Code", SqlDbType.Char,1));
                    cmd.Parameters["@Sex_Code"].Value = newRecord.SEX_CODE;
                    cmd.Parameters.Add(new SqlParameter("@Designation_Code", SqlDbType.Int,10));
                    cmd.Parameters["@Designation_Code"].Value = newRecord.DESIGNATION_CODE;
                    cmd.Parameters.Add(new SqlParameter("@DOB", SqlDbType.DateTime));
                    cmd.Parameters["@DOB"].Value = newRecord.DOB;
                    cmd.Parameters.Add(new SqlParameter("@Email_Id", SqlDbType.VarChar,50));
                    cmd.Parameters["@Email_Id"].Value = newRecord.EMAIL_ID;
                    cmd.Parameters.Add(new SqlParameter("@Mobile_No", SqlDbType.VarChar,10));
                    cmd.Parameters["@Mobile_No"].Value = newRecord.MOBILE_NO;
                    cmd.Parameters.Add(new SqlParameter("@Nationality_Code", SqlDbType.Int,10));
                    cmd.Parameters["@Nationality_Code"].Value = newRecord.NATIONALITY_CODE;
                    cmd.Parameters.Add(new SqlParameter("@Educational_Qualification_Code", SqlDbType.Int,10));
                    cmd.Parameters["@Educational_Qualification_Code"].Value = newRecord.EDUCATIONAL_QUALIFICATION_CODE;
                    cmd.Parameters.Add(new SqlParameter("@Registration_Number", SqlDbType.Int,10));
                    cmd.Parameters["@Registration_Number"].Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(new SqlParameter("@Enter_by", SqlDbType.VarChar,50));
                    cmd.Parameters["@Enter_by"].Value = newRecord.ENTER_BY;
                    cmd.Parameters.Add(new SqlParameter("@Enter_Date", SqlDbType.DateTime));
                    cmd.Parameters["@Enter_Date"].Value = newRecord.ENTER_DATE;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    return (int) cmd.Parameters["@Registration_Number"].Value;
                }
            }
        }
        public static void DeleteCandidate_Registration(int pRegistration_Number)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("DeleteCandidate_Registration", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Registration_Number", SqlDbType.Int,10));
                    cmd.Parameters["@Registration_Number"].Value = pRegistration_Number;
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static void UpdateCandidate_Registration(TYP_CANDIDATE_REGISTRATION newRecord)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("UpdateCandidate_Registration", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Organisation_Code", SqlDbType.Int,10));
                    cmd.Parameters["@Organisation_Code"].Value = newRecord.ORGANISATION_CODE;
                    cmd.Parameters.Add(new SqlParameter("@Employee_ID", SqlDbType.VarChar,12));
                    cmd.Parameters["@Employee_ID"].Value = newRecord.EMPLOYEE_ID;
                    cmd.Parameters.Add(new SqlParameter("@Candidate_Name", SqlDbType.VarChar,100));
                    cmd.Parameters["@Candidate_Name"].Value = newRecord.CANDIDATE_NAME;
                    cmd.Parameters.Add(new SqlParameter("@Sex_Code", SqlDbType.Char,1));
                    cmd.Parameters["@Sex_Code"].Value = newRecord.SEX_CODE;
                    cmd.Parameters.Add(new SqlParameter("@Designation_Code", SqlDbType.Int,10));
                    cmd.Parameters["@Designation_Code"].Value = newRecord.DESIGNATION_CODE;
                    cmd.Parameters.Add(new SqlParameter("@DOB", SqlDbType.DateTime));
                    cmd.Parameters["@DOB"].Value = newRecord.DOB;
                    cmd.Parameters.Add(new SqlParameter("@Email_Id", SqlDbType.VarChar,50));
                    cmd.Parameters["@Email_Id"].Value = newRecord.EMAIL_ID;
                    cmd.Parameters.Add(new SqlParameter("@Mobile_No", SqlDbType.VarChar,10));
                    cmd.Parameters["@Mobile_No"].Value = newRecord.MOBILE_NO;
                    cmd.Parameters.Add(new SqlParameter("@Nationality_Code", SqlDbType.Int,10));
                    cmd.Parameters["@Nationality_Code"].Value = newRecord.NATIONALITY_CODE;
                    cmd.Parameters.Add(new SqlParameter("@Educational_Qualification_Code", SqlDbType.Int,10));
                    cmd.Parameters["@Educational_Qualification_Code"].Value = newRecord.EDUCATIONAL_QUALIFICATION_CODE;
                    cmd.Parameters.Add(new SqlParameter("@Registration_Number", SqlDbType.Int,10));
                    cmd.Parameters["@Registration_Number"].Value = newRecord.REGISTRATION_NUMBER;
                    cmd.Parameters.Add(new SqlParameter("@Enter_by", SqlDbType.VarChar,50));
                    cmd.Parameters["@Enter_by"].Value = newRecord.ENTER_BY;
                    cmd.Parameters.Add(new SqlParameter("@Enter_Date", SqlDbType.DateTime));
                    cmd.Parameters["@Enter_Date"].Value = newRecord.ENTER_DATE;
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static void UpdateCandidate_Registration_Relieved(TYP_CANDIDATE_REGISTRATION newRecord)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("UpdateCandidate_Registration_Relieved", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Organisation_Code", SqlDbType.Int, 10));
                    cmd.Parameters["@Organisation_Code"].Value = newRecord.ORGANISATION_CODE;
                    cmd.Parameters.Add(new SqlParameter("@Employee_ID", SqlDbType.VarChar, 12));
                    cmd.Parameters["@Employee_ID"].Value = newRecord.EMPLOYEE_ID;
                    cmd.Parameters.Add(new SqlParameter("@Candidate_Name", SqlDbType.VarChar, 100));
                    cmd.Parameters["@Candidate_Name"].Value = newRecord.CANDIDATE_NAME;
                    cmd.Parameters.Add(new SqlParameter("@Sex_Code", SqlDbType.Char, 1));
                    cmd.Parameters["@Sex_Code"].Value = newRecord.SEX_CODE;
                    cmd.Parameters.Add(new SqlParameter("@Designation_Code", SqlDbType.Int, 10));
                    cmd.Parameters["@Designation_Code"].Value = newRecord.DESIGNATION_CODE;
                    cmd.Parameters.Add(new SqlParameter("@DOB", SqlDbType.DateTime));
                    cmd.Parameters["@DOB"].Value = newRecord.DOB;
                    cmd.Parameters.Add(new SqlParameter("@Email_Id", SqlDbType.VarChar, 50));
                    cmd.Parameters["@Email_Id"].Value = newRecord.EMAIL_ID;
                    cmd.Parameters.Add(new SqlParameter("@Mobile_No", SqlDbType.VarChar, 10));
                    cmd.Parameters["@Mobile_No"].Value = newRecord.MOBILE_NO;
                    cmd.Parameters.Add(new SqlParameter("@Nationality_Code", SqlDbType.Int, 10));
                    cmd.Parameters["@Nationality_Code"].Value = newRecord.NATIONALITY_CODE;
                    cmd.Parameters.Add(new SqlParameter("@Educational_Qualification_Code", SqlDbType.Int, 10));
                    cmd.Parameters["@Educational_Qualification_Code"].Value = newRecord.EDUCATIONAL_QUALIFICATION_CODE;
                    cmd.Parameters.Add(new SqlParameter("@Registration_Number", SqlDbType.Int, 10));
                    cmd.Parameters["@Registration_Number"].Value = newRecord.REGISTRATION_NUMBER;
                    cmd.Parameters.Add(new SqlParameter("@Enter_by", SqlDbType.VarChar, 50));
                    cmd.Parameters["@Enter_by"].Value = newRecord.ENTER_BY;
                    cmd.Parameters.Add(new SqlParameter("@Enter_Date", SqlDbType.DateTime));
                    cmd.Parameters["@Enter_Date"].Value = newRecord.ENTER_DATE;
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static DataSet GetAllCandidate_Registration()
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            DataSet myset = new DataSet();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetAllCandidate_Registration", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                            sda.Fill(myset);
                    }
                }
            }
            return myset;
        }
        public static DataSet GetCandidate_Registration(int pOrganisation_Code, string pEmployee_ID, string pCandidate_Name, string pSex_Code, int pDesignation_Code, System.DateTime pDOB, string pEmail_Id, string pMobile_No, int pNationality_Code, int pEducational_Qualification_Code, int pRegistration_Number)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            DataSet myset = new DataSet();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetCandidate_Registration", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Organisation_Code", SqlDbType.Int,10));
                    cmd.Parameters["@Organisation_Code"].Value = pOrganisation_Code;
                    cmd.Parameters.Add(new SqlParameter("@Employee_ID", SqlDbType.VarChar,12));
                    cmd.Parameters["@Employee_ID"].Value = pEmployee_ID;
                    cmd.Parameters.Add(new SqlParameter("@Candidate_Name", SqlDbType.VarChar,100));
                    cmd.Parameters["@Candidate_Name"].Value = pCandidate_Name;
                    cmd.Parameters.Add(new SqlParameter("@Sex_Code", SqlDbType.Char,1));
                    cmd.Parameters["@Sex_Code"].Value = pSex_Code;
                    cmd.Parameters.Add(new SqlParameter("@Designation_Code", SqlDbType.Int,10));
                    cmd.Parameters["@Designation_Code"].Value = pDesignation_Code;
                    cmd.Parameters.Add(new SqlParameter("@DOB", SqlDbType.DateTime));
                    cmd.Parameters["@DOB"].Value = pDOB;
                    cmd.Parameters.Add(new SqlParameter("@Email_Id", SqlDbType.VarChar,50));
                    cmd.Parameters["@Email_Id"].Value = pEmail_Id;
                    cmd.Parameters.Add(new SqlParameter("@Mobile_No", SqlDbType.VarChar,10));
                    cmd.Parameters["@Mobile_No"].Value = pMobile_No;
                    cmd.Parameters.Add(new SqlParameter("@Nationality_Code", SqlDbType.Int,10));
                    cmd.Parameters["@Nationality_Code"].Value = pNationality_Code;
                    cmd.Parameters.Add(new SqlParameter("@Educational_Qualification_Code", SqlDbType.Int,10));
                    cmd.Parameters["@Educational_Qualification_Code"].Value = pEducational_Qualification_Code;
                    cmd.Parameters.Add(new SqlParameter("@Registration_Number", SqlDbType.Int,10));
                    cmd.Parameters["@Registration_Number"].Value = pRegistration_Number;
                    con.Open();
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                            sda.Fill(myset);
                    }
                }
            }
            return myset;
        }
        //deep

        //public static DataSet GetCandidate_Details(int pOrganisation_Code, string pEmployee_ID, string pCandidate_Name, string pSex_Code, int pDesignation_Code, System.DateTime pDOB, string pEmail_Id, string pMobile_No, int pNationality_Code, int pEducational_Qualification_Code, int pRegistration_Number)
        //{
        //    string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
        //    DataSet myset = new DataSet();
        //    using (SqlConnection con = new SqlConnection(ConnectionString))
        //    {
        //        using (SqlCommand cmd = new SqlCommand("GetCandidate_Details", con))
        //        {
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.Add(new SqlParameter("@Organisation_Code", SqlDbType.Int, 10));
        //            cmd.Parameters["@Organisation_Code"].Value = pOrganisation_Code;
        //            cmd.Parameters.Add(new SqlParameter("@Employee_ID", SqlDbType.VarChar, 12));
        //            cmd.Parameters["@Employee_ID"].Value = pEmployee_ID;
        //            cmd.Parameters.Add(new SqlParameter("@Candidate_Name", SqlDbType.VarChar, 100));
        //            cmd.Parameters["@Candidate_Name"].Value = pCandidate_Name;
        //            cmd.Parameters.Add(new SqlParameter("@Sex_Code", SqlDbType.Char, 1));
        //            cmd.Parameters["@Sex_Code"].Value = pSex_Code;
        //            cmd.Parameters.Add(new SqlParameter("@Designation_Code", SqlDbType.Int, 10));
        //            cmd.Parameters["@Designation_Code"].Value = pDesignation_Code;
        //            cmd.Parameters.Add(new SqlParameter("@DOB", SqlDbType.DateTime));
        //            cmd.Parameters["@DOB"].Value = pDOB;
        //            cmd.Parameters.Add(new SqlParameter("@Email_Id", SqlDbType.VarChar, 50));
        //            cmd.Parameters["@Email_Id"].Value = pEmail_Id;
        //            cmd.Parameters.Add(new SqlParameter("@Mobile_No", SqlDbType.VarChar, 10));
        //            cmd.Parameters["@Mobile_No"].Value = pMobile_No;
        //            cmd.Parameters.Add(new SqlParameter("@Nationality_Code", SqlDbType.Int, 10));
        //            cmd.Parameters["@Nationality_Code"].Value = pNationality_Code;
        //            cmd.Parameters.Add(new SqlParameter("@Educational_Qualification_Code", SqlDbType.Int, 10));
        //            cmd.Parameters["@Educational_Qualification_Code"].Value = pEducational_Qualification_Code;
        //            cmd.Parameters.Add(new SqlParameter("@Registration_Number", SqlDbType.Int, 10));
        //            cmd.Parameters["@Registration_Number"].Value = pRegistration_Number;
        //            con.Open();
        //            using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
        //            {
        //                sda.Fill(myset);
        //            }
        //        }
        //    }
        //    return myset;
        //}

        //deep
        public static DataSet GetCandidate_Registration_Relieved(int pOrganisation_Code, string pEmployee_ID, string pCandidate_Name, string pSex_Code, int pDesignation_Code, System.DateTime pDOB, string pEmail_Id, string pMobile_No, int pNationality_Code, int pEducational_Qualification_Code, int pRegistration_Number)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            DataSet myset = new DataSet();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetCandidate_Registration_Relieved", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Organisation_Code", SqlDbType.Int, 10));
                    cmd.Parameters["@Organisation_Code"].Value = pOrganisation_Code;
                    cmd.Parameters.Add(new SqlParameter("@Employee_ID", SqlDbType.VarChar, 12));
                    cmd.Parameters["@Employee_ID"].Value = pEmployee_ID;
                    cmd.Parameters.Add(new SqlParameter("@Candidate_Name", SqlDbType.VarChar, 100));
                    cmd.Parameters["@Candidate_Name"].Value = pCandidate_Name;
                    cmd.Parameters.Add(new SqlParameter("@Sex_Code", SqlDbType.Char, 1));
                    cmd.Parameters["@Sex_Code"].Value = pSex_Code;
                    cmd.Parameters.Add(new SqlParameter("@Designation_Code", SqlDbType.Int, 10));
                    cmd.Parameters["@Designation_Code"].Value = pDesignation_Code;
                    cmd.Parameters.Add(new SqlParameter("@DOB", SqlDbType.DateTime));
                    cmd.Parameters["@DOB"].Value = pDOB;
                    cmd.Parameters.Add(new SqlParameter("@Email_Id", SqlDbType.VarChar, 50));
                    cmd.Parameters["@Email_Id"].Value = pEmail_Id;
                    cmd.Parameters.Add(new SqlParameter("@Mobile_No", SqlDbType.VarChar, 10));
                    cmd.Parameters["@Mobile_No"].Value = pMobile_No;
                    cmd.Parameters.Add(new SqlParameter("@Nationality_Code", SqlDbType.Int, 10));
                    cmd.Parameters["@Nationality_Code"].Value = pNationality_Code;
                    cmd.Parameters.Add(new SqlParameter("@Educational_Qualification_Code", SqlDbType.Int, 10));
                    cmd.Parameters["@Educational_Qualification_Code"].Value = pEducational_Qualification_Code;
                    cmd.Parameters.Add(new SqlParameter("@Registration_Number", SqlDbType.Int, 10));
                    cmd.Parameters["@Registration_Number"].Value = pRegistration_Number;
                    con.Open();
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(myset);
                    }
                }
            }
            return myset;
        }

        public static DataSet GetCandidate_Registration_Orgwise(int pOrganisation_Code, string pEmployee_ID, string pCandidate_Name, string pSex_Code, int pDesignation_Code, System.DateTime pDOB, string pEmail_Id, string pMobile_No, int pNationality_Code, int pEducational_Qualification_Code, int pRegistration_Number)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            DataSet myset = new DataSet();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetCandidate_Registration_Orgwise", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Organisation_Code", SqlDbType.Int, 10));
                    cmd.Parameters["@Organisation_Code"].Value = pOrganisation_Code;
                    cmd.Parameters.Add(new SqlParameter("@Employee_ID", SqlDbType.VarChar, 12));
                    cmd.Parameters["@Employee_ID"].Value = pEmployee_ID;
                    cmd.Parameters.Add(new SqlParameter("@Candidate_Name", SqlDbType.VarChar, 100));
                    cmd.Parameters["@Candidate_Name"].Value = pCandidate_Name;
                    cmd.Parameters.Add(new SqlParameter("@Sex_Code", SqlDbType.Char, 1));
                    cmd.Parameters["@Sex_Code"].Value = pSex_Code;
                    cmd.Parameters.Add(new SqlParameter("@Designation_Code", SqlDbType.Int, 10));
                    cmd.Parameters["@Designation_Code"].Value = pDesignation_Code;
                    cmd.Parameters.Add(new SqlParameter("@DOB", SqlDbType.DateTime));
                    cmd.Parameters["@DOB"].Value = pDOB;
                    cmd.Parameters.Add(new SqlParameter("@Email_Id", SqlDbType.VarChar, 50));
                    cmd.Parameters["@Email_Id"].Value = pEmail_Id;
                    cmd.Parameters.Add(new SqlParameter("@Mobile_No", SqlDbType.VarChar, 10));
                    cmd.Parameters["@Mobile_No"].Value = pMobile_No;
                    cmd.Parameters.Add(new SqlParameter("@Nationality_Code", SqlDbType.Int, 10));
                    cmd.Parameters["@Nationality_Code"].Value = pNationality_Code;
                    cmd.Parameters.Add(new SqlParameter("@Educational_Qualification_Code", SqlDbType.Int, 10));
                    cmd.Parameters["@Educational_Qualification_Code"].Value = pEducational_Qualification_Code;
                    cmd.Parameters.Add(new SqlParameter("@Registration_Number", SqlDbType.Int, 10));
                    cmd.Parameters["@Registration_Number"].Value = pRegistration_Number;
                    con.Open();
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(myset);
                    }
                }
            }
            return myset;
        }

       

    }
}
