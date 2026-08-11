using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using BCASClassLibrary;


namespace BCASClassLibrary
{
    public class TYP_NominationEntryBySO:TYP_NominationEntryBySOAttrib 
    {

        //public DataTable InsertNominationDetails()
        //{
        //    using (SqlCommand cmd = SqlHelper.GetSqlCommand("InsertNominationDetails"))
        //    {
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        SqlParameter param = new SqlParameter();
        //        param = cmd.Parameters.Add("@i_Schedule_ID", SqlDbType.Int); param.Direction = ParameterDirection.Input; if (base.b_Schedule_ID) param.Value = base.i_Schedule_ID; else param.Value = DBNull.Value;
        //        param = cmd.Parameters.Add("@i_Registration_No", SqlDbType.Int); param.Direction = ParameterDirection.Input; if (base.b_Registration_No) param.Value = base.i_Registration_No; else param.Value = DBNull.Value;
        //        //param = cmd.Parameters.Add("@n_Fee_Amount", SqlDbType.Int); param.Direction = ParameterDirection.Input; if (base.b_Fee_Amount) param.Value = base.n_Fee_Amount; else param.Value = DBNull.Value;
        //        //param = cmd.Parameters.Add("@c_BCAS_Approval_Status", SqlDbType.VarChar, 1); param.Direction = ParameterDirection.Input; if (base.b_BCAS_Approval_Status) param.Value = base.c_BCAS_Approval_Status; else param.Value = DBNull.Value;
        //        return SqlHelper.ExecuteProcedure(cmd);

        //    }

        //}



        public DataTable InsertNominationDetails()
        {
            using (SqlCommand cmd = SqlHelper.GetSqlCommand("InsertNominationDetails"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter param = new SqlParameter();
                
                param = cmd.Parameters.Add("@i_Schedule_ID", SqlDbType.Int); param.Direction = ParameterDirection.Input; if (base.b_Schedule_ID) param.Value = base.i_Schedule_ID; else param.Value = DBNull.Value;
                param = cmd.Parameters.Add("@i_Registration_No", SqlDbType.Int); param.Direction = ParameterDirection.Input; if (base.b_Registration_No) param.Value = base.i_Registration_No; else param.Value = DBNull.Value;
                //param = cmd.Parameters.Add("@n_Fee_Amount", SqlDbType.Int); param.Direction = ParameterDirection.Input; if (base.b_Fee_Amount) param.Value = base.n_Fee_Amount; else param.Value = DBNull.Value;
                //param = cmd.Parameters.Add("@c_BCAS_Approval_Status", SqlDbType.VarChar, 1); param.Direction = ParameterDirection.Input; if (base.b_BCAS_Approval_Status) param.Value = base.c_BCAS_Approval_Status; else param.Value = DBNull.Value;
                param = cmd.Parameters.Add("@i_Nominating_OrgCode", SqlDbType.Int); param.Direction = ParameterDirection.Input; if (base.b_Nominating_OrgCode) param.Value = base.i_Nominating_OrgCode; else param.Value = DBNull.Value;
                param = cmd.Parameters.Add("@v_Nominated_By", SqlDbType.VarChar, 50); param.Direction = ParameterDirection.Input; if (base.b_Nominated_By) param.Value = base.v_Nominated_By; else param.Value = DBNull.Value;
                //param = cmd.Parameters.Add("@d_Nominating_Date", SqlDbType.DateTime); param.Direction = ParameterDirection.Input; if (base.b_Nominating_Date) param.Value = base.d_Nominating_Date; else param.Value = DBNull.Value;
                //param = cmd.Parameters.Add("@v_BCAS_Approval_By", SqlDbType.VarChar, 50); param.Direction = ParameterDirection.Input; if (base.b_BCAS_Approval_By) param.Value = base.v_BCAS_Approval_By; else param.Value = DBNull.Value;
                //param = cmd.Parameters.Add("@d_BCAS_Approval_Date", SqlDbType.DateTime); param.Direction = ParameterDirection.Input; if (base.b_BCAS_Approval_Date) param.Value = base.d_BCAS_Approval_Date; else param.Value = DBNull.Value;                                       
                param = cmd.Parameters.Add("@i_Exam_Location_Code", SqlDbType.Int); param.Direction = ParameterDirection.Input; if (base.b_Exam_Location_Code) param.Value = base.i_Exam_Location_Code; else param.Value = DBNull.Value;

                param = cmd.Parameters.Add("@c_Whether_Reappearing", SqlDbType.VarChar, 1); param.Direction = ParameterDirection.Input; if (base.b_Whether_Reappearing) param.Value = base.c_Whether_Reappearing; else param.Value = DBNull.Value;
                
                return SqlHelper.ExecuteProcedure(cmd);

            }

        }
        //deep when this method using, above method useless becouse we convert query insert to update mode and also create new store procedure
        public DataTable UpdateNominationDetails()
        {
            using (SqlCommand cmd = SqlHelper.GetSqlCommand("UpdateNominationDetails"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter param = new SqlParameter();

                param = cmd.Parameters.Add("@i_Schedule_ID", SqlDbType.Int); param.Direction = ParameterDirection.Input; if (base.b_Schedule_ID) param.Value = base.i_Schedule_ID; else param.Value = DBNull.Value;
                param = cmd.Parameters.Add("@i_Registration_No", SqlDbType.Int); param.Direction = ParameterDirection.Input; if (base.b_Registration_No) param.Value = base.i_Registration_No; else param.Value = DBNull.Value;
               param = cmd.Parameters.Add("@i_Nominating_OrgCode", SqlDbType.Int); param.Direction = ParameterDirection.Input; if (base.b_Nominating_OrgCode) param.Value = base.i_Nominating_OrgCode; else param.Value = DBNull.Value;
                param = cmd.Parameters.Add("@v_Nominated_By", SqlDbType.VarChar, 50); param.Direction = ParameterDirection.Input; if (base.b_Nominated_By) param.Value = base.v_Nominated_By; else param.Value = DBNull.Value;
                param = cmd.Parameters.Add("@i_Exam_Location_Code", SqlDbType.Int); param.Direction = ParameterDirection.Input; if (base.b_Exam_Location_Code) param.Value = base.i_Exam_Location_Code; else param.Value = DBNull.Value;

                param = cmd.Parameters.Add("@c_Whether_Reappearing", SqlDbType.VarChar, 1); param.Direction = ParameterDirection.Input; if (base.b_Whether_Reappearing) param.Value = base.c_Whether_Reappearing; else param.Value = DBNull.Value;

                return SqlHelper.ExecuteProcedure(cmd);

            }

        }
        //deep

        public DataTable GetCandidateDetailsForNomination(int RegdNo, int OrgCode)
            //, int i_Schedule)//deep add one int variable i_schedule for details finding
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            DataTable myDt = new DataTable();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetCandidateDetailsForNomination", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Regd_No", SqlDbType.Int ));
                    cmd.Parameters["@Regd_No"].Value = RegdNo;

                    cmd.Parameters.Add(new SqlParameter("@OrgCode", SqlDbType.Int));
                    cmd.Parameters["@OrgCode"].Value = OrgCode;
                    //DEEP
                    //cmd.Parameters.Add(new SqlParameter("@i_Schedule", SqlDbType.Int));
                    //cmd.Parameters["@i_Schedule"].Value = i_Schedule;        
                    //DEEP
                    con.Open();
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(myDt);
                    }
                    con.Close();
                }
            }
            return myDt;
        }
        //deep
        public DataTable GetCandidateFeeStatus(int RegdNo, int OrgCode, int i_Schedule)//deep add one int variable i_schedule for details finding
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            DataTable myDt = new DataTable();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetCandidateFeeStatus", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Regd_No", SqlDbType.Int));
                    cmd.Parameters["@Regd_No"].Value = RegdNo;

                    cmd.Parameters.Add(new SqlParameter("@OrgCode", SqlDbType.Int));
                    cmd.Parameters["@OrgCode"].Value = OrgCode;
                    //DEEP
                    cmd.Parameters.Add(new SqlParameter("@i_Schedule", SqlDbType.Int));
                    cmd.Parameters["@i_Schedule"].Value = i_Schedule;
                    //DEEP
                    con.Open();
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(myDt);
                    }
                    con.Close();
                }
            }
            return myDt;
        }
        //deep


        public DataTable getExamLocations(int i_Schedule)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            DataTable myDt = new DataTable();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetExamLoacation", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@i_Schedule", SqlDbType.Int));
                    cmd.Parameters["@i_Schedule"].Value = i_Schedule;                    
                    con.Open();
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(myDt);
                    }
                }
            }
            return myDt;
        }

        
        public DataTable Get_NominationDate(int i_Schedule)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            DataTable myDt = new DataTable();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("Get_NominationDate", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@i_Schedule_ID", SqlDbType.Int));
                    cmd.Parameters["@i_Schedule_ID"].Value = i_Schedule;
                    con.Open();
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(myDt);
                    }
                }
            }
            return myDt;
        }

        public DataTable Get_ReappearingNominationDate(int i_Schedule)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            DataTable myDt = new DataTable();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("Get_ReappearingNominationDate", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@i_Schedule_ID", SqlDbType.Int));
                    cmd.Parameters["@i_Schedule_ID"].Value = i_Schedule;
                    con.Open();
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(myDt);
                    }
                }
            }
            return myDt;
        }

    }

}
