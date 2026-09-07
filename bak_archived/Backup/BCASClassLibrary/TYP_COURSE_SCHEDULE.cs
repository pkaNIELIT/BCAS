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
    public class TYP_COURSE_SCHEDULE
    {
        /// <summary>
        /// private bool m_IsNull; 
        /// To Determine Whether Current object is null or not
        /// </summary>
        private bool m_IsNull;

        /// <summary>
        /// Class Fields...same as in Table
        /// </summary>
        private int m_SCHEDULE_ID;
        private int m_COURSE_CODE;
        private System.DateTime m_START_DATE;
        private System.DateTime m_END_DATE;
        private System.DateTime m_DATE_OF_EXAM;
        private System.DateTime m_LAST_DATE_FOR_NOMINATION;
        private System.DateTime m_LAST_DATE_FOR_BCAS_ACCEPTANCE;
        private System.DateTime m_LAST_DATE_FOR_PAYMENTS;
        private System.DateTime m_LAST_DATE_FOR_NOMINATION_REAPPEARING;
        private System.DateTime m_LAST_DATE_FOR_REAPPEARING_FEE_PAYMENTS;
        private string m_ENTER_BY;
        private System.DateTime m_ENTER_DATE;
        /// <summary>
        /// Overloaded Constructors of current class
        /// </summary>
        public TYP_COURSE_SCHEDULE()
        {
          // TODO : Add code to initialise the object
        }

        public TYP_COURSE_SCHEDULE(string str)
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

        public static TYP_COURSE_SCHEDULE Null
        {
           get
           {
              TYP_COURSE_SCHEDULE obj = new TYP_COURSE_SCHEDULE();
              obj.m_IsNull = true;
              return obj;
           }
        }

        /// <summary>
        /// Code of various properties
        /// </summary>
        public int SCHEDULE_ID
        {
           get {return this.m_SCHEDULE_ID;}
           set {this.m_SCHEDULE_ID = value;}
        }
        public int COURSE_CODE
        {
           get {return this.m_COURSE_CODE;}
           set {this.m_COURSE_CODE = value;}
        }
        public System.DateTime START_DATE
        {
           get {return this.m_START_DATE;}
           set {this.m_START_DATE = value;}
        }
        public System.DateTime END_DATE
        {
           get {return this.m_END_DATE;}
           set {this.m_END_DATE = value;}
        }
        public System.DateTime DATE_OF_EXAM
        {
           get {return this.m_DATE_OF_EXAM;}
           set {this.m_DATE_OF_EXAM = value;}
        }
        public System.DateTime LAST_DATE_FOR_NOMINATION
        {
           get {return this.m_LAST_DATE_FOR_NOMINATION;}
           set {this.m_LAST_DATE_FOR_NOMINATION = value;}
        }
        public System.DateTime LAST_DATE_FOR_BCAS_ACCEPTANCE
        {
           get {return this.m_LAST_DATE_FOR_BCAS_ACCEPTANCE;}
           set {this.m_LAST_DATE_FOR_BCAS_ACCEPTANCE = value;}
        }
        public System.DateTime LAST_DATE_FOR_PAYMENTS
        {
           get {return this.m_LAST_DATE_FOR_PAYMENTS;}
           set {this.m_LAST_DATE_FOR_PAYMENTS = value;}
        }
        public System.DateTime LAST_DATE_FOR_NOMINATION_REAPPEARING
        {
           get {return this.m_LAST_DATE_FOR_NOMINATION_REAPPEARING;}
           set {this.m_LAST_DATE_FOR_NOMINATION_REAPPEARING = value;}
        }
        public System.DateTime LAST_DATE_FOR_REAPPEARING_FEE_PAYMENTS
        {
           get {return this.m_LAST_DATE_FOR_REAPPEARING_FEE_PAYMENTS;}
           set {this.m_LAST_DATE_FOR_REAPPEARING_FEE_PAYMENTS = value;}
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

        public static int InsertCourse_Schedule_Exam_location(TYP_COURSE_SCHEDULE newRecord)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("InsertCourse_Schedule_Exam_location", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Sr_No", SqlDbType.Int,10));
                    cmd.Parameters.Add(new SqlParameter("@Course_Schedule_ID", SqlDbType.Int,10));
                    cmd.Parameters.Add(new SqlParameter("@Exam_Location_Code", SqlDbType.Int,10));
                    cmd.Parameters.Add(new SqlParameter("@Enter_By", SqlDbType.VarChar,50));
                    cmd.Parameters["@Enter_By"].Value = newRecord.ENTER_BY;
                    cmd.Parameters.Add(new SqlParameter("@Enter_Date", SqlDbType.DateTime));
                    cmd.Parameters["@Enter_Date"].Value = newRecord.ENTER_DATE;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    return (int) cmd.Parameters["@Schedule_ID"].Value;
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

        public static void UpdateCourse_Schedule_Exam_location(TYP_COURSE_SCHEDULE newRecord)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("UpdateCourse_Schedule_Exam_location", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Sr_No", SqlDbType.Int,10));
                    cmd.Parameters.Add(new SqlParameter("@Course_Schedule_ID", SqlDbType.Int,10));
                    cmd.Parameters.Add(new SqlParameter("@Exam_Location_Code", SqlDbType.Int,10));
                    cmd.Parameters.Add(new SqlParameter("@Enter_By", SqlDbType.VarChar,50));
                    cmd.Parameters["@Enter_By"].Value = newRecord.ENTER_BY;
                    cmd.Parameters.Add(new SqlParameter("@Enter_Date", SqlDbType.DateTime));
                    cmd.Parameters["@Enter_Date"].Value = newRecord.ENTER_DATE;
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

        public static int InsertCourse_Schedule(TYP_COURSE_SCHEDULE newRecord)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("InsertCourse_Schedule", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Schedule_ID", SqlDbType.Int,10));
                    cmd.Parameters["@Schedule_ID"].Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(new SqlParameter("@Course_Code", SqlDbType.Int,10));
                    cmd.Parameters["@Course_Code"].Value = newRecord.COURSE_CODE;
                    cmd.Parameters.Add(new SqlParameter("@Start_Date", SqlDbType.DateTime));
                    cmd.Parameters["@Start_Date"].Value = newRecord.START_DATE;
                    cmd.Parameters.Add(new SqlParameter("@End_Date", SqlDbType.DateTime));
                    cmd.Parameters["@End_Date"].Value = newRecord.END_DATE;
                    cmd.Parameters.Add(new SqlParameter("@Date_of_Exam", SqlDbType.DateTime));
                    cmd.Parameters["@Date_of_Exam"].Value = newRecord.DATE_OF_EXAM;
                    cmd.Parameters.Add(new SqlParameter("@Last_date_for_nomination", SqlDbType.DateTime));
                    cmd.Parameters["@Last_date_for_nomination"].Value = newRecord.LAST_DATE_FOR_NOMINATION;
                    cmd.Parameters.Add(new SqlParameter("@Last_date_for_bcas_acceptance", SqlDbType.DateTime));
                    cmd.Parameters["@Last_date_for_bcas_acceptance"].Value = newRecord.LAST_DATE_FOR_BCAS_ACCEPTANCE;
                    cmd.Parameters.Add(new SqlParameter("@Last_date_for_payments", SqlDbType.DateTime));
                    cmd.Parameters["@Last_date_for_payments"].Value = newRecord.LAST_DATE_FOR_PAYMENTS;
                    cmd.Parameters.Add(new SqlParameter("@Last_date_for_nomination_reappearing", SqlDbType.DateTime));
                    cmd.Parameters["@Last_date_for_nomination_reappearing"].Value = newRecord.LAST_DATE_FOR_NOMINATION_REAPPEARING;
                    cmd.Parameters.Add(new SqlParameter("@Last_date_for_reappearing_fee_payments", SqlDbType.DateTime));
                    cmd.Parameters["@Last_date_for_reappearing_fee_payments"].Value = newRecord.LAST_DATE_FOR_REAPPEARING_FEE_PAYMENTS;
                    cmd.Parameters.Add(new SqlParameter("@Enter_By", SqlDbType.VarChar,50));
                    cmd.Parameters["@Enter_By"].Value = newRecord.ENTER_BY;
                    cmd.Parameters.Add(new SqlParameter("@Enter_date", SqlDbType.DateTime));
                    cmd.Parameters["@Enter_date"].Value = newRecord.ENTER_DATE;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    return (int) cmd.Parameters["@Schedule_ID"].Value;
                }
            }
        }

        public static void DeleteCourse_Schedule(int pSchedule_ID)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("DeleteCourse_Schedule", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Schedule_ID", SqlDbType.Int,10));
                    cmd.Parameters["@Schedule_ID"].Value = pSchedule_ID;
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void UpdateCourse_Schedule(TYP_COURSE_SCHEDULE newRecord)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("UpdateCourse_Schedule", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Schedule_ID", SqlDbType.Int,10));
                    cmd.Parameters["@Schedule_ID"].Value = newRecord.SCHEDULE_ID;
                    cmd.Parameters.Add(new SqlParameter("@Course_Code", SqlDbType.Int,10));
                    cmd.Parameters["@Course_Code"].Value = newRecord.COURSE_CODE;
                    cmd.Parameters.Add(new SqlParameter("@Start_Date", SqlDbType.DateTime));
                    cmd.Parameters["@Start_Date"].Value = newRecord.START_DATE;
                    cmd.Parameters.Add(new SqlParameter("@End_Date", SqlDbType.DateTime));
                    cmd.Parameters["@End_Date"].Value = newRecord.END_DATE;
                    cmd.Parameters.Add(new SqlParameter("@Date_of_Exam", SqlDbType.DateTime));
                    cmd.Parameters["@Date_of_Exam"].Value = newRecord.DATE_OF_EXAM;
                    cmd.Parameters.Add(new SqlParameter("@Last_date_for_nomination", SqlDbType.DateTime));
                    cmd.Parameters["@Last_date_for_nomination"].Value = newRecord.LAST_DATE_FOR_NOMINATION;
                    cmd.Parameters.Add(new SqlParameter("@Last_date_for_bcas_acceptance", SqlDbType.DateTime));
                    cmd.Parameters["@Last_date_for_bcas_acceptance"].Value = newRecord.LAST_DATE_FOR_BCAS_ACCEPTANCE;
                    cmd.Parameters.Add(new SqlParameter("@Last_date_for_payments", SqlDbType.DateTime));
                    cmd.Parameters["@Last_date_for_payments"].Value = newRecord.LAST_DATE_FOR_PAYMENTS;
                    cmd.Parameters.Add(new SqlParameter("@Last_date_for_nomination_reappearing", SqlDbType.DateTime));
                    cmd.Parameters["@Last_date_for_nomination_reappearing"].Value = newRecord.LAST_DATE_FOR_NOMINATION_REAPPEARING;
                    cmd.Parameters.Add(new SqlParameter("@Last_date_for_reappearing_fee_payments", SqlDbType.DateTime));
                    cmd.Parameters["@Last_date_for_reappearing_fee_payments"].Value = newRecord.LAST_DATE_FOR_REAPPEARING_FEE_PAYMENTS;
                    cmd.Parameters.Add(new SqlParameter("@Enter_By", SqlDbType.VarChar,50));
                    cmd.Parameters["@Enter_By"].Value = newRecord.ENTER_BY;
                    cmd.Parameters.Add(new SqlParameter("@Enter_date", SqlDbType.DateTime));
                    cmd.Parameters["@Enter_date"].Value = newRecord.ENTER_DATE;
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static DataSet GetAllCourse_Schedule()
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            DataSet myset = new DataSet();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetAllCourse_Schedule", con))
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

        public static DataSet GetCourse_Schedule(int pSchedule_ID, int pCourse_Code, System.DateTime pStart_Date, System.DateTime pEnd_Date, System.DateTime pDate_of_Exam, System.DateTime pLast_date_for_nomination, System.DateTime pLast_date_for_bcas_acceptance, System.DateTime pLast_date_for_payments, System.DateTime pLast_date_for_nomination_reappearing, System.DateTime pLast_date_for_reappearing_fee_payments)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            DataSet myset = new DataSet();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetCourse_Schedule", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Schedule_ID", SqlDbType.Int,10));
                    cmd.Parameters["@Schedule_ID"].Value = pSchedule_ID;
                    cmd.Parameters.Add(new SqlParameter("@Course_Code", SqlDbType.Int,10));
                    cmd.Parameters["@Course_Code"].Value = pCourse_Code;
                    cmd.Parameters.Add(new SqlParameter("@Start_Date", SqlDbType.DateTime));
                    cmd.Parameters["@Start_Date"].Value = pStart_Date;
                    cmd.Parameters.Add(new SqlParameter("@End_Date", SqlDbType.DateTime));
                    cmd.Parameters["@End_Date"].Value = pEnd_Date;
                    cmd.Parameters.Add(new SqlParameter("@Date_of_Exam", SqlDbType.DateTime));
                    cmd.Parameters["@Date_of_Exam"].Value = pDate_of_Exam;
                    cmd.Parameters.Add(new SqlParameter("@Last_date_for_nomination", SqlDbType.DateTime));
                    cmd.Parameters["@Last_date_for_nomination"].Value = pLast_date_for_nomination;
                    cmd.Parameters.Add(new SqlParameter("@Last_date_for_bcas_acceptance", SqlDbType.DateTime));
                    cmd.Parameters["@Last_date_for_bcas_acceptance"].Value = pLast_date_for_bcas_acceptance;
                    cmd.Parameters.Add(new SqlParameter("@Last_date_for_payments", SqlDbType.DateTime));
                    cmd.Parameters["@Last_date_for_payments"].Value = pLast_date_for_payments;
                    cmd.Parameters.Add(new SqlParameter("@Last_date_for_nomination_reappearing", SqlDbType.DateTime));
                    cmd.Parameters["@Last_date_for_nomination_reappearing"].Value = pLast_date_for_nomination_reappearing;
                    cmd.Parameters.Add(new SqlParameter("@Last_date_for_reappearing_fee_payments", SqlDbType.DateTime));
                    cmd.Parameters["@Last_date_for_reappearing_fee_payments"].Value = pLast_date_for_reappearing_fee_payments;
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
