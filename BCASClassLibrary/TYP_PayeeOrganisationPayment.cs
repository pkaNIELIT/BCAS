using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace BCASClassLibrary
{
   
    public class TYP_PayeeOrganisationPayment:TYP_PayeeOrganisationPaymentAttrib 
    {

       public DataTable InsertTransactionLog()
       {
           using (SqlCommand cmd = SqlHelper.GetSqlCommand("InsertTransaction_log"))
           {
               cmd.CommandType = CommandType.StoredProcedure;
               SqlParameter param = new SqlParameter();
               //param = cmd.Parameters.Add("@i_Transaction_id", SqlDbType.Int); param.Direction = ParameterDirection.Input; if (base.b_Transaction_id) param.Value = base.i_Transaction_id; else param.Value = DBNull.Value;
               param = cmd.Parameters.Add("@i_Payee_Organisation_Code", SqlDbType.Int); param.Direction = ParameterDirection.Input; if (base.b_Payee_Organisation_Code) param.Value = base.i_Payee_Organisation_Code; else param.Value = DBNull.Value;
               param = cmd.Parameters.Add("@i_Sponsoring_Organisation_Code", SqlDbType.Int); param.Direction = ParameterDirection.Input; if (base.b_Sponsoring_Organisation_Code) param.Value = base.i_Sponsoring_Organisation_Code; else param.Value = DBNull.Value;
               param = cmd.Parameters.Add("@n_Amount_To_Pay", SqlDbType.Int); param.Direction = ParameterDirection.Input; if (base.b_Amount_To_Pay) param.Value = base.n_Amount_To_Pay; else param.Value = DBNull.Value;
               //param = cmd.Parameters.Add("@d_Date_of_Payment", SqlDbType.DateTime); param.Direction = ParameterDirection.Input; if (base.b_Date_of_Payment) param.Value = base.d_Date_of_Payment; else param.Value = DBNull.Value;
               param = cmd.Parameters.Add("@v_User_ID", SqlDbType.VarChar, 50); param.Direction = ParameterDirection.Input; if (base.b_User_ID) param.Value = base.v_User_ID; else param.Value = DBNull.Value;
               return SqlHelper.ExecuteProcedure(cmd);

           }

       }

       public DataTable GetDetailsForMakePayment(int Transaction_id)
       {
           string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
           DataTable myDt = new DataTable();
           using (SqlConnection con = new SqlConnection(ConnectionString))
           {
               using (SqlCommand cmd = new SqlCommand("GetDetailsForMakePayment", con))
               {
                   cmd.CommandType = CommandType.StoredProcedure;
                   cmd.Parameters.Add(new SqlParameter("@i_Transaction_id", SqlDbType.Int));
                   cmd.Parameters["@i_Transaction_id"].Value = Transaction_id;
                   con.Open();
                   using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                   {
                       sda.Fill(myDt);
                   }
               }
           }
           return myDt;
       }
       public DataTable GetDetailsForMakePaymentBySpon(int Transaction_id)
       {
           string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
           DataTable myDt = new DataTable();
           using (SqlConnection con = new SqlConnection(ConnectionString))
           {
               using (SqlCommand cmd = new SqlCommand("GetDetailsBySponForMakePayment", con))
               {
                   cmd.CommandType = CommandType.StoredProcedure;
                   cmd.Parameters.Add(new SqlParameter("@i_Transaction_id", SqlDbType.Int));
                   cmd.Parameters["@i_Transaction_id"].Value = Transaction_id;
                   con.Open();
                   using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                   {
                       sda.Fill(myDt);
                   }
               }
           }
           return myDt;
       }
       //public DataTable InsertOnlineTransaction(int orgid, Int64 ProfromaInvoiceID, string ProformaInvoiceNo, double TotalAmount, string User_ID)
       //{
       //    string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
       //    DataTable myDt = new DataTable();
       //    using (SqlConnection con = new SqlConnection(ConnectionString))
       //    {
       //        using (SqlCommand cmd = new SqlCommand("InsertOnlineTransaction", con))
       //        {
       //            cmd.CommandType = CommandType.StoredProcedure;
       //            cmd.Parameters.Add(new SqlParameter("@orgid", SqlDbType.Int));
       //            cmd.Parameters["@orgid"].Value = orgid;
       //            cmd.Parameters.Add(new SqlParameter("@ProfromaInvoiceID", SqlDbType.BigInt));
       //            cmd.Parameters["@ProfromaInvoiceID"].Value = ProfromaInvoiceID;
       //            cmd.Parameters.Add(new SqlParameter("@ProformaInvoiceNo", SqlDbType.VarChar));
       //            cmd.Parameters["@ProformaInvoiceNo"].Value = ProformaInvoiceNo;
       //            cmd.Parameters.Add(new SqlParameter("@TotalAmount", SqlDbType.Decimal));
       //            cmd.Parameters["@TotalAmount"].Value = TotalAmount;
       //            cmd.Parameters.Add(new SqlParameter("@User_ID", SqlDbType.Int));
       //            cmd.Parameters["@User_ID"].Value = User_ID;

       //            con.Open();
       //            cmd.ExecuteNonQuery();
       //        }
       //    }
       //    return myDt;
       //}
       public DataTable InsertOnlineTransaction( int orgid, Int64 ProfromaInvoiceID, string ProformaInvoiceNo, double TotalAmount, string User_ID)
       {
           using (SqlCommand cmd = SqlHelper.GetSqlCommand("InsertOnlineTransaction"))
           {
               cmd.CommandType = CommandType.StoredProcedure;
               SqlParameter param = new SqlParameter(); 
                param = cmd.Parameters.Add("@orgid", SqlDbType.Int); 
                 param.Direction = ParameterDirection.Input;
                 param.Value = orgid;
                 param = cmd.Parameters.Add("@ProfromaInvoiceID", SqlDbType.BigInt);
                 param.Direction = ParameterDirection.Input;
                 param.Value = ProfromaInvoiceID;
                 param = cmd.Parameters.Add("@ProformaInvoiceNo", SqlDbType.VarChar);
                 param.Direction = ParameterDirection.Input;
                 param.Value = ProformaInvoiceNo;
                 param = cmd.Parameters.Add("@TotalAmount", SqlDbType.Decimal);
                 param.Direction = ParameterDirection.Input;
                 param.Value = TotalAmount;

                 param = cmd.Parameters.Add("@User_ID", SqlDbType.VarChar, 50); 
                 param.Direction = ParameterDirection.Input;
                 param.Value = User_ID;

               return SqlHelper.ExecuteProcedure(cmd);

           }

       }

       public DataTable GetCourseWisePaymentDetails(string roleName, int organisationCode, int scheduleId, DateTime? fromDate, DateTime? toDate)
       {
           string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
           DataTable myDt = new DataTable();
           using (SqlConnection con = new SqlConnection(ConnectionString))
           {
               using (SqlCommand cmd = new SqlCommand("GetCourseWisePaymentDetails", con))
               {
                   cmd.CommandType = CommandType.StoredProcedure;
                   cmd.Parameters.Add(new SqlParameter("@RoleName", SqlDbType.VarChar, 50));
                   cmd.Parameters["@RoleName"].Value = roleName;
                   cmd.Parameters.Add(new SqlParameter("@OrganisationCode", SqlDbType.Int));
                   cmd.Parameters["@OrganisationCode"].Value = organisationCode;
                   cmd.Parameters.Add(new SqlParameter("@ScheduleID", SqlDbType.Int));
                   cmd.Parameters["@ScheduleID"].Value = scheduleId;
                   cmd.Parameters.Add(new SqlParameter("@FromDate", SqlDbType.DateTime));
                   cmd.Parameters["@FromDate"].Value = fromDate.HasValue ? (object)fromDate.Value : DBNull.Value;
                   cmd.Parameters.Add(new SqlParameter("@ToDate", SqlDbType.DateTime));
                   cmd.Parameters["@ToDate"].Value = toDate.HasValue ? (object)toDate.Value : DBNull.Value;
                   con.Open();
                   using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                   {
                       sda.Fill(myDt);
                   }
               }
           }
           return myDt;
       }

       //public void InsertOnlineTransaction(int orgid, Int64 ProfromaInvoiceID, string ProformaInvoiceNo, double TotalAmount, string User_ID)
       //{
       //    try
       //    {             
       //        string constr = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
       //        using (SqlConnection Conn = new SqlConnection(constr))
       //        {
       //            using (SqlCommand cmd = new SqlCommand("InsertOnlineTransaction", Conn))
       //            {
       //                Conn.Open();
       //                cmd.CommandType = CommandType.StoredProcedure;
       //                cmd.Parameters.Add(new SqlParameter("@orgid", SqlDbType.Int));
       //                cmd.Parameters["@orgid"].Value = orgid;
       //                cmd.Parameters.Add(new SqlParameter("@ProfromaInvoiceID", SqlDbType.BigInt));
       //                cmd.Parameters["@ProfromaInvoiceID"].Value = ProfromaInvoiceID;
       //                cmd.Parameters.Add(new SqlParameter("@ProformaInvoiceNo", SqlDbType.VarChar));
       //                cmd.Parameters["@ProformaInvoiceNo"].Value = ProformaInvoiceNo;
       //                cmd.Parameters.Add(new SqlParameter("@TotalAmount", SqlDbType.Decimal));
       //                cmd.Parameters["@TotalAmount"].Value = TotalAmount;
       //                cmd.Parameters.Add(new SqlParameter("@User_ID", SqlDbType.Int));
       //                cmd.Parameters["@User_ID"].Value = User_ID;
       //                cmd.ExecuteNonQuery();
       //            }
       //        }
       //    }
       //    catch (Exception ex)
       //    {
              
       //    }
       //}

    }
}


// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using System.Data;
// using System.Data.SqlClient;
// using System.Configuration;

// namespace BCASClassLibrary
// {
   
//     public class TYP_PayeeOrganisationPayment:TYP_PayeeOrganisationPaymentAttrib 
//     {

//        public DataTable InsertTransactionLog()
//        {
//            using (SqlCommand cmd = SqlHelper.GetSqlCommand("InsertTransaction_log"))
//            {
//                cmd.CommandType = CommandType.StoredProcedure;
//                SqlParameter param = new SqlParameter();
//                //param = cmd.Parameters.Add("@i_Transaction_id", SqlDbType.Int); param.Direction = ParameterDirection.Input; if (base.b_Transaction_id) param.Value = base.i_Transaction_id; else param.Value = DBNull.Value;
//                param = cmd.Parameters.Add("@i_Payee_Organisation_Code", SqlDbType.Int); param.Direction = ParameterDirection.Input; if (base.b_Payee_Organisation_Code) param.Value = base.i_Payee_Organisation_Code; else param.Value = DBNull.Value;
//                param = cmd.Parameters.Add("@i_Sponsoring_Organisation_Code", SqlDbType.Int); param.Direction = ParameterDirection.Input; if (base.b_Sponsoring_Organisation_Code) param.Value = base.i_Sponsoring_Organisation_Code; else param.Value = DBNull.Value;
//                param = cmd.Parameters.Add("@n_Amount_To_Pay", SqlDbType.Int); param.Direction = ParameterDirection.Input; if (base.b_Amount_To_Pay) param.Value = base.n_Amount_To_Pay; else param.Value = DBNull.Value;
//                //param = cmd.Parameters.Add("@d_Date_of_Payment", SqlDbType.DateTime); param.Direction = ParameterDirection.Input; if (base.b_Date_of_Payment) param.Value = base.d_Date_of_Payment; else param.Value = DBNull.Value;
//                param = cmd.Parameters.Add("@v_User_ID", SqlDbType.VarChar, 50); param.Direction = ParameterDirection.Input; if (base.b_User_ID) param.Value = base.v_User_ID; else param.Value = DBNull.Value;
//                return SqlHelper.ExecuteProcedure(cmd);

//            }

//        }

//        public DataTable GetDetailsForMakePayment(int Transaction_id)
//        {
//            string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
//            DataTable myDt = new DataTable();
//            using (SqlConnection con = new SqlConnection(ConnectionString))
//            {
//                using (SqlCommand cmd = new SqlCommand("GetDetailsForMakePayment", con))
//                {
//                    cmd.CommandType = CommandType.StoredProcedure;
//                    cmd.Parameters.Add(new SqlParameter("@i_Transaction_id", SqlDbType.Int));
//                    cmd.Parameters["@i_Transaction_id"].Value = Transaction_id;
//                    con.Open();
//                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
//                    {
//                        sda.Fill(myDt);
//                    }
//                }
//            }
//            return myDt;
//        }
//        public DataTable GetDetailsForMakePaymentBySpon(int Transaction_id)
//        {
//            string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
//            DataTable myDt = new DataTable();
//            using (SqlConnection con = new SqlConnection(ConnectionString))
//            {
//                using (SqlCommand cmd = new SqlCommand("GetDetailsBySponForMakePayment", con))
//                {
//                    cmd.CommandType = CommandType.StoredProcedure;
//                    cmd.Parameters.Add(new SqlParameter("@i_Transaction_id", SqlDbType.Int));
//                    cmd.Parameters["@i_Transaction_id"].Value = Transaction_id;
//                    con.Open();
//                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
//                    {
//                        sda.Fill(myDt);
//                    }
//                }
//            }
//            return myDt;
//        }
//        //public DataTable InsertOnlineTransaction(int orgid, Int64 ProfromaInvoiceID, string ProformaInvoiceNo, double TotalAmount, string User_ID)
//        //{
//        //    string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
//        //    DataTable myDt = new DataTable();
//        //    using (SqlConnection con = new SqlConnection(ConnectionString))
//        //    {
//        //        using (SqlCommand cmd = new SqlCommand("InsertOnlineTransaction", con))
//        //        {
//        //            cmd.CommandType = CommandType.StoredProcedure;
//        //            cmd.Parameters.Add(new SqlParameter("@orgid", SqlDbType.Int));
//        //            cmd.Parameters["@orgid"].Value = orgid;
//        //            cmd.Parameters.Add(new SqlParameter("@ProfromaInvoiceID", SqlDbType.BigInt));
//        //            cmd.Parameters["@ProfromaInvoiceID"].Value = ProfromaInvoiceID;
//        //            cmd.Parameters.Add(new SqlParameter("@ProformaInvoiceNo", SqlDbType.VarChar));
//        //            cmd.Parameters["@ProformaInvoiceNo"].Value = ProformaInvoiceNo;
//        //            cmd.Parameters.Add(new SqlParameter("@TotalAmount", SqlDbType.Decimal));
//        //            cmd.Parameters["@TotalAmount"].Value = TotalAmount;
//        //            cmd.Parameters.Add(new SqlParameter("@User_ID", SqlDbType.Int));
//        //            cmd.Parameters["@User_ID"].Value = User_ID;

//        //            con.Open();
//        //            cmd.ExecuteNonQuery();
//        //        }
//        //    }
//        //    return myDt;
//        //}
//        public DataTable InsertOnlineTransaction(int orgid, Int64 ProfromaInvoiceID, string ProformaInvoiceNo, double TotalAmount, string User_ID)
//        {
//            using (SqlCommand cmd = SqlHelper.GetSqlCommand("InsertOnlineTransaction"))
//            {
//                cmd.CommandType = CommandType.StoredProcedure;
//                SqlParameter param = new SqlParameter();
//                param = cmd.Parameters.Add("@orgid", SqlDbType.Int); 
//                  param.Direction = ParameterDirection.Input;
//                  param.Value = orgid;
//                  param = cmd.Parameters.Add("@ProfromaInvoiceID", SqlDbType.BigInt);
//                  param.Direction = ParameterDirection.Input;
//                  param.Value = ProfromaInvoiceID;
//                  param = cmd.Parameters.Add("@ProformaInvoiceNo", SqlDbType.VarChar);
//                  param.Direction = ParameterDirection.Input;
//                  param.Value = ProformaInvoiceNo;
//                  param = cmd.Parameters.Add("@TotalAmount", SqlDbType.Decimal);
//                  param.Direction = ParameterDirection.Input;
//                  param.Value = TotalAmount;

//                  param = cmd.Parameters.Add("@User_ID", SqlDbType.VarChar, 50); 
//                  param.Direction = ParameterDirection.Input;
//                  param.Value = User_ID;

//                return SqlHelper.ExecuteProcedure(cmd);

//            }

//        }
//        //public void InsertOnlineTransaction(int orgid, Int64 ProfromaInvoiceID, string ProformaInvoiceNo, double TotalAmount, string User_ID)
//        //{
//        //    try
//        //    {             
//        //        string constr = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
//        //        using (SqlConnection Conn = new SqlConnection(constr))
//        //        {
//        //            using (SqlCommand cmd = new SqlCommand("InsertOnlineTransaction", Conn))
//        //            {
//        //                Conn.Open();
//        //                cmd.CommandType = CommandType.StoredProcedure;
//        //                cmd.Parameters.Add(new SqlParameter("@orgid", SqlDbType.Int));
//        //                cmd.Parameters["@orgid"].Value = orgid;
//        //                cmd.Parameters.Add(new SqlParameter("@ProfromaInvoiceID", SqlDbType.BigInt));
//        //                cmd.Parameters["@ProfromaInvoiceID"].Value = ProfromaInvoiceID;
//        //                cmd.Parameters.Add(new SqlParameter("@ProformaInvoiceNo", SqlDbType.VarChar));
//        //                cmd.Parameters["@ProformaInvoiceNo"].Value = ProformaInvoiceNo;
//        //                cmd.Parameters.Add(new SqlParameter("@TotalAmount", SqlDbType.Decimal));
//        //                cmd.Parameters["@TotalAmount"].Value = TotalAmount;
//        //                cmd.Parameters.Add(new SqlParameter("@User_ID", SqlDbType.Int));
//        //                cmd.Parameters["@User_ID"].Value = User_ID;
//        //                cmd.ExecuteNonQuery();
//        //            }
//        //        }
//        //    }
//        //    catch (Exception ex)
//        //    {
              
//        //    }
//        //}

//     }
// }
