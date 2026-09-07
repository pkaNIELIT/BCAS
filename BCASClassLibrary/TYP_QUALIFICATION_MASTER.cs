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
    public class TYP_QUALIFICATION_MASTER
    {
        /// <summary>
        /// private bool m_IsNull; 
        /// To Determine Whether Current object is null or not
        /// </summary>
        private bool m_IsNull;

        /// <summary>
        /// Class Fields...same as in Table
        /// </summary>
        private int m_QUALIFICATION_CODE;
        private string m_DESCRIPTION;
        private string m_ENTER_BY;
        private System.DateTime m_ENTER_DATE;
        /// <summary>
        /// Overloaded Constructors of current class
        /// </summary>
        public TYP_QUALIFICATION_MASTER()
        {
          // TODO : Add code to initialise the object
        }

        public TYP_QUALIFICATION_MASTER(string str)
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

        public static TYP_QUALIFICATION_MASTER Null
        {
           get
           {
              TYP_QUALIFICATION_MASTER obj = new TYP_QUALIFICATION_MASTER();
              obj.m_IsNull = true;
              return obj;
           }
        }

        /// <summary>
        /// Code of various properties
        /// </summary>
        public int QUALIFICATION_CODE
        {
           get {return this.m_QUALIFICATION_CODE;}
           set {this.m_QUALIFICATION_CODE = value;}
        }
        public string DESCRIPTION
        {
           get {return this.m_DESCRIPTION;}
           set {this.m_DESCRIPTION = value;}
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
        public static int InsertQualification_Master(TYP_QUALIFICATION_MASTER newRecord)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("InsertQualification_Master", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Qualification_Code", SqlDbType.Int,10));
                    cmd.Parameters["@Qualification_Code"].Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(new SqlParameter("@Description", SqlDbType.VarChar,50));
                    cmd.Parameters["@Description"].Value = newRecord.DESCRIPTION;
                    cmd.Parameters.Add(new SqlParameter("@Enter_By", SqlDbType.VarChar,50));
                    cmd.Parameters["@Enter_By"].Value = newRecord.ENTER_BY;
                    cmd.Parameters.Add(new SqlParameter("@Enter_Date", SqlDbType.DateTime));
                    cmd.Parameters["@Enter_Date"].Value = newRecord.ENTER_DATE;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    return (int) cmd.Parameters["@Qualification_Code"].Value;
                }
            }
        }
        public static void DeleteQualification_Master(int pQualification_Code)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("DeleteQualification_Master", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Qualification_Code", SqlDbType.Int,10));
                    cmd.Parameters["@Qualification_Code"].Value = pQualification_Code;
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static void UpdateQualification_Master(TYP_QUALIFICATION_MASTER newRecord)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("UpdateQualification_Master", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Qualification_Code", SqlDbType.Int,10));
                    cmd.Parameters["@Qualification_Code"].Value = newRecord.QUALIFICATION_CODE;
                    cmd.Parameters.Add(new SqlParameter("@Description", SqlDbType.VarChar,50));
                    cmd.Parameters["@Description"].Value = newRecord.DESCRIPTION;
                    cmd.Parameters.Add(new SqlParameter("@Enter_By", SqlDbType.VarChar,50));
                    cmd.Parameters["@Enter_By"].Value = newRecord.ENTER_BY;
                    cmd.Parameters.Add(new SqlParameter("@Enter_Date", SqlDbType.DateTime));
                    cmd.Parameters["@Enter_Date"].Value = newRecord.ENTER_DATE;
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static DataSet GetAllQualification_Master()
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            DataSet myset = new DataSet();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetAllQualification_Master", con))
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
        public static DataSet GetQualification_Master(int pQualification_Code, string pDescription)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            DataSet myset = new DataSet();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetQualification_Master", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Qualification_Code", SqlDbType.Int,10));
                    cmd.Parameters["@Qualification_Code"].Value = pQualification_Code;
                    cmd.Parameters.Add(new SqlParameter("@Description", SqlDbType.VarChar,50));
                    cmd.Parameters["@Description"].Value = pDescription;
                    con.Open();
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                            sda.Fill(myset);
                    }
                }
            }
            return myset;
        }

        //public static MemoryStream Execute(int pQualification_Code, string pDescription)
        //{
        //    kanchan.ReportExecutionService myReport = new kanchan.ReportExecutionService();

        //    myReport.Credentials = System.Net.CredentialCache.DefaultCredentials;
        //    myReport.Url = CommonClass.URL;

        //    myReport.LoadReport("/BCASReports/RepQualification_Master", null);

        //    kanchan.ParameterValue[] parameters = new kanchan.ParameterValue[2];
        //    parameters[0] = new kanchan.ParameterValue();
        //    parameters[0].Name = "Qualification_Code";
        //    parameters[0].Value = pQualification_Code.ToString();
        //    parameters[1] = new kanchan.ParameterValue();
        //    parameters[1].Name = "Description";
        //    parameters[1].Value = pDescription.ToString();

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
