using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using BCASClassLibrary;


namespace BCASClassLibrary
{
    public class TYP_BCAS_Approval : TYP_BCAS_ApprovalAttrib
    {
        public DataTable GetCourseName()
        {
            using (SqlCommand cmd = SqlHelper.GetSqlCommand("GetCourseName"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                return SqlHelper.ExecuteProcedure(cmd);
            }
        }


        //public DataTable GetNomination()
        //{
        //    using (SqlCommand cmd = SqlHelper.GetSqlCommand("GetNominatedCandidates"))
        //    {
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        SqlParameter param = new SqlParameter();
        //        //param = cmd.Parameters.Add("@i_Course_Code", SqlDbType.Int); param.Direction = ParameterDirection.Input; if (base.b_Course_Code) param.Value = base.i_Course_Code; else param.Value = DBNull.Value;
        //        param = cmd.Parameters.Add("@v_Course_Name", SqlDbType.VarChar, 100); param.Direction = ParameterDirection.Input; if (base.b_Course_Name) param.Value = base.v_Course_Name; else param.Value = DBNull.Value;
        //        param = cmd.Parameters.Add("@v_Enter_By", SqlDbType.VarChar, 50); param.Direction = ParameterDirection.Input; if (base.b_Enter_By) param.Value = base.v_Enter_By; else param.Value = DBNull.Value;
        //        //param = cmd.Parameters.Add("@d_Enter_Date", SqlDbType.DateTime); param.Direction = ParameterDirection.Input; if (base.b_Enter_Date) param.Value = base.d_Enter_Date; else param.Value = DBNull.Value;
        //        return SqlHelper.ExecuteProcedure(cmd);
        //    }
        //}

        public DataTable getNomination(int Schedule_ID, int Organisation)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            DataTable myDt = new DataTable();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetNominatedCandidates", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Schedule_ID", SqlDbType.Int));
                    cmd.Parameters["@Schedule_ID"].Value = Schedule_ID;
                    cmd.Parameters.Add(new SqlParameter("@Organisation_Code", SqlDbType.Int));
                    cmd.Parameters["@Organisation_Code"].Value = Organisation;
                    con.Open();
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(myDt);
                    }
                }
            }
            return myDt;
        }

        public DataTable UpdateNominationApproval()
        {
            using (SqlCommand cmd = SqlHelper.GetSqlCommand("Update_NominationApprovalStatus"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter param = new SqlParameter();

                param = cmd.Parameters.Add("@i_Schedule_ID", SqlDbType.Int); param.Direction = ParameterDirection.Input; if (base.b_Schedule_ID) param.Value = base.i_Schedule_ID; else param.Value = DBNull.Value;
                param = cmd.Parameters.Add("@i_Registration_No", SqlDbType.Int); param.Direction = ParameterDirection.Input; if (base.b_Registration_No) param.Value = base.i_Registration_No; else param.Value = DBNull.Value;
                param = cmd.Parameters.Add("@v_BCAS_Approval_By", SqlDbType.VarChar, 50); param.Direction = ParameterDirection.Input; if (base.b_BCAS_Approval_By) param.Value = base.v_BCAS_Approval_By; else param.Value = DBNull.Value;
                return SqlHelper.ExecuteProcedure(cmd);

            }

        }


        public DataTable getBCASCourseSchedule()
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            DataTable myDt = new DataTable();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetBCASCourseSchedule", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(myDt);
                    }
                }
            }
            return myDt;
        }


        public DataTable getQryBCASCourseSchedule()
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            DataTable myDt = new DataTable();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetQryBCASCourseSchedule", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(myDt);
                    }
                }
            }
            return myDt;
        }



        public DataTable getGetAllOrganisation()
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            DataTable myDt = new DataTable();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetAllOrganisation", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(myDt);
                    }
                }
            }
            return myDt;
        }

        public DataTable GetAllExamLocation()
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            DataTable myDt = new DataTable();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT [Exam_Location_Code], [Exam_Location_Name] FROM [ExamLocationMaster] ORDER BY [Exam_Location_Name]", con))
                {
                    cmd.CommandType = CommandType.Text;
                    con.Open();

                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {                        
                        sda.Fill(myDt);
                    }
                }
            }
            return myDt;
        }

      //deep create this method for the nominated candidate cancel exists or not
        public DataTable CheckQryBCASNominatedCandidate(int RegNo, int ScheduleId)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            DataTable myDt = new DataTable();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("Check_NominatedCandidateDetails", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Registration_Number", SqlDbType.Int));
                    cmd.Parameters["@Registration_Number"].Value = RegNo;

                    cmd.Parameters.Add(new SqlParameter("@scheduleID", SqlDbType.Int));
                    cmd.Parameters["@scheduleID"].Value = ScheduleId;

                    con.Open();
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(myDt);
                    }
                }
            }
            return myDt;
        }
        // registration number fetch from db table for dowlnoading photo signature
        public DataTable GetBCASNominatedCandidateRegistrationNumber(int RegNo, int ScheduleId)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            DataTable myDt = new DataTable();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("NominatedCandidateDetails_DownloadPhotoSign", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Registration_Number", SqlDbType.Int));
                    cmd.Parameters["@Registration_Number"].Value = RegNo;

                    cmd.Parameters.Add(new SqlParameter("@scheduleID", SqlDbType.Int));
                    cmd.Parameters["@scheduleID"].Value = ScheduleId;

                    con.Open();
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(myDt);
                    }
                }
            }
            return myDt;
        }
        //deep end this method

    }
}
