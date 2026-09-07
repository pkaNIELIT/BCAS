using System;
using System.Text;

using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Configuration;
using System.Collections.Generic;

namespace BCASClassLibrary
{
    public class TYP_USERROLEFORM
    {
        /// <summary>
        /// private bool m_IsNull; 
        /// To Determine Whether Current object is null or not
        /// </summary>
        private bool m_IsNull;

        /// <summary>
        /// Class Fields...same as in Table
        /// </summary>
        private string m_ROLENAME;
        private string m_FORMNAME;
        private string m_ENTER_BY;
        private System.DateTime m_ENTER_DATE;

        /// <summary>
        /// Overloaded Constructors of current class
        /// </summary>
        public TYP_USERROLEFORM()
        {
            // TODO : Add code to initialise the object
        }

        public TYP_USERROLEFORM(string str)
        {
            // TODO : Add code to initialise the object based on the given string 
        }

        /// <summary>
        /// Implementation of Null Interface
        /// </summary>
        public virtual bool IsNull
        {
            get
            {
                return this.m_IsNull;
            }
        }

        public static TYP_USERROLEFORM Null
        {
            get
            {
                TYP_USERROLEFORM obj = new TYP_USERROLEFORM();
                obj.m_IsNull = true;
                return obj;
            }
        }

        /// <summary>
        /// Code of various properties
        /// </summary>
        public string ROLENAME
        {
            get
            {
                return this.m_ROLENAME;
            }
            set
            {
                this.m_ROLENAME = value;
            }
        }

        public string FORMNAME
        {
            get
            {
                return this.m_FORMNAME;
            }
            set
            {
                this.m_FORMNAME = value;
            }
        }

        public string ENTER_BY
        {
            get
            {
                return this.m_ENTER_BY;
            }
            set
            {
                this.m_ENTER_BY = value;
            }
        }

        public System.DateTime ENTER_DATE
        {
            get
            {
                return this.m_ENTER_DATE;
            }
            set
            {
                this.m_ENTER_DATE = value;
            }
        }

        /// <summary>
        /// Select method for retrieving Data Set
        /// </summary>
        /// <returns></returns>
        public static DataSet GetAllRoleForm()
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            DataSet myset = new DataSet();
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand("GetAllRoleForm", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            sda.Fill(myset);
                        }
                    }
                }
                return myset;
            }
            catch (SqlException)
            {
                // Replace the error with something less specific.
                // You could also log the error now.
                throw new ApplicationException("Data error.");
            }
        }

        /// <summary>
        /// Get Roles corresponsing to Forms or vice versa
        /// </summary>
        /// <param name="pRoleName"></param>
        /// <param name="pFormName"></param>
        /// <returns></returns>
        public static DataSet GetRoleForm(string pRoleName, string pFormName)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;

            DataSet myset = new DataSet();
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("GetRoleForm", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new SqlParameter("@ROLENAME", SqlDbType.VarChar, 40));
                        cmd.Parameters["@ROLENAME"].Value = pRoleName;
                        cmd.Parameters.Add(new SqlParameter("@FORMNAME", SqlDbType.VarChar, 100));
                        cmd.Parameters["@FORMNAME"].Value = pFormName;

                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            sda.Fill(myset);
                        }
                    }
                }
                return myset;
            }
            catch (SqlException err)
            {
                // Replace the error with something less specific.
                // You could also log the error now.
                throw new ApplicationException("Data error.");
            }
        }

        /// <summary>
        /// Select all Forms pertaining to current Role
        /// </summary>
        /// <param name="pRoleName"></param>
        /// <returns></returns>
        public static DataSet GetFormsForRole(string pRoleName)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;

            DataSet myset = new DataSet();
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("GetFormsForRole", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new SqlParameter("@ROLENAME", SqlDbType.VarChar, 40));
                        cmd.Parameters["@ROLENAME"].Value = pRoleName;
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            sda.Fill(myset);
                        }
                    }
                }
                return myset;
            }
            catch (SqlException err)
            {
                // Replace the error with something less specific.
                // You could also log the error now.
                throw new ApplicationException("Data error.");
            }
        }

        /// <summary>
        /// Delete method for deleting current Record
        /// </summary>
        /// <param name="pRoleName"></param>
        /// <param name="pFormName"></param>
        public static void Delete(string pRoleName, string pFormName)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;

            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("DeleteRoleForm", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@ROLENAME", SqlDbType.VarChar, 40));
                        cmd.Parameters["@ROLENAME"].Value = pRoleName;
                        cmd.Parameters.Add(new SqlParameter("@FORMNAME", SqlDbType.VarChar, 100));
                        cmd.Parameters["@FORMNAME"].Value = pFormName;

                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException err)
            {
                // Replace the error with something less specific.
                // You could also log the error now.
                throw new ApplicationException("Data error.");
            }
        }

        /// <summary>
        /// Insert method for inserting New Records
        /// </summary>
        /// <returns></returns>
        public static void Insert(string pFormName, string pRoleName, string enter_by)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("InsertRoleForm", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        //Parameter Details---------------------------------------
                        cmd.Parameters.Add(new SqlParameter("@ROLENAME", SqlDbType.VarChar, 40));
                        cmd.Parameters["@ROLENAME"].Value = pRoleName;
                        cmd.Parameters.Add(new SqlParameter("@FORMNAME", SqlDbType.VarChar, 100));
                        cmd.Parameters["@FORMNAME"].Value = pFormName;
                        cmd.Parameters.Add(new SqlParameter("@ENTER_BY", SqlDbType.VarChar, 10));
                        cmd.Parameters["@ENTER_BY"].Value = enter_by;
                        cmd.Parameters.Add(new SqlParameter("@ENTER_DATE", SqlDbType.DateTime));
                        cmd.Parameters["@ENTER_DATE"].Value = DateTime.Now.Date;
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException err)
            {
                // Replace the error with something less specific.
                // You could also log the error now.
                throw new ApplicationException("Data error.");
            }
        }

        /// <summary>
        /// Update method for updating current Record
        /// </summary>
        /// <returns></returns>
        public static void Update(TYP_USERROLEFORM newRecord)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;

            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("UpdateRoleForm", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        //Parameter Details---------------------------------------
                        cmd.Parameters.Add(new SqlParameter("@ROLENAME", SqlDbType.VarChar, 40));
                        cmd.Parameters["@ROLENAME"].Value = newRecord.ROLENAME;
                        cmd.Parameters.Add(new SqlParameter("@FORMNAME", SqlDbType.VarChar, 100));
                        cmd.Parameters["@FORMNAME"].Value = newRecord.FORMNAME;
                        cmd.Parameters.Add(new SqlParameter("@ENTER_BY", SqlDbType.VarChar, 10));
                        cmd.Parameters["@ENTER_BY"].Value = newRecord.ENTER_BY;
                        cmd.Parameters.Add(new SqlParameter("@ENTER_DATE", SqlDbType.DateTime));
                        cmd.Parameters["@ENTER_DATE"].Value = newRecord.ENTER_DATE;
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException err)
            {
                // Replace the error with something less specific.
                // You could also log the error now.
                throw new ApplicationException("Data error.");
            }
        }
    }
}
