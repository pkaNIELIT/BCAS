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
    public class TYP_WORKING_EXPERIENCE
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
        private string m_ORGANISATION;
        private string m_DESIGNATION;
        private System.DateTime m_DATE_FROM;
        private System.DateTime m_DATE_TO;
        private string m_REMARKS;
        private bool m_REMARKSIsNull;
        private string m_ENTER_BY;
        private System.DateTime m_ENTER_DATE;
        /// <summary>
        /// Overloaded Constructors of current class
        /// </summary>
        public TYP_WORKING_EXPERIENCE()
        {
          // TODO : Add code to initialise the object
        }

        public TYP_WORKING_EXPERIENCE(string str)
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

        public static TYP_WORKING_EXPERIENCE Null
        {
           get
           {
              TYP_WORKING_EXPERIENCE obj = new TYP_WORKING_EXPERIENCE();
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
        public string ORGANISATION
        {
           get {return this.m_ORGANISATION;}
           set {this.m_ORGANISATION = value;}
        }
        public string DESIGNATION
        {
           get {return this.m_DESIGNATION;}
           set {this.m_DESIGNATION = value;}
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
        public static int InsertWorking_Experience(TYP_WORKING_EXPERIENCE newRecord)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("InsertWorking_Experience", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Sr_no", SqlDbType.Int,10));
                    cmd.Parameters["@Sr_no"].Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(new SqlParameter("@Registration_Number", SqlDbType.Int,10));
                    cmd.Parameters["@Registration_Number"].Value = newRecord.REGISTRATION_NUMBER;
                    cmd.Parameters.Add(new SqlParameter("@Organisation", SqlDbType.VarChar,100));
                    cmd.Parameters["@Organisation"].Value = newRecord.ORGANISATION;
                    cmd.Parameters.Add(new SqlParameter("@Designation", SqlDbType.VarChar,50));
                    cmd.Parameters["@Designation"].Value = newRecord.DESIGNATION;
                    cmd.Parameters.Add(new SqlParameter("@Date_From", SqlDbType.DateTime));
                    cmd.Parameters["@Date_From"].Value = newRecord.DATE_FROM;
                    cmd.Parameters.Add(new SqlParameter("@Date_To", SqlDbType.DateTime));
                    cmd.Parameters["@Date_To"].Value = newRecord.DATE_TO;
                    cmd.Parameters.Add(new SqlParameter("@Remarks", SqlDbType.VarChar,100));
                    if (newRecord.REMARKSIsNull)
                        cmd.Parameters["@REMARKS"].Value = DBNull.Value;
                    else
                        cmd.Parameters["@REMARKS"].Value = newRecord.REMARKS;
                    cmd.Parameters.Add(new SqlParameter("@Enter_By", SqlDbType.VarChar,50));
                    cmd.Parameters["@Enter_By"].Value = newRecord.ENTER_BY;
                    cmd.Parameters.Add(new SqlParameter("@Enter_date", SqlDbType.DateTime));
                    cmd.Parameters["@Enter_date"].Value = newRecord.ENTER_DATE;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    return (int) cmd.Parameters["@Sr_no"].Value;
                }
            }
        }
        public static void DeleteWorking_Experience(int pSr_no)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("DeleteWorking_Experience", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Sr_no", SqlDbType.Int,10));
                    cmd.Parameters["@Sr_no"].Value = pSr_no;
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static void UpdateWorking_Experience(TYP_WORKING_EXPERIENCE newRecord)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("UpdateWorking_Experience", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Sr_no", SqlDbType.Int,10));
                    cmd.Parameters["@Sr_no"].Value = newRecord.SR_NO;
                    cmd.Parameters.Add(new SqlParameter("@Registration_Number", SqlDbType.Int,10));
                    cmd.Parameters["@Registration_Number"].Value = newRecord.REGISTRATION_NUMBER;
                    cmd.Parameters.Add(new SqlParameter("@Organisation", SqlDbType.VarChar,100));
                    cmd.Parameters["@Organisation"].Value = newRecord.ORGANISATION;
                    cmd.Parameters.Add(new SqlParameter("@Designation", SqlDbType.VarChar,50));
                    cmd.Parameters["@Designation"].Value = newRecord.DESIGNATION;
                    cmd.Parameters.Add(new SqlParameter("@Date_From", SqlDbType.DateTime));
                    cmd.Parameters["@Date_From"].Value = newRecord.DATE_FROM;
                    cmd.Parameters.Add(new SqlParameter("@Date_To", SqlDbType.DateTime));
                    cmd.Parameters["@Date_To"].Value = newRecord.DATE_TO;
                    cmd.Parameters.Add(new SqlParameter("@Remarks", SqlDbType.VarChar,100));
                    if (newRecord.REMARKSIsNull)
                        cmd.Parameters["@REMARKS"].Value = DBNull.Value;
                    else
                        cmd.Parameters["@REMARKS"].Value = newRecord.REMARKS;
                    cmd.Parameters.Add(new SqlParameter("@Enter_By", SqlDbType.VarChar,50));
                    cmd.Parameters["@Enter_By"].Value = newRecord.ENTER_BY;
                    cmd.Parameters.Add(new SqlParameter("@Enter_date", SqlDbType.DateTime));
                    cmd.Parameters["@Enter_date"].Value = newRecord.ENTER_DATE;
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static DataSet GetAllWorking_Experience()
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            DataSet myset = new DataSet();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetAllWorking_Experience", con))
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
        public static DataSet GetWorking_Experience(int pSr_no, int pRegistration_Number, string pOrganisation, string pDesignation, System.DateTime pDate_From, System.DateTime pDate_To, string pRemarks)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            DataSet myset = new DataSet();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetWorking_Experience", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Sr_no", SqlDbType.Int,10));
                    cmd.Parameters["@Sr_no"].Value = pSr_no;
                    cmd.Parameters.Add(new SqlParameter("@Registration_Number", SqlDbType.Int,10));
                    cmd.Parameters["@Registration_Number"].Value = pRegistration_Number;
                    cmd.Parameters.Add(new SqlParameter("@Organisation", SqlDbType.VarChar,100));
                    cmd.Parameters["@Organisation"].Value = pOrganisation;
                    cmd.Parameters.Add(new SqlParameter("@Designation", SqlDbType.VarChar,50));
                    cmd.Parameters["@Designation"].Value = pDesignation;
                    cmd.Parameters.Add(new SqlParameter("@Date_From", SqlDbType.DateTime));
                    cmd.Parameters["@Date_From"].Value = pDate_From;
                    cmd.Parameters.Add(new SqlParameter("@Date_To", SqlDbType.DateTime));
                    cmd.Parameters["@Date_To"].Value = pDate_To;
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

        

    }
}
