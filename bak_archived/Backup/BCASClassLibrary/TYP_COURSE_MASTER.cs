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
    public class TYP_COURSE_MASTER
    {
        /// <summary>
        /// private bool m_IsNull; 
        /// To Determine Whether Current object is null or not
        /// </summary>
        private bool m_IsNull;

        /// <summary>
        /// Class Fields...same as in Table
        /// </summary>
        private int m_COURSE_CODE;
        private string m_COURSE_NAME;
        private string m_ENTER_BY;
        private System.DateTime m_ENTER_DATE;
        /// <summary>
        /// Overloaded Constructors of current class
        /// </summary>
        public TYP_COURSE_MASTER()
        {
          // TODO : Add code to initialise the object
        }

        public TYP_COURSE_MASTER(string str)
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

        public static TYP_COURSE_MASTER Null
        {
           get
           {
              TYP_COURSE_MASTER obj = new TYP_COURSE_MASTER();
              obj.m_IsNull = true;
              return obj;
           }
        }

        /// <summary>
        /// Code of various properties
        /// </summary>
        public int COURSE_CODE
        {
           get {return this.m_COURSE_CODE;}
           set {this.m_COURSE_CODE = value;}
        }
        public string COURSE_NAME
        {
           get {return this.m_COURSE_NAME;}
           set {this.m_COURSE_NAME = value;}
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
        public static int InsertCourse_Master(TYP_COURSE_MASTER newRecord)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("InsertCourse_Master", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Course_Code", SqlDbType.Int,10));
                    cmd.Parameters["@Course_Code"].Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(new SqlParameter("@Course_Name", SqlDbType.VarChar,100));
                    cmd.Parameters["@Course_Name"].Value = newRecord.COURSE_NAME;
                    cmd.Parameters.Add(new SqlParameter("@Enter_By", SqlDbType.VarChar,50));
                    cmd.Parameters["@Enter_By"].Value = newRecord.ENTER_BY;
                    cmd.Parameters.Add(new SqlParameter("@Enter_Date", SqlDbType.DateTime));
                    cmd.Parameters["@Enter_Date"].Value = newRecord.ENTER_DATE;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    return (int) cmd.Parameters["@Course_Code"].Value;
                }
            }
        }
        public static void DeleteCourse_Master(int pCourse_Code)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("DeleteCourse_Master", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Course_Code", SqlDbType.Int,10));
                    cmd.Parameters["@Course_Code"].Value = pCourse_Code;
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static void UpdateCourse_Master(TYP_COURSE_MASTER newRecord)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("UpdateCourse_Master", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Course_Code", SqlDbType.Int,10));
                    cmd.Parameters["@Course_Code"].Value = newRecord.COURSE_CODE;
                    cmd.Parameters.Add(new SqlParameter("@Course_Name", SqlDbType.VarChar,100));
                    cmd.Parameters["@Course_Name"].Value = newRecord.COURSE_NAME;
                    cmd.Parameters.Add(new SqlParameter("@Enter_By", SqlDbType.VarChar,50));
                    cmd.Parameters["@Enter_By"].Value = newRecord.ENTER_BY;
                    cmd.Parameters.Add(new SqlParameter("@Enter_Date", SqlDbType.DateTime));
                    cmd.Parameters["@Enter_Date"].Value = newRecord.ENTER_DATE;
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static DataSet GetAllCourse_Master()
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            DataSet myset = new DataSet();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetAllCourse_Master", con))
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
        public static DataSet GetCourse_Master(int pCourse_Code, string pCourse_Name)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            DataSet myset = new DataSet();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetCourse_Master", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Course_Code", SqlDbType.Int,10));
                    cmd.Parameters["@Course_Code"].Value = pCourse_Code;
                    cmd.Parameters.Add(new SqlParameter("@Course_Name", SqlDbType.VarChar,100));
                    cmd.Parameters["@Course_Name"].Value = pCourse_Name;
                    con.Open();
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                            sda.Fill(myset);
                    }
                }
            }
            return myset;
        }

        //public static MemoryStream Execute(int pCourse_Code, string pCourse_Name)
        //{
        //    kanchan.ReportExecutionService myReport = new kanchan.ReportExecutionService();

        //    myReport.Credentials = System.Net.CredentialCache.DefaultCredentials;
        //    myReport.Url = CommonClass.URL;

        //    myReport.LoadReport("/BCASReports/RepCourse_Master", null);

        //    kanchan.ParameterValue[] parameters = new kanchan.ParameterValue[2];
        //    parameters[0] = new kanchan.ParameterValue();
        //    parameters[0].Name = "Course_Code";
        //    parameters[0].Value = pCourse_Code.ToString();
        //    parameters[1] = new kanchan.ParameterValue();
        //    parameters[1].Name = "Course_Name";
        //    parameters[1].Value = pCourse_Name.ToString();

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
