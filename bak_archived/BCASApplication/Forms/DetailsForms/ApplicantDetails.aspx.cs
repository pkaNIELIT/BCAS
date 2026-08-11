using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Threading;
using BCASClassLibrary;
using System.Configuration;
using System.Data.SqlClient;

public partial class Forms_DetailsForms_ApplicantDetails : System.Web.UI.Page
{

    public String CurrentCity
    {
        get
        {
            return drpCity.SelectedItem.Text;
        }
    }

    
    public DropDownList pbddl_OrgCode
    {
        get
        {
            return ddl_OrgCode;
        }

    }

    public TextBox pbTxt_EmpID
    {
        get
        {
            return txt_EmpID;
        }
    }

    public TextBox pbTxt_EbasId
    {
        get
        {
            return Txt_EbasId;
        }
    }
    public TextBox pbTxt_BcasId
    {
        get
        {
            return Txt_BcasId;
        }
    }

    public DropDownList pbddl_Designation
    {
        get
        {
            return ddl_Designation;
        }
    }

    public DropDownList pbddl_drpStates
    {
        get
        {
            return drpStates;
        }
    }

    public DropDownList pbddl_drpCity
    {
        get
        {
            return drpCity;
        }
    }


    public TextBox pbTxt_candidate_name
    {
        get
        {
            return txt_candidate_name;
        }
    }
    //deep
    public TextBox pbTxt_organisation_name_of_candidate
    {
        get
        {
            return txt_organisation_name_of_candidate;
        }
    }

    //public TextBox pbTxt_Aadhaar_Number
    //{
    //    get
    //    {
    //        if (txt_Aadhaar_Number.Text.Length ==12)
    //        {
    //            string aadhar = txt_Aadhaar_Number.Text;
    //            string EncAdh = EncryptDecrypt.EncryptString(aadhar);
    //            txt_Aadhaar_Number.Text = EncAdh;
    //            Session["EEA"] = "";
    //            Session["EEA"] = EncAdh;
    //            return txt_Aadhaar_Number;
    //        }
    //        ////if (txt_Aadhaar_Number.Text.Length > 12)
    //        ////{
    //        ////    txt_Aadhaar_Number.Text = Session["EEA"].ToString();
    //        ////    return txt_Aadhaar_Number;
    //        ////}
    //        ////else
    //        ////{
    //        ////    string aadhar = txt_Aadhaar_Number.Text;
    //        ////    string EncAdh = EncryptDecrypt.EncryptString(aadhar);
    //        ////    txt_Aadhaar_Number.Text = EncAdh;
    //        ////    Session["EEA"] = "";
    //        ////    Session["EEA"] = EncAdh;
    //        ////    return txt_Aadhaar_Number;
    //        //////}
    //        //string aadhar = txt_Aadhaar_Number.Text;
    //        //string EncAdh = EncryptDecrypt.EncryptString(aadhar);
    //        if (Session["EEA"].ToString() != "")
    //        {
    //            txt_Aadhaar_Number.Text = Session["EEA"].ToString();
    //        }
    //        //Session["EEA"] = EncAdh;
    //        return txt_Aadhaar_Number ;

    //    }
    //}

    public HiddenField pbTxt_Aadhaar_No
    {
        get
        {            
            return HAdh;
        }
    }
    //deep

    public DropDownList pbDdl_gender_code
    {
        get
        {
            return ddl_gender_code;
        }
    }

    public TextBox pbTxt_date_of_birth
    {
        get
        {
            return txt_date_of_birth;
        }
    }

    public DropDownList pbddl_Nationality
    {
        get
        {
            return ddl_Nationality;
        }
    }

    public TextBox pbTxt_mobile_no
    {
        get
        {
            return txt_mobile_no;
        }
    }

    public TextBox pbTxt_email_id
    {
        get
        {
            return txt_email_id;
        }
    }

    public DropDownList pbddl_Qualification
    {
        get
        {
            return ddl_Qualification;
        }
    }

    

    public CheckBox pbChk_declaration
    {
        get
        {
            return chk_declaration;
        }
    }

    public DropDownList pbddl_billerOrgCode
    {
        get
        {
            return ddl_BillerOrgCode;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        

        if (txt_email_id.Text != "")
        {
            chk_declaration.Checked = true;
            //if (chk_declaration.Checked == false)
            //{
            //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Check the Declaration!!')", true);
            //    return;
            //}
        }
        btn_confirm.Attributes.Add("OnClick", "ConfirmRecord()");
        if (!Page.IsPostBack)
        {
            if ((Convert.ToString(HttpContext.Current.Session["UserId"]).Trim().Length == 0))
            {
                HttpContext.Current.Response.Redirect("~/Home.aspx");
            }
            else
            {
                Com_v_date_of_birth.ValueToCompare = DateTime.Now.ToShortDateString();
            }

        }

    }
   
    //protected void chk_declaration_CheckedChanged(object sender, EventArgs e)
    //{        
    //    //string EncAdh1 = EncryptDecrypt.DecryptString("vznwrprvRRsBxPx3ucpZ1A==");
    //    //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Script", "ConfirmRecord()", true);
    //    //if (chk_declaration.Checked == true)
    //    //{
    //    //    txt_Aadhaar_Number.Enabled = false;
    //    //    lblA.Text = txt_Aadhaar_Number.Text;
    //    //    lblA.Visible = true;
    //    //   // txt_Aadhaar_Number.Visible = false;
    //    //}
    //    //else
    //    //{
    //    //    txt_Aadhaar_Number.Enabled = true;
    //    //    lblA.Visible = false;
    //    //    txt_Aadhaar_Number.Visible = true;
    //    //}
    //    //if (txt_Aadhaar_Number.Text.Length==12)
    //    //{
    //    //  //txt_Aadhaar_Number.Text = "785";
    //    //string aadhar = txt_Aadhaar_Number.Text;
    //    //string EncAdh = EncryptDecrypt.EncryptString(aadhar);
    //    //txt_Aadhaar_Number.Text = EncAdh;
    //    //Session["EEA"] = EncAdh;
    //    //}
    //    btn_confirm.Enabled = true;
    //}

    protected void drpStates_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (drpStates.SelectedValue != "0")
        {
            drpCity.Items.Clear();
            drpCity.Items.Insert(0, new ListItem("--Select--", "0"));
            Int64 StateID = Convert.ToInt64(drpStates.SelectedValue);
            string constr = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "Select ID , Name  FROM Location where Parent_ID=@StateID order by name";

                    cmd.Parameters.AddWithValue("@StateID", StateID);
                    cmd.Connection = con;
                    con.Open();
                    SqlDataAdapter adpt = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adpt.Fill(dt);
                    drpCity.DataSource = dt;
                   // drpCity.DataBind();
                    drpCity.DataTextField = "Name";
                    drpCity.DataValueField = "ID";
                    drpCity.DataBind(); 
                    con.Close();
                }
            }
        }
    }
    protected void txt_EmpID_TextChanged(object sender, EventArgs e)
    {
        Txt_BcasId.Text = txt_EmpID.Text;
    }
    //Added_10_06_2024
    protected void btnViewCandidateDetailsFrmEBCASNo_Click(object sender, EventArgs e)
    {
        try
        {
            TYP_ApplicantDetails obj = new TYP_ApplicantDetails();
            string encasNo = Txt_EbasId.Text.ToString();
            DataTable dt = obj.getCandidateDetailsFrmEBCASNo(encasNo);
            if (dt.Rows.Count > 0)
            {
                // object columnValue = dataTable.Rows[0]["ColumnName"];
                txt_candidate_name.Text = dt.Rows[0]["name"].ToString();
                txt_date_of_birth.Text = dt.Rows[0]["Dateofbirth"].ToString();
                btn_confirm.Visible = true;
                //txt_candidate_name.Text = dataTable.Rows[0]["Dateofbirth"].ToString();
            }
            else
            {
                txt_candidate_name.Text = "";
                txt_date_of_birth.Text = "";
                btn_confirm.Visible = false;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('No Candidate Details available With this EBCASNo');", true);
                
            }
        }
        catch
        {
            btn_confirm.Visible = false;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Exception", "alert('Exception Occured : Please Enter EBCAS ID');", true);
        }
    }

    //Added_10_06_2024

}
