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
    public class TYP_NEFT_RTGS_PayeePayments:TYP_NEFT_RTGS_PayeePaymentsAttrib  
    {
        public DataTable Update_NEFT_RTGS_Pymt()
        {
            using (SqlCommand cmd = SqlHelper.GetSqlCommand("Update_NEFT-RTGS-Pymt"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter param = new SqlParameter();
                param = cmd.Parameters.Add("@v_UTR_Number", SqlDbType.VarChar, 50); param.Direction = ParameterDirection.Input; if (base.b_UTR_Number) param.Value = base.v_UTR_Number; else param.Value = DBNull.Value;
                param = cmd.Parameters.Add("@d_Transaction_date", SqlDbType.DateTime); param.Direction = ParameterDirection.Input; if (base.b_Transaction_date) param.Value = base.d_Transaction_date; else param.Value = DBNull.Value;
                param = cmd.Parameters.Add("@n_Amount", SqlDbType.Float); param.Direction = ParameterDirection.Input; if (base.b_Amount) param.Value = base.n_Amount; else param.Value = DBNull.Value;
                param = cmd.Parameters.Add("@i_Payee_Organisation_Code", SqlDbType.Int); param.Direction = ParameterDirection.Input; if (base.b_Payee_Organisation_Code) param.Value = base.i_Payee_Organisation_Code; else param.Value = DBNull.Value;
                param = cmd.Parameters.Add("@i_Sponsoring_Organisation_Code", SqlDbType.Int); param.Direction = ParameterDirection.Input; if (base.b_Sponsoring_Organisation_Code) param.Value = base.i_Sponsoring_Organisation_Code; else param.Value = DBNull.Value;
                param = cmd.Parameters.Add("@v_Verified_By", SqlDbType.VarChar, 50); param.Direction = ParameterDirection.Input; if (base.b_Verified_By) param.Value = base.v_Verified_By; else param.Value = DBNull.Value;             
                
                return SqlHelper.ExecuteProcedure(cmd);

            }

        }


    }

}
