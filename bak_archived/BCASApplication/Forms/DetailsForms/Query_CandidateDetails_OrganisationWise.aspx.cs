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

public partial class Query_CandidateDetails_OrganisationWise : System.Web.UI.Page
{
    TYP_BCAS_Approval objBCAS = new TYP_BCAS_Approval();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if ((Convert.ToString(HttpContext.Current.Session["UserId"]).Trim().Length == 0))
            {
                HttpContext.Current.Response.Redirect("~/Home.aspx");
            }
            else
            {
                using (DataTable dtOrg = objBCAS.getGetAllOrganisation())
                {
                    if (dtOrg.Rows.Count > 0)
                    {
                        ddlOrganisation.DataSource = dtOrg;
                        ddlOrganisation.DataBind();
                    }

                }          
                
            }
        }
    }

    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlOrganisation.SelectedValue = GridView1.SelectedRow.Cells[1].Text;
        txt_employee_id.Text = GridView1.SelectedRow.Cells[2].Text;
        txt_candidate_name.Text = GridView1.SelectedRow.Cells[3].Text;
        ddl_gender_code.SelectedValue = GridView1.SelectedRow.Cells[4].Text;
        ddl_Designation.SelectedValue = GridView1.SelectedRow.Cells[5].Text;
        txt_dob.Text = Convert.ToDateTime(GridView1.SelectedRow.Cells[6].Text).Date.ToString("dd/MM/yyyy");
        txt_email_id.Text = GridView1.SelectedRow.Cells[7].Text;
        txt_mobile_no.Text = GridView1.SelectedRow.Cells[8].Text;
        ddl_Nationality.SelectedValue = GridView1.SelectedRow.Cells[9].Text;
        ddl_Qualification.SelectedValue = GridView1.SelectedRow.Cells[10].Text;
        txt_registration_number.Text = GridView1.SelectedRow.Cells[11].Text;


        //********

        Img_Photo.Visible = true;
        //Img_Photo.ImageUrl = "~//Upload//Photograph//" + txt_registration_number.Text + ".jpeg";
        //Img_Photo.DataBind();
        Img_Photo.ImageUrl = "~//Upload//Photograph//" + txt_registration_number.Text + ".jpeg";

        Img_Photo.DataBind();
        ImgSign.Visible = true;
        ImgSign.ImageUrl = "~//Upload//Signature//" + txt_registration_number.Text + ".jpeg";
        ImgSign.DataBind();
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
            e.Row.Cells[6].Text = Convert.ToDateTime(e.Row.Cells[6].Text).Date.ToString("dd-MMM-yyyy").ToUpper().Trim();
            //e.Row.Cells[16].Text = Convert.ToDateTime(e.Row.Cells[16].Text).Date.ToString("dd-MMM-yyyy").ToUpper().Trim();
        }
    }
    protected void Btn_Search_Click(object sender, EventArgs e)
    {

    }
}
