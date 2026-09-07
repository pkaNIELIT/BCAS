using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace BCASClassLibrary
{

    public class TYP_rptSuccessfulTransaction 
    {        
      
        public DataTable getSuccessfulTransaction(DateTime fromDate_of_Payment, DateTime toDate_of_Payment)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            DataTable myDt = new DataTable();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetSuccessfulTransaction", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;                   
                    cmd.Parameters.Add(new SqlParameter("@fromDate_of_Payment", SqlDbType.DateTime));
                    cmd.Parameters["@fromDate_of_Payment"].Value = fromDate_of_Payment;
                    cmd.Parameters.Add(new SqlParameter("@toDate_of_Payment", SqlDbType.DateTime));
                    cmd.Parameters["@toDate_of_Payment"].Value = toDate_of_Payment;
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
