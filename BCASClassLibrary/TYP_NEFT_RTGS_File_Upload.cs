using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace BCASClassLibrary
{

    public class TYP_NEFT_RTGS_File_Upload : TYP_NEFT_RTGS_File_UploadAttrib 
    {

        public Int32 UserID;

        public Boolean InsertNEFT_RTGS_File(SqlConnection con, SqlTransaction tran)
        {
            using (SqlCommand cmd = new SqlCommand("InsertRtgs_Neft_File", con, tran))
            {
                cmd.CommandType = CommandType.StoredProcedure; SqlParameter param = new SqlParameter();
                param = cmd.Parameters.Add("@i_Serial_No", SqlDbType.Int); param.Direction = ParameterDirection.Input; if (base.b_Serial_No) param.Value = base.i_Serial_No; else param.Value = DBNull.Value;
                param = cmd.Parameters.Add("@v_UTR_Number", SqlDbType.VarChar, 50); param.Direction = ParameterDirection.Input; if (base.b_UTR_Number) param.Value = base.v_UTR_Number; else param.Value = DBNull.Value;
                param = cmd.Parameters.Add("@v_Sender_IFSC", SqlDbType.VarChar, 50); param.Direction = ParameterDirection.Input; if (base.b_Sender_IFSC) param.Value = base.v_Sender_IFSC; else param.Value = DBNull.Value;
                param = cmd.Parameters.Add("@v_Sender_Name", SqlDbType.VarChar, 100); param.Direction = ParameterDirection.Input; if (base.b_Sender_Name) param.Value = base.v_Sender_Name; else param.Value = DBNull.Value;
                param = cmd.Parameters.Add("@d_Transaction_date", SqlDbType.DateTime); param.Direction = ParameterDirection.Input; if (base.b_Transaction_date) param.Value = base.d_Transaction_date; else param.Value = DBNull.Value;
                param = cmd.Parameters.Add("@n_Amount", SqlDbType.Float); param.Direction = ParameterDirection.Input; if (base.b_Amount) param.Value = base.n_Amount; else param.Value = DBNull.Value;
                param = cmd.Parameters.Add("@v_Enter_By", SqlDbType.VarChar, 50); param.Direction = ParameterDirection.Input; if (base.b_Enter_By) param.Value = base.v_Enter_By; else param.Value = DBNull.Value;
                //param = cmd.Parameters.Add("@d_Enter_date", SqlDbType.DateTime); param.Direction = ParameterDirection.Input; if (base.b_Enter_date) param.Value = base.d_Enter_date; else param.Value = DBNull.Value;

                using (DataTable dt = SqlHelper.ExecuteProcedure(cmd))
                {
                    Int32 ReturnStatus = Convert.ToInt32(dt.Rows[0][0].ToString());
                    if (ReturnStatus == 0)
                        return true;
                    return false;
                }
            }
        }

        //Insert Record from CSV into data base using Data table  */       
        public Int32 PortDataToServer(string fileName, string filePath, string userID)
        {

            Boolean retsatus = false;
            UTL_CSV_FILES MyCsv = new UTL_CSV_FILES();
            MyCsv.WriteSchemaFile(fileName, filePath);

            BCAS_SqlConnention bcasCon = new BCAS_SqlConnention();

            using (SqlConnection conn = new SqlConnection(bcasCon.GetBCASConnectionString()))
            {
                SqlTransaction tran;
                conn.Open();
                tran = conn.BeginTransaction();
                try
                {

                    UTL_CSV_FILES UpldData = new UTL_CSV_FILES();
                    DataTable dt = new DataTable();
                    dt = UpldData.ReadCsvFile(fileName, filePath);

                    if (dt.Rows.Count > 0)
                    {
                       
                        foreach (DataRow dr in dt.Rows)
                        {
                            //Assign The values to atributes

                            i_Serial_No = Convert.ToInt32(dr["Serial_No"].ToString().Trim ());
                            v_UTR_Number = dr["UTR_Number"].ToString().Trim();
                            v_Sender_IFSC = dr["Sender_IFSC"].ToString().Trim();
                            v_Sender_Name = dr["Sender_Name"].ToString().Trim();
                            d_Transaction_date = Convert.ToDateTime(dr["Transaction_date"].ToString().Trim());
                            n_Amount = Convert.ToInt32(dr["Amount"].ToString().Trim());
                            v_Enter_By = userID;

                            retsatus = InsertNEFT_RTGS_File(conn, tran);
                            if (!retsatus)
                            {
                                break;
                            }

                        }

                    }
                    if (retsatus)
                    {
                        tran.Commit();
                        return 1;
                    }
                    else
                    {
                        tran.Rollback();
                        return 0;
                    }
                }
                catch
                {
                    tran.Rollback();
                    return 0;
                }


            }


        }


        public DataTable DeleteNEFT_RTGS_File()
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            DataTable myDt = new DataTable();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("DeleteRtgs_Neft_File", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(myDt);
                    }
                }
            }
            return myDt;
        }


        public DataTable GetDuplicateRtgs_Neft_Details()
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            DataTable myDt = new DataTable();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetDuplicateRecords_Rtgs_Neft_Details", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(myDt);
                    }
                }
            }
            return myDt;
        }


        public DataTable InsertRtgs_Neft_Details()
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            DataTable myDt = new DataTable();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("InsertRtgs_Neft_Details", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(myDt);
                    }
                }
            }
            return myDt;
        }


        public DataTable CSV_BulkInsert_NEFT_RTGS_File()
        {
            using (SqlCommand cmd = SqlHelper.GetSqlCommand("CSV_Bulk_Insert"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter param = new SqlParameter();
                param = cmd.Parameters.Add("@v_filePath", SqlDbType.VarChar, 1000); param.Direction = ParameterDirection.Input; if (base.b_filePath) param.Value = base.v_filePath; else param.Value = DBNull.Value;                
                return SqlHelper.ExecuteProcedure(cmd);

            }

        }


        public DataTable BULK_InsertRtgs_Neft_Details()
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            DataTable myDt = new DataTable();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("BULK_InsertRtgs_Neft_Details", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    //*******
                    SqlParameter param = new SqlParameter();
                    param = cmd.Parameters.Add("@v_Enter_By", SqlDbType.VarChar, 50); param.Direction = ParameterDirection.Input; if (base.b_Enter_By) param.Value = base.v_Enter_By; else param.Value = DBNull.Value;
                    //*******
                    con.Open();
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(myDt);
                    }
                }
            }
            return myDt;
        }


        public DataTable Manual_Insert_Rtgs_Neft_Details()
        {
            using (SqlCommand cmd = SqlHelper.GetSqlCommand("Manual_Insert_Rtgs_Neft_Details"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter param = new SqlParameter();
                param = cmd.Parameters.Add("@i_Serial_No", SqlDbType.Int); param.Direction = ParameterDirection.Input; if (base.b_Serial_No) param.Value = base.i_Serial_No; else param.Value = DBNull.Value;
                param = cmd.Parameters.Add("@v_UTR_Number", SqlDbType.VarChar, 50); param.Direction = ParameterDirection.Input; if (base.b_UTR_Number) param.Value = base.v_UTR_Number; else param.Value = DBNull.Value;
                param = cmd.Parameters.Add("@v_Sender_IFSC", SqlDbType.VarChar, 50); param.Direction = ParameterDirection.Input; if (base.b_Sender_IFSC) param.Value = base.v_Sender_IFSC; else param.Value = DBNull.Value;
                param = cmd.Parameters.Add("@v_Sender_Name", SqlDbType.VarChar, 100); param.Direction = ParameterDirection.Input; if (base.b_Sender_Name) param.Value = base.v_Sender_Name; else param.Value = DBNull.Value;
                param = cmd.Parameters.Add("@d_Transaction_date", SqlDbType.DateTime); param.Direction = ParameterDirection.Input; if (base.b_Transaction_date) param.Value = base.d_Transaction_date; else param.Value = DBNull.Value;
                param = cmd.Parameters.Add("@n_Amount", SqlDbType.Float); param.Direction = ParameterDirection.Input; if (base.b_Amount) param.Value = base.n_Amount; else param.Value = DBNull.Value;
                param = cmd.Parameters.Add("@v_Enter_By", SqlDbType.VarChar, 50); param.Direction = ParameterDirection.Input; if (base.b_Enter_By) param.Value = base.v_Enter_By; else param.Value = DBNull.Value;
               
                return SqlHelper.ExecuteProcedure(cmd);

            }

        }

        
    }

}
