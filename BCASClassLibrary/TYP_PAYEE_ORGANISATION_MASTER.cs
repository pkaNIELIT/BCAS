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
    public class TYP_PAYEE_ORGANISATION_MASTER
    {
        /// <summary>
        /// private bool m_IsNull; 
        /// To Determine Whether Current object is null or not
        /// </summary>
        private bool m_IsNull;

        /// <summary>
        /// Class Fields...same as in Table
        /// </summary>
        private int m_PAYEE_ORGANISATION_CODE;
        private string m_PAYEE_ORGANISATION_NAME;
        private string m_States;
        private int m_SCode;
        private string m_GSTIN;
        private string m_ENTER_BY;
        private System.DateTime m_ENTER_DATE;
        /// <summary>
        /// Overloaded Constructors of current class
        /// </summary>
        public TYP_PAYEE_ORGANISATION_MASTER()
        {
          // TODO : Add code to initialise the object
        }

        public TYP_PAYEE_ORGANISATION_MASTER(string str)
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

        public static TYP_PAYEE_ORGANISATION_MASTER Null
        {
           get
           {
              TYP_PAYEE_ORGANISATION_MASTER obj = new TYP_PAYEE_ORGANISATION_MASTER();
              obj.m_IsNull = true;
              return obj;
           }
        }

        /// <summary>
        /// Code of various properties
        /// </summary>
        public int PAYEE_ORGANISATION_CODE
        {
           get {return this.m_PAYEE_ORGANISATION_CODE;}
           set {this.m_PAYEE_ORGANISATION_CODE = value;}
        }
        public string PAYEE_ORGANISATION_NAME
        {
           get {return this.m_PAYEE_ORGANISATION_NAME;}
           set {this.m_PAYEE_ORGANISATION_NAME = value;}
        }
        public int SCODE
        {
            get { return this.m_SCode; }
            set { this.m_SCode = value; }
        }
        public string States
        {
            get { return this.m_States; }
            set { this.m_States = value; }
        }
        public string GSTIN
        {
            get { return this.m_GSTIN; }
            set { this.m_GSTIN = value; }
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
        public static int InsertPayee_Organisation_Master(TYP_PAYEE_ORGANISATION_MASTER newRecord)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("InsertPayee_Organisation_Master", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Payee_Organisation_Code", SqlDbType.Int,10));
                    cmd.Parameters["@Payee_Organisation_Code"].Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(new SqlParameter("@Payee_Organisation_Name", SqlDbType.VarChar,100));
                    cmd.Parameters["@Payee_Organisation_Name"].Value = newRecord.PAYEE_ORGANISATION_NAME;
                    cmd.Parameters.Add(new SqlParameter("@SCode", SqlDbType.Int, 10));
                    cmd.Parameters["@SCode"].Value =Convert.ToInt32 ( newRecord .SCODE);
                    cmd.Parameters.Add(new SqlParameter("@States", SqlDbType.VarChar, 50));
                    cmd.Parameters["@States"].Value = newRecord.States;
                    cmd.Parameters.Add(new SqlParameter("@GSTIN", SqlDbType.VarChar, 50));
                    cmd.Parameters["@GSTIN"].Value = newRecord.GSTIN;
                    cmd.Parameters.Add(new SqlParameter("@Enter_By", SqlDbType.VarChar,50));
                    cmd.Parameters["@Enter_By"].Value = newRecord.ENTER_BY;
                    cmd.Parameters.Add(new SqlParameter("@Enter_Date", SqlDbType.DateTime));
                    cmd.Parameters["@Enter_Date"].Value = newRecord.ENTER_DATE;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    return (int) cmd.Parameters["@Payee_Organisation_Code"].Value;
                }
            }
        }
        public static void DeletePayee_Organisation_Master(int pPayee_Organisation_Code)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("DeletePayee_Organisation_Master", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Payee_Organisation_Code", SqlDbType.Int,10));
                    cmd.Parameters["@Payee_Organisation_Code"].Value = pPayee_Organisation_Code;
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static void UpdatePayee_Organisation_Master(TYP_PAYEE_ORGANISATION_MASTER newRecord)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("UpdatePayee_Organisation_Master", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Payee_Organisation_Code", SqlDbType.Int,10));
                    cmd.Parameters["@Payee_Organisation_Code"].Value = newRecord.PAYEE_ORGANISATION_CODE;
                    cmd.Parameters.Add(new SqlParameter("@Payee_Organisation_Name", SqlDbType.VarChar,100));
                    cmd.Parameters["@Payee_Organisation_Name"].Value = newRecord.PAYEE_ORGANISATION_NAME;
                    cmd.Parameters.Add(new SqlParameter("@SCode", SqlDbType.Int, 10));
                    cmd.Parameters["@SCode"].Value = Convert.ToInt32(newRecord.SCODE);
                    cmd.Parameters.Add(new SqlParameter("@States", SqlDbType.VarChar, 50));
                    cmd.Parameters["@States"].Value = newRecord.States;
                    cmd.Parameters.Add(new SqlParameter("@GSTIN", SqlDbType.VarChar, 50));
                    cmd.Parameters["@GSTIN"].Value = newRecord.GSTIN;
                    cmd.Parameters.Add(new SqlParameter("@Enter_By", SqlDbType.VarChar,50));
                    cmd.Parameters["@Enter_By"].Value = newRecord.ENTER_BY;
                    cmd.Parameters.Add(new SqlParameter("@Enter_Date", SqlDbType.DateTime));
                    cmd.Parameters["@Enter_Date"].Value = newRecord.ENTER_DATE;
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static DataSet GetAllPayee_Organisation_Master()
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            DataSet myset = new DataSet();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetAllPayee_Organisation_Master", con))
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
        public static DataSet GetStates()
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            DataSet myset = new DataSet();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("getStates", con))
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
        public static DataSet GetPayee_Organisation_Master(int pPayee_Organisation_Code, string pPayee_Organisation_Name)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            DataSet myset = new DataSet();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetPayee_Organisation_Master", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Payee_Organisation_Code", SqlDbType.Int,10));
                    cmd.Parameters["@Payee_Organisation_Code"].Value = pPayee_Organisation_Code;
                    cmd.Parameters.Add(new SqlParameter("@Payee_Organisation_Name", SqlDbType.VarChar,100));
                    cmd.Parameters["@Payee_Organisation_Name"].Value = pPayee_Organisation_Name;
                    con.Open();
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                            sda.Fill(myset);
                    }
                }
            }
            return myset;
        }

        public static int GetPayee_OrganisationCode(string pOrganisation_Name)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            DataSet myset = new DataSet();
            int Code = 0;
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetPayee_OrganisationCode", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Organisation_Code", SqlDbType.Int, 10));
                    cmd.Parameters["@Organisation_Code"].Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(new SqlParameter("@Organisation_Name", SqlDbType.VarChar, 100));
                    cmd.Parameters["@Organisation_Name"].Value = pOrganisation_Name;

                    con.Open();
                    //cmd.ExecuteScalar();
                    //Code = Convert.ToInt32(cmd.Parameters["@Organisation_Code"].Value);
                    Code = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            return Code;
        }
 

    }
}
