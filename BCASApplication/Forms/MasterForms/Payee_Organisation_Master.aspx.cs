using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using BCASClassLibrary;
using System.IO;

public partial class Payee_Organisation_Master : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Convert.ToString(HttpContext.Current.Session["UserId"]).Trim().Length == 0))
        {
            HttpContext.Current.Response.Redirect("~/Home.aspx");
        }
        else
        {

            if (!Page.IsPostBack)
            {
                enabled_disabled_controls('L');
                BindDropDown();
            }
        }
    }

    
    protected void BindDropDown()
    {
        drpState.Items.Clear();
        // ListItem li = new ListItem("Select", "0");
        // drpState.Items.Add(li);
        
        DataSet ds = new DataSet();
        
        ds = GetStates();
        int i = 0;
        while (i < ds.Tables[0].Rows.Count)
        {
            ListItem li = new ListItem();
            li.Value = ds.Tables[0].Rows[i][0].ToString();
            li.Text = ds.Tables[0].Rows[i][1].ToString();
            i++;
            drpState.Items.Add(li);
        }
        drpState.DataBind();
        //drpState.Items[0].Selected = true;
    }


    public class TYP_PAYEE_ORGANISATION_MASTER
    {
        /// <summary>
        /// private bool m_IsNull; 
        /// To Determine Whether Current object is null or not
        /// </summary>
        private bool m_IsNull;

        /// <summary>
        /// Class Fields...same as in Table
        /// </summary>
        private int m_PAYEE_ORGANISATION_CODE;
        private string m_PAYEE_ORGANISATION_NAME;
        private string m_States;
        private int m_SCode;
        private string m_GSTIN;
        private string m_ENTER_BY;
        private System.DateTime m_ENTER_DATE;
        //ForPincode_18_10_2024
        private long m_PAYEE_LOCATION_PINCODE;
        private string Payee_Address;
        /// <summary>
        /// Overloaded Constructors of current class
        /// </summary>
        public TYP_PAYEE_ORGANISATION_MASTER()
        {
            // TODO : Add code to initialise the object
        }

        public TYP_PAYEE_ORGANISATION_MASTER(string str)
        {
            // TODO : Add code to initialise the object based on the given string 
        }

        /// <summary>
        /// Implementation of Null Interface
        /// </summary>
        public virtual bool IsNull
        {
            get { return this.m_IsNull; }
        }

        public static TYP_PAYEE_ORGANISATION_MASTER Null
        {
            get
            {
                TYP_PAYEE_ORGANISATION_MASTER obj = new TYP_PAYEE_ORGANISATION_MASTER();
                obj.m_IsNull = true;
                return obj;
            }
        }

        /// <summary>
        /// Code of various properties
        /// </summary>
        public int PAYEE_ORGANISATION_CODE
        {
            get { return this.m_PAYEE_ORGANISATION_CODE; }
            set { this.m_PAYEE_ORGANISATION_CODE = value; }
        }
        public string PAYEE_ORGANISATION_NAME
        {
            get { return this.m_PAYEE_ORGANISATION_NAME; }
            set { this.m_PAYEE_ORGANISATION_NAME = value; }
        }
        public int SCODE
        {
            get { return this.m_SCode; }
            set { this.m_SCode = value; }
        }
        public string States
        {
            get { return this.m_States; }
            set { this.m_States = value; }
        }
        public string GSTIN
        {
            get { return this.m_GSTIN; }
            set { this.m_GSTIN = value; }
        }
        public string ENTER_BY
        {
            get { return this.m_ENTER_BY; }
            set { this.m_ENTER_BY = value; }
        }
        //ForPincode_18_08_2024
        public string PAYEE_ADDRESS
        {
            get { return this.Payee_Address; }
            set { this.Payee_Address = value; }
        } 
        public long PAYEE_LOCATION_PINCODE
        {
            get { return this.m_PAYEE_LOCATION_PINCODE; }
            set { this.m_PAYEE_LOCATION_PINCODE = value; }
        }
        public System.DateTime ENTER_DATE
        {
            get { return this.m_ENTER_DATE; }
            set { this.m_ENTER_DATE = value; }
        }

    }

    protected void btn_insert_Click(object sender, EventArgs e)
    {
        lbl_choice.Text = "I";
        enabled_disabled_controls('I');
        txt_payee_organisation_name.Focus();
    }

    protected void btn_update_Click(object sender, EventArgs e)
    {
        lbl_choice.Text = "U";
        enabled_disabled_controls('U');
        txt_payee_organisation_name.Focus();
    }

    protected void btn_search_Click(object sender, EventArgs e)
    {
        lbl_choice.Text = "F";
        enabled_disabled_controls('F');
        txt_payee_organisation_code.Focus();
    }

    protected void btn_save_Click(object sender, EventArgs e)
    {
        if (lbl_choice.Text == "I" || lbl_choice.Text == "U")
        {
            lbl_errors.Visible = true;
            if (!ValidateData())
                return;
            lbl_errors.Visible = false;
        }

        try
        {
           
            TYP_PAYEE_ORGANISATION_MASTER newobj = new TYP_PAYEE_ORGANISATION_MASTER();

            if (lbl_choice.Text == "I")
                newobj.PAYEE_ORGANISATION_CODE = 0;
            else
                newobj.PAYEE_ORGANISATION_CODE = Convert.ToInt32(txt_payee_organisation_code.Text);
            newobj.PAYEE_ORGANISATION_NAME = txt_payee_organisation_name.Text.ToUpper();
            //ForPincode_30_07_2024
            newobj.PAYEE_LOCATION_PINCODE = Convert.ToInt64(txt_payee_location_pincode.Text);
            newobj.States = drpState.SelectedItem.Text;
            newobj.SCODE = Convert.ToInt32(txt_scode.Text.Trim().Length==0? "0":txt_scode.Text.Trim());
            newobj.GSTIN = txtGSTIN.Text;
            newobj.ENTER_DATE = DateTime.Now;
            newobj.ENTER_BY = HttpContext.Current.User.Identity.Name;
            newobj.PAYEE_ADDRESS = txt_payee_address.Text.Trim();

            switch (lbl_choice.Text)
            {
                case "I":
                    //txt_payee_organisation_code.Text = TYP_PAYEE_ORGANISATION_MASTER.InsertPayee_Organisation_Master(newobj).ToString();
                    //Date 18/102024
                    txt_payee_organisation_code.Text =InsertPayee_Organisation_Master(newobj).ToString();
                    lbl_error.Text = " Record Successfully Saved ";
                    break;
                case "U":
                    // TYP_PAYEE_ORGANISATION_MASTER.UpdatePayee_Organisation_Master(newobj);
                    //date 18/102024
                    UpdatePayee_Organisation_Master(newobj);
                    lbl_error.Text = " Record Successfully Updated ";
                    break;
                //case "D":
                //    TYP_PAYEE_ORGANISATION_MASTER.DeletePayee_Organisation_Master(Convert.ToInt32(txt_payee_organisation_code.Text));
                //    lbl_error.Text = " Record Successfully Deleted ";
                //    break;
            }

            GridParameter(txt_payee_organisation_code.Text, "*");
            enabled_disabled_controls('S');
        }
        catch (SqlException ex)
        {
            Int32 ErrorCode = ex.Number;

            switch (ErrorCode)
            {
                case 2627:
                    lbl_error.Text = "Primary key Already Exists, unique constraint violated";
                    break;
                default:
                    lbl_error.Text = ex.Message;
                    break;
            }
            enabled_disabled_controls('N');
        }

        finally
        {
            btn_insert.Focus();
        }
    }

    protected void btn_cancel_Click(object sender, EventArgs e)
    {
        enabled_disabled_controls('C');
        btn_insert.Focus();
    }

  
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Set values from the selected row into textboxes
        txt_payee_organisation_code.Text = GridView1.SelectedRow.Cells[1].Text;
        txt_payee_organisation_name.Text = GridView1.SelectedRow.Cells[2].Text;
        txt_payee_location_pincode.Text = GridView1.SelectedRow.Cells[8].Text;
        txt_scode.Text= GridView1.SelectedRow.Cells[4].Text;

        var x = drpState.Items.Count;
        // Handle drop-down selection for state
        if (GridView1.SelectedRow.Cells[3].Text != "&nbsp;" && drpState.Items.FindByText(GridView1.SelectedRow.Cells[3].Text) != null)
        {
            drpState.SelectedItem.Text = GridView1.SelectedRow.Cells[3].Text;
        }

        // Handle GSTIN if it's not empty
        if (GridView1.SelectedRow.Cells[5].Text != "&nbsp;")
        {
            txtGSTIN.Text = GridView1.SelectedRow.Cells[5].Text;
        }

        if (GridView1.SelectedRow.Cells[9].Text != "&nbsp;")
        {
            txt_payee_address.Text = GridView1.SelectedRow.Cells[9].Text;
        }

        // Enable or disable controls based on your requirement
        enabled_disabled_controls('G');
    }

    void enabled_disabled_controls(char choice)
    {
        switch (choice)
        {
            case 'L': //Login
                lbl_error.Text = "";
                btn_insert.Enabled = true;
                btn_search.Enabled = true;
                //ForPincodeUpdate_30_07_2024
                btn_update.Enabled = true;
                btn_save.Enabled = false;
                btn_cancel.Enabled = false;
                
                enabled_disabled_validation(false);
                frm_panel.Enabled = false;
                break;

            case 'N':  //Exception
                btn_insert.Enabled = false;
                btn_search.Enabled = false;
                btn_update.Enabled = false;
                btn_save.Enabled = true;
                btn_cancel.Enabled = true;
                
                frm_panel.Enabled = true;
                enabled_disabled_validation(true);
                enabled_disabled_controls(true);
                break;
            case 'S':
                // on save
                btn_insert.Enabled = true;
                btn_search.Enabled = true;
                btn_update.Enabled = false;
                btn_save.Enabled = false;
                btn_cancel.Enabled = false;
               

                enabled_disabled_validation(false);
                frm_panel.Enabled = false;
                enabled_disabled_controls(false);
                clear_contents();
                break;
            case 'C':
                //on  cancel
                btn_insert.Enabled = true;
                btn_search.Enabled = true;
                btn_update.Enabled = false;
                btn_save.Enabled = false;
                btn_cancel.Enabled = false;
                

                enabled_disabled_validation(false);
                frm_panel.Enabled = false;
                enabled_disabled_controls(false);
                clear_contents();
                break;
            case 'I':
                lbl_error.Text = "";
                btn_insert.Enabled = false;
                btn_update.Enabled = false;
                btn_search.Enabled = false;
               
                btn_save.Enabled = true;
                btn_cancel.Enabled = true;
                frm_panel.Enabled = true;
                enabled_disabled_validation(true);
                enabled_disabled_controls(true);
                txt_payee_organisation_code.Enabled = true;
                break;
            case 'F':
                btn_insert.Enabled = false;
                btn_search.Enabled = false;
                btn_update.Enabled = false;
                
                btn_save.Enabled = false;
                btn_cancel.Enabled = true;
                frm_panel.Enabled = true;
                enabled_disabled_validation(false);
                enabled_disabled_controls(true);
                break;
            case 'U':
                btn_insert.Enabled = false;
                btn_update.Enabled = false;
                btn_search.Enabled = false;
                
                btn_save.Enabled = true;
                btn_cancel.Enabled = true;
                frm_panel.Enabled = true;
                enabled_disabled_validation(true);
                enabled_disabled_controls(true);
                txt_payee_organisation_code.Enabled = false;
                //ForPincode_30_07_2024
                txt_payee_location_pincode.Enabled = true;
                break;
            case 'G':
                lbl_error.Text = "";
                btn_insert.Enabled = false;
                btn_search.Enabled = false;
                btn_update.Enabled = true;
                
                btn_save.Enabled = false;
                btn_cancel.Enabled = true;

                enabled_disabled_validation(false);
                frm_panel.Enabled = false;
                break;
        };
    }

    void clear_contents()
    {
        lbl_choice.Text = "";
        lbl_error.Text = "";
        txt_payee_organisation_code.Text = "";
        txt_payee_organisation_name.Text = "";
        txtGSTIN.Text = "";
        txt_payee_address.Text = string.Empty;
        //ForPincode_30_07_2024
        txt_payee_location_pincode.Text = "";
        BindDropDown();
        
    }

    void enabled_disabled_controls(bool flag)
    {
        txt_payee_organisation_code.Enabled = flag;
        txt_payee_organisation_name.Enabled = flag;
        txt_payee_address.Enabled = flag;
        //ForPincode_30_07_2024
        txt_payee_location_pincode.Enabled = flag;
    }

    void enabled_disabled_validation(bool flag)
    {
        mev_payee_organisation_name.Enabled = flag;
        txt_payee_address.Enabled = flag;
        //ForPincode_30_07_2024
        mev_payee_location_pincode.Enabled = flag;
    }
           
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            foreach (DataControlFieldCell cell in e.Row.Cells)
            {
                cell.Width = cell.Text.Trim().Length;
            }
        }
        else if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[7].Text = Convert.ToDateTime(e.Row.Cells[7].Text).Date.ToString("dd-MM-yyyy").ToUpper().Trim();




        }
    }
    protected void btn_display_Click(object sender, EventArgs e)
    {
        lbl_error.Text = "";
        string pPayee_Organisation_Code, pPayee_Organisation_Name;

        if (txt_payee_organisation_code.Text == "")
            pPayee_Organisation_Code = "-1";
        else
            pPayee_Organisation_Code = txt_payee_organisation_code.Text.ToUpper();

        if (txt_payee_organisation_name.Text == "")
            pPayee_Organisation_Name = "*";
        else
            pPayee_Organisation_Name = txt_payee_organisation_name.Text.ToUpper();

             //-----Setting Grid Parameters-----------------------------------------
        GridParameter(pPayee_Organisation_Code, pPayee_Organisation_Name);
    }

    private void GridParameter(string pPayee_Organisation_Code, string pPayee_Organisation_Name)
    {
        Parameter objPayee_Organisation_CodeParameter = new Parameter("pPayee_Organisation_Code", TypeCode.String, pPayee_Organisation_Code);
        Parameter objPayee_Organisation_NameParameter = new Parameter("pPayee_Organisation_Name", TypeCode.String, pPayee_Organisation_Name);

        //set up object data source parameters
        ObjectDataSource1.SelectParameters["pPayee_Organisation_Code"] = objPayee_Organisation_CodeParameter;
        ObjectDataSource1.SelectParameters["pPayee_Organisation_Name"] = objPayee_Organisation_NameParameter;

        //Rebinding Data Grid
        GridView1.DataBind();
    }




    public static int InsertPayee_Organisation_Master(TYP_PAYEE_ORGANISATION_MASTER newRecord)
    {
        string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("InsertPayee_Organisation_Master", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Payee_Organisation_Code", SqlDbType.Int, 10));
                cmd.Parameters["@Payee_Organisation_Code"].Direction = ParameterDirection.Output;
                cmd.Parameters.Add(new SqlParameter("@Payee_Organisation_Name", SqlDbType.VarChar, 100));
                cmd.Parameters["@Payee_Organisation_Name"].Value = newRecord.PAYEE_ORGANISATION_NAME;
                cmd.Parameters.Add(new SqlParameter("@SCode", SqlDbType.Int, 10));
                cmd.Parameters["@SCode"].Value = Convert.ToInt32(newRecord.SCODE);
                cmd.Parameters.Add(new SqlParameter("@States", SqlDbType.VarChar, 50));
                cmd.Parameters["@States"].Value = newRecord.States;
                cmd.Parameters.Add(new SqlParameter("@GSTIN", SqlDbType.VarChar, 50));
                cmd.Parameters["@GSTIN"].Value = newRecord.GSTIN;
                cmd.Parameters.Add(new SqlParameter("@Enter_By", SqlDbType.VarChar, 50));
                cmd.Parameters["@Enter_By"].Value = newRecord.ENTER_BY;
                cmd.Parameters.Add(new SqlParameter("@Enter_Date", SqlDbType.DateTime));
                cmd.Parameters["@Enter_Date"].Value = newRecord.ENTER_DATE;
                //AddedForPincode_30_07_2024
                cmd.Parameters.Add(new SqlParameter("@Payee_Location_Pincode", SqlDbType.BigInt));
                cmd.Parameters["@Payee_Location_Pincode"].Value = newRecord.PAYEE_LOCATION_PINCODE;
                //Added 16/10/2024
                cmd.Parameters.Add(new SqlParameter("@Payee_address", SqlDbType.VarChar, 4000));
                cmd.Parameters["@Payee_address"].Value = newRecord.PAYEE_ADDRESS;

                SqlParameter updateFlagParam = new SqlParameter("@insert_flag", SqlDbType.TinyInt);
                updateFlagParam.Direction = ParameterDirection.Output;  // Set as output
                cmd.Parameters.Add(updateFlagParam);

                con.Open();
                cmd.ExecuteNonQuery();
                return (int)cmd.Parameters["@Payee_Organisation_Code"].Value;
            }
        }
    }


    public static void UpdatePayee_Organisation_Master(TYP_PAYEE_ORGANISATION_MASTER newRecord)
    {
        string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("UpdatePayee_Organisation_Master", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Payee_Organisation_Code", SqlDbType.Int, 10));
                cmd.Parameters["@Payee_Organisation_Code"].Value = newRecord.PAYEE_ORGANISATION_CODE;
                cmd.Parameters.Add(new SqlParameter("@Payee_Organisation_Name", SqlDbType.VarChar, 100));
                cmd.Parameters["@Payee_Organisation_Name"].Value = newRecord.PAYEE_ORGANISATION_NAME;
                cmd.Parameters.Add(new SqlParameter("@SCode", SqlDbType.Int, 10));
                cmd.Parameters["@SCode"].Value = Convert.ToInt32(newRecord.SCODE);
                cmd.Parameters.Add(new SqlParameter("@States", SqlDbType.VarChar, 50));
                cmd.Parameters["@States"].Value = newRecord.States;
                cmd.Parameters.Add(new SqlParameter("@GSTIN", SqlDbType.VarChar, 50));
                cmd.Parameters["@GSTIN"].Value = newRecord.GSTIN;
                cmd.Parameters.Add(new SqlParameter("@Enter_By", SqlDbType.VarChar, 50));
                cmd.Parameters["@Enter_By"].Value = newRecord.ENTER_BY;
                cmd.Parameters.Add(new SqlParameter("@Enter_Date", SqlDbType.DateTime));
                cmd.Parameters["@Enter_Date"].Value = newRecord.ENTER_DATE;
                //AddedForPincode_30_07_2024
                cmd.Parameters.Add(new SqlParameter("@Payee_Location_Pincode", SqlDbType.BigInt));
                cmd.Parameters["@Payee_Location_Pincode"].Value = newRecord.PAYEE_LOCATION_PINCODE;
                //Added 16/10/2024 
                cmd.Parameters.Add(new SqlParameter("@Payee_address", SqlDbType.VarChar, 4000));
                cmd.Parameters["@Payee_address"].Value = newRecord.PAYEE_ADDRESS;

                SqlParameter updateFlagParam = new SqlParameter("@update_flag", SqlDbType.TinyInt);
                updateFlagParam.Direction = ParameterDirection.Output;  // Set as output
                cmd.Parameters.Add(updateFlagParam);

                con.Open();
                cmd.ExecuteNonQuery();



            }
        }
    }

    public static DataSet GetStates()
    {
        string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
        DataSet myset = new DataSet();
        using (SqlConnection con = new SqlConnection(ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("getStates", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    sda.Fill(myset);
                }
            }
        }
        return myset;
    }

    public static DataSet GetPayee_Organisation_Master(int pPayee_Organisation_Code, string pPayee_Organisation_Name)
    {
        string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
        DataSet myset = new DataSet();
        using (SqlConnection con = new SqlConnection(ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("GetPayee_Organisation_Master", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Payee_Organisation_Code", SqlDbType.Int, 10));
                cmd.Parameters["@Payee_Organisation_Code"].Value = pPayee_Organisation_Code;
                cmd.Parameters.Add(new SqlParameter("@Payee_Organisation_Name", SqlDbType.VarChar, 100));
                cmd.Parameters["@Payee_Organisation_Name"].Value = pPayee_Organisation_Name;
                con.Open();
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    sda.Fill(myset);
                }
            }
        }
        return myset;
    }


    protected bool ValidateData()
    {
        bool valid = true;
        bullet_errors.Items.Clear();
        //--------------------------------------------------------------------
        mev_payee_organisation_name.Validate();
        if (!mev_payee_organisation_name.IsValid)
        {
            bullet_errors.Items.Add("Payee Organisation Name must be Entered");
            valid = false;
        }

        return valid;
    }
}
