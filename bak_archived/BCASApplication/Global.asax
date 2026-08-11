<%@ Application Language="C#" %>


<script runat="server">

    void Application_Start(object sender, EventArgs e)
    {

        // Code that runs on application startup
        System.Collections.Generic.Dictionary<string, Int32> LoginUsersList = new System.Collections.Generic.Dictionary<string, Int32>();
        Application["LoginUsersList"] = LoginUsersList;

        System.Collections.Generic.Dictionary<String, HttpSessionState> sessionData =
               new System.Collections.Generic.Dictionary<String, HttpSessionState>();
        Application["s"] = sessionData;

    }

    void Application_End(object sender, EventArgs e)
    {
        //  Code that runs on application shutdown

    }

    void Application_Error(object sender, EventArgs e)
    {
        // Code that runs when an unhandled error occurs

    }

    void Session_Start(object sender, EventArgs e)
    {
        // Code that runs when a new session is started
        // Session["AppSession"] = "0";
        Session["LogID"] = "0";
        try
        {
            System.Collections.Generic.Dictionary<String, HttpSessionState> sessionData = (System.Collections.Generic.Dictionary<String, HttpSessionState>)Application["s"];

            if (sessionData != null && sessionData.ContainsKey(HttpContext.Current.Session.SessionID))
            {
                sessionData.Remove(HttpContext.Current.Session.SessionID);
                sessionData.Add(HttpContext.Current.Session.SessionID,
                                HttpContext.Current.Session);
            }
            else
            {
                //sessionData.Add(HttpContext.Current.Session.SessionID, HttpContext.Current.Session);
            }
            Application["s"] = sessionData;


        }
        catch (Exception)
        {

        }
        //// Code that runs when a new session is started
        // HttpCookie mycookie;
        // mycookie = new HttpCookie(HttpContext.Current.Response.Cookies["ASP.NET_SessionId"].Name, HttpContext.Current.Response.Cookies["ASP.NET_SessionId"].Value);
        // HttpContext.Current.Response.Cookies.Remove("ASP.NET_SessionId");
        // mycookie.Path = "/BCASApplication/Home.aspx/";
        //// mycookie.Path = "/bcas.nielit.gov.in/";
        // HttpContext.Current.Response.Cookies.Add(mycookie);

        HttpCookie mycookie;
        mycookie = new HttpCookie(HttpContext.Current.Response.Cookies["NET_SessionId"].Name, HttpContext.Current.Response.Cookies["NET_SessionId"].Value);
        HttpContext.Current.Response.Cookies.Remove("NET_SessionId");
        mycookie.Path = "/BCASApplication/Home.aspx/";
        HttpContext.Current.Response.Cookies.Add(mycookie);

    }

    void Session_End(object sender, EventArgs e)
    {
        try
        {
            System.Collections.Generic.Dictionary<string, Int32> LoginUsersList = new System.Collections.Generic.Dictionary<string, Int32>();
            System.Collections.Generic.Dictionary<string, HttpSessionState> sessionData = (System.Collections.Generic.Dictionary<string, HttpSessionState>)Application["s"];
            //if ( sessionData.ContainsKey(Session.SessionID))
            //sessionData.Remove(Session.SessionID);
            Application["s"] = sessionData;
            LoginUsersList = (System.Collections.Generic.Dictionary<string, Int32>)Application["LoginUsersList"];
            //int logID = Convert.ToInt32(Session["logID"]);
            //string userID = Session["UID"].ToString();
            //if (LoginUsersList.ContainsValue(logID))
            //    LoginUsersList.Remove(userID);
            //EConnect.DAL.EConnectContext context = new EConnect.DAL.EConnectContext();
            //EConnect.URM.LoginLog log = context.LoginLogs.Find(logID);
            //log.LogoutTime = DateTime.Now;
            //context.Entry(log).State = System.Data.Entity.EntityState.Modified;
            //context.SaveChanges();
            //context.Dispose();
            //string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            //using (System.Data.SqlClient.SqlConnection Conn = new System.Data.SqlClient.SqlConnection(ConnectionString))
            //{
            //    using (System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("update UserDetails_LOGIN_LOG  set LOGOUT_TIME=@DateTime    where ID= " + logID, Conn))
            //    {
            //        Conn.Open();
            //        cmd.CommandType = System.Data.CommandType.Text;
            //        cmd.Parameters.AddWithValue("@DateTime", System.DateTime.Now);                    
            //        cmd.ExecuteNonQuery();
            //    }
            //}
        }
        catch (Exception)
        {

        }
    }

</script>
