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
    public class TYP_SPONSORING_ORGANISATION_MASTER
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
        private string m_ORGANISATION_NAME;
        private string m_ORGANISATION_ADDRESS;
        private string m_ENTER_BY;
        private bool m_ENTER_BYIsNull;
        private System.DateTime m_ENTER_DATE;
        private bool m_ENTER_DATEIsNull;
        /// <summary>
        /// Overloaded Constructors of current class
        /// </summary>
        public TYP_SPONSORING_ORGANISATION_MASTER()
        {
          // TODO : Add code to initialise the object
        }

        public TYP_SPONSORING_ORGANISATION_MASTER(string str)
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

        public static TYP_SPONSORING_ORGANISATION_MASTER Null
        {
           get
           {
              TYP_SPONSORING_ORGANISATION_MASTER obj = new TYP_SPONSORING_ORGANISATION_MASTER();
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
        public string ORGANISATION_NAME
        {
           get {return this.m_ORGANISATION_NAME;}
           set {this.m_ORGANISATION_NAME = value;}
        }
        public string ORGANISATION_ADDRESS
        {
           get {return this.m_ORGANISATION_ADDRESS;}
           set {this.m_ORGANISATION_ADDRESS = value;}
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
        public static int InsertSponsoring_Organisation_Master(TYP_SPONSORING_ORGANISATION_MASTER newRecord)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("InsertSponsoring_Organisation_Master", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Organisation_Code", SqlDbType.Int,10));
                    cmd.Parameters["@Organisation_Code"].Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(new SqlParameter("@Organisation_Name", SqlDbType.VarChar,100));
                    cmd.Parameters["@Organisation_Name"].Value = newRecord.ORGANISATION_NAME;
                    cmd.Parameters.Add(new SqlParameter("@Organisation_Address", SqlDbType.VarChar,500));
                    cmd.Parameters["@Organisation_Address"].Value = newRecord.ORGANISATION_ADDRESS;
                    cmd.Parameters.Add(new SqlParameter("@Enter_by", SqlDbType.VarChar,50));
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
                    return (int) cmd.Parameters["@Organisation_Code"].Value;
                }
            }
        }
        public static void DeleteSponsoring_Organisation_Master(int pOrganisation_Code)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("DeleteSponsoring_Organisation_Master", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Organisation_Code", SqlDbType.Int,10));
                    cmd.Parameters["@Organisation_Code"].Value = pOrganisation_Code;
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static void UpdateSponsoring_Organisation_Master(TYP_SPONSORING_ORGANISATION_MASTER newRecord)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("UpdateSponsoring_Organisation_Master", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Organisation_Code", SqlDbType.Int,10));
                    cmd.Parameters["@Organisation_Code"].Value = newRecord.ORGANISATION_CODE;
                    cmd.Parameters.Add(new SqlParameter("@Organisation_Name", SqlDbType.VarChar,100));
                    cmd.Parameters["@Organisation_Name"].Value = newRecord.ORGANISATION_NAME;
                    cmd.Parameters.Add(new SqlParameter("@Organisation_Address", SqlDbType.VarChar,500));
                    cmd.Parameters["@Organisation_Address"].Value = newRecord.ORGANISATION_ADDRESS;
                    cmd.Parameters.Add(new SqlParameter("@Enter_by", SqlDbType.VarChar,50));
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
        public static DataSet GetAllSponsoring_Organisation_Master()
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            DataSet myset = new DataSet();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetAllSponsoring_Organisation_Master", con))
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
        public static DataSet GetSponsoring_Organisation_Master(int pOrganisation_Code, string pOrganisation_Name, string pOrganisation_Address)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            DataSet myset = new DataSet();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetSponsoring_Organisation_Master", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Organisation_Code", SqlDbType.Int,10));
                    cmd.Parameters["@Organisation_Code"].Value = pOrganisation_Code;
                    cmd.Parameters.Add(new SqlParameter("@Organisation_Name", SqlDbType.VarChar,100));
                    cmd.Parameters["@Organisation_Name"].Value = pOrganisation_Name;
                    cmd.Parameters.Add(new SqlParameter("@Organisation_Address", SqlDbType.VarChar,500));
                    cmd.Parameters["@Organisation_Address"].Value = pOrganisation_Address;
                    con.Open();
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                            sda.Fill(myset);
                    }
                }
            }
            return myset;
        }
        public static int GetSponsoring_OrganisationCode(string pOrganisation_Name)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            DataSet myset = new DataSet();
            int Code = 0;
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetSponsoring_OrganisationCode", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Organisation_Code", SqlDbType.Int, 10));
                    cmd.Parameters["@Organisation_Code"].Direction=ParameterDirection .Output ;
                    cmd.Parameters.Add(new SqlParameter("@Organisation_Name", SqlDbType.VarChar, 100));
                    cmd.Parameters["@Organisation_Name"].Value = pOrganisation_Name;
                  
                    con.Open();
                    Code=Convert.ToInt32 (cmd.ExecuteScalar());
                }
            }
            return Code;
        }

        //public static MemoryStream Execute(int pOrganisation_Code, string pOrganisation_Name, string pOrganisation_Address)
        //{
        //    kanchan.ReportExecutionService myReport = new kanchan.ReportExecutionService();

        //    myReport.Credentials = System.Net.CredentialCache.DefaultCredentials;
        //    myReport.Url = CommonClass.URL;

        //    myReport.LoadReport("/RSBReports/RepSponsoring_Organisation_Master", null);

        //    kanchan.ParameterValue[] parameters = new kanchan.ParameterValue[2];
        //    parameters[0] = new kanchan.ParameterValue();
        //    parameters[0].Name = "Organisation_Code";
        //    parameters[0].Value = pOrganisation_Code.ToString();
        //    parameters[1] = new kanchan.ParameterValue();
        //    parameters[1].Name = "Organisation_Name";
        //    parameters[1].Value = pOrganisation_Name.ToString();
        //    parameters[2] = new kanchan.ParameterValue();
        //    parameters[2].Name = "Organisation_Address";
        //    parameters[2].Value = pOrganisation_Address.ToString();

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
