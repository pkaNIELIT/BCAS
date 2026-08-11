using System;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using BCASClassLibrary;

public partial class Reports_repNominationAccepted : System.Web.UI.Page
{
    Table tbl = new Table();
    TYP_BCAS_Approval objBCAS = new TYP_BCAS_Approval();
    protected void Page_Load(object sender, EventArgs e)
    {
        //ReportViewer1.Visible = false;

        if (!IsPostBack)
        {

            if ((Convert.ToString(HttpContext.Current.Session["UserId"]).Trim().Length == 0))
            {
                HttpContext.Current.Response.Redirect("~/Home.aspx");
            }
            else
            {
                //DisplayReports();
                //lblHeading.Text = "Nomination Accepted by BCAS of :  " + Session["OrgName"].ToString();

                using (DataTable dtOrg = objBCAS.getGetAllOrganisation())
                {
                    if (dtOrg.Rows.Count > 0)
                    {
                         ddlOrganisation.DataSource = dtOrg;
                        ddlOrganisation.DataBind();
                    }

                }

                lblHeading.Text = "Nomination Accepted by BCAS";

            }

        }

    }

    protected void btnDisplay_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            DisplayReports();

            //if (Convert.ToInt32(ddlOrganisation.SelectedValue) != 999999)
            //{
            //    OrgWiseNominationAccepted();
            //}
            //else if (Convert.ToInt32(ddlOrganisation.SelectedValue) == 999999)
            //{
            //    TotalNominations();
            //}
           
        }

    }
     
    protected void imgPDF_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            hw.AddAttribute("border", "1");
            hw.RenderBeginTag(HtmlTextWriterTag.Font);
            hw.AddStyleAttribute(HtmlTextWriterStyle.FontSize, "10");

            divReportData.Visible = true;
            tbl.BorderStyle = BorderStyle.Solid;
            //tbl.CssClass = "sample3";
            tbl.CssClass = "head1";

            tbl.Width = Unit.Percentage(500);

            ShowTableDataPDF();
            divReportData.Controls.Add(tbl);


            divReportData.RenderControl(hw);
            hw.RenderEndTag();
            Response.Clear();
            StringReader sr = new StringReader(sw.ToString());
            //Document pdfDoc = new Document(PageSize.A4.Rotate(), 5f, 5f, 5f, 0f);
            Document pdfDoc = new Document(PageSize.A4.Rotate());

            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            pdfDoc.Open();
            htmlparser.Parse(sr);
            pdfDoc.Close();
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=NominatedCandidates.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);

            Response.Write(pdfDoc);
            Response.End();

        }
        catch (Exception ex)
        {
            //ShowAlert(ex.Message);
        }
    }
    protected void imgExcel_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            System.IO.StringWriter StringWrite = new System.IO.StringWriter();
            Html32TextWriter htmlWrite;
            divReportData.Visible = true;
            ShowTableDataPDF();
            //divReportData.Controls.Add(tbl);
            SanitizeControls(divReportData);
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename=RegisteredCandidates.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.xls";
            htmlWrite = new Html32TextWriter(StringWrite);
            divReportData.RenderControl(htmlWrite);
            
            Response.Write(StringWrite.ToString());
            Response.End();



            //StringWriter sw = new StringWriter();
            //HtmlTextWriter hw = new HtmlTextWriter(sw);
            //hw.AddAttribute("border", "1");
            //hw.RenderBeginTag(HtmlTextWriterTag.Font);
            //hw.AddStyleAttribute(HtmlTextWriterStyle.FontSize, "10");

            //divReportData.Visible = true;
            //tbl.BorderStyle = BorderStyle.Solid;
            ////tbl.CssClass = "sample3";
            //tbl.CssClass = "head1";

            //tbl.Width = Unit.Percentage(400);

            //ShowTableDataPDF();
            //divReportData.Controls.Add(tbl);

            //divReportData.RenderControl(hw);
            //hw.RenderEndTag();
            //Response.Clear();
            //StringReader sr = new StringReader(sw.ToString());
            //Document pdfDoc = new Document(PageSize.A4.Rotate(), 5f, 5f, 5f, 0f);

            //HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            //PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            //pdfDoc.Open();
            //htmlparser.Parse(sr);
            //pdfDoc.Close();
            //Response.ContentType = "application/vnd-xls";
            //Response.AddHeader("content-disposition", "attachment;filename=CandidateDetails.xls");
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);

            //Response.Write(pdfDoc);
            //Response.End();

        }
        catch (Exception ex)
        {
            //ShowAlert(ex.Message);
        }
    }
    protected void ShowTableHeaderPDF()
    {
        try
        {


            TableHeaderRow th5 = new TableHeaderRow();
            th5.CssClass = "head1";
            TableHeaderCell tcCol151 = new TableHeaderCell();
            tcCol151.Width = Unit.Percentage(19);
            tcCol151.Height = Unit.Pixel(24);
            // tcCol151.BackColor = System.Drawing.Color.Blue;
            tcCol151.HorizontalAlign = HorizontalAlign.Center;
            tcCol151.BorderWidth = 1;
            tcCol151.BorderColor = System.Drawing.Color.Chocolate;
            tcCol151.Text = "<b><b><b>Bureau of Civil Aviation Security</b></b> </b>";
            tcCol151.ColumnSpan = 10;
            th5.Cells.Add(tcCol151);

            tbl.Rows.Add(th5);

            TableHeaderRow th4 = new TableHeaderRow();
            th4.CssClass = "head1";
            TableHeaderCell tcCol141 = new TableHeaderCell();
            tcCol141.Width = Unit.Percentage(19);
            // tcCol141.Height = Unit.Pixel(14);
            tcCol141.HorizontalAlign = HorizontalAlign.Center;
            tcCol141.BorderWidth = 1;
            tcCol141.BorderColor = System.Drawing.Color.Chocolate;
            tcCol141.Text = "<b> Nominations for  " +  ddlSchedule.SelectedItem.Text.ToString() + ", generated on:</b> " + DateTime.Now.ToString("dd-MMM-yyyy");
            tcCol141.ColumnSpan = 10;
            th4.Cells.Add(tcCol141);

            tbl.Rows.Add(th4);

            TableHeaderRow th3 = new TableHeaderRow();
            th3.CssClass = "head1";
            TableHeaderCell tcCol131 = new TableHeaderCell();
            tcCol131.Width = Unit.Percentage(19);
            tcCol131.Height = Unit.Pixel(34);
            tcCol131.BorderWidth = 1;
            tcCol131.BorderColor = System.Drawing.Color.Chocolate;
            tcCol131.Text = "&nbsp;";
            tcCol131.ColumnSpan = 10;
            th3.Cells.Add(tcCol131);

            tbl.Rows.Add(th3);

            TableHeaderRow th2 = new TableHeaderRow();
            th2.CssClass = "head1";

            TableHeaderCell tcCol11 = new TableHeaderCell();
            tcCol11.Width = Unit.Percentage(1);
            // tcCol11.RowSpan = 2;
            tcCol11.HorizontalAlign = HorizontalAlign.Center;
            tcCol11.Text = "S. No.";
            tcCol11.BorderWidth = 1;
            tcCol11.BorderColor = System.Drawing.Color.White;
            th2.Cells.Add(tcCol11);

            TableHeaderCell tcCol15a = new TableHeaderCell();
            // tcCol15.ColumnSpan = 2;
            tcCol15a.Width = Unit.Percentage(15);
            tcCol15a.HorizontalAlign = HorizontalAlign.Center;
            tcCol15a.Text = "Organisation Name";
            tcCol15a.BorderWidth = 1;
            tcCol15a.BorderColor = System.Drawing.Color.White;
            tcCol15a.ColumnSpan = 2;
            th2.Cells.Add(tcCol15a);

            TableHeaderCell tcCol14 = new TableHeaderCell();
            // tcCol14.ColumnSpan = 2;
            tcCol14.Width = Unit.Percentage(15);
            tcCol14.HorizontalAlign = HorizontalAlign.Center;
            tcCol14.Text = "Regd No.";
            tcCol14.BorderWidth = 1;
            tcCol14.BorderColor = System.Drawing.Color.White;
            th2.Cells.Add(tcCol14);

            

            TableHeaderCell tcCol15 = new TableHeaderCell();
            // tcCol15.ColumnSpan = 2;
            tcCol15.Width = Unit.Percentage(15);
            tcCol15.HorizontalAlign = HorizontalAlign.Center;
            tcCol15.Text = "Candidate Name";
            tcCol15.BorderWidth = 1;
            tcCol15.BorderColor = System.Drawing.Color.White;
            th2.Cells.Add(tcCol15);

            TableHeaderCell tcCol17 = new TableHeaderCell();
            //tcCol16.ColumnSpan = 2;
            tcCol17.Width = Unit.Percentage(15);
            tcCol17.HorizontalAlign = HorizontalAlign.Center;
            tcCol17.Text = "Designation";
            tcCol17.BorderWidth = 1;
            tcCol17.BorderColor = System.Drawing.Color.White;
            th2.Cells.Add(tcCol17);

            TableHeaderCell tcCol18a = new TableHeaderCell();
            //tcCol16.ColumnSpan = 2;
            tcCol18a.Width = Unit.Percentage(15);
            tcCol18a.HorizontalAlign = HorizontalAlign.Center;
            tcCol18a.Text = "DOB";
            tcCol18a.BorderWidth = 1;
            tcCol18a.BorderColor = System.Drawing.Color.White;
            th2.Cells.Add(tcCol18a);

            TableHeaderCell tcCol18 = new TableHeaderCell();
            //tcCol16.ColumnSpan = 2;
            tcCol18.Width = Unit.Percentage(15);
            tcCol18.HorizontalAlign = HorizontalAlign.Center;
            tcCol18.Text = "Mobile";
            tcCol18.BorderWidth = 1;
            tcCol18.BorderColor = System.Drawing.Color.White;
            th2.Cells.Add(tcCol18);

            TableHeaderCell tcCol3 = new TableHeaderCell();
            tcCol3.Width = Unit.Percentage(8);
            // tcCol3.ColumnSpan = 3;
            tcCol3.HorizontalAlign = HorizontalAlign.Center;
            tcCol3.Text = "Email ";
            tcCol3.BorderWidth = 1;
            tcCol3.ColumnSpan = 2;
            tcCol3.BorderColor = System.Drawing.Color.White;
            th2.Cells.Add(tcCol3);

           
            tbl.Rows.Add(th2);
            divReportData.Controls.Add(tbl);


        }
        catch (Exception ex)
        {
            //ShowAlert(ex.Message);
        }

    }
    protected void ShowTableHeader()
    {
        try
        {


            //TableHeaderRow th2 = new TableHeaderRow();
            // th2.CssClass = "head1";

            /*  TableHeaderRow th3 = new TableHeaderRow();
              th3.CssClass = "head1";
              TableHeaderCell tcCol131 = new TableHeaderCell();
              tcCol131.Width = Unit.Percentage(19);
              tcCol131.Height = Unit.Pixel(34);
              tcCol131.BorderWidth = 1;
              tcCol131.BorderColor = System.Drawing.Color.Chocolate;
              tcCol131.Text = "&nbsp;";
              tcCol131.ColumnSpan = 10;
              th3.Cells.Add(tcCol131);

              tbl.Rows.Add(th3);*/

            TableHeaderRow th2 = new TableHeaderRow();
            th2.CssClass = "head1";

           

            TableHeaderCell tcCol11 = new TableHeaderCell();
            tcCol11.Width = Unit.Percentage(1);
            // tcCol11.RowSpan = 2;
            tcCol11.HorizontalAlign = HorizontalAlign.Center;
            tcCol11.Text = "S. No.";
            tcCol11.BorderWidth = 1;
            tcCol11.BorderColor = System.Drawing.Color.White;
            th2.Cells.Add(tcCol11);

            TableHeaderCell tcCol15a = new TableHeaderCell();
            // tcCol15.ColumnSpan = 2;
            tcCol15a.Width = Unit.Percentage(15);
            tcCol15a.HorizontalAlign = HorizontalAlign.Center;
            tcCol15a.Text = "Organisation Name";
            tcCol15a.BorderWidth = 1;
            tcCol15a.BorderColor = System.Drawing.Color.White;
            th2.Cells.Add(tcCol15a);

            TableHeaderCell tcCol14 = new TableHeaderCell();
            // tcCol14.ColumnSpan = 2;
            tcCol14.Width = Unit.Percentage(15);
            tcCol14.HorizontalAlign = HorizontalAlign.Center;
            tcCol14.Text = "Regd No.";
            tcCol14.BorderWidth = 1;
            tcCol14.BorderColor = System.Drawing.Color.White;
            th2.Cells.Add(tcCol14);



            TableHeaderCell tcCol15 = new TableHeaderCell();
            // tcCol15.ColumnSpan = 2;
            tcCol15.Width = Unit.Percentage(15);
            tcCol15.HorizontalAlign = HorizontalAlign.Center;
            tcCol15.Text = "Candidate Name";
            tcCol15.BorderWidth = 1;
            tcCol15.BorderColor = System.Drawing.Color.White;
            th2.Cells.Add(tcCol15);

            TableHeaderCell tcCol17 = new TableHeaderCell();
            //tcCol16.ColumnSpan = 2;
            tcCol17.Width = Unit.Percentage(15);
            tcCol17.HorizontalAlign = HorizontalAlign.Center;
            tcCol17.Text = "Designation";
            tcCol17.BorderWidth = 1;
            tcCol17.BorderColor = System.Drawing.Color.White;
            th2.Cells.Add(tcCol17);

            TableHeaderCell tcCol18a = new TableHeaderCell();
            //tcCol16.ColumnSpan = 2;
            tcCol18a.Width = Unit.Percentage(15);
            tcCol18a.HorizontalAlign = HorizontalAlign.Center;
            tcCol18a.Text = "DOB";
            tcCol18a.BorderWidth = 1;
            tcCol18a.BorderColor = System.Drawing.Color.White;
            th2.Cells.Add(tcCol18a);

            TableHeaderCell tcCol18 = new TableHeaderCell();
            //tcCol16.ColumnSpan = 2;
            tcCol18.Width = Unit.Percentage(15);
            tcCol18.HorizontalAlign = HorizontalAlign.Center;
            tcCol18.Text = "Mobile";
            tcCol18.BorderWidth = 1;
            tcCol18.BorderColor = System.Drawing.Color.White;
            th2.Cells.Add(tcCol18);

            TableHeaderCell tcCol3 = new TableHeaderCell();
            tcCol3.Width = Unit.Percentage(8);
            // tcCol3.ColumnSpan = 3;
            tcCol3.HorizontalAlign = HorizontalAlign.Center;
            tcCol3.Text = "Email ";
            tcCol3.BorderWidth = 1;
            tcCol3.BorderColor = System.Drawing.Color.White;
            th2.Cells.Add(tcCol3);

            tbl.Rows.Add(th2);
            divReportData.Controls.Add(tbl);
        }
        catch (Exception ex)
        {
            // ShowAlert(ex.Message);
        }

    }
    protected void ShowTableData()
    {
        try
        {
            // ShowTableHeader();

            int i = 0, j = 0;
            Int64 totUnderTrained = 0, totCertified = 0;
            string constr = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(constr);

            SqlCommand scCommand = new SqlCommand("repNominationAccepted", new SqlConnection(con.ConnectionString));

            scCommand.CommandType = CommandType.StoredProcedure;

            scCommand.Parameters.Add("@Schedule", SqlDbType.BigInt).Value = Convert.ToInt32(ddlSchedule.SelectedValue.ToString());    
            scCommand.Parameters.Add("@Org", SqlDbType.BigInt).Value = Convert.ToInt32(ddlOrganisation.SelectedValue.ToString());

            scCommand.CommandTimeout = 50000;

            if (scCommand.Connection.State == ConnectionState.Closed)
            {
                scCommand.Connection.Open();
            }

            SqlDataAdapter da = new SqlDataAdapter(scCommand);
            DataSet ds = new DataSet();

            da.Fill(ds);

            for (j = 0; j < ds.Tables[0].Rows.Count; j++)
            {
                TableRow tr = new TableRow();
                if (i % 2 == 0)
                    tr.CssClass = "gdalternate1";
                else
                    tr.CssClass = "gdrow1";
                i++;
                TableCell tdRow = new TableCell();
                tdRow.Width = Unit.Percentage(1);
                tdRow.Text = (j + 1).ToString();
                tdRow.HorizontalAlign = HorizontalAlign.Center;
                tdRow.BorderWidth = 1;
                tdRow.BorderColor = System.Drawing.Color.White;
                tr.Cells.Add(tdRow);

                TableCell tdRow15 = new TableCell();
                tdRow15.Width = Unit.Percentage(10);
                tdRow15.Text = ds.Tables[0].Rows[j]["Organisation_Name"].ToString();
                tdRow15.HorizontalAlign = HorizontalAlign.Center;
                tdRow15.BorderWidth = 1;
                tdRow15.BorderColor = System.Drawing.Color.White;
                tr.Cells.Add(tdRow15);

                TableCell tdRow3 = new TableCell();
                tdRow3.Width = Unit.Percentage(10);
                tdRow3.Text = ds.Tables[0].Rows[j]["Registration_Number"].ToString();
                tdRow3.HorizontalAlign = HorizontalAlign.Right;
                tdRow3.BorderWidth = 1;
                tdRow3.BorderColor = System.Drawing.Color.White;
                tr.Cells.Add(tdRow3);

                TableCell tdRow14 = new TableCell();
                tdRow14.Width = Unit.Percentage(15);
                tdRow14.Text = ds.Tables[0].Rows[j]["Candidate_Name"].ToString();
                tdRow14.HorizontalAlign = HorizontalAlign.Center;
                tdRow14.BorderWidth = 1;
                tdRow14.BorderColor = System.Drawing.Color.White;
                tr.Cells.Add(tdRow14);

                TableCell tdRow4 = new TableCell();
                tdRow4.Width = Unit.Percentage(12);
                tdRow4.Text = ds.Tables[0].Rows[j]["Designation"].ToString();
                tdRow4.HorizontalAlign = HorizontalAlign.Center;
                tdRow4.BorderWidth = 1;
                tdRow4.BorderColor = System.Drawing.Color.White;
                tr.Cells.Add(tdRow4);

                TableCell tdRow21 = new TableCell();
                tdRow21.Width = Unit.Percentage(8);
                tdRow21.Text = ds.Tables[0].Rows[j]["DOB"].ToString();
                tdRow21.HorizontalAlign = HorizontalAlign.Center;
                tdRow21.BorderWidth = 1;
                tdRow21.BorderColor = System.Drawing.Color.White;
                tr.Cells.Add(tdRow21);


                TableCell tdRow22 = new TableCell();
                tdRow22.Width = Unit.Percentage(8);
                tdRow22.Text = ds.Tables[0].Rows[j]["Mobile_No"].ToString();
                tdRow22.HorizontalAlign = HorizontalAlign.Right;
                tdRow22.BorderWidth = 1;
                tdRow22.BorderColor = System.Drawing.Color.White;
                tr.Cells.Add(tdRow22);

                TableCell tdRow23 = new TableCell();
                tdRow23.Width = Unit.Percentage(8);

                tdRow23.Text = ds.Tables[0].Rows[j]["Email_Id"].ToString();
                tdRow23.HorizontalAlign = HorizontalAlign.Center;
                tdRow23.BorderWidth = 1;
                tdRow23.BorderColor = System.Drawing.Color.White;
                tr.Cells.Add(tdRow23);

               
                tbl.Rows.Add(tr);
                divReportData.Controls.Add(tbl);
                // LblRptSubHeader.Text = strHead;
            }
           
            scCommand.Connection.Close();
        }
        catch (Exception ex)
        {
            //ShowAlert(ex.Message);
        }
    }
    protected void ShowTableDataPDF()
    {
        try
        {
            ShowTableHeaderPDF();

            //TableRow tdRow35 = new TableRow();
            //tbl.Rows.Add(tdRow35);
            //divReportData.Controls.Add(tbl);
            ShowTableDataPDFDisplay();

        }
        catch (Exception ex)
        {
            // ShowAlert(ex.Message);
        }
    }
    protected void ShowTableDataPDFDisplay()
    {
        try
        {
            // ShowTableHeader();

            int i = 0, j = 0;
            Int64 totUnderTrained = 0, totCertified = 0;
            string constr = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(constr);

            SqlCommand scCommand = new SqlCommand("repNominationAccepted", new SqlConnection(con.ConnectionString));

            scCommand.CommandType = CommandType.StoredProcedure;

            scCommand.Parameters.Add("@Schedule", SqlDbType.BigInt).Value = Convert.ToInt32(ddlSchedule.SelectedValue.ToString());    
                scCommand.Parameters.Add("@Org", SqlDbType.BigInt).Value = Convert.ToInt32(ddlOrganisation.SelectedValue.ToString());


            scCommand.CommandTimeout = 50000;

            if (scCommand.Connection.State == ConnectionState.Closed)
            {
                scCommand.Connection.Open();
            }

            SqlDataAdapter da = new SqlDataAdapter(scCommand);
            DataSet ds = new DataSet();

            da.Fill(ds);

            for (j = 0; j < ds.Tables[0].Rows.Count; j++)
            {
                TableRow tr = new TableRow();
                if (i % 2 == 0)
                    tr.CssClass = "gdalternate1";
                else
                    tr.CssClass = "gdrow1";
                i++;
                TableCell tdRow = new TableCell();
                tdRow.Width = Unit.Percentage(1);
                tdRow.Text = (j + 1).ToString();
                tdRow.HorizontalAlign = HorizontalAlign.Center;
                tdRow.BorderWidth = 1;
                tdRow.BorderColor = System.Drawing.Color.White;
                tr.Cells.Add(tdRow);


                TableCell tdRow15 = new TableCell();
                tdRow15.Width = Unit.Percentage(10);
                tdRow15.Text = ds.Tables[0].Rows[j]["Organisation_Name"].ToString();
                tdRow15.HorizontalAlign = HorizontalAlign.Center;
                tdRow15.BorderWidth = 1;
                tdRow15.ColumnSpan = 2;
                tdRow15.BorderColor = System.Drawing.Color.White;
                tr.Cells.Add(tdRow15);

                TableCell tdRow3 = new TableCell();
                tdRow3.Width = Unit.Percentage(10);
                tdRow3.Text = ds.Tables[0].Rows[j]["Registration_Number"].ToString();
                tdRow3.HorizontalAlign = HorizontalAlign.Right;
                tdRow3.BorderWidth = 1;
                tdRow3.BorderColor = System.Drawing.Color.White;
                tr.Cells.Add(tdRow3);

                TableCell tdRow14 = new TableCell();
                tdRow14.Width = Unit.Percentage(15);
                tdRow14.Text = ds.Tables[0].Rows[j]["Candidate_Name"].ToString();
                tdRow14.HorizontalAlign = HorizontalAlign.Center;
                tdRow14.BorderWidth = 1;
                tdRow14.BorderColor = System.Drawing.Color.White;
                tr.Cells.Add(tdRow14);

                TableCell tdRow4 = new TableCell();
                tdRow4.Width = Unit.Percentage(2);
                tdRow4.Text = ds.Tables[0].Rows[j]["Designation"].ToString();
                tdRow4.HorizontalAlign = HorizontalAlign.Center;
                tdRow4.BorderWidth = 1;
                tdRow4.BorderColor = System.Drawing.Color.White;
                tr.Cells.Add(tdRow4);

                TableCell tdRow21 = new TableCell();
                tdRow21.Width = Unit.Percentage(8);
                tdRow21.Text = ds.Tables[0].Rows[j]["DOB"].ToString();
                tdRow21.HorizontalAlign = HorizontalAlign.Right;
                tdRow21.BorderWidth = 1;
                tdRow21.BorderColor = System.Drawing.Color.White;
                tr.Cells.Add(tdRow21);


                TableCell tdRow22 = new TableCell();
                tdRow22.Width = Unit.Percentage(8);
                tdRow22.Text = ds.Tables[0].Rows[j]["Mobile_No"].ToString();
                tdRow22.HorizontalAlign = HorizontalAlign.Right;
                tdRow22.BorderWidth = 1;
                tdRow22.BorderColor = System.Drawing.Color.White;
                tr.Cells.Add(tdRow22);

                TableCell tdRow23 = new TableCell();
                tdRow23.Width = Unit.Percentage(8);
                tdRow23.Text = ds.Tables[0].Rows[j]["Email_Id"].ToString();
                  tdRow23.ColumnSpan = 2;
                tdRow23.HorizontalAlign = HorizontalAlign.Left;
                tdRow23.BorderWidth = 1;
                tdRow23.BorderColor = System.Drawing.Color.White;
                tr.Cells.Add(tdRow23);


                tbl.Rows.Add(tr);
                divReportData.Controls.Add(tbl);
                // LblRptSubHeader.Text = strHead;
            }

            scCommand.Connection.Close();
        }
        catch (Exception ex)
        {
            //ShowAlert(ex.Message);
        }


    }
    private void DisplayReports()
    {
        imgExcel.Visible = true;
        imgpdf.Visible = true;
        ShowTableHeader();
        ShowTableData();
    }
    // Helper method to recursively HTML-encode text-based controls
    private void SanitizeControls(Control parentControl)
    {
        foreach (Control c in parentControl.Controls)
        {
            // Recursively call for child controls
            SanitizeControls(c);

            // HTML-encode text content of common display controls
            // C# 5 compatible syntax (using 'as' operator and a null check)
            LiteralControl literal = c as LiteralControl;
            if (literal != null)
            {
                literal.Text = HttpUtility.HtmlEncode(literal.Text);
            }
            else
            {
                Label label = c as Label;
                if (label != null)
                {
                    label.Text = HttpUtility.HtmlEncode(label.Text);
                }
                else
                {
                    HyperLink hyperLink = c as HyperLink;
                    if (hyperLink != null)
                    {
                        hyperLink.Text = HttpUtility.HtmlEncode(hyperLink.Text);
                    }
                }
            }
        }
    }
}
