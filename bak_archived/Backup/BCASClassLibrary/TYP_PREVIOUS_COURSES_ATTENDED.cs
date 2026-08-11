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
    public class TYP_PREVIOUS_COURSES_ATTENDED
    {
        /// <summary>
        /// private bool m_IsNull; 
        /// To Determine Whether Current object is null or not
        /// </summary>
        private bool m_IsNull;

        /// <summary>
        /// Class Fields...same as in Table
        /// </summary>
        private int m_SR_NO;
        private int m_REGISTRATION_NUMBER;
        private int m_COURSE_CODE;
        private System.DateTime m_DATE_FROM;
        private System.DateTime m_DATE_TO;
        private int m_MARKS;
        private string m_RESULT;
        private string m_REMARKS;
        private bool m_REMARKSIsNull;
        private string m_ENTER_BY;
        private System.DateTime m_ENTER_DATE;
        /// <summary>
        /// Overloaded Constructors of current class
        /// </summary>
        public TYP_PREVIOUS_COURSES_ATTENDED()
        {
          // TODO : Add code to initialise the object
        }

        public TYP_PREVIOUS_COURSES_ATTENDED(string str)
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

        public static TYP_PREVIOUS_COURSES_ATTENDED Null
        {
           get
           {
              TYP_PREVIOUS_COURSES_ATTENDED obj = new TYP_PREVIOUS_COURSES_ATTENDED();
              obj.m_IsNull = true;
              return obj;
           }
        }

        /// <summary>
        /// Code of various properties
        /// </summary>
        public int SR_NO
        {
           get {return this.m_SR_NO;}
           set {this.m_SR_NO = value;}
        }
        public int REGISTRATION_NUMBER
        {
           get {return this.m_REGISTRATION_NUMBER;}
           set {this.m_REGISTRATION_NUMBER = value;}
        }
        public int COURSE_CODE
        {
           get {return this.m_COURSE_CODE;}
           set {this.m_COURSE_CODE = value;}
        }
        public System.DateTime DATE_FROM
        {
           get {return this.m_DATE_FROM;}
           set {this.m_DATE_FROM = value;}
        }
        public System.DateTime DATE_TO
        {
           get {return this.m_DATE_TO;}
           set {this.m_DATE_TO = value;}
        }
        public int MARKS
        {
           get {return this.m_MARKS;}
           set {this.m_MARKS = value;}
        }
        public string RESULT
        {
           get {return this.m_RESULT;}
           set {this.m_RESULT = value;}
        }
        public string REMARKS
        {
           get {return this.m_REMARKS;}
           set {this.m_REMARKS = value;}
        }
        public bool REMARKSIsNull
        {
           get {return this.m_REMARKSIsNull;}
           set {this.m_REMARKSIsNull = value;}
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
        public static int InsertPrevious_Courses_attended(TYP_PREVIOUS_COURSES_ATTENDED newRecord)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("InsertPrevious_Courses_attended", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Sr_No", SqlDbType.Int,10));
                    cmd.Parameters["@Sr_No"].Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(new SqlParameter("@Registration_Number", SqlDbType.Int,10));
                    cmd.Parameters["@Registration_Number"].Value = newRecord.REGISTRATION_NUMBER;
                    cmd.Parameters.Add(new SqlParameter("@Course_Code", SqlDbType.Int,10));
                    cmd.Parameters["@Course_Code"].Value = newRecord.COURSE_CODE;
                    cmd.Parameters.Add(new SqlParameter("@Date_from", SqlDbType.DateTime));
                    cmd.Parameters["@Date_from"].Value = newRecord.DATE_FROM;
                    cmd.Parameters.Add(new SqlParameter("@Date_To", SqlDbType.DateTime));
                    cmd.Parameters["@Date_To"].Value = newRecord.DATE_TO;
                    cmd.Parameters.Add(new SqlParameter("@Marks", SqlDbType.Int,10));
                    cmd.Parameters["@Marks"].Value = newRecord.MARKS;
                    cmd.Parameters.Add(new SqlParameter("@Result", SqlDbType.Char,1));
                    cmd.Parameters["@Result"].Value = newRecord.RESULT;
                    cmd.Parameters.Add(new SqlParameter("@Remarks", SqlDbType.VarChar,100));
                    if (newRecord.REMARKSIsNull)
                        cmd.Parameters["@REMARKS"].Value = DBNull.Value;
                    else
                        cmd.Parameters["@REMARKS"].Value = newRecord.REMARKS;
                    cmd.Parameters.Add(new SqlParameter("@Enter_By", SqlDbType.VarChar,50));
                    cmd.Parameters["@Enter_By"].Value = newRecord.ENTER_BY;
                    cmd.Parameters.Add(new SqlParameter("@Enter_Date", SqlDbType.DateTime));
                    cmd.Parameters["@Enter_Date"].Value = newRecord.ENTER_DATE;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    return (int) cmd.Parameters["@Sr_No"].Value;
                }
            }
        }
        public static void DeletePrevious_Courses_attended(int pSr_No)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("DeletePrevious_Courses_attended", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Sr_No", SqlDbType.Int,10));
                    cmd.Parameters["@Sr_No"].Value = pSr_No;
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static void UpdatePrevious_Courses_attended(TYP_PREVIOUS_COURSES_ATTENDED newRecord)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("UpdatePrevious_Courses_attended", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Sr_No", SqlDbType.Int,10));
                    cmd.Parameters["@Sr_No"].Value = newRecord.SR_NO;
                    cmd.Parameters.Add(new SqlParameter("@Registration_Number", SqlDbType.Int,10));
                    cmd.Parameters["@Registration_Number"].Value = newRecord.REGISTRATION_NUMBER;
                    cmd.Parameters.Add(new SqlParameter("@Course_Code", SqlDbType.Int,10));
                    cmd.Parameters["@Course_Code"].Value = newRecord.COURSE_CODE;
                    cmd.Parameters.Add(new SqlParameter("@Date_from", SqlDbType.DateTime));
                    cmd.Parameters["@Date_from"].Value = newRecord.DATE_FROM;
                    cmd.Parameters.Add(new SqlParameter("@Date_To", SqlDbType.DateTime));
                    cmd.Parameters["@Date_To"].Value = newRecord.DATE_TO;
                    cmd.Parameters.Add(new SqlParameter("@Marks", SqlDbType.Int,10));
                    cmd.Parameters["@Marks"].Value = newRecord.MARKS;
                    cmd.Parameters.Add(new SqlParameter("@Result", SqlDbType.Char,1));
                    cmd.Parameters["@Result"].Value = newRecord.RESULT;
                    cmd.Parameters.Add(new SqlParameter("@Remarks", SqlDbType.VarChar,100));
                    if (newRecord.REMARKSIsNull)
                        cmd.Parameters["@REMARKS"].Value = DBNull.Value;
                    else
                        cmd.Parameters["@REMARKS"].Value = newRecord.REMARKS;
                    cmd.Parameters.Add(new SqlParameter("@Enter_By", SqlDbType.VarChar,50));
                    cmd.Parameters["@Enter_By"].Value = newRecord.ENTER_BY;
                    cmd.Parameters.Add(new SqlParameter("@Enter_Date", SqlDbType.DateTime));
                    cmd.Parameters["@Enter_Date"].Value = newRecord.ENTER_DATE;
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static DataSet GetAllPrevious_Courses_attended()
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            DataSet myset = new DataSet();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetAllPrevious_Courses_attended", con))
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
        public static DataSet GetPrevious_Courses_attended(int pSr_No, int pRegistration_Number, int pCourse_Code, System.DateTime pDate_from, System.DateTime pDate_To, int pMarks, string pResult, string pRemarks)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            DataSet myset = new DataSet();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetPrevious_Courses_attended", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Sr_No", SqlDbType.Int,10));
                    cmd.Parameters["@Sr_No"].Value = pSr_No;
                    cmd.Parameters.Add(new SqlParameter("@Registration_Number", SqlDbType.Int,10));
                    cmd.Parameters["@Registration_Number"].Value = pRegistration_Number;
                    cmd.Parameters.Add(new SqlParameter("@Course_Code", SqlDbType.Int,10));
                    cmd.Parameters["@Course_Code"].Value = pCourse_Code;
                    cmd.Parameters.Add(new SqlParameter("@Date_from", SqlDbType.DateTime));
                    cmd.Parameters["@Date_from"].Value = pDate_from;
                    cmd.Parameters.Add(new SqlParameter("@Date_To", SqlDbType.DateTime));
                    cmd.Parameters["@Date_To"].Value = pDate_To;
                    cmd.Parameters.Add(new SqlParameter("@Marks", SqlDbType.Int,10));
                    cmd.Parameters["@Marks"].Value = pMarks;
                    cmd.Parameters.Add(new SqlParameter("@Result", SqlDbType.Char,1));
                    cmd.Parameters["@Result"].Value = pResult;
                    cmd.Parameters.Add(new SqlParameter("@Remarks", SqlDbType.VarChar,100));
                    cmd.Parameters["@Remarks"].Value = pRemarks;
                    con.Open();
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                            sda.Fill(myset);
                    }
                }
            }
            return myset;
        }

        //public static MemoryStream Execute(int pSr_No, int pRegistration_Number, int pCourse_Code, System.DateTime pDate_from, System.DateTime pDate_To, int pMarks, string pResult, string pRemarks)
        //{
        //    kanchan.ReportExecutionService myReport = new kanchan.ReportExecutionService();

        //    myReport.Credentials = System.Net.CredentialCache.DefaultCredentials;
        //    myReport.Url = CommonClass.URL;

        //    myReport.LoadReport("/BCASReports/RepPrevious_Courses_attended", null);

        //    kanchan.ParameterValue[] parameters = new kanchan.ParameterValue[2];
        //    parameters[0] = new kanchan.ParameterValue();
        //    parameters[0].Name = "Sr_No";
        //    parameters[0].Value = pSr_No.ToString();
        //    parameters[1] = new kanchan.ParameterValue();
        //    parameters[1].Name = "Registration_Number";
        //    parameters[1].Value = pRegistration_Number.ToString();
        //    parameters[2] = new kanchan.ParameterValue();
        //    parameters[2].Name = "Course_Code";
        //    parameters[2].Value = pCourse_Code.ToString();
        //    parameters[3] = new kanchan.ParameterValue();
        //    parameters[3].Name = "Date_from";
        //    parameters[3].Value = pDate_from.ToString();
        //    parameters[4] = new kanchan.ParameterValue();
        //    parameters[4].Name = "Date_To";
        //    parameters[4].Value = pDate_To.ToString();
        //    parameters[5] = new kanchan.ParameterValue();
        //    parameters[5].Name = "Marks";
        //    parameters[5].Value = pMarks.ToString();
        //    parameters[6] = new kanchan.ParameterValue();
        //    parameters[6].Name = "Result";
        //    parameters[6].Value = pResult.ToString();
        //    parameters[7] = new kanchan.ParameterValue();
        //    parameters[7].Name = "Remarks";
        //    parameters[7].Value = pRemarks.ToString();

        //    myReport.SetExecutionParameters(parameters, "en-us"); 

        //    string format = "PDF";
        //    string mimeType = "application/pdf";
        //    string deviceInfo = String.Empty;
        //    string extension = String.Empty;
        //    string encoding = String.Empty;
        //    kanchan.Warning[] warnings = null;
        //    string[] streamIDs = null;

            
        //    MemoryStream oStream = new MemoryStream(myReport.Render(format, deviceInfo, out extension, out mimeType, out encoding, out warnings, out streamIDs));
        //    myReport.Dispose();
        //    return oStream;
        //}

    }
}
