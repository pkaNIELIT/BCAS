using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using BCASClassLibrary;

namespace BCASClassLibrary
{
   
    public class TYP_CREATE_USER:TYP_CREATE_USERAttrib 
    {

        public DataTable InsertUserDetails()
        {

            using (SqlCommand cmd = SqlHelper.GetSqlCommand("InsertUserDetails"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter param = new SqlParameter();
                param = cmd.Parameters.Add("@v_UserName", SqlDbType.VarChar, 256); param.Direction = ParameterDirection.Input; if (base.b_UserName) param.Value = base.v_UserName; else param.Value = DBNull.Value;
                param = cmd.Parameters.Add("@i_OrganisationCode", SqlDbType.Int); param.Direction = ParameterDirection.Input; if (base.b_OrganisationCode) param.Value = base.i_OrganisationCode; else param.Value = DBNull.Value;
                param = cmd.Parameters.Add("@c_Organisation_Type", SqlDbType.VarChar, 1); param.Direction = ParameterDirection.Input; if (base.b_Organisation_Type) param.Value = base.c_Organisation_Type; else param.Value = DBNull.Value;
                param = cmd.Parameters.Add("@v_Name", SqlDbType.VarChar, 100); param.Direction = ParameterDirection.Input; if (base.b_Name) param.Value = base.v_Name; else param.Value = DBNull.Value;
                param = cmd.Parameters.Add("@v_Email", SqlDbType.VarChar, 50); param.Direction = ParameterDirection.Input; if (base.b_Email) param.Value = base.v_Email; else param.Value = DBNull.Value;

                param = cmd.Parameters.Add("@v_Mobile_No", SqlDbType.VarChar, 10); param.Direction = ParameterDirection.Input; if (base.b_Mobile_No) param.Value = base.v_Mobile_No; else param.Value = DBNull.Value;

                param = cmd.Parameters.Add("@v_Created_By", SqlDbType.VarChar, 50); param.Direction = ParameterDirection.Input; if (base.b_Created_By) param.Value = base.v_Created_By; else param.Value = DBNull.Value;
                //param = cmd.Parameters.Add("@d_Created_On", SqlDbType.DateTime); param.Direction = ParameterDirection.Input; if (base.b_Created_On) param.Value = base.d_Created_On; else param.Value = DBNull.Value;
                
                return SqlHelper.ExecuteProcedure(cmd);

            }


        }

        //public DataTable getCombinedOrganisation(string orgType)
        //{
        //    string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
        //    DataTable myDt = new DataTable();
        //    using (SqlConnection con = new SqlConnection(ConnectionString))
        //    {
        //        using (SqlCommand cmd = new SqlCommand("GetCombinedOrganisations", con))
        //        {
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.Add(new SqlParameter("@i_Registration_Number", SqlDbType.Int));
        //            cmd.Parameters["@i_Registration_Number"].Value = RegNo;
        //            con.Open();
        //            using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
        //            {
        //                sda.Fill(myDt);
        //            }
        //        }
        //    }
        //    return myDt;
        //}


    }
}
