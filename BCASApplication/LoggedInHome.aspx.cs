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
using System.Collections.Generic;
using System.Web.SessionState;
public partial class LoggedInHome : System.Web.UI.Page 
{
    String result = null;
    int a = 0;
    //TYP_ApplicantDetails objBCAS = new TYP_ApplicantDetails();

    TYP_USER_Details objUserDetails = new TYP_USER_Details();
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
                //

               // Dictionary<string, string> LoginUsersList = new Dictionary<string, string>();
               //LoginUsersList = (Dictionary<string, string>)Application["LoginUsersList"];
                // LoginUsersList = (Dictionary<string, string>);

                // LoginUsersList.Add(UID, UserID);
                //Application["LoginUsersList"] = LoginUsersList;
                //Application.Lock();


                // Application.Lock();
                Session["PrevLogin"] = "NO";
                HttpCookie reqCookies = Request.Cookies["ASP.NET_SessionId"];
                string SessionIds = "";
                 if(Session["UserIDs"]!=null)
                    SessionIds = Session["UserIDs"].ToString();
                 string Sid = "";
                 if(Session["login"]!=null)
                     Sid = Session["login"].ToString();
                string sessionId = System.Web.HttpContext.Current.Session.SessionID;
               // string SessionIds = Request.Cookies["ASP.NET_SessionId"].Value;
                string rd = "";
                if(Session["UID"]!=null)
                    rd = Session["UID"].ToString();
                // if (LoginUsersList.ContainsValue(Session["UID"].ToString()) && (LoginUsersList.ContainsKey(SessionIds)))
            //    if (LoginUsersList.ContainsKey(rd))
            //    {
                   
            //        LoginUsersList.Remove(rd);
                    
            //           // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Duplicate login not allowed. You have been logged out from another location')", true);
            //        //
            //            //Session.Clear();
            //            //Session.Abandon();
            //            //Session.RemoveAll();
            //            //FormsAuthentication.SignOut();
            //            //Roles.DeleteCookie();
            //            //Session.Clear();

            //            //if (Request.Cookies["ASP.NET_SessionId"] != null)
            //            //{
            //            //    Response.Cookies["ASP.NET_SessionId"].Value = string.Empty;
            //            //    Response.Cookies["ASP.NET_SessionId"].Expires = DateTime.Now.AddMonths(-20);
            //            //    //  Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", string.Empty));
            //            //}

            //            //if (Request.Cookies["AuthToken"] != null)
            //            //{
            //            //    Response.Cookies["AuthToken"].Value = string.Empty;
            //            //    Response.Cookies["AuthToken"].Expires = DateTime.Now.AddMonths(-20);
            //            //    // Response.Cookies.Add(new HttpCookie("AuthToken", ""));
            //            //}
            //            //Session["PrevLogin"] = "YES";
            //            //Response.Redirect("~/Home.aspx");

            //        //
                       
            //    //}
            //    //if (LoginUsersList.ContainsKey(SessionIds))
            //    //{
            //    //    LoginUsersList.Remove(SessionIds);
            //    //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Duplicate login not allowed. You have been logged out from another location')", true);

            //   // }
            //    Session["MLogin"] = sessionId;
            //   // LoginUsersList.Add(Session["UID"].ToString(), sessionId);
            //   // LoginUsersList.Add(sessionId, Session["UID"].ToString());
            //    Application["LoginUsersList"] = LoginUsersList;
            //    Application.UnLock();

            //    Session["AppSession"] = Session["UID"].ToString();
            //    //hfVal.Value = "1";
            //    ////string tt = Session["UserID"].ToString();
            //    //if (Session["UserID"] != null )
            //    //{
            //    //    string script = "  if (document.getElementById('" + hfVal.ClientID + "')) {" +
            //    //        "if (document.getElementById('" + hfVal.ClientID + "').value == '1') { " +
            //    //            " document.getElementById('" + hfVal.ClientID + "').value = '0';" +
            //    //            "window.top.location = 'LoggedInHome.aspx';" +
            //    //        "}" +
            //    //        "else { " +
            //    //            "if (window.top.location.href.indexOf('Home.aspx') >= 0) " +
            //    //            "{" +
            //    //                "window.top.location.href = 'Home.aspx';" +
            //    //            "} " +
            //    //        "} " +
            //    //    "}";
            //    //    Page.ClientScript.RegisterStartupScript(this.GetType(), "", script, true);
            //    //}

            //}


                //LoginStatus.Visible = Convert.ToBoolean(Session["View_Login"].ToString());
                // Added null check for user_id session
                if (Session["user_id"] != null && !string.IsNullOrEmpty(Session["user_id"].ToString()))
                {
                    string UserName = Session["user_id"].ToString();
                    using (DataTable dt = objUserDetails.GetOrganisationType(UserName))
                    {
                        if (dt.Rows.Count > 0)
                        {
                            if (dt.Rows[0]["Organisation_Type"].ToString() == "P")
                            {

                                using (DataTable new_dt = objUserDetails.GetUserOrganisationPayee(UserName))
                                {
                                    if (new_dt.Rows.Count > 0)
                                    {
                                        Session["UserOrg"] = new_dt.Rows[0]["Payee_Organisation_Code"].ToString();
                                        Session["OrgName"] = new_dt.Rows[0]["Payee_Organisation_Name"].ToString();
                                    }
                                }
                            }
                            else
                            {
                                using (DataTable new_dt1 = objUserDetails.getUserOrganisation(UserName))
                                {
                                    if (new_dt1.Rows.Count > 0)
                                    {
                                        Session["UserOrg"] = new_dt1.Rows[0]["Organisation_Code"].ToString();

                                        Session["OrgName"] = new_dt1.Rows[0]["Organisation_Name"].ToString();

                                    }

                                }

                            }

                        }

                    }
                }
                else
                {
                    // If user_id session is not set, redirect to home
                    Response.Redirect("~/Home.aspx");
                }

            }
           
        }


    }

    protected void LoginStatus_LoggedOut(object sender, EventArgs e)
    {
        //Session["login"] = false;

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
            //Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", string.Empty));
        }

        if (Request.Cookies["AuthToken"] != null)
        {
            Response.Cookies["AuthToken"].Value = string.Empty;
            Response.Cookies["AuthToken"].Expires = DateTime.Now.AddMonths(-20);
           // Response.Cookies.Add(new HttpCookie("AuthToken", ""));
        }

        //Session.Clear();
        //Session.Abandon();
        //Session.RemoveAll();
        //if (Request.Cookies["ASP.NET_SessionId"] != null)
        //{
        //    Response.Cookies["ASP.NET_SessionId"].Expires = DateTime.Now.AddDays(-1);
        //    Response.Cookies["ASP.NET_SessionId"].Value = string.Empty;
        //    Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", string.Empty));
        //}
        //if (Request.Cookies["AuthToken"] != null)
        //{
        //    Response.Cookies["AuthToken"].Value = string.Empty;
        //    Response.Cookies["AuthToken"].Expires = DateTime.Now.AddMonths(-20);
        //    Response.Cookies.Add(new HttpCookie("AuthToken", ""));
        //}




    }
    ////deep
    //protected void popup()
    //{
    //    Page.ClientScript.RegisterStartupScript(this.GetType(),"Scripts","<script>alert('Bcas Fee will be 500/- and service tax 14% from 01 June 2015.');</script>");

    //}
    ////deep





}
