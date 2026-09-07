using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using BCASClassLibrary;
using Ionic.Zip;
using System.IO;
using System.Collections.Generic;

public partial class Forms_DetailsForms_BCAS_Download_PhotoSign : System.Web.UI.Page
{
    TYP_BCAS_Approval objBCAS = new TYP_BCAS_Approval();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if ((Convert.ToString(HttpContext.Current.Session["UserId"]).Trim().Length == 0))
            {
                HttpContext.Current.Response.Redirect("~/Home.aspx");
            }
            else
            {
                using (DataTable dt = objBCAS.getQryBCASCourseSchedule())
                {
                    if (dt.Rows.Count > 0)
                    {
                        ddlSchedule.DataSource = dt;
                        ddlSchedule.DataBind();
                    }
                }
            }
        }
    }

    protected void btnDownload_Click(object sender, EventArgs e)
    {
        // ---------- SINGLE FILE DOWNLOAD ----------
        if (TxtRegNo.Text != "0")
        {
            var regn = TxtRegNo.Text.Trim();
            int coursecode = 0;

            using (DataTable dt = objBCAS.GetBCASNominatedCandidateCourse(Convert.ToInt32(ddlSchedule.SelectedValue)))
            {
                if (dt.Rows.Count > 0)
                    coursecode = Convert.ToInt32(dt.Rows[0][0]);
            }

            string Path1;
            string downloadName;

            if (ddlimage.SelectedValue == "photo")
            {
                if (coursecode == 3 || coursecode == 4)
                {
                    Path1 = "~/upload/Screeners/Photograph/" + regn + ".jpeg";
                    if (!File.Exists(Server.MapPath(Path1)))
                        Path1 = "~/upload/Photograph/" + regn + ".jpeg";
                }
                else
                {
                    Path1 = "~/upload/Photograph/" + regn + ".jpeg";
                }
                downloadName = regn + "_Photo.jpeg";
            }
            else
            {
                if (coursecode == 3 || coursecode == 4)
                {
                    Path1 = "~/upload/Screeners/Signature/" + regn + ".jpeg";
                    if (!File.Exists(Server.MapPath(Path1)))
                        Path1 = "~/upload/Signature/" + regn + ".jpeg";
                }
                else
                {
                    Path1 = "~/upload/Signature/" + regn + ".jpeg";
                }
                downloadName = regn + "_Sign.jpeg";
            }

            string filePath = Server.MapPath(Path1);
            if (File.Exists(filePath))
            {
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "image/jpeg";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + downloadName);
                Response.WriteFile(filePath);
                Response.Flush();
                Response.End();
            }
            else
            {
                Label1.Text = (ddlimage.SelectedValue == "photo") ? "Photo not found." : "Signature not found.";
            }

            return;
        }

        // ---------- BULK (ZIP) DOWNLOAD ----------
        using (DataTable dt = objBCAS.GetBCASNominatedCandidateRegistrationNumber(
                    Convert.ToInt32(TxtRegNo.Text), Convert.ToInt32(ddlSchedule.SelectedValue)))
        {
            if (dt.Rows.Count == 0)
            {
                Label1.Text = "No Record Found";
                return;
            }

            int coursecode = 0;
            using (DataTable dt1 = objBCAS.GetBCASNominatedCandidateCourse(Convert.ToInt32(ddlSchedule.SelectedValue)))
            {
                if (dt1.Rows.Count > 0)
                    coursecode = Convert.ToInt32(dt1.Rows[0][0]);
            }

            int count = 0;

            using (ZipFile zip = new ZipFile())
            {
                zip.AlternateEncodingUsage = ZipOption.AsNecessary;

                foreach (DataRow dtRow in dt.Rows)
                {
                    string regn = dtRow["Registration_No"].ToString().Trim();

                    string fileName = (rbtnImageName.SelectedValue == "Rollno"
                            ? dtRow["Roll_No"].ToString().Trim()
                            : regn) + ".jpeg";

                    if (fileName == ".jpeg" || regn.Length == 0)
                        continue;

                    string subFolder, path;

                    if (ddlimage.SelectedValue == "photo")
                    {
                        subFolder = "Photograph";
                        if (coursecode == 3 || coursecode == 4)
                        {
                            path = "~/upload/Screeners/Photograph/" + regn + ".jpeg";
                            if (!File.Exists(Server.MapPath(path)))
                                path = "~/upload/Photograph/" + regn + ".jpeg";
                        }
                        else
                        {
                            path = "~/upload/Photograph/" + regn + ".jpeg";
                        }
                    }
                    else
                    {
                        subFolder = "Signature";
                        if (coursecode == 3 || coursecode == 4)
                        {
                            path = "~/upload/Screeners/Signature/" + regn + ".jpeg";
                            if (!File.Exists(Server.MapPath(path)))
                                path = "~/upload/Signature/" + regn + ".jpeg";
                        }
                        else
                        {
                            path = "~/upload/Signature/" + regn + ".jpeg";
                        }
                    }

                    string filePath = Server.MapPath(path);
                    string entryName = subFolder + "/" + fileName;

                    if (File.Exists(filePath) && !zip.ContainsEntry(entryName))
                    {
                        zip.AddFile(filePath, subFolder).FileName = entryName;
                        count++;
                    }
                }

                if (count == 0)
                {
                    Label1.Text = "No Photo/Signature File Found to Download.";
                    return;
                }

                Label1.Text = "Total Photo/Signature " + count + " File(s) Downloaded.";

                string zipName = string.Format("{0}.zip",
                    ddlimage.SelectedItem.Text + "_" + DateTime.Now.ToString("yyyy-MMM-dd-HHmm"));

                using (MemoryStream ms = new MemoryStream())
                {
                    zip.Save(ms);
                    ms.Position = 0;

                    Response.Clear();
                    Response.Buffer = true;
                    Response.ContentType = "application/zip";
                    Response.AddHeader("content-disposition", "attachment; filename=" + zipName);
                    Response.BinaryWrite(ms.ToArray());
                    Response.Flush();
                    Response.End();
                }
            }
        }
    }
}