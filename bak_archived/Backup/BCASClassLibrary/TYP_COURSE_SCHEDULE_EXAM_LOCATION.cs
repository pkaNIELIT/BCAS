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
    public class TYP_COURSE_SCHEDULE_EXAM_LOCATION
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
        private int m_COURSE_SCHEDULE_ID;
        private int m_EXAM_LOCATION_CODE;
        private string m_ENTER_BY;
        private bool m_ENTER_BYIsNull;
        private System.DateTime m_ENTER_DATE;
        private bool m_ENTER_DATEIsNull;
        /// <summary>
        /// Overloaded Constructors of current class
        /// </summary>
        public TYP_COURSE_SCHEDULE_EXAM_LOCATION()
        {
          // TODO : Add code to initialise the object
        }

        public TYP_COURSE_SCHEDULE_EXAM_LOCATION(string str)
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

        public static TYP_COURSE_SCHEDULE_EXAM_LOCATION Null
        {
           get
           {
              TYP_COURSE_SCHEDULE_EXAM_LOCATION obj = new TYP_COURSE_SCHEDULE_EXAM_LOCATION();
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
        public int COURSE_SCHEDULE_ID
        {
           get {return this.m_COURSE_SCHEDULE_ID;}
           set {this.m_COURSE_SCHEDULE_ID = value;}
        }
        public int EXAM_LOCATION_CODE
        {
           get {return this.m_EXAM_LOCATION_CODE;}
           set {this.m_EXAM_LOCATION_CODE = value;}
        }
        public string ENTER_BY
        {
           get {return this.m_ENTER_BY;}
           set {this.m_ENTER_BY = value;}
        }
        public bool ENTER_BYIsNull
        {
           get {return this.m_ENTER_BYIsNull;}
           set {this.m_ENTER_BYIsNull = value;}
        }
        public System.DateTime ENTER_DATE
        {
           get {return this.m_ENTER_DATE;}
           set {this.m_ENTER_DATE = value;}
        }
        public bool ENTER_DATEIsNull
        {
           get {return this.m_ENTER_DATEIsNull;}
           set {this.m_ENTER_DATEIsNull = value;}
        }
        public static int InsertCourse_Schedule_Exam_location(TYP_COURSE_SCHEDULE_EXAM_LOCATION newRecord)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("InsertCourse_Schedule_Exam_location", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Sr_No", SqlDbType.Int,10));
                    cmd.Parameters["@Sr_No"].Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(new SqlParameter("@Course_Schedule_ID", SqlDbType.Int,10));
                    cmd.Parameters["@Course_Schedule_ID"].Value = newRecord.COURSE_SCHEDULE_ID;
                    cmd.Parameters.Add(new SqlParameter("@Exam_Location_Code", SqlDbType.Int,10));
                    cmd.Parameters["@Exam_Location_Code"].Value = newRecord.EXAM_LOCATION_CODE;
                    cmd.Parameters.Add(new SqlParameter("@Enter_By", SqlDbType.VarChar,50));
                    if (newRecord.ENTER_BYIsNull)
                        cmd.Parameters["@ENTER_BY"].Value = DBNull.Value;
                    else
                        cmd.Parameters["@ENTER_BY"].Value = newRecord.ENTER_BY;
                    cmd.Parameters.Add(new SqlParameter("@Enter_Date", SqlDbType.DateTime));
                    if (newRecord.ENTER_DATEIsNull)
                        cmd.Parameters["@ENTER_DATE"].Value = DBNull.Value;
                    else
                        cmd.Parameters["@ENTER_DATE"].Value = newRecord.ENTER_DATE;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    return (int) cmd.Parameters["@Sr_No"].Value;
                }
            }
        }
        public static void DeleteCourse_Schedule_Exam_location(int pSr_No)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("DeleteCourse_Schedule_Exam_location", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Sr_No", SqlDbType.Int,10));
                    cmd.Parameters["@Sr_No"].Value = pSr_No;
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static void UpdateCourse_Schedule_Exam_location(TYP_COURSE_SCHEDULE_EXAM_LOCATION newRecord)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("UpdateCourse_Schedule_Exam_location", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Sr_No", SqlDbType.Int,10));
                    cmd.Parameters["@Sr_No"].Value = newRecord.SR_NO;
                    cmd.Parameters.Add(new SqlParameter("@Course_Schedule_ID", SqlDbType.Int,10));
                    cmd.Parameters["@Course_Schedule_ID"].Value = newRecord.COURSE_SCHEDULE_ID;
                    cmd.Parameters.Add(new SqlParameter("@Exam_Location_Code", SqlDbType.Int,10));
                    cmd.Parameters["@Exam_Location_Code"].Value = newRecord.EXAM_LOCATION_CODE;
                    cmd.Parameters.Add(new SqlParameter("@Enter_By", SqlDbType.VarChar,50));
                    if (newRecord.ENTER_BYIsNull)
                        cmd.Parameters["@ENTER_BY"].Value = DBNull.Value;
                    else
                        cmd.Parameters["@ENTER_BY"].Value = newRecord.ENTER_BY;
                    cmd.Parameters.Add(new SqlParameter("@Enter_Date", SqlDbType.DateTime));
                    if (newRecord.ENTER_DATEIsNull)
                        cmd.Parameters["@ENTER_DATE"].Value = DBNull.Value;
                    else
                        cmd.Parameters["@ENTER_DATE"].Value = newRecord.ENTER_DATE;
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static DataSet GetAllCourse_Schedule_Exam_location()
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            DataSet myset = new DataSet();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetAllCourse_Schedule_Exam_location", con))
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
        public static DataSet GetCourse_Schedule_Exam_location(int pSr_No, int pCourse_Schedule_ID, int pExam_Location_Code)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            DataSet myset = new DataSet();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetCourse_Schedule_Exam_location", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Sr_No", SqlDbType.Int,10));
                    cmd.Parameters["@Sr_No"].Value = pSr_No;
                    cmd.Parameters.Add(new SqlParameter("@Course_Schedule_ID", SqlDbType.Int,10));
                    cmd.Parameters["@Course_Schedule_ID"].Value = pCourse_Schedule_ID;
                    cmd.Parameters.Add(new SqlParameter("@Exam_Location_Code", SqlDbType.Int,10));
                    cmd.Parameters["@Exam_Location_Code"].Value = pExam_Location_Code;
                    con.Open();
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                            sda.Fill(myset);
                    }
                }
            }
            return myset;
        }

        //public static MemoryStream Execute(int pSr_No, int pCourse_Schedule_ID, int pExam_Location_Code)
        //{
        //    kanchan.ReportExecutionService myReport = new kanchan.ReportExecutionService();

        //    myReport.Credentials = System.Net.CredentialCache.DefaultCredentials;
        //    myReport.Url = CommonClass.URL;

        //    myReport.LoadReport("/BCASReports/RepCourse_Schedule_Exam_location", null);

        //    kanchan.ParameterValue[] parameters = new kanchan.ParameterValue[2];
        //    parameters[0] = new kanchan.ParameterValue();
        //    parameters[0].Name = "Sr_No";
        //    parameters[0].Value = pSr_No.ToString();
        //    parameters[1] = new kanchan.ParameterValue();
        //    parameters[1].Name = "Course_Schedule_ID";
        //    parameters[1].Value = pCourse_Schedule_ID.ToString();
        //    parameters[2] = new kanchan.ParameterValue();
        //    parameters[2].Name = "Exam_Location_Code";
        //    parameters[2].Value = pExam_Location_Code.ToString();

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
