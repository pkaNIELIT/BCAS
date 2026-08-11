using System;
using System.Collections.Generic;
using System.Data.SqlClient ;
using System.Data;
using BCASClassLibrary;

namespace BCASClassLibrary
{
    public class TYP_DB_Backup : DatabaseBkpAttrib 
    {
        
        public bool  Database_Backup()
        {
            BCAS_SqlConnention conStr = new BCAS_SqlConnention();
            conStr.GetBCASConnectionString ();
            string con1;
            con1 = conStr.GetBCASConnectionString ();
            SqlConnection conn = new SqlConnection(con1);       
           
            using (SqlCommand cmd = SqlHelper.GetSqlCommand("DB_Backup"))
            {
                try
                { 
                        cmd.Connection = conn;
                        conn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlParameter param = new SqlParameter();
                        param = cmd.Parameters.Add("@v_filePath", SqlDbType.VarChar, 1000); param.Direction = ParameterDirection.Input; if (base.b_filePath) param.Value = base.v_filePath; else param.Value = DBNull.Value;
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        return true ;
                }
                catch (Exception ex)
                {
                    return false ;
                    

                }             

            }
        }    


    }


}
