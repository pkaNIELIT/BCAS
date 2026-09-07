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

public partial class Forms_DetailForms_CustomErrorPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session.Abandon();
        Session.Clear();
        Session.RemoveAll();
        //ClearCookies();

        HttpContext.Current.Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
        HttpContext.Current.Response.Cache.SetValidUntilExpires(false);
        HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        HttpContext.Current.Response.Cache.SetNoStore();
        //######
        if (!IsPostBack)
        {
            //Session.Abandon();
            //Session.Clear();
            //Session.RemoveAll();
            Session["login"] = false;


        }
        Session["login"] = false;
        Session["View_Login"] = false;

    }

    //public void ClearCookies()
    //{
    //    string[] myCookies = Request.Cookies.AllKeys;
    //    foreach (string cookie in myCookies)
    //    {
    //        Response.Cookies[cookie].Expires = DateTime.Now.AddDays(-1);
    //    }
    //}

}
