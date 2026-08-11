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
    public class TYP_COUNTRY_MASTER
    {
        /// <summary>
        /// private bool m_IsNull; 
        /// To Determine Whether Current object is null or not
        /// </summary>
        private bool m_IsNull;

        /// <summary>
        /// Class Fields...same as in Table
        /// </summary>
        private int m_COUNTRY_CODE;
        private string m_COUNTRY_NAME;
        private string m_ENTER_BY;
        private bool m_ENTER_BYIsNull;
        private System.DateTime m_ENTER_DATE;
        private bool m_ENTER_DATEIsNull;
        /// <summary>
        /// Overloaded Constructors of current class
        /// </summary>
        public TYP_COUNTRY_MASTER()
        {
          // TODO : Add code to initialise the object
        }

        public TYP_COUNTRY_MASTER(string str)
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

        public static TYP_COUNTRY_MASTER Null
        {
           get
           {
              TYP_COUNTRY_MASTER obj = new TYP_COUNTRY_MASTER();
              obj.m_IsNull = true;
              return obj;
           }
        }

        /// <summary>
        /// Code of various properties
        /// </summary>
        public int COUNTRY_CODE
        {
           get {return this.m_COUNTRY_CODE;}
           set {this.m_COUNTRY_CODE = value;}
        }
        public string COUNTRY_NAME
        {
           get {return this.m_COUNTRY_NAME;}
           set {this.m_COUNTRY_NAME = value;}
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
        public static int InsertCountry_Master(TYP_COUNTRY_MASTER newRecord)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("InsertCountry_Master", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Country_Code", SqlDbType.Int,10));
                    cmd.Parameters["@Country_Code"].Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(new SqlParameter("@Country_Name", SqlDbType.VarChar,50));
                    cmd.Parameters["@Country_Name"].Value = newRecord.COUNTRY_NAME;
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
                    return (int) cmd.Parameters["@Country_Code"].Value;
                }
            }
        }
        public static void DeleteCountry_Master(int pCountry_Code)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("DeleteCountry_Master", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Country_Code", SqlDbType.Int,10));
                    cmd.Parameters["@Country_Code"].Value = pCountry_Code;
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static void UpdateCountry_Master(TYP_COUNTRY_MASTER newRecord)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("UpdateCountry_Master", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Country_Code", SqlDbType.Int,10));
                    cmd.Parameters["@Country_Code"].Value = newRecord.COUNTRY_CODE;
                    cmd.Parameters.Add(new SqlParameter("@Country_Name", SqlDbType.VarChar,50));
                    cmd.Parameters["@Country_Name"].Value = newRecord.COUNTRY_NAME;
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
        public static DataSet GetAllCountry_Master()
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            DataSet myset = new DataSet();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetAllCountry_Master", con))
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
        public static DataSet GetCountry_Master(int pCountry_Code, string pCountry_Name)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            DataSet myset = new DataSet();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetCountry_Master", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Country_Code", SqlDbType.Int,10));
                    cmd.Parameters["@Country_Code"].Value = pCountry_Code;
                    cmd.Parameters.Add(new SqlParameter("@Country_Name", SqlDbType.VarChar,50));
                    cmd.Parameters["@Country_Name"].Value = pCountry_Name;
                    con.Open();
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                            sda.Fill(myset);
                    }
                }
            }
            return myset;
        }

        //public static MemoryStream Execute(int pCountry_Code, string pCountry_Name)
        //{
        //    kanchan.ReportExecutionService myReport = new kanchan.ReportExecutionService();

        //    myReport.Credentials = System.Net.CredentialCache.DefaultCredentials;
        //    myReport.Url = CommonClass.URL;

        //    myReport.LoadReport("/BCASReports/RepCountry_Master", null);

        //    kanchan.ParameterValue[] parameters = new kanchan.ParameterValue[2];
        //    parameters[0] = new kanchan.ParameterValue();
        //    parameters[0].Name = "Country_Code";
        //    parameters[0].Value = pCountry_Code.ToString();
        //    parameters[1] = new kanchan.ParameterValue();
        //    parameters[1].Name = "Country_Name";
        //    parameters[1].Value = pCountry_Name.ToString();

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
