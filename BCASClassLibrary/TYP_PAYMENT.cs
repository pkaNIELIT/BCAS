using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CrisExamLibrary
{
   public class TYP_PAYMENT: PaymentAttrib 
    {

       public DataTable InsertTransaction_log()
       {
           using (SqlCommand cmd = SqlHelper.GetSqlCommand("InsertTransaction_log"))
           {
               cmd.CommandType = CommandType.StoredProcedure;
               SqlParameter param = new SqlParameter();
               //param = cmd.Parameters.Add("@i_Transaction_id", SqlDbType.Int); param.Direction = ParameterDirection.Input; if (base.b_Transaction_id) param.Value = base.i_Transaction_id; else param.Value = DBNull.Value;
               param = cmd.Parameters.Add("@i_Application_no", SqlDbType.Int); param.Direction = ParameterDirection.Input; if (base.b_Application_no) param.Value = base.i_Application_no; else param.Value = DBNull.Value;
               param = cmd.Parameters.Add("@i_Application_Fee", SqlDbType.Int); param.Direction = ParameterDirection.Input; if (base.b_Application_Fee) param.Value = base.i_Application_Fee; else param.Value = DBNull.Value;
               //param = cmd.Parameters.Add("@d_Date_of_Payment", SqlDbType.DateTime); param.Direction = ParameterDirection.Input; if (base.b_Date_of_Payment) param.Value = base.d_Date_of_Payment; else param.Value = DBNull.Value;               
               return SqlHelper.ExecuteProcedure(cmd);
           }
       }


    }
}
