using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using BCASClassLibrary;
using System.Web.Configuration;
using System.Globalization;
using System.Collections.Generic;
using System.Web.SessionState;

public partial class MasterPage : System.Web.UI.MasterPage
{

    //protected void Page_PreInit(object sender, EventArgs e)
    //{
    //    // This is necessary because Safari and Chrome browsers don't display the Menu control correctly.
    //    // All webpages displaying an ASP.NET menu control must inherit this class.
    //    if (Request.ServerVariables["http_user_agent"].IndexOf("Safari", StringComparison.CurrentCultureIgnoreCase) != -1)
    //        Page.ClientTarget = "uplevel";
    //}

    //protected void Page_PreInit(object sender, EventArgs e)
    //{
    //    if (Request.ServerVariables["http_user_agent"].IndexOf("Chrome", StringComparison.CurrentCultureIgnoreCase) != -1)
    //        Page.ClientTarget = "uplevel";
    //    if (Request.ServerVariables["http_user_agent"].IndexOf("Safari", StringComparison.CurrentCultureIgnoreCase) != -1)
    //        Page.ClientTarget = "uplevel";
    //}

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Request.UserAgent.IndexOf("AppleWebKit") > 0)
        {
            Request.Browser.Adapters.Clear();
        }

        if ((Page.Title != "BCAS--> Home"))
        {
            //&&&&&&& Added on 12.09.2013
            if ((Page.Title != "CustomErrorPage"))
            {
                //if ((!Page.IsPostBack) && (Convert.ToBoolean(Session["login"].ToString())))
                if ((!Page.IsPostBack) )
                {
                    //Dictionary<string, string> LoginUsersList = new Dictionary<string, string>();
                    //LoginUsersList = (Dictionary<string, string>)Application["LoginUsersList"];
                    //string SessionIds = Session["UserIDs"].ToString();
                    //LoginUsersList.Add(Session["UID"].ToString(), SessionIds);
                    //Application["LoginUsersList"] = LoginUsersList;
                    //Application.UnLock();
                    //*******30.12.2013
                    if ((Page.Title != "BCAS--> Home") && (Page.Title != "BCAS-->HomePage"))
                    {

                        
                        //current page name
                        string pagename = System.IO.Path.GetFileName(Request.PhysicalPath);
                        //DataSet ds1 = (DataSet)Session["Forms"];
                        DataTable dt1 = (DataTable)Session["Forms"];
                        dt1.PrimaryKey = new DataColumn[] { dt1.Columns[0] };

                        DataRow foundRow = dt1.Rows.Find(pagename);
                        if (foundRow == null)
                        {
                            HttpContext.Current.Response.Redirect("~/Home.aspx");
                        }
                        //***********till here

                    }

                    //************&&&&&&&&&&&&&&


                    //if ((Convert.ToString(HttpContext.Current.Session["UserId"]).Trim().Length == 0))
                    //{
                    //    //HttpContext.Current.Response.Redirect("~/frmRestrict.aspx");
                    //}

                    //lnkHome.Text = "Home||";
                    //lnkHome.Visible = true;

                    lblWelcome.Text = "Welcome:";
                    lblWelcome.Visible = true;

                    // Added null check for Session variables
                    if (Session["View_Login"] != null)
                    {
                        LoginStatus.Visible = Convert.ToBoolean(Session["View_Login"].ToString());
                    }
                    else
                    {
                        LoginStatus.Visible = false;
                    }

                    // Added null check for UserId session variable
                    if (Session["UserId"] != null && !string.IsNullOrEmpty(Session["UserId"].ToString()))
                    {
                        String UserName = Session["UserId"].ToString();
                        DataTable menudata = TYP_MENUMASTER.GetMenuData(UserName);
                        //DataTable menudata = TYP_MENUMASTER.GetMenuData(HttpContext.Current.User.Identity.Name);

                        //DataTable menudata = TYP_MENUMASTER.GetMenuData(HttpContext.Current.User.Identity.Name);
                        if (menudata != null && menudata.Rows.Count > 0)
                        {
                            AddTopMenuItems(menudata);
                        }
                        else
                        {
                            // If no menu data, redirect to login
                            HttpContext.Current.Response.Redirect("~/Home.aspx");
                        }
                    }
                    else
                    {
                        // If UserId session is null or empty, redirect to login
                        HttpContext.Current.Response.Redirect("~/Home.aspx");
                    }

                }

            }
            //&&&&&&& Till Here


            //********* Commented on 12.09.2013
            //if ((!Page.IsPostBack) && (Convert.ToBoolean(Session["login"].ToString())))
            //{
            //    //if ((Convert.ToString(HttpContext.Current.Session["UserId"]).Trim().Length == 0))
            //    //{
            //    //    //HttpContext.Current.Response.Redirect("~/frmRestrict.aspx");
            //    //}
            //    lnkHome.Text = "Home||";
            //    lnkHome.Visible = true;

            //    lblWelcome.Text = "Welcome:";
            //    lblWelcome.Visible = true;
            //    LoginStatus.Visible = Convert.ToBoolean(Session["View_Login"].ToString());

            //    String UserName = Session["UserId"].ToString();
            //    DataTable menudata = TYP_MENUMASTER.GetMenuData(UserName);
            //    //DataTable menudata = TYP_MENUMASTER.GetMenuData(HttpContext.Current.User.Identity.Name);

            //    //DataTable menudata = TYP_MENUMASTER.GetMenuData(HttpContext.Current.User.Identity.Name);
            //    AddTopMenuItems(menudata);
            //}

            //****** Till Here

        }
        else
        {
            lblWelcome.Visible = false;
            //lnkHome.Visible = false;
            if (!IsPostBack)
            {
                Session["login"] = false;
            }
        }


    }
    private static string CleanMenuText(string text)
    {
        return System.Text.RegularExpressions.Regex.Replace(text, @"<img\s[^>]*>", "").Trim();
    }
    private void AddTopMenuItems(DataTable menuData)
    {
        DataView view = new DataView(menuData);
        view.RowFilter = "PARENTMENUITEM IS NULL";
        foreach (DataRowView row in view)
        {
            MenuItem NewMenuItem = new MenuItem(CleanMenuText(row["MENUDESCRIPTION"].ToString()), row["MENUITEMCODE"].ToString(), "", row["FORMNAME"].ToString());
            Menu1.Items.Add(NewMenuItem);
            AddChildMenuItems(menuData, NewMenuItem);
        }
    }

    private void AddChildMenuItems(DataTable menuData, MenuItem parentMenuItem)
    {
        DataView view = new DataView(menuData);
        view.RowFilter = "PARENTMENUITEM=" + parentMenuItem.Value;
        foreach (DataRowView row in view)
        {
            MenuItem NewMenuItem = new MenuItem(CleanMenuText(row["MENUDESCRIPTION"].ToString()), row["MENUITEMCODE"].ToString(), "", row["FORMNAME"].ToString());
            parentMenuItem.ChildItems.Add(NewMenuItem);
            AddChildMenuItems(menuData, NewMenuItem);
        }
    }


    protected void LoginStatus_LoggedOut(object sender, EventArgs e)
    {
       // Cache.Remove(Session["UID"].ToString());

        Session.Clear();
        Session.Abandon();
        HttpContext.Current.Session.Clear();
        HttpContext.Current.Session.Abandon();
        HttpContext.Current.Application.RemoveAll();
        HttpContext.Current.Items.Clear();
        FormsAuthentication.SignOut();
        HttpContext.Current.Response.Cookies.Clear();

        Response.Cache.SetExpires(DateTime.UtcNow.AddSeconds(2));
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetNoStore();
        Session.Clear();
        Session.Abandon();
        Session.RemoveAll();
        FormsAuthentication.SignOut();
        Roles.DeleteCookie();
        Session.Clear();

        if (Request.Cookies["ASP.NET_SessionId"] != null)
        {
            Response.Cookies["ASP.NET_SessionId"].Value = string.Empty;
            Response.Cookies["ASP.NET_SessionId"].Expires = DateTime.Now.AddMonths(-20);
            Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", string.Empty));
        }

        if (Request.Cookies["AuthToken"] != null)
        {
            Response.Cookies["AuthToken"].Value = string.Empty;
            Response.Cookies["AuthToken"].Expires = DateTime.Now.AddMonths(-20);
            Response.Cookies.Add(new HttpCookie("AuthToken", ""));
        }
        Cache.Remove("Key1"); 
       Response.Redirect("~/Home.aspx");
       //HttpResponse.RemoveOutputCacheItem("~/Home.aspx");
    }



    //protected void lnkHome_Click(object sender, EventArgs e)
    //{        
    //    Session["login"] = false;
    //    Session.Abandon();
    //    Session.Clear();
    //    Session.RemoveAll();
    //    Response.Redirect("~/Home.aspx");
    //}

}
