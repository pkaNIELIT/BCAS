using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using BCASClassLibrary;


namespace BCASClassLibrary
{
    public class Utility
    {


        #region Connection String

        public string constr = "Server = MISIDDIQUE; Database = BCAS; Uid=sa; Pwd = sa123;";           //FOR ARIF        

        #endregion


        public Boolean CheckDDMMYYYY(string ValDate)
        {
            string strDocDate = ValDate;
            if (strDocDate.Length < 10 || strDocDate.Length > 10)
            {
                return false;
            }
            else if (strDocDate.IndexOf("/") < 0)
            {
                return false;
            }
            else if (int.Parse(strDocDate.Substring(0, 2)) > 31)
            {
                return false;
            }
            else if (int.Parse(strDocDate.Substring(3, 2)) > 12)
            {
                return false;
            }

            else
            {
                return true;
            }
        }

        // Convert DD/MM/YYYY format to mm/dd/yyyy format from string value
        public string ConvertMMDDYYYY(string ValDate)
        {
            string dat;
            dat = ValDate.Substring(3, 2) + '/' + ValDate.Substring(0, 2) + '/' + ValDate.Substring(6, 4);
            return dat;
        }

        // Convert DD/MM/YYYY format to mm/dd/yyyy format from string value
        public string ConvertMMDDYYYY(DateTime ValDate)
        {
            string dat = ValDate.ToString();
            dat = dat.Substring(0, 2) + '/' + dat.Substring(3, 2) + '/' + dat.Substring(6, 4);
            return dat;
        }


        //public static DataSet GetAllLETTERS_RECEIVED()
        //{
        //    string ConnectionString = ConfigurationManager.ConnectionStrings["RSBConnString"].ConnectionString;
        //    DataSet myset = new DataSet();
        //    using (SqlConnection con = new SqlConnection(ConnectionString))
        //    {
        //        using (SqlCommand cmd = new SqlCommand("GetAllLETTERS_RECEIVED", con))
        //        {
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            con.Open();
        //            using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
        //            {
        //                sda.Fill(myset);
        //            }
        //        }
        //    }
        //    return myset;
        //}


        //public DataTable GetPassword(string UserName)
        //{

        //    string ConnectionString = ConfigurationManager.ConnectionStrings["CrisExamConnectionString"].ConnectionString;
        //    DataTable myDt = new DataTable();
        //    using (SqlConnection con = new SqlConnection(ConnectionString))
        //    {
        //        using (SqlCommand cmd = new SqlCommand("GetPassword", con))
        //        {
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.Add(new SqlParameter("@UserName", SqlDbType.NVarChar, 256));
        //            cmd.Parameters["@UserName"].Value = UserName;
        //            con.Open();
        //            using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
        //            {
        //                sda.Fill(myDt);
        //            }
        //        }
        //    }
        //    return myDt;
        //}

        public DataTable ReturnDataTable(string SqlQry)
        {
            DataTable obdt = new DataTable();
            SqlDataAdapter obda = new SqlDataAdapter(SqlQry, constr);
            obdt.Clear();
            obda.Fill(obdt);
            return obdt;

        }

        
    }
}
