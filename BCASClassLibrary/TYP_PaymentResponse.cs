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

    public class TYP_PaymentResponse:TYP_PaymentResponseAttrib 
    {

        public DataTable Insert_PaymentResponse()
        {
            using (SqlCommand cmd = SqlHelper.GetSqlCommand("InsertPaymentResponse"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter param = new SqlParameter();

                //param = cmd.Parameters.Add("@i_S_No", SqlDbType.Int); param.Direction = ParameterDirection.Input; if (base.b_S_No) param.Value = base.i_S_No; else param.Value = DBNull.Value;
                param = cmd.Parameters.Add("@i_Transaction_id", SqlDbType.Int); param.Direction = ParameterDirection.Input; if (base.b_Transaction_id) param.Value = base.i_Transaction_id; else param.Value = DBNull.Value;
                param = cmd.Parameters.Add("@i_Payee_Organisation_Code", SqlDbType.Int); param.Direction = ParameterDirection.Input; if (base.b_Payee_Organisation_Code) param.Value = base.i_Payee_Organisation_Code; else param.Value = DBNull.Value;
                param = cmd.Parameters.Add("@i_Sponsoring_Organisation_Code", SqlDbType.Int); param.Direction = ParameterDirection.Input; if (base.b_Sponsoring_Organisation_Code) param.Value = base.i_Sponsoring_Organisation_Code; else param.Value = DBNull.Value;
                param = cmd.Parameters.Add("@v_Transaction_Reference_No", SqlDbType.VarChar, 100); param.Direction = ParameterDirection.Input; if (base.b_Transaction_Reference_No) param.Value = base.v_Transaction_Reference_No; else param.Value = DBNull.Value;
                param = cmd.Parameters.Add("@n_Fee_Amount_Deducted", SqlDbType.Float); param.Direction = ParameterDirection.Input; if (base.b_Fee_Amount_Deducted) param.Value = base.n_Fee_Amount_Deducted; else param.Value = DBNull.Value;
                param = cmd.Parameters.Add("@v_Payment_Status", SqlDbType.VarChar, 10); param.Direction = ParameterDirection.Input; if (base.b_Payment_Status) param.Value = base.v_Payment_Status; else param.Value = DBNull.Value;
                param = cmd.Parameters.Add("@d_Transaction_Date", SqlDbType.DateTime); param.Direction = ParameterDirection.Input; if (base.b_Transaction_Date) param.Value = base.d_Transaction_Date; else param.Value = DBNull.Value;
                param = cmd.Parameters.Add("@v_Response_String", SqlDbType.VarChar, -1); param.Direction = ParameterDirection.Input; if (base.b_Response_String) param.Value = base.v_Response_String; else param.Value = DBNull.Value;
                //param = cmd.Parameters.Add("@d_Enter_Date", SqlDbType.DateTime); param.Direction = ParameterDirection.Input; if (base.b_Enter_Date) param.Value = base.d_Enter_Date; else param.Value = DBNull.Value;

                return SqlHelper.ExecuteProcedure(cmd);

            }


        }

        public DataTable Update_PaymentResponse()
        {
            using (SqlCommand cmd = SqlHelper.GetSqlCommand("Update_PaymentResponse"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter param = new SqlParameter();
                param = cmd.Parameters.Add("@i_Transaction_id", SqlDbType.Int); param.Direction = ParameterDirection.Input; if (base.b_Transaction_id) param.Value = base.i_Transaction_id; else param.Value = DBNull.Value;
                param = cmd.Parameters.Add("@n_Amount_Recieved", SqlDbType.Float); param.Direction = ParameterDirection.Input; if (base.b_Amount_Recieved) param.Value = base.n_Amount_Recieved; else param.Value = DBNull.Value;
                param = cmd.Parameters.Add("@i_Payee_Organisation_Code", SqlDbType.Int); param.Direction = ParameterDirection.Input; if (base.b_Payee_Organisation_Code) param.Value = base.i_Payee_Organisation_Code; else param.Value = DBNull.Value;
                param = cmd.Parameters.Add("@i_Sponsoring_Organisation_Code", SqlDbType.Int); param.Direction = ParameterDirection.Input; if (base.b_Sponsoring_Organisation_Code) param.Value = base.i_Sponsoring_Organisation_Code; else param.Value = DBNull.Value;
                param = cmd.Parameters.Add("@v_Reference_No", SqlDbType.VarChar, 100); param.Direction = ParameterDirection.Input; if (base.b_Reference_No) param.Value = base.v_Reference_No; else param.Value = DBNull.Value;
               // param = cmd.Parameters.Add("@v_Response_String", SqlDbType.VarChar, 100); param.Direction = ParameterDirection.Input; if (base.b_Response_String) param.Value = base.v_Response_String; else param.Value = DBNull.Value;

                param = cmd.Parameters.Add("@d_Date_of_Payment", SqlDbType.DateTime); param.Direction = ParameterDirection.Input; if (base.b_Date_of_Payment) param.Value = base.d_Date_of_Payment; else param.Value = DBNull.Value;
                //param = cmd.Parameters.Add("@d_Enter_Date", SqlDbType.DateTime); param.Direction = ParameterDirection.Input; if (base.b_Enter_Date) param.Value = base.d_Enter_Date; else param.Value = DBNull.Value;
                //param = cmd.Parameters.Add("@n_Amount_To_Pay", SqlDbType.Int); param.Direction = ParameterDirection.Input; if (base.b_Amount_To_Pay) param.Value = base.n_Amount_To_Pay; else param.Value = DBNull.Value;

                return SqlHelper.ExecuteProcedure(cmd);

            }


        }

        public DataTable Update_PaymentResponseOfSpon()
        {
            using (SqlCommand cmd = SqlHelper.GetSqlCommand("UpdateOfSpon_PaymentResponse"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter param = new SqlParameter();
                param = cmd.Parameters.Add("@i_Transaction_id", SqlDbType.Int); param.Direction = ParameterDirection.Input; if (base.b_Transaction_id) param.Value = base.i_Transaction_id; else param.Value = DBNull.Value;
                param = cmd.Parameters.Add("@n_Amount_Recieved", SqlDbType.Float); param.Direction = ParameterDirection.Input; if (base.b_Amount_Recieved) param.Value = base.n_Amount_Recieved; else param.Value = DBNull.Value;
                param = cmd.Parameters.Add("@i_Payee_Organisation_Code", SqlDbType.Int); param.Direction = ParameterDirection.Input; if (base.b_Payee_Organisation_Code) param.Value = base.i_Payee_Organisation_Code; else param.Value = DBNull.Value;
                param = cmd.Parameters.Add("@i_Sponsoring_Organisation_Code", SqlDbType.Int); param.Direction = ParameterDirection.Input; if (base.b_Sponsoring_Organisation_Code) param.Value = base.i_Sponsoring_Organisation_Code; else param.Value = DBNull.Value;
                param = cmd.Parameters.Add("@v_Reference_No", SqlDbType.VarChar, 100); param.Direction = ParameterDirection.Input; if (base.b_Reference_No) param.Value = base.v_Reference_No; else param.Value = DBNull.Value;
                param = cmd.Parameters.Add("@v_Response_String", SqlDbType.VarChar, -1); param.Direction = ParameterDirection.Input; if (base.b_Response_String) param.Value = base.v_Response_String; else param.Value = DBNull.Value;
                param = cmd.Parameters.Add("@v_Payment_Status", SqlDbType.VarChar, 10); param.Direction = ParameterDirection.Input; if (base.b_Payment_Status) param.Value = base.v_Payment_Status; else param.Value = DBNull.Value;
                param = cmd.Parameters.Add("@ResponseDescription", SqlDbType.VarChar, 100); param.Direction = ParameterDirection.Input; if (base.b_ResponseDescription) param.Value = base.v_ResponseDescription; else param.Value = DBNull.Value;

                param = cmd.Parameters.Add("@d_Date_of_Payment", SqlDbType.DateTime); param.Direction = ParameterDirection.Input; if (base.b_Date_of_Payment) param.Value = base.d_Date_of_Payment; else param.Value = DBNull.Value;
                //param = cmd.Parameters.Add("@d_Enter_Date", SqlDbType.DateTime); param.Direction = ParameterDirection.Input; if (base.b_Enter_Date) param.Value = base.d_Enter_Date; else param.Value = DBNull.Value;
                //param = cmd.Parameters.Add("@n_Amount_To_Pay", SqlDbType.Int); param.Direction = ParameterDirection.Input; if (base.b_Amount_To_Pay) param.Value = base.n_Amount_To_Pay; else param.Value = DBNull.Value;

                return SqlHelper.ExecuteProcedure(cmd);

            }


        }



        public DataTable InsertTempPaymetResponse()
        {
            using (SqlCommand cmd = SqlHelper.GetSqlCommand("InsertTempResponse"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter param = new SqlParameter();
                param = cmd.Parameters.Add("@v_Response_String", SqlDbType.VarChar, -1); param.Direction = ParameterDirection.Input; if (base.b_Response_String) param.Value = base.v_Response_String; else param.Value = DBNull.Value;
                return SqlHelper.ExecuteProcedure(cmd);

            }
        }

        
        public DataTable InsertTemp_ChkSum_Matched()
        {
            using (SqlCommand cmd = SqlHelper.GetSqlCommand("InsertTemp_ChkSum_Matched"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter param = new SqlParameter();
                param = cmd.Parameters.Add("@v_Response_String", SqlDbType.VarChar, -1); param.Direction = ParameterDirection.Input; if (base.b_Response_String) param.Value = base.v_Response_String; else param.Value = DBNull.Value;
                return SqlHelper.ExecuteProcedure(cmd);

            }
        }


        public DataTable Insert_PaymentBalance()
        {
            using (SqlCommand cmd = SqlHelper.GetSqlCommand("InsertPaymentBalance"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter param = new SqlParameter();
                param = cmd.Parameters.Add("@i_Sponsoring_Organisation_Code", SqlDbType.Int); param.Direction = ParameterDirection.Input; if (base.b_Sponsoring_Organisation_Code) param.Value = base.i_Sponsoring_Organisation_Code; else param.Value = DBNull.Value;
                param = cmd.Parameters.Add("@i_Payee_Organisation_Code", SqlDbType.Int); param.Direction = ParameterDirection.Input; if (base.b_Payee_Organisation_Code) param.Value = base.i_Payee_Organisation_Code; else param.Value = DBNull.Value;                
                return SqlHelper.ExecuteProcedure(cmd);

            }

        }

        public DataTable getSponsOrgName(int Sponsoring_Organisation_Code)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            DataTable myDt = new DataTable();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetSponsOrgName", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@i_Sponsoring_Organisation_Code", SqlDbType.Int));
                    cmd.Parameters["@i_Sponsoring_Organisation_Code"].Value = Sponsoring_Organisation_Code;                    
                    con.Open();
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(myDt);
                    }
                }
            }
            return myDt;
        }


        public DataTable getPayeeOrgName(int Payee_Organisation_Code, int  online_Transaction_ID)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            DataTable myDt = new DataTable();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetPayeeOrgName", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;                    
                    cmd.Parameters.Add(new SqlParameter("@i_Payee_Organisation_Code", SqlDbType.Int));
                    cmd.Parameters["@i_Payee_Organisation_Code"].Value = Payee_Organisation_Code;
                    cmd.Parameters.Add(new SqlParameter("@i_Online_Transaction_ID", SqlDbType.Int));
                    cmd.Parameters["@i_Online_Transaction_ID"].Value = online_Transaction_ID;
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
