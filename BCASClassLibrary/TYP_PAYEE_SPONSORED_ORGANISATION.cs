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
    public class TYP_PAYEE_SPONSORED_ORGANISATION
    {
        /// <summary>
        /// private bool m_IsNull; 
        /// To Determine Whether Current object is null or not
        /// </summary>
        private bool m_IsNull;

        /// <summary>
        /// Class Fields...same as in Table
        /// </summary>
        private int m_S_NO;
        private int m_SPONSORING_ORGANISATION_CODE;
        private int m_PAYEE_ORGANISATION_CODE;
        private string m_ENTER_BY;
        private System.DateTime m_ENTER_DATE;
        /// <summary>
        /// Overloaded Constructors of current class
        /// </summary>
        public TYP_PAYEE_SPONSORED_ORGANISATION()
        {
          // TODO : Add code to initialise the object
        }

        public TYP_PAYEE_SPONSORED_ORGANISATION(string str)
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

        public static TYP_PAYEE_SPONSORED_ORGANISATION Null
        {
           get
           {
              TYP_PAYEE_SPONSORED_ORGANISATION obj = new TYP_PAYEE_SPONSORED_ORGANISATION();
              obj.m_IsNull = true;
              return obj;
           }
        }

        /// <summary>
        /// Code of various properties
        /// </summary>
        public int S_NO
        {
           get {return this.m_S_NO;}
           set {this.m_S_NO = value;}
        }
        public int SPONSORING_ORGANISATION_CODE
        {
           get {return this.m_SPONSORING_ORGANISATION_CODE;}
           set {this.m_SPONSORING_ORGANISATION_CODE = value;}
        }
        public int PAYEE_ORGANISATION_CODE
        {
           get {return this.m_PAYEE_ORGANISATION_CODE;}
           set {this.m_PAYEE_ORGANISATION_CODE = value;}
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
        public static int InsertPayee_Sponsored_Organisation(TYP_PAYEE_SPONSORED_ORGANISATION newRecord)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("InsertPayee_Sponsored_Organisation", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@S_No", SqlDbType.Int,10));
                    cmd.Parameters["@S_No"].Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(new SqlParameter("@Sponsoring_Organisation_Code", SqlDbType.Int,10));
                    cmd.Parameters["@Sponsoring_Organisation_Code"].Value = newRecord.SPONSORING_ORGANISATION_CODE;
                    cmd.Parameters.Add(new SqlParameter("@Payee_Organisation_Code", SqlDbType.Int,10));
                    cmd.Parameters["@Payee_Organisation_Code"].Value = newRecord.PAYEE_ORGANISATION_CODE;
                    cmd.Parameters.Add(new SqlParameter("@Enter_By", SqlDbType.VarChar,50));
                    cmd.Parameters["@Enter_By"].Value = newRecord.ENTER_BY;
                    cmd.Parameters.Add(new SqlParameter("@Enter_Date", SqlDbType.DateTime));
                    cmd.Parameters["@Enter_Date"].Value = newRecord.ENTER_DATE;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    return (int) cmd.Parameters["@S_No"].Value;
                }
            }
        }
        public static void DeletePayee_Sponsored_Organisation(int pS_No)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("DeletePayee_Sponsored_Organisation", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@S_No", SqlDbType.Int,10));
                    cmd.Parameters["@S_No"].Value = pS_No;
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static void UpdatePayee_Sponsored_Organisation(TYP_PAYEE_SPONSORED_ORGANISATION newRecord)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("UpdatePayee_Sponsored_Organisation", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@S_No", SqlDbType.Int,10));
                    cmd.Parameters["@S_No"].Value = newRecord.S_NO;
                    cmd.Parameters.Add(new SqlParameter("@Sponsoring_Organisation_Code", SqlDbType.Int,10));
                    cmd.Parameters["@Sponsoring_Organisation_Code"].Value = newRecord.SPONSORING_ORGANISATION_CODE;
                    cmd.Parameters.Add(new SqlParameter("@Payee_Organisation_Code", SqlDbType.Int,10));
                    cmd.Parameters["@Payee_Organisation_Code"].Value = newRecord.PAYEE_ORGANISATION_CODE;
                    cmd.Parameters.Add(new SqlParameter("@Enter_By", SqlDbType.VarChar,50));
                    cmd.Parameters["@Enter_By"].Value = newRecord.ENTER_BY;
                    cmd.Parameters.Add(new SqlParameter("@Enter_Date", SqlDbType.DateTime));
                    cmd.Parameters["@Enter_Date"].Value = newRecord.ENTER_DATE;
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static DataSet GetAllPayee_Sponsored_Organisation()
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            DataSet myset = new DataSet();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetAllPayee_Sponsored_Organisation", con))
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
        public static DataSet GetPayee_Sponsored_Organisation(int pS_No, int pSponsoring_Organisation_Code, int pPayee_Organisation_Code)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            DataSet myset = new DataSet();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetPayee_Sponsored_Organisation", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@S_No", SqlDbType.Int,10));
                    cmd.Parameters["@S_No"].Value = pS_No;
                    cmd.Parameters.Add(new SqlParameter("@Sponsoring_Organisation_Code", SqlDbType.Int,10));
                    cmd.Parameters["@Sponsoring_Organisation_Code"].Value = pSponsoring_Organisation_Code;
                    cmd.Parameters.Add(new SqlParameter("@Payee_Organisation_Code", SqlDbType.Int,10));
                    cmd.Parameters["@Payee_Organisation_Code"].Value = pPayee_Organisation_Code;
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
