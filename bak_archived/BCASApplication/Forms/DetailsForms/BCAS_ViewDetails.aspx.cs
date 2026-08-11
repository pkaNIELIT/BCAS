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

public partial class Forms_DetailsForms_BCAS_ViewDetails : System.Web.UI.Page
{
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

                if ((Request.QueryString["Regd_No"] != null) && (Request.QueryString["Regd_No"].ToString().Trim().Length > 0))
                {
                    lblError.Visible = false;
                    GridView1.Visible = true;
                    grdPrevCourses.Visible = true;
                    grdWorkingExp.Visible = true;

                }
                else
                {
                    GridView1.Visible = false;
                    grdPrevCourses.Visible = false;
                    grdWorkingExp.Visible = false;

                    lblError.Text = "Error";
                    lblError.Visible = true;
                }
            }
        }


    }
}
