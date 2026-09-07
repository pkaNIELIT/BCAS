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
    public class TYP_EXAMLOCATIONMASTER
    {
        /// <summary>
        /// private bool m_IsNull; 
        /// To Determine Whether Current object is null or not
        /// </summary>
        private bool m_IsNull;

        /// <summary>
        /// Class Fields...same as in Table
        /// </summary>
        private int m_EXAM_LOCATION_CODE;
        private string m_EXAM_LOCATION_NAME;
        private string m_ENTER_BY;
        private bool m_ENTER_BYIsNull;
        private System.DateTime m_ENTER_DATE;
        private bool m_ENTER_DATEIsNull;
        /// <summary>
        /// Overloaded Constructors of current class
        /// </summary>
        public TYP_EXAMLOCATIONMASTER()
        {
          // TODO : Add code to initialise the object
        }

        public TYP_EXAMLOCATIONMASTER(string str)
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

        public static TYP_EXAMLOCATIONMASTER Null
        {
           get
           {
              TYP_EXAMLOCATIONMASTER obj = new TYP_EXAMLOCATIONMASTER();
              obj.m_IsNull = true;
              return obj;
           }
        }

        /// <summary>
        /// Code of various properties
        /// </summary>
        public int EXAM_LOCATION_CODE
        {
           get {return this.m_EXAM_LOCATION_CODE;}
           set {this.m_EXAM_LOCATION_CODE = value;}
        }
        public string EXAM_LOCATION_NAME
        {
           get {return this.m_EXAM_LOCATION_NAME;}
           set {this.m_EXAM_LOCATION_NAME = value;}
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
        public static int InsertExamLocationMaster(TYP_EXAMLOCATIONMASTER newRecord)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("InsertExamLocationMaster", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Exam_Location_Code", SqlDbType.Int,10));
                    cmd.Parameters["@Exam_Location_Code"].Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(new SqlParameter("@Exam_Location_Name", SqlDbType.VarChar,200));
                    cmd.Parameters["@Exam_Location_Name"].Value = newRecord.EXAM_LOCATION_NAME;
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
                    return (int) cmd.Parameters["@Exam_Location_Code"].Value;
                }
            }
        }
        public static void DeleteExamLocationMaster(int pExam_Location_Code)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("DeleteExamLocationMaster", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Exam_Location_Code", SqlDbType.Int,10));
                    cmd.Parameters["@Exam_Location_Code"].Value = pExam_Location_Code;
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static void UpdateExamLocationMaster(TYP_EXAMLOCATIONMASTER newRecord)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("UpdateExamLocationMaster", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Exam_Location_Code", SqlDbType.Int,10));
                    cmd.Parameters["@Exam_Location_Code"].Value = newRecord.EXAM_LOCATION_CODE;
                    cmd.Parameters.Add(new SqlParameter("@Exam_Location_Name", SqlDbType.VarChar,200));
                    cmd.Parameters["@Exam_Location_Name"].Value = newRecord.EXAM_LOCATION_NAME;
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
        public static DataSet GetAllExamLocationMaster()
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            DataSet myset = new DataSet();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetAllExamLocationMaster", con))
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
        public static DataSet GetExamLocationMaster(int pExam_Location_Code, string pExam_Location_Name)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            DataSet myset = new DataSet();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetExamLocationMaster", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Exam_Location_Code", SqlDbType.Int,10));
                    cmd.Parameters["@Exam_Location_Code"].Value = pExam_Location_Code;
                    cmd.Parameters.Add(new SqlParameter("@Exam_Location_Name", SqlDbType.VarChar,200));
                    cmd.Parameters["@Exam_Location_Name"].Value = pExam_Location_Name;
                    con.Open();
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                            sda.Fill(myset);
                    }
                }
            }
            return myset;
        }

        //public static MemoryStream Execute(int pExam_Location_Code, string pExam_Location_Name)
        //{
        //    kanchan.ReportExecutionService myReport = new kanchan.ReportExecutionService();

        //    myReport.Credentials = System.Net.CredentialCache.DefaultCredentials;
        //    myReport.Url = CommonClass.URL;

        //    myReport.LoadReport("/BCASReports/RepExamLocationMaster", null);

        //    kanchan.ParameterValue[] parameters = new kanchan.ParameterValue[2];
        //    parameters[0] = new kanchan.ParameterValue();
        //    parameters[0].Name = "Exam_Location_Code";
        //    parameters[0].Value = pExam_Location_Code.ToString();
        //    parameters[1] = new kanchan.ParameterValue();
        //    parameters[1].Name = "Exam_Location_Name";
        //    parameters[1].Value = pExam_Location_Name.ToString();

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
