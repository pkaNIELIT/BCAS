using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Web.Caching;

namespace BCASClassLibrary
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:ImageVerifier runat=server></{0}:ImageVerifier>")]
    public class ImageVerifier : WebControl, IHttpHandler
    {
        private string m_UniqueID = string.Empty;
        public ImageVerifier() : base(HtmlTextWriterTag.Img)
        {
        }

        private string MyUniqueID
        {
            get
            {
                if (m_UniqueID == string.Empty) m_UniqueID = Guid.NewGuid().ToString();
                return m_UniqueID;
            }
        }
        private string GetRandomText()
        {
            string uniqueID = Guid.NewGuid().ToString();
            string randString = "";
            for (int i = 0, j = 0; i < uniqueID.Length && j < 5; i++)
            {
                char l_ch = uniqueID.ToCharArray()[i];
                if ((l_ch >= 'A' && l_ch <= 'Z') || (l_ch >= 'a' && l_ch <= 'z') || (l_ch >= '0' && l_ch <= '9'))
                {
                    randString += l_ch;
                    j++;
                }
            }
            return randString;
        }
        public string Text
        {
            get
            {
                return string.Format("{0}", HttpContext.Current.Cache[this.MyUniqueID]);
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            Page.RegisterRequiresControlState(this);
        }
        protected override void LoadControlState(object savedState)
        {
            Pair p = savedState as Pair;
            if (p != null)
            {
                m_UniqueID = p.Second as string;
            }
        }
        protected override object SaveControlState()
        {
            Object baseState = base.SaveControlState();
            Pair prState = new Pair(baseState, this.MyUniqueID);
            return prState;
        }
        protected override void Render(HtmlTextWriter output)
        {
            output.AddAttribute(HtmlTextWriterAttribute.Src, "ImageVerifier.axd?uid=" + this.MyUniqueID);
            base.Render(output);
            output.Write("<script language='javascript'>");
            output.Write("function RefreshImageVerifier(id,srcname)");
            output.Write("{ var elm = document.getElementById(id);");
            output.Write("  var dt = new Date();");
            output.Write("  elm.src=srcname + '&ts=' + dt;");
            output.Write("  return false;");
            output.Write("}</script>");
            output.Write("&nbsp;<a href='#' onclick=\"return RefreshImageVerifier('" + this.ClientID + "','ImageVerifier.axd?&uid=" + this.MyUniqueID + "');\">Refresh</a>");
        }

        public void ProcessRequest(HttpContext context)
        {
            Bitmap bmp = new Bitmap(180, 40);
            Graphics g = Graphics.FromImage(bmp);

            string randString = GetRandomText();
            string myUID = context.Request["uid"].ToString();
            if (context.Cache[myUID] == null)
                context.Cache.Add(myUID, randString, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(5), System.Web.Caching.CacheItemPriority.Normal, null);
            else
                context.Cache[myUID] = randString;

            g.FillRectangle(Brushes.WhiteSmoke, 0, 0, 180, 40);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            Random rand = new Random();
            for (int i = 0; i < randString.Length; i++)
            {
                Font drawFont = new Font("Arial", 18, FontStyle.Italic | (rand.Next() % 2 == 0 ? FontStyle.Bold : FontStyle.Regular));
                g.DrawString(randString.Substring(i, 1), drawFont, Brushes.Black, i * 35 + 10, rand.Next() % 12);
            }

            Point[] pt = new Point[15];
            for (int i = 0; i < 15; i++)
            {
                pt[i] = new Point(rand.Next() % 180, rand.Next() % 35);
                g.DrawEllipse(Pens.LightSteelBlue, pt[i].X, pt[i].Y, rand.Next() % 30 + 1, rand.Next() % 30 + 1);
            }
            context.Response.Clear();
            context.Response.ClearHeaders();
            context.Response.ContentType = "image/jpeg";
            bmp.Save(context.Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);
            context.Response.End();
        }
        //g.DrawLines(Pens.BlueViolet, pt);
        public bool IsReusable
        {
            get
            {
                return true;
            }
        }
    }
}
