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

public partial class Forms_DetailsForms_ResetApplication : System.Web.UI.Page
{

    TYP_ApplicantDetails objApplicant = new TYP_ApplicantDetails();

    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Convert.ToString(HttpContext.Current.Session["UserId"]).Trim().Length == 0))
        {
            HttpContext.Current.Response.Redirect("~/Home.aspx");
        }
        else
        {
            grdDisplay.Visible = false;
        }

        

    }

    protected void btnRelieve_Click(object sender, EventArgs e)
    {
        if (txtRegNo.Text.ToString().Trim().Length != 0)
        {

            objApplicant.i_Registration_Number = Convert.ToInt32(txtRegNo.Text.ToString().Trim());
            
            using (DataTable dt = objApplicant.ResetApplicationStatus())
            {
                //if (Convert.ToInt32(dt.Rows[0][0].ToString()) != 0 && Convert.ToInt32(dt.Rows[0][1].ToString()) != 0)
                if (Convert.ToInt32(dt.Rows[0][0].ToString()) == -786)
                {
                    lbl_ErrorMsg.Text = "";
                    lbl_Success.Text = "Data Does Not Exist";
                    grdDisplay.Visible = false;

                }
                else
                {
                    lbl_ErrorMsg.Text = "";
                    lbl_Success.Text = "Application Reset Successful";
                    grdDisplay.DataBind();

                }

            }
        }
        else
        {
            lbl_ErrorMsg.Text = "Please Enter Registration Number!!!";

        }

    }


    protected void LoginButton_Click(object sender, EventArgs e)
    {
        lbl_Success.Text = "";
        lbl_ErrorMsg.Text = "";       
        grdDisplay.DataBind();
        grdDisplay.Visible = true;

       
    }
}
