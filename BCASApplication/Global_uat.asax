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

    // SA May-2026: block plaintext HTTP POST submissions + generate per-request CSP nonce
    void Application_BeginRequest(object sender, EventArgs e)
    {
        var request = HttpContext.Current.Request;
        bool isHttps = request.IsSecureConnection ||
            string.Equals(request.Headers["X-Forwarded-Proto"], "https", StringComparison.OrdinalIgnoreCase);
        if (!isHttps && request.HttpMethod == "POST")
        {
            HttpContext.Current.Response.StatusCode = 400;
            HttpContext.Current.Response.StatusDescription = "Bad Request";
            HttpContext.Current.Response.End();
            return;
        }
        var bytes = new byte[18];
        using (var rng = new System.Security.Cryptography.RNGCryptoServiceProvider())
            rng.GetBytes(bytes);
        HttpContext.Current.Items["csp_nonce"] =
            Convert.ToBase64String(bytes).Replace('+', '-').Replace('/', '_').TrimEnd('=');
    }

    // SA May-2026: attach nonce filter before handler runs so all <script> tags receive the nonce
    void Application_PreRequestHandlerExecute(object sender, EventArgs e)
    {
        var nonce = HttpContext.Current.Items["csp_nonce"] as string;
        if (nonce != null)
            HttpContext.Current.Response.Filter =
                new CspNonceFilter(HttpContext.Current.Response.Filter, nonce);
    }

    // SA May-2026: security headers on every response (IIS 6 + IIS 10)
    void Application_EndRequest(object sender, EventArgs e)
    {
        var ctx = HttpContext.Current;
        if (ctx == null) return;
        var response = ctx.Response;
        var nonce = ctx.Items["csp_nonce"] as string;

        try { response.Headers.Remove("X-XSS-Protection"); } catch { }

        AddIfMissing(response, "X-Content-Type-Options",    "nosniff");
        AddIfMissing(response, "X-Frame-Options",           "SAMEORIGIN");
        AddIfMissing(response, "Strict-Transport-Security", "max-age=31536000; includeSubDomains; preload");
        AddIfMissing(response, "Referrer-Policy",           "strict-origin-when-cross-origin");
        AddIfMissing(response, "Permissions-Policy",        "camera=(), microphone=(), geolocation=(), fullscreen=(self)");

        string scriptSrc = (nonce != null)
            ? "script-src 'self' 'nonce-" + nonce + "'; "
            : "script-src 'self'; ";

        AddIfMissing(response, "Content-Security-Policy",
            "default-src 'self'; " +
            scriptSrc +
            "style-src 'self' 'unsafe-inline'; " +
            "img-src 'self' data:; " +
            "font-src 'self' data:; " +
            "object-src 'none'; " +
            "base-uri 'self'; " +
            "form-action 'self'; " +
            "frame-ancestors 'self';");
    }

    void AddIfMissing(HttpResponse response, string header, string value)
    {
        if (string.IsNullOrEmpty(response.Headers[header]))
            response.AppendHeader(header, value);
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
