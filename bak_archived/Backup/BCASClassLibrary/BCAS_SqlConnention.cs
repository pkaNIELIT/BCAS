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
    class BCAS_SqlConnention
    {

        // Retrive the connection to use
        public string GetBCASConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["BCASConnectionString"].ToString();
        }

        // Identify the Connection String and combine it with connection and command objects and return command object
        // It recomended to distruct these connection and command object in furthe code
        public SqlCommand GetSqlCommand(string CommandText)
        {
            SqlConnection Conn = new SqlConnection(GetBCASConnectionString());            
            SqlCommand cmd = new SqlCommand(CommandText);
            cmd.Connection = Conn;
            return cmd;
        }

    }


   
}
