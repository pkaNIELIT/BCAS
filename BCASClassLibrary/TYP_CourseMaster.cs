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
    public class TYP_CourseMaster : TYP_CourseMasterAttrib
    {
        public DataTable InsertCourseMaster()
        {
            using (SqlCommand cmd = SqlHelper.GetSqlCommand("InsertCourseMaster"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter param = new SqlParameter();
                //param = cmd.Parameters.Add("@i_Course_Code", SqlDbType.Int); param.Direction = ParameterDirection.Input; if (base.b_Course_Code) param.Value = base.i_Course_Code; else param.Value = DBNull.Value;
                param = cmd.Parameters.Add("@v_Course_Name", SqlDbType.VarChar, 100); param.Direction = ParameterDirection.Input; if (base.b_Course_Name) param.Value = base.v_Course_Name; else param.Value = DBNull.Value;
                param = cmd.Parameters.Add("@v_Enter_By", SqlDbType.VarChar, 50); param.Direction = ParameterDirection.Input; if (base.b_Enter_By) param.Value = base.v_Enter_By; else param.Value = DBNull.Value;
                //param = cmd.Parameters.Add("@d_Enter_Date", SqlDbType.DateTime); param.Direction = ParameterDirection.Input; if (base.b_Enter_Date) param.Value = base.d_Enter_Date; else param.Value = DBNull.Value;
                return SqlHelper.ExecuteProcedure(cmd);
            }


        }
    }
}
