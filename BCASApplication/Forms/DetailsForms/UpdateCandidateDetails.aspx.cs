using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using BCASClassLibrary;

public partial class Forms_DetailsForms_UpdateCandidateDetails : System.Web.UI.Page
{

    TYP_ApplicantDetails objApplicant = new TYP_ApplicantDetails();

    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Convert.ToString(HttpContext.Current.Session["UserId"]).Trim().Length == 0))
        {
            HttpContext.Current.Response.Redirect("~/Home.aspx");
        }

        if (!IsPostBack)
        {
            CandidateDetail.Visible = false;
            grdDisplay.Visible = false;
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtRegNo.Text.ToString().Trim().Length != 0)
            {

                objApplicant.i_Registration_Number = Convert.ToInt32(txtRegNo.Text.ToString().Trim());

                objApplicant.v_PostedAtStateId = drpStates.SelectedItem.Text;

                objApplicant.v_PostedAtCityId = drpCity.SelectedItem.Text;

                objApplicant.v_Email_Id = txt_email_id.Text;

                objApplicant.v_Mobile_No = txt_mobile_no.Text;

                objApplicant.v_EbasId = Txt_EbasId.Text;




                using (DataTable dt = objApplicant.UpdateCandidateDetails())
                {
                    if (Convert.ToInt32(dt.Rows[0][0].ToString()) == -786)
                    {
                        lbl_ErrorMsg.Text = "";
                        lbl_Success.Text = "Data Does Not Exist";
                        grdDisplay.Visible = false;

                    }
                    else
                    {
                        lbl_ErrorMsg.Text = "";
                        lbl_Success.Text = "Candidate Details Updation Successful";
                        grdDisplay.DataBind();
                        grdDisplay.Visible = true;

                        btnUpdate.Visible = false;


                    }

                }
            }
            else
            {
                lbl_ErrorMsg.Text = "Please Enter Registration Number!!!";

            }
        }
        catch (Exception ex)
        {
            lbl_ErrorMsg.Text = "Some Exception Occurred!!!";
        }

    }

    protected void showHideControls(bool flag)
    {
        Label4.Visible = flag;
        txt_email_id.Visible = flag;
        btnViewCandidateDetailsScreenersFrmEBCASNo.Visible = flag;
        lbl_Postedat.Visible = flag;
        drpStates.Visible = flag;
        Label3.Visible = flag;
        drpCity.Visible = flag;
        lbl_mobile_no.Visible = flag;
        txt_mobile_no.Visible = flag;
        lbl_email_id.Visible = flag;
        txt_email_id.Visible = flag;
        btnUpdate.Visible = flag;
        Txt_EbasId.Visible = flag;

    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        try
        {
            if(txtRegNo .Text.Trim().Length==0 )
            { 
                lblError.Text = "Please Enter Registration No.";
                lblError.Visible = true;
                showHideControls(false);

                return;
            }
            showHideControls(true);
            lblError.Visible = false;
            drpStates.Items.Clear();
            drpCity.Items.Clear();
            lbl_Success.Text = "";
            lbl_ErrorMsg.Text = "";
            grdDisplay.DataBind();
            drpStates.DataBind();
            drpCity.DataBind();
            grdDisplay.Visible = true;
            CandidateDetail.Visible = true;

            drpCity.Items.Insert(0, new ListItem("--Select--", "0"));
            drpStates.Items.Insert(0, new ListItem("--Select--", "0"));
            drpStates.Enabled = true;
            drpCity.Enabled = true;


            Txt_EbasId.Text = "";
            Txt_EbasId.Enabled = true;


            if ((grdDisplay.Rows[0].Cells[3].Text.ToString() != "&nbsp;"))
            {
                drpStates.Items.FindByText(grdDisplay.Rows[0].Cells[3].Text.ToString()).Selected = true;
                drpStates.SelectedValue = drpStates.Items.FindByText(grdDisplay.Rows[0].Cells[3].Text.ToString()).Value;
                BindCity(drpStates.SelectedValue);
                drpStates.Enabled = false;
            }
            else
                btnUpdate.Visible = true;
            if ((grdDisplay.Rows[0].Cells[4].Text.ToString() != "&nbsp;"))
            {
                drpCity.Items.FindByText(grdDisplay.Rows[0].Cells[4].Text.ToString()).Selected = true;
                drpCity.Enabled = false;
            }
            else
                btnUpdate.Visible = true;
            if ((grdDisplay.Rows[0].Cells[5].Text.ToString() != "&nbsp;"))
            {
                Txt_EbasId.Text = grdDisplay.Rows[0].Cells[5].Text.ToString();
                Txt_EbasId.Enabled = false;
                btnViewCandidateDetailsScreenersFrmEBCASNo.Visible = false;
            }
            else
                btnUpdate.Visible = true;
            if ((grdDisplay.Rows[0].Cells[8].Text.ToString() != "&nbsp;"))
            {
                txt_mobile_no.Text = grdDisplay.Rows[0].Cells[8].Text.ToString();
                txt_mobile_no.Enabled = false;
            }
            else
                btnUpdate.Visible = true;
            if ((grdDisplay.Rows[0].Cells[9].Text.ToString() != "&nbsp;"))
            {
                txt_email_id.Text = grdDisplay.Rows[0].Cells[9].Text.ToString();
                txt_email_id.Enabled = false;
            }
            else
                btnUpdate.Visible = true;
        }

        catch (Exception ex)
        {
            lbl_ErrorMsg.Text = "Some Exception Occurred!!!";
        }

    }



    protected void BindCity(string value)
    {
        try
        {
            string constr = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            drpCity.Items.Clear();
            drpCity.Items.Insert(0, new ListItem("--Select--", "0"));
            Int64 StateID = Convert.ToInt64(value);
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
        catch (Exception ex)
        {
            lbl_ErrorMsg.Text = "Some Exception Occurred!!!";
        }

    }

    protected void drpStates_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
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
            //grdDisplay.Visible=true;
        }
        catch (Exception ex)
        {
            lbl_ErrorMsg.Text = "Some Exception Occurred!!!";
        }
    }



    protected void btnViewCandidateDetailsScreenersFrmEBCASNo_Click(object sender, EventArgs e)
    {
        grdDisplay.Visible = true;
        try
        {
            TYP_ApplicantDetails obj = new TYP_ApplicantDetails();
            string encasNo = Txt_EbasId.Text.ToString();
            DataTable dt = obj.getCandidateDetailsFrmEBCASNo(encasNo);
            if (dt.Rows.Count > 0)
            {

            }
            else
            {
                Txt_EbasId.Text = "";
                txtRegNo.Text = "";
                grdDisplay.Visible = false;
                CandidateDetail.Visible = false;

                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('No Candidate Details available With this EBCASNo');", true);

            }
        }
        catch
        {
            //btn_confirm.Visible = false;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Exception", "alert('Exception Occured : Please Enter EBCAS No');", true);
        }
    }


    protected void btnNewReg_Click(object sender, EventArgs e)
    {
        grdDisplay.Visible = false;
        CandidateDetail.Visible = false;
        txtRegNo.Text = "";

    }
}
