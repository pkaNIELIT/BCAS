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

public partial class Forms_DetailsForms_QrySuccessfulTransations : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if ((Convert.ToString(HttpContext.Current.Session["UserId"]).Trim().Length == 0))
            {
                HttpContext.Current.Response.Redirect("~/Home.aspx");
            }

            grdSucccessfulTransaction.Visible = false;
           

        }
    }

    protected void btnCount_Click(object sender, EventArgs e)
    {
        grdSucccessfulTransaction.DataBind();
        grdSucccessfulTransaction.Visible = true;
        
    }


}
