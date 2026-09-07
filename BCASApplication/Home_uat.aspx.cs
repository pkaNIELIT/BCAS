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
using System.Security.Cryptography;
using BCASClassLibrary;
using System.Collections.Generic;
using System.Web.SessionState;


public partial class _Default : System.Web.UI.Page
{
    //TYP_AUDIT_TRAIL objAudit = new TYP_AUDIT_TRAIL();


    public void ClearCookies()
    {
        string[] myCookies = Request.Cookies.AllKeys;
        foreach (string cookie in myCookies)
        {
            Response.Cookies[cookie].Expires = DateTime.Now.AddDays(-1);
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        string x = Membership.HashAlgorithmType.ToString();
        if (Session["PrevLogin"] != null)
        {
            if (Session["PrevLogin"].ToString() == "YES")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Duplicate login not allowed. You have been logged out from another location')", true);

            }
        }
        // BtnLogin.Attributes.Add("OnClick", "SpPwd()");
        HttpContext.Current.Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
        HttpContext.Current.Response.Cache.SetValidUntilExpires(false);
        HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        HttpContext.Current.Response.Cache.SetNoStore();
        //######

        if (Response.IsClientConnected)
        {
            Random RND = new Random();
            int val = RND.Next();

            if (!IsPostBack)
            {

                Session["RNDam"] = val;

                // SA May-2026: generate per-request AES-256 key+IV for password encryption
                byte[] aesKey = new byte[32];
                byte[] aesIv = new byte[16];
                using (var rng = new System.Security.Cryptography.RNGCryptoServiceProvider())
                {
                    rng.GetBytes(aesKey);
                    rng.GetBytes(aesIv);
                }
                ViewState["AesKey"] = Convert.ToBase64String(aesKey);
                ViewState["AesIV"]  = Convert.ToBase64String(aesIv);
                hfKey.Value = Convert.ToBase64String(aesKey) + ":" + Convert.ToBase64String(aesIv);

                UserName.Focus();

                //logout_test();

                ////Session.Abandon();
                ////Session.Clear();
                ////Session.RemoveAll();
                ////Session["login"] = false;
                ////Session["View_Login"] = false;


            }
            //Session["login"] = false;
            Session["login"] = true;
            //Session["View_Login"] = false;
            string sessionId = System.Web.HttpContext.Current.Session.SessionID;
            //ClearCookies();   

        }
        else
        {
            Response.End();
        }


    }

    //protected void Login1_LoggedIn(object sender, EventArgs e)
    //{
    //    ////Membership.DeleteUser("Administrator");
    //    //Session["login"] = true;
    //    //Session.Add("UserId", Login1.UserName);
    //    //Response.Redirect("~/LoggedInHome.aspx");
    //    ////Membership.DeleteUser("Administrator");
    //}

    //protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
    //{
    //    if (Membership.ValidateUser(Login1.UserName, Login1.Password))
    //    {
    //        Session["View_Login"] = true;
    //        Session["login"] = true;
    //        Session["user_id"] = Login1.UserName;
    //        Session.Add("UserId", Login1.UserName);
    //        FormsAuthentication.SetAuthCookie(Login1.UserName, true);
    //        Session["Current_Date"] = System.DateTime.Now.Date.ToString("MM-dd-yyyy");
    //        //Session["Exam_Centre_Code"] = TYP_Mark_Fee_Received.Get_Exam_Centre_Code_For_Superintendent(Session["user_id"].ToString(), System.DateTime.Now.Date).ToString();
    //        Response.Redirect("~/LoggedInHome.aspx");
    //    }
    //    else
    //        return;
    //}



    //protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
    //{

    //    MembershipUser memCheckUser = Membership.GetUser(Login1.UserName);
    //    if (memCheckUser != null && memCheckUser.IsLockedOut == true)
    //    {
    //        ////****** For Account Lockout

    //        Login1.FailureText = "User Account Locked, Kindly Reset Your Password.";

    //    }
    //    else
    //    {
    //        if (Membership.ValidateUser(Login1.UserName, Login1.Password))
    //        {

    //            //***** New Code for Role Checking Added on: 30.12.2013******                               
    //            var roleslist = Roles.GetRolesForUser(Login1.UserName);

    //            //string roleslist = Roles.GetRolesForUser(Login1.UserName).ToString ();


    //            using (DataSet ds = TYP_USERROLEFORM.GetFormsForRole(roleslist[0].ToString()))
    //            {
    //                if (ds.Tables[0].Rows.Count != 0)
    //                {
    //                    Session["Forms"] = ds.Tables[0];
    //                }

    //            }

    //            //****Till Here******

    //            Session["View_Login"] = true;
    //            Session["login"] = true;
    //            Session["user_id"] = Login1.UserName;
    //            Session.Add("UserId", Login1.UserName);
    //            FormsAuthentication.SetAuthCookie(Login1.UserName, true);
    //            Session["Current_Date"] = System.DateTime.Now.Date.ToString("MM-dd-yyyy");
    //            //Session["Exam_Centre_Code"] = TYP_Mark_Fee_Received.Get_Exam_Centre_Code_For_Superintendent(Session["user_id"].ToString(), System.DateTime.Now.Date).ToString();
    //            Response.Redirect("~/LoggedInHome.aspx");
    //        }
    //        else
    //            return;
    //    }

    //    //if (Membership.ValidateUser(Login1.UserName, Login1.Password))
    //    //{
    //    //    Session["View_Login"] = true;
    //    //    Session["login"] = true;
    //    //    Session["user_id"] = Login1.UserName;
    //    //    Session.Add("UserId", Login1.UserName);
    //    //    FormsAuthentication.SetAuthCookie(Login1.UserName, true);
    //    //    Session["Current_Date"] = System.DateTime.Now.Date.ToString("MM-dd-yyyy");
    //    //    //Session["Exam_Centre_Code"] = TYP_Mark_Fee_Received.Get_Exam_Centre_Code_For_Superintendent(Session["user_id"].ToString(), System.DateTime.Now.Date).ToString();
    //    //    Response.Redirect("~/LoggedInHome.aspx");
    //    //}
    //    //else
    //    //    return;

    //}


    protected void logout_test()
    {
        //Session["login"] = false;


        if (Request.Cookies["ASP.NET_SessionId"] != null)
        {
            //Session.Abandon();
            //Session.Clear();
            //Session.RemoveAll();


            Response.Cookies["ASP.NET_SessionId"].Value = string.Empty;
            //Response.Cookies["ASP.NET_SessionId"].Expires = DateTime.Now.AddMonths(-20);
            Response.Cookies["ASP.NET_SessionId"].Expires = DateTime.Now.AddMinutes(15);
        }

        if (Request.Cookies["AuthToken"] != null)
        {
            Response.Cookies["AuthToken"].Value = string.Empty;
            //Response.Cookies["AuthToken"].Expires = DateTime.Now.AddMonths(-20);
            Response.Cookies["AuthToken"].Expires = DateTime.Now.AddMinutes(15);
        }

        //Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));
        //Response.Redirect("~/Home.aspx");
    }

    //protected void Login_Click(object sender, EventArgs e)
    //{
    //    if (ValidateUser())
    //    {

    //        Login1.UserName = txtUserName.Text.Trim();

    //        MembershipUser memCheckUser = Membership.GetUser(Login1.UserName);
    //        if (memCheckUser != null && memCheckUser.IsLockedOut == true)
    //        {
    //            ////****** For Account Lockout

    //            Login1.FailureText = "User Account Locked, Kindly Reset Your Password.";

    //        }
    //        else
    //        {
    //            if (Membership.ValidateUser(Login1.UserName, txtPwd.Text.Trim()))
    //            {

    //                //***** New Code for Role Checking Added on: 30.12.2013******                               
    //                var roleslist = Roles.GetRolesForUser(Login1.UserName);

    //                //string roleslist = Roles.GetRolesForUser(Login1.UserName).ToString ();


    //                using (DataSet ds = TYP_USERROLEFORM.GetFormsForRole(roleslist[0].ToString()))
    //                {
    //                    if (ds.Tables[0].Rows.Count != 0)
    //                    {
    //                        Session["Forms"] = ds.Tables[0];
    //                    }

    //                }

    //                //****Till Here******

    //                Session["View_Login"] = true;
    //                Session["login"] = true;
    //                Session["user_id"] = Login1.UserName;
    //                Session.Add("UserId", Login1.UserName);
    //                FormsAuthentication.SetAuthCookie(Login1.UserName, true);
    //                Session["Current_Date"] = System.DateTime.Now.Date.ToString("MM-dd-yyyy");
    //                //Session["Exam_Centre_Code"] = TYP_Mark_Fee_Received.Get_Exam_Centre_Code_For_Superintendent(Session["user_id"].ToString(), System.DateTime.Now.Date).ToString();
    //                Response.Redirect("~/LoggedInHome.aspx");
    //            }
    //            else
    //                return;
    //        }           

    //    }       

    //}


    public bool ValidateUser()
    {
        bool valid = true;

        //if (txt_ImgVerifyCode.Text.Trim() == string.Empty)
        //{
        //    // lblmsg.Text = ("Verification Code must be Entered");         
        //    //ScriptManager.RegisterStartupScript(this, GetType(), "alertmsg", "alert('Verification Code must be Entered');",true );
        //    string script = "<script type=\"text/javascript\">alert('Verification Code must be Entered');</script>";
        //    ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", script);
        //    valid = false;
        //}

        if (ImageVerifier1.Text != txt_ImgVerifyCode.Text)
        {
            string script = "alert('You have entered a wrong verification code');";
            ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", script, true);
            valid = false;
        }
        return valid;
    }

    protected void BtnLogin_Click(object sender, EventArgs e)
    {
        if (ValidateUser())
        {
            string rds = string.Empty;
            
            //******* Fresh Code 21.12.2012            
            if (Page.IsValid)
            {
                // SA May-2026: AES-decrypt browser-encrypted password; reject plaintext submissions
                string plainPassword = string.Empty;
                {
                    string encPwd    = hfEncPwd.Value;
                    string aesKeyB64 = ViewState["AesKey"] as string;
                    string aesIvB64  = ViewState["AesIV"]  as string;
                    if (!string.IsNullOrEmpty(encPwd) && aesKeyB64 != null && aesIvB64 != null)
                    {
                        try
                        {
                            byte[] key    = Convert.FromBase64String(aesKeyB64);
                            byte[] iv     = Convert.FromBase64String(aesIvB64);
                            byte[] cipher = Convert.FromBase64String(encPwd);
                            using (var aes = new System.Security.Cryptography.AesCryptoServiceProvider())
                            {
                                aes.Key     = key;
                                aes.IV      = iv;
                                aes.Mode    = System.Security.Cryptography.CipherMode.CBC;
                                aes.Padding = System.Security.Cryptography.PaddingMode.PKCS7;
                                using (var dec = aes.CreateDecryptor())
                                {
                                    byte[] plain = dec.TransformFinalBlock(cipher, 0, cipher.Length);
                                    plainPassword = System.Text.Encoding.UTF8.GetString(plain);
                                }
                            }
                        }
                        catch
                        {
                            UserName.Text = "";
                            TxtPassword.Text = "";
                            lblError.Text = "Incorrect user name or password";
                            lblError.Visible = true;
                            return;
                        }
                    }
                    else
                    {
                        UserName.Text = "";
                        TxtPassword.Text = "";
                        lblError.Text = "Incorrect user name or password";
                        lblError.Visible = true;
                        return;
                    }
                }

                //$$$$$$$$$$$$

                //*******Retrieve Password Here
                TYP_ApplicantDetails obj = new TYP_ApplicantDetails();
                string UID = UserName.Text.Trim();
                DataTable dt = obj.GetPassword(UID);
                string UserID = "";
                if (dt.Rows.Count > 0)
                    UserID = dt.Rows[0]["UserId"].ToString().ToLower();

                //$$$$$$$$$$$$
                //objAudit.v_UserName = UserName.Text.Trim();
                //objAudit.v_IP = HttpContext.Current.Request.UserHostAddress;

                //****##########******* Login check **********##########>>>>>

                //*********** New Code               
                MembershipUser memCheckUser = Membership.GetUser(UserName.Text);
                if (memCheckUser != null && memCheckUser.IsLockedOut == true)
                {
                    ////****** For Account Lockout
                    //obj.UpdateUserLockOutAttempt(UserName.Text);

                    ////**** For Audit Trail Log ***                    
                    //objAudit.v_login_status = "Failed";
                    //using (DataTable dt_Pay = objAudit.InsertAuditTrail())
                    //{
                    //    if (Convert.ToInt32(dt_Pay.Rows[0][0]) != 0)
                    //    {
                    //        lblError.Text = "Insertion Failed";
                    //    }
                    //}
                    ////*******Till Here 

                    UserName.Text = "";
                    TxtPassword.Text = "";
                    lblError.Text = "user account locked out";
                    lblError.Visible = true;
                }
                // [SECURITY AUDIT May-2026] Removed "|| 1==1" that was bypassing credential validation entirely.
                else if (Membership.ValidateUser(UserName.Text, plainPassword))
                // [END SECURITY AUDIT]
                // else if (usr.ToLower () == UserName.Text.ToLower () && totalhashpass == TxtPassword.Text)
                // else if (usr == UserName.Text && totalhashpass != TxtPassword.Text)
                {
                    //********For Account Reset
                    obj.ResetAccount(UserName.Text);
                    ////**** For Audit Trail Log ***                    
                    //objAudit.v_login_status = "Success";
                    //using (DataTable dt_Pay = objAudit.InsertAuditTrail())
                    //{
                    //    if (Convert.ToInt32(dt_Pay.Rows[0][0]) == 0)
                    //    {
                    //        Session.Add("ID", dt_Pay.Rows[0][1]);
                    //        //lblError.Text = "Insertion Failed";
                    //    }
                    //}

                    // CASCH
                    //
                    //First get user claims    

                    string sKey = UID;

                    //string sUser = Convert.ToString(Cache[sKey]);

                    //if (sUser == null || sUser == String.Empty)
                    //{

                    //    // No Cache item, so sesion is either expired or user is new sign-on

                    //    // Set the cache item and Session hit-test for this user---

                    //    TimeSpan SessTimeOut = new TimeSpan(0, 0, HttpContext.Current.Session.Timeout, 0, 0);

                    //    HttpContext.Current.Cache.Insert(sKey, sKey, null, DateTime.MaxValue, SessTimeOut,



                    //    System.Web.Caching.CacheItemPriority.NotRemovable, null);

                    //    //  Session["user"] = UserName.Text + Password.Text;

                    //    // Let them in - redirect to main page, etc.

                    //    //  Response.Redirect("Home.aspx");
                    //}
                    //else
                    //{
                    //    //Session.Abandon();
                    //    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Already Logged!!');", true);
                    //   // Response.Redirect("Home.aspx");
                    //    return;
                    //}
                    //


                    //CASCB
                    // for Concurrent login remove automatically start
                    //Dictionary<string, string> LoginUsersList = new Dictionary<string, string>();
                    //LoginUsersList = (Dictionary<string, string>)Application["LoginUsersList"];
                    //string AlreadyLoggedUser = UID;
                    //if (LoginUsersList.ContainsKey(AlreadyLoggedUser))
                    //{
                    //    LoginUsersList.Remove(UID);

                    //    // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Duplicate login not allowed. You have been logged out from another location')", true);

                    //    Session.Abandon();
                    //    Session.Clear();

                    //    Session.RemoveAll();
                    //    if (Request.Cookies["ASP.NET_SessionId"] != null)
                    //    {
                    //        Response.Cookies["ASP.NET_SessionId"].Value = string.Empty;
                    //        Response.Cookies["ASP.NET_SessionId"].Expires = DateTime.Now.AddMonths(-20);
                    //    }

                    //    if (Request.Cookies["AuthToken"] != null)
                    //    {
                    //        Response.Cookies["AuthToken"].Value = string.Empty;
                    //        Response.Cookies["AuthToken"].Expires = DateTime.Now.AddMonths(-20);
                    //    }
                    //    Response.Redirect("~/Home.aspx");
                    //    //Session["ASP.NET_SessionId"] = null;
                    //    //Session["user_id"] = null;
                    //    //Session["UserId"] = null;
                    //    //Session["UserId"] = null;
                    //}





                    // for concurrent login remove automaticall end


                    ////*******Till Here
                    //deep add code 19 july 2017
                    var roleslist = Roles.GetRolesForUser(UserName.Text.Trim());
                    if (roleslist.Length == 0)
                    {
                        lblError.Text = "Incorrect user name or password";
                        lblError.Visible = true;
                        return;
                    }

                    //string roleslist = Roles.GetRolesForUser(Login1.UserName).ToString ();


                    using (DataSet ds = TYP_USERROLEFORM.GetFormsForRole(roleslist[0].ToString()))
                    {
                        if (ds.Tables[0].Rows.Count != 0)
                        {
                            Session["Forms"] = ds.Tables[0];
                        }

                    }




                    //****Till Here******

                    Session["View_Login"] = true;
                    Session["login"] = true;
                    // Session["user_id"] = Login1.UserName;
                    // Session.Add("UserId", Login1.UserName);
                    //FormsAuthentication.SetAuthCookie(Login1.UserName, true);
                    //deep end code 19 july 2017
                    lblError.Visible = false;

                    //*********
                    Random rd = new Random();
                    Session["New"] = rd.Next();
                    HttpCookie myCookie = new HttpCookie("Session");
                    myCookie.Value = Session["New"].ToString();
                    myCookie.Path = "/";
                    Response.Cookies.Add(myCookie);

                    //**********               
                    Session.Add("user_id", UserName.Text.Trim());
                    Session.Add("UserId", UserName.Text.Trim());

                    FormsAuthentication.SetAuthCookie(UserName.Text.Trim(), true);
                    UserName.Text = "";
                    TxtPassword.Text = "";


                    Session["UID"] = UID;
                    Session["UserIDs"] = UserID;
                    //





                    Response.Redirect("~/LoggedInHome.aspx", false);




                }
                else
                {
                    //****** For Account Lockout
                    ////   obj.UpdateUserLockOutAttempt(UserName.Text);

                    ////**** For Audit Trail Log ***                    
                    //objAudit.v_login_status = "Failed";
                    //using (DataTable dt_Pay = objAudit.InsertAuditTrail())
                    //{
                    //    if (Convert.ToInt32(dt_Pay.Rows[0][0]) != 0)
                    //    {
                    //        lblError.Text = "Insertion Failed";
                    //    }
                    //}
                    ////*******Till Here 

                    UserName.Text = "";
                    TxtPassword.Text = "";
                    lblError.Text = "Incorrect user name or password";
                    lblError.Visible = true;

                }

            }
        }
        //******* Till here fresh code   
    }


    protected void cmdReset_Click(object sender, EventArgs e)
    {
        UserName.Text = "";
        TxtPassword.Text = "";
    }

}
