using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using BCASClassLibrary;

/// <summary>
/// Summary description for Session
/// </summary>
public class Password
{
    public static string Generate_Password()
    {
        Random rdm = new Random();
        return (rdm.Next(1111111111, 2122222222).ToString());

    }
}
