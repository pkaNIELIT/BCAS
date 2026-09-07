<%@ WebHandler Language="C#" Class="ImageHandler" %>

using System;
using System.Web;
using System.Data;
using BCASClassLibrary;

public class ImageHandler : IHttpHandler
{
    public void ProcessRequest (HttpContext context) {
        //DataSet ds = new DataSet();
        //    string pPersonnelID = context.Request.QueryString["ID"];
        //    ds = TYP_USERPERSONMASTER.SelectPhoto(pPersonnelID);
        //    context.Response.BinaryWrite((byte[])ds.Tables[0].Rows[0]["SIGNEDPHOTO"]);
        //    context.Response.End();
        //    ds.Dispose();
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}