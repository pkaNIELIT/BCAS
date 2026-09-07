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
    public class TYP_FeePaymentsBySO:TYP_FeePaymentsBySOAttrib 
    {
        //this method is change update fee payment to insert fee payment in nomination courses table using new store proc testproc1
        public DataTable Update_Fee_PaymentBalance()
        {
            using (SqlCommand cmd = SqlHelper.GetSqlCommand("TestProc1"))
            {

                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter param = new SqlParameter();
                param = cmd.Parameters.Add("@i_Schedule_ID", SqlDbType.Int); param.Direction = ParameterDirection.Input; if (base.b_Schedule_ID) param.Value = base.i_Schedule_ID; else param.Value = DBNull.Value;
                param = cmd.Parameters.Add("@i_Registration_No", SqlDbType.Int); param.Direction = ParameterDirection.Input; if (base.b_Registration_No) param.Value = base.i_Registration_No; else param.Value = DBNull.Value;
              
                param = cmd.Parameters.Add("@v_Fee_Payment_By", SqlDbType.VarChar, 50); param.Direction = ParameterDirection.Input; if (base.b_Fee_Payment_By) param.Value = base.v_Fee_Payment_By; else param.Value = DBNull.Value;

                param = cmd.Parameters.Add("@i_Sponsoring_Organisation_Code", SqlDbType.Int); param.Direction = ParameterDirection.Input; if (base.b_Sponsoring_Organisation_Code) param.Value = base.i_Sponsoring_Organisation_Code; else param.Value = DBNull.Value;
                param = cmd.Parameters.Add("@i_Payee_Organisation_Code", SqlDbType.Int); param.Direction = ParameterDirection.Input; if (base.b_Payee_Organisation_Code) param.Value = base.i_Payee_Organisation_Code; else param.Value = DBNull.Value;

                return SqlHelper.ExecuteProcedure(cmd);

            }

        }


        public DataTable Update_Fee()
        {
            using (SqlCommand cmd = SqlHelper.GetSqlCommand("Update_Fees"))
            {
                
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter param = new SqlParameter();
                param = cmd.Parameters.Add("@i_Schedule_ID", SqlDbType.Int); param.Direction = ParameterDirection.Input; if (base.b_Schedule_ID) param.Value = base.i_Schedule_ID; else param.Value = DBNull.Value;
                param = cmd.Parameters.Add("@i_Registration_No", SqlDbType.Int); param.Direction = ParameterDirection.Input; if (base.b_Registration_No) param.Value = base.i_Registration_No; else param.Value = DBNull.Value;                
                param = cmd.Parameters.Add("@n_Fee_Amount", SqlDbType.Int); param.Direction = ParameterDirection.Input; if (base.b_Fee_Amount) param.Value = base.n_Fee_Amount; else param.Value = DBNull.Value;                
                param = cmd.Parameters.Add("@n_Service_Tax", SqlDbType.Float); param.Direction = ParameterDirection.Input; if (base.b_Service_Tax) param.Value = base.n_Service_Tax; else param.Value = DBNull.Value;
                param = cmd.Parameters.Add("@n_Total_Fee_With_Service_Tax", SqlDbType.Float); param.Direction = ParameterDirection.Input; if (base.b_Total_Fee_With_Service_Tax) param.Value = base.n_Total_Fee_With_Service_Tax; else param.Value = DBNull.Value;
                param = cmd.Parameters.Add("@v_Fee_Payment_By", SqlDbType.VarChar, 50); param.Direction = ParameterDirection.Input; if (base.b_Fee_Payment_By) param.Value = base.v_Fee_Payment_By; else param.Value = DBNull.Value;                             
                return SqlHelper.ExecuteProcedure(cmd);

            }

        }


        public DataTable Update_PaymentBalance()
        {

            using (SqlCommand cmd = SqlHelper.GetSqlCommand("Update_PaymentBalance"))
            {

                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter param = new SqlParameter();
                param = cmd.Parameters.Add("@i_Sponsoring_Organisation_Code", SqlDbType.Int); param.Direction = ParameterDirection.Input; if (base.b_Sponsoring_Organisation_Code) param.Value = base.i_Sponsoring_Organisation_Code; else param.Value = DBNull.Value;
                param = cmd.Parameters.Add("@i_Payee_Organisation_Code", SqlDbType.Int); param.Direction = ParameterDirection.Input; if (base.b_Payee_Organisation_Code) param.Value = base.i_Payee_Organisation_Code; else param.Value = DBNull.Value;
                //param = cmd.Parameters.Add("@n_Total_Amount", SqlDbType.Float);  param.Direction = ParameterDirection.Input; if (base. b_Total_Amount) param.Value = base.n_Total_Amount; else param.Value = DBNull.Value;
                param = cmd.Parameters.Add("@n_Balance_Amount", SqlDbType.Float); param.Direction = ParameterDirection.Input; if (base.b_Balance_Amount) param.Value = base.n_Balance_Amount; else param.Value = DBNull.Value;
                return SqlHelper.ExecuteProcedure(cmd);

            }

        }



        public DataTable getCandidateDetails_ForFeePymt(int RegNo, int Sponsoring_Organisation_Code,int ScheduleID)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            DataTable myDt = new DataTable();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetCandidateDetails_ForFeePymt", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@i_Registration_Number", SqlDbType.Int));
                    cmd.Parameters["@i_Registration_Number"].Value = RegNo;
                    cmd.Parameters.Add(new SqlParameter("@i_Sponsoring_Organisation_Code", SqlDbType.Int));
                    cmd.Parameters["@i_Sponsoring_Organisation_Code"].Value = Sponsoring_Organisation_Code;
                    cmd.Parameters.Add(new SqlParameter("@i_Schedule_ID", SqlDbType.Int));
                    cmd.Parameters["@i_Schedule_ID"].Value = ScheduleID;
                    con.Open();
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(myDt);
                    }
                }
            }
            return myDt;
        }


        public DataTable getAmountBalance(int Sponsoring_Organisation_Code, int Payee_Organisation_Code)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            DataTable myDt = new DataTable();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetPaymentBalance", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@i_Sponsoring_Organisation_Code", SqlDbType.Int));
                    cmd.Parameters["@i_Sponsoring_Organisation_Code"].Value = Sponsoring_Organisation_Code;
                    cmd.Parameters.Add(new SqlParameter("@i_Payee_Organisation_Code", SqlDbType.Int));
                    cmd.Parameters["@i_Payee_Organisation_Code"].Value = Payee_Organisation_Code;
                    con.Open();
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(myDt);
                    }
                }
            }
            return myDt;
        }


        //public DataTable getApplicableFees()
        //{
        //    string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
        //    DataTable myDt = new DataTable();
        //    using (SqlConnection con = new SqlConnection(ConnectionString))
        //    {
        //        using (SqlCommand cmd = new SqlCommand("GetApplicableFees", con))
        //        {
        //            cmd.CommandType = CommandType.StoredProcedure;                    
        //            con.Open();
        //            using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
        //            {
        //                sda.Fill(myDt);
        //            }
        //        }
        //    }
        //    return myDt;
        //}
        
        //public DataTable getApplicableServiceTax()
        //{
        //    string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
        //    DataTable myDt = new DataTable();
        //    using (SqlConnection con = new SqlConnection(ConnectionString))
        //    {
        //        using (SqlCommand cmd = new SqlCommand("GetApplicableServiceTax", con))
        //        {
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            con.Open();
        //            using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
        //            {
        //                sda.Fill(myDt);
        //            }
        //        }
        //    }
        //    return myDt;
        //}

        public DataTable Get_LastFeePaymentDate(int i_Schedule)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            DataTable myDt = new DataTable();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("Get_LastFeePaymentDate", con))
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

        public DataTable Get_LastFeePaymentDate_Reappearing(int i_Schedule)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            DataTable myDt = new DataTable();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("Get_LastFeePaymentDate_Reappearing", con))
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
