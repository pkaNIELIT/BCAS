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



    }
}
