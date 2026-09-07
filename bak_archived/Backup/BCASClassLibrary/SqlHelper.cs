using System;
using System.Data;
using System.Data.SqlClient;
using BCASClassLibrary;

namespace BCASClassLibrary
{
    public static class SqlHelper
    {
        public static SqlCommand GetSqlCommand(string CommandText)
        {

            BCAS_SqlConnention AppCon = new BCAS_SqlConnention();            
            return AppCon.GetSqlCommand(CommandText);
        }

        //Execute Procedure and return the result table
        public static DataTable ExecuteProcedure(SqlCommand cmd)
        {
            using (DataSet ds = new DataSet())
            {
                using (SqlDataAdapter adp = new SqlDataAdapter(cmd))
                {
                    adp.Fill(ds);
                    return ds.Tables[0];
                }

            }
        }

    }
}
