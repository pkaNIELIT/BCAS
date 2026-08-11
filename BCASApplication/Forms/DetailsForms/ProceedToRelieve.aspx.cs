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
using BCASClassLibrary;

public partial class Forms_DetailsForms_ProceedToRelieve : System.Web.UI.Page
{
    TYP_ApplicantDetails objApplicant = new TYP_ApplicantDetails();
   

    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Convert.ToString(HttpContext.Current.Session["UserId"]).Trim().Length == 0))
        {
            HttpContext.Current.Response.Redirect("~/Home.aspx");
        }

    }
    protected void LoginButton_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32(txtRegNo.ToString().Length) != 0)
        {

            //**********    

            objApplicant.i_Registration_Number = Convert.ToInt32(txtRegNo.Text);

            using (DataTable dt = objApplicant.GetCandidateDetails_for_Relieving(Convert.ToInt32(txtRegNo.Text), Convert.ToInt32(Session["UserOrg"])))
            {
                if (dt.Rows.Count != 0)
                {

                    Session["lbl_Apl_no"] = txtRegNo.Text;
                    lblError.Visible = false;
                    Response.Redirect("~/Forms/DetailsForms/RelieveFromOrganisation.aspx");

                }
                else
                {
                    lblError.Text = "No Record Found!!!";
                    lblError.Visible = true;
                }

            }
            //###########            

        }
    }
}
