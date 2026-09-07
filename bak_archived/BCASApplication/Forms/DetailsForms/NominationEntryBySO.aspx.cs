using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using BCASClassLibrary;


public partial class Forms_DetailsForms_NominationEntryBySO : System.Web.UI.Page
{

    TYP_NominationEntryBySO objSO = new TYP_NominationEntryBySO();   
    TYP_USER_Details objUserDetails = new TYP_USER_Details();
    
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (!IsPostBack)
        {
            if ((Convert.ToString(HttpContext.Current.Session["UserId"]).Trim().Length == 0))
            {
                HttpContext.Current.Response.Redirect("~/Home.aspx");
            }
            else
            {
                
                //lblOrgName.Text = "Sponsoring Organisation" + Session["OrgName"].ToString();
                ddlSchedule.DataBind();

                if (Convert.ToString(HttpContext.Current.Session["OrgName"]).Trim().Length == 0)
                {
                    lblHeading.Visible = false;
                }
                else
                {
                    lblHeading.Visible = true;
                    lblOrgName.Text = "Sponsoring Organisation: " + Session["OrgName"].ToString();
                }

                
            }
        }

        GridView1.Visible = false;
        grdPrevCourses.Visible = false;
        grdWorkingExp.Visible = false;

        //Added Madhur for editing payee in nomination
        GridViewBtnEdit.Visible = false;
        divBilling.Visible = false;
    } 


    protected void btnSaveNomination_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
           
            if (Convert.ToInt32(ddlExamLocation.SelectedIndex) == 0 || Convert.ToInt32(ddlExamLocation.Text.Length) == 0 || txtRegNo.Text.Length == 0 || Convert.ToInt32(ddlSchedule.SelectedIndex) == 0)
            {
                lblError.Text = "Please Select All Values!!!!!";
                lblError.Visible = true;
            }
            else
            {
                using (DataTable dt_display = objSO.GetCandidateDetailsForNomination(Convert.ToInt32(txtRegNo.Text), Convert.ToInt32(Session["UserOrg"])))
                    //, Convert.ToInt32(ddlSchedule.SelectedValue)))
                {
                    if (dt_display.Rows.Count > 0)
                    {

                        //******* Check For Re_Appearing
                        if (RadioButtonList1.SelectedValue == "N")
                        {
                            using (DataTable dt_NominationDate = objSO.Get_NominationDate(Convert.ToInt32(ddlSchedule.SelectedValue)))
                            {
                                if (Convert.ToInt32(dt_NominationDate.Rows[0][0]) == -786)
                                {
                                    lblError.Text = "Nomination Date Over for the Selected Schedule.";
                                }
                                else
                                {
                                    objSO.i_Schedule_ID = Convert.ToInt32(ddlSchedule.SelectedValue);
                                    //objSO.i_Registration_No = Convert.ToInt32(GridView1.DataKeys[0].Values["Registration_Number"]);
                                    objSO.i_Registration_No = Convert.ToInt32(txtRegNo.Text);
                                    objSO.i_Nominating_OrgCode = Convert.ToInt32(Session["UserOrg"]);
                                    objSO.v_Nominated_By = Session["user_id"].ToString();
                                    objSO.i_Exam_Location_Code = Convert.ToInt32(ddlExamLocation.SelectedValue);
                                    objSO.c_Whether_Reappearing = RadioButtonList1.SelectedValue;

                                    using (DataTable dt_Insert = objSO.InsertNominationDetails())
                                    {
                                        if (Convert.ToInt32(dt_Insert.Rows[0][0]) != 0)
                                        {
                                            if (Convert.ToInt32(dt_Insert.Rows[0][0]) == 2627)
                                            {
                                                lblError.Text = "Already Nominated";
                                            }
                                            else
                                            {
                                                lblError.Text = "Nomination Failed Due To Error!!!";
                                            }

                                        }
                                        else
                                        {
                                            lblError.Text = "Nomination Successful";

                                        }
                                    }

                                }

                            }

                        }
                        else if (RadioButtonList1.SelectedValue == "Y")
                        {

                            using (DataTable dt_ReapperDate = objSO.Get_ReappearingNominationDate (Convert.ToInt32(ddlSchedule.SelectedValue)))
                            {
                                if (Convert.ToInt32(dt_ReapperDate.Rows[0][0]) == -786)
                                {
                                    lblError.Text = "Reappearing Date is Over for the Selected Schedule of Exam.";
                                }
                                else
                                {
                                    objSO.i_Schedule_ID = Convert.ToInt32(ddlSchedule.SelectedValue);
                                    //objSO.i_Registration_No = Convert.ToInt32(GridView1.DataKeys[0].Values["Registration_Number"]);
                                    objSO.i_Registration_No = Convert.ToInt32(txtRegNo.Text);
                                    objSO.i_Nominating_OrgCode = Convert.ToInt32(Session["UserOrg"]);
                                    objSO.v_Nominated_By = Session["user_id"].ToString();
                                    objSO.i_Exam_Location_Code = Convert.ToInt32(ddlExamLocation.SelectedValue);
                                    objSO.c_Whether_Reappearing = RadioButtonList1.SelectedValue;

                                    using (DataTable dt_Insert = objSO.InsertNominationDetails())
                                    {
                                        if (Convert.ToInt32(dt_Insert.Rows[0][0]) != 0)
                                        {
                                            if (Convert.ToInt32(dt_Insert.Rows[0][0]) == 2627)
                                            {
                                                lblError.Text = "Already Nominated";
                                            }
                                            else
                                            {
                                                lblError.Text = "Nomination Failed Due To Error!!!";
                                            }

                                        }
                                        else
                                        {
                                            lblError.Text = "Nomination Successful";

                                        }
                                    }

                                }

                            }

                        }                        
                        //Till Here

                       
                    }
                    else
                    {
                        lblError.Text = "No Record Found or Photo/Sign not uploaded for Nomination!!!!!";

                    }

                }
                lblError.Visible = true;
                
            }
                 
        }

       

    }

    protected void  btnDisplay_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            lblError.Visible = false;
            GridView1.DataBind();
            GridView1.Visible = true;

            grdPrevCourses.DataBind();
            grdPrevCourses.Visible = true;
            grdWorkingExp.DataBind();
            grdWorkingExp.Visible = true;
        }


    }

    protected void ddlSchedule_SelectedIndexChanged(object sender, EventArgs e)
    {
        int ScheduleID;
        ScheduleID = Convert.ToInt32(ddlSchedule.SelectedValue);
        using (DataTable dt = objSO.getExamLocations(ScheduleID))
        {
            ddlExamLocation.Items.Clear();
            if (dt.Rows.Count > 0)
            {
                ddlExamLocation.AppendDataBoundItems = true;
                ddlExamLocation.Items.Insert(0, new ListItem("Select", string.Empty));
                ddlExamLocation.DataSource = dt;
                ddlExamLocation.DataTextField = "Exam_Location_Name"; // This is text displayed
                ddlExamLocation.DataValueField = "Exam_Location_Code"; // This is the value returned
                ddlExamLocation.DataBind();

            }

        }
        //ddlExamLocation.DataBind();
    }
    //Added Madhur for Payee Org
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // Check if the cell value is null or empty
            //if (string.IsNullOrEmpty(e.Row.Cells[10].Text))
            if (e.Row.Cells[10].Text=="0")
            {
                GridViewBtnEdit.Visible = true;
                // Replace with whatever action you want to take for null values
                /*  e.Row.Cells[10].Text = "Data is Null";*/ // For example, replace null with "N/A"

            }
        }
    }


    protected void GridViewBtnEdit_Click(object sender, EventArgs e)
    {
        GridView1.Visible = true;
        divBilling.Visible = true;
    }

    protected void btnUpdatePayee_Click(object sender, EventArgs e)
    {
        //string UserOrg=Session["UserOrg"].ToString();
        //string billerOrgCode = ddl_BillerOrgCode.SelectedValue.ToString();
        //string regNo = txtRegNo.Text;
        //string schedule= ddlSchedule.SelectedValue.ToString();

        string strConnString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString; // get it from Web.config file
        using (SqlConnection objConn = new SqlConnection(strConnString))
        {
            objConn.Open();

            SqlCommand objCmd2 = new SqlCommand("updatePayeeOrg", objConn);

            objCmd2.CommandType = CommandType.StoredProcedure;

            try
            {

                objCmd2.Parameters.Clear();
                objCmd2.CommandType = CommandType.StoredProcedure;
                objCmd2.Parameters.AddWithValue("@Organisation_Code", Session["UserOrg"].ToString());
                objCmd2.Parameters.AddWithValue("@BillerOrgCode", ddl_BillerOrgCode.SelectedValue.ToString());
                objCmd2.Parameters.AddWithValue("@Schedule", ddlSchedule.SelectedValue.ToString());
                objCmd2.Parameters.AddWithValue("@RegNo", txtRegNo.Text);
                objCmd2.ExecuteNonQuery();
                ddl_BillerOrgCode.SelectedValue = "0";

            }
            catch (Exception)
            {
                //objTrans.Rollback();
                Session["Msg"] = "Some Error Occured";
            }
            finally
            {
                objConn.Close();

            }
        }

    }
    //// Till here added madhur
}
