using System;
using System.Text;

using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Configuration;
using System.Collections.Generic;

namespace BCASClassLibrary
{
    public class TYP_MENUMASTER 
    {

        /// <summary>
        /// private bool m_IsNull; 
        /// To Determine Whether Current object is null or not
        /// </summary>
        
        private bool m_IsNull;

        /// <summary>
        /// Class Fields...same as in Table
        /// </summary>

        private int m_MENUITEMCODE;
        private string m_MENUDESCRIPTION;
        private int m_MENULEVEL;
        private string m_WHETHERLEAFMENU;
        private int m_PARENTMENUITEM;
        private string m_FORMNAME;

        /// <summary>
        /// Overloaded Constructors of current class
        /// </summary>
               
        public TYP_MENUMASTER()
        {
            // TODO : Add code to initialise the object
            
        }

        public TYP_MENUMASTER(string str)
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

        public static TYP_MENUMASTER Null
        {
            get
            {
                TYP_MENUMASTER obj = new TYP_MENUMASTER();
                obj.m_IsNull = true;
                return obj;
            }
        }

        /// <summary>
        /// Code of various properties
        /// </summary>

        public int MENUITEMCODE
        {
            get
            {
                return this.m_MENUITEMCODE;
            }
            set
            {
                this.m_MENUITEMCODE = value;
            }
        }

        public string MENUDESCRIPTION
        {
            get
            {
                return this.m_MENUDESCRIPTION;
            }
            set
            {
                this.m_MENUDESCRIPTION = value;
            }
        }

        public int MENULEVEL
        {
            get
            {
                return this.m_MENULEVEL;
            }
            set
            {
                this.m_MENULEVEL = value;
            }
        }

        public string WHETHERLEAFMENU
        {
            get
            {
                return this.m_WHETHERLEAFMENU;
            }
            set
            {
                this.m_WHETHERLEAFMENU = value;
            }
        }

        public int PARENTMENUITEM
        {
            get
            {
                return this.m_PARENTMENUITEM;
            }
            set
            {
                this.m_PARENTMENUITEM = value;
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
        /// <summary>
        /// GetFormNames is used to retrieve all the Form Names
        /// Depending upon the role of the current User
        /// </summary>
        /// <returns></returns>
        public static DataSet GetFormNames()
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            DataSet myset = new DataSet();

            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("GetFormNames", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            con.Open();
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
        /// GetMenuData for retreiving Main Menu Data
        /// </summary>
        /// <returns></returns>
        public static DataTable GetMenuData(string pUserName)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            DataTable dtblMenu = new DataTable();
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("GetMenuData", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@USERNAME", SqlDbType.VarChar, 50));
                        cmd.Parameters["@USERNAME"].Value = pUserName;

                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            con.Open();
                            sda.Fill(dtblMenu);
                        }
                    }
                }
                return dtblMenu;
            }
            catch (SqlException)
            {
                // Replace the error with something less specific.
                // You could also log the error now.
                throw new ApplicationException("Data error.");
            }
        }
    }
}
