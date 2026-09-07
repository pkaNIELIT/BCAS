using BCASClassLibrary;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class Reports_repNominatedCandidates : System.Web.UI.Page
{
    TYP_BCAS_Approval objBCAS = new TYP_BCAS_Approval();
    Table tbl = new Table();
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
                using (DataTable dtOrg = objBCAS.getGetAllOrganisation())
                {
                    if (dtOrg.Rows.Count > 0)
                    {
                        ddlOrganisation.DataSource = dtOrg;
                        ddlOrganisation.DataBind();
                        if (Session["OrgName"] != null)
                        {
                            string s = Session["OrgName"].ToString();
                            ddlOrganisation.SelectedItem.Text = Session["OrgName"].ToString();
                            lblOrg.Text = TYP_SPONSORING_ORGANISATION_MASTER.GetSponsoring_OrganisationCode(Session["OrgName"].ToString()).ToString();
                            ddlOrganisation.Enabled = false;
                        }
                        else
                            ddlOrganisation.Enabled = true;
                    }
                }

                //DisplayReports();
                if (Convert.ToString(HttpContext.Current.Session["OrgName"]).Trim().Length == 0)
                {
                    lblHeading.Visible = false;
                }
                else 
                {
                    lblHeading.Visible = true;
                    lblHeading.Text = "Nominated Candidates of :  " + Session["OrgName"].ToString();
                    //if (ddlOrganisation.Items.FindByText(Session["OrgName"].ToString()) != null)
                    //{
                    //    ddlOrganisation.Items.FindByText(Session["OrgName"].ToString()).Selected = true;
                    //    ddlOrganisation.Enabled = false;
                    //}
                    //else
                    //{
                    //    using (DataTable dtOrg = objBCAS.getGetAllOrganisation())
                    //    {
                    //        if (dtOrg.Rows.Count > 0)
                    //        {
                    //            ddlOrganisation.DataSource = dtOrg;
                    //            ddlOrganisation.DataBind();
                    //        }

                    //    }
                    //}
                }
                

            }

        }

    }
    
    protected void btnDisplay_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            imgExcel.Visible = true;
            imgpdf.Visible = true;
            ShowTableHeader();
            ShowTableData();
            lblHeading.Visible = true;
            lblHeading.Text = "Nominated Candidates of :  " + ddlOrganisation.SelectedItem.Text;
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
            tcCol141.Text = "<b> Report of candidates Generated on:</b> " + DateTime.Now.ToString("dd-MMM-yyyy");
            tcCol141.ColumnSpan = 10;
            th4.Cells.Add(tcCol141);

            tbl.Rows.Add(th4);


            TableHeaderRow th6 = new TableHeaderRow();
            th6.CssClass = "head1";
            TableHeaderCell tcCol161 = new TableHeaderCell();
            tcCol161.Width = Unit.Percentage(19);
            // tcCol141.Height = Unit.Pixel(14);
            tcCol161.HorizontalAlign = HorizontalAlign.Center;
            tcCol161.BorderWidth = 1;
            tcCol161.BorderColor = System.Drawing.Color.Chocolate;
            tcCol161.Text = "<b> Nominated Candidates of :</b> " + ddlOrganisation.SelectedItem.Text;
            tcCol161.ColumnSpan = 10;
            th6.Cells.Add(tcCol161);

            tbl.Rows.Add(th6);


            TableHeaderRow th7 = new TableHeaderRow();
            th7.CssClass = "head1";
            TableHeaderCell tcCol171 = new TableHeaderCell();
            tcCol171.Width = Unit.Percentage(19);
            tcCol171.HorizontalAlign = HorizontalAlign.Center;
            tcCol171.BorderWidth = 1;
            tcCol171.BorderColor = System.Drawing.Color.Chocolate;
            tcCol171.Text = "<b> Course Name :</b> " + ddlSchedule.SelectedItem.Text;
            tcCol171.ColumnSpan = 10;
            th7.Cells.Add(tcCol171);

            tbl.Rows.Add(th7);


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

            //TableHeaderCell tc2 = new TableHeaderCell();
            //tc2.Width = Unit.Percentage(100);

            TableHeaderCell tcCol11 = new TableHeaderCell();
            tcCol11.Width = Unit.Percentage(1);
            // tcCol11.RowSpan = 2;
            tcCol11.HorizontalAlign = HorizontalAlign.Center;
            tcCol11.Text = "S. No.";
            tcCol11.BorderWidth = 1;
            tcCol11.BorderColor = System.Drawing.Color.White;
            th2.Cells.Add(tcCol11);

            TableHeaderCell tcCol12 = new TableHeaderCell();
            tcCol12.Width = Unit.Percentage(15);
            //tcCol12.RowSpan = 2;
            tcCol12.HorizontalAlign = HorizontalAlign.Center;
            tcCol12.Text = "Registration Number";
            tcCol12.BorderWidth = 1;
            tcCol12.BorderColor = System.Drawing.Color.White;
            th2.Cells.Add(tcCol12);

            TableHeaderCell tcCol13 = new TableHeaderCell();
            //tcCol13.ColumnSpan = 2;
            tcCol13.Width = Unit.Percentage(15);
            tcCol13.HorizontalAlign = HorizontalAlign.Center;
            tcCol13.Text = "Candidate Name";
            tcCol13.BorderWidth = 1;
            tcCol13.BorderColor = System.Drawing.Color.White;
            th2.Cells.Add(tcCol13);

            TableHeaderCell tcCol14 = new TableHeaderCell();
            // tcCol14.ColumnSpan = 2;
            tcCol14.Width = Unit.Percentage(15);
            tcCol14.HorizontalAlign = HorizontalAlign.Center;
            tcCol14.Text = "Designation";
            tcCol14.BorderWidth = 1;
            tcCol14.BorderColor = System.Drawing.Color.White;
            th2.Cells.Add(tcCol14);

            TableHeaderCell tcCol15a = new TableHeaderCell();
            // tcCol15.ColumnSpan = 2;
            tcCol15a.Width = Unit.Percentage(15);
            tcCol15a.HorizontalAlign = HorizontalAlign.Center;
            tcCol15a.Text = "Organisation Name";
            tcCol15a.BorderWidth = 1;
            tcCol15a.BorderColor = System.Drawing.Color.White;
            th2.Cells.Add(tcCol15a);

            TableHeaderCell tcCol15 = new TableHeaderCell();
            // tcCol15.ColumnSpan = 2;
            tcCol15.Width = Unit.Percentage(15);
            tcCol15.HorizontalAlign = HorizontalAlign.Center;
            tcCol15.Text = "DOB";
            tcCol15.BorderWidth = 1;
            tcCol15.BorderColor = System.Drawing.Color.White;
            th2.Cells.Add(tcCol15);

            TableHeaderCell tcCol16 = new TableHeaderCell();
            //tcCol16.ColumnSpan = 2;
            tcCol16.Width = Unit.Percentage(15);
            tcCol16.HorizontalAlign = HorizontalAlign.Center;
            tcCol16.Text = "Mobile No";
            tcCol16.BorderWidth = 1;
            tcCol16.BorderColor = System.Drawing.Color.White;
            th2.Cells.Add(tcCol16);

            TableHeaderCell tcCol17 = new TableHeaderCell();
            
            tcCol17.Width = Unit.Percentage(15);
            tcCol17.HorizontalAlign = HorizontalAlign.Center;
            tcCol17.Text = "Email Id";
            tcCol17.BorderWidth = 1;
            tcCol17.BorderColor = System.Drawing.Color.White;
            th2.Cells.Add(tcCol17);

            TableHeaderCell tcCol18a = new TableHeaderCell();
            
            tcCol18a.Width = Unit.Percentage(15);
            tcCol18a.HorizontalAlign = HorizontalAlign.Center;
            tcCol18a.Text = "Payee Organisation";
            tcCol18a.BorderWidth = 1;
           // tcCol18a.ColumnSpan = 2;
            tcCol18a.BorderColor = System.Drawing.Color.White;
            th2.Cells.Add(tcCol18a);

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


            TableHeaderRow th2 = new TableHeaderRow();
            th2.CssClass = "head1";

            //TableHeaderCell tc2 = new TableHeaderCell();
            //tc2.Width = Unit.Percentage(100);

            TableHeaderCell tcCol11 = new TableHeaderCell();
            tcCol11.Width = Unit.Percentage(1);
            // tcCol11.RowSpan = 2;
            tcCol11.HorizontalAlign = HorizontalAlign.Center;
            tcCol11.Text = "S. No.";
            tcCol11.BorderWidth = 1;
            tcCol11.BorderColor = System.Drawing.Color.White;
            th2.Cells.Add(tcCol11);

            TableHeaderCell tcCol12 = new TableHeaderCell();
            tcCol12.Width = Unit.Percentage(15);
            //tcCol12.RowSpan = 2;
            tcCol12.HorizontalAlign = HorizontalAlign.Center;
            tcCol12.Text = "Registration Number";
            tcCol12.BorderWidth = 1;
            tcCol12.BorderColor = System.Drawing.Color.White;
            th2.Cells.Add(tcCol12);

            TableHeaderCell tcCol13 = new TableHeaderCell();
            //tcCol13.ColumnSpan = 2;
            tcCol13.Width = Unit.Percentage(15);
            tcCol13.HorizontalAlign = HorizontalAlign.Center;
            tcCol13.Text = "Candidate Name";
            tcCol13.BorderWidth = 1;
            tcCol13.BorderColor = System.Drawing.Color.White;
            th2.Cells.Add(tcCol13);

            TableHeaderCell tcCol14 = new TableHeaderCell();
            // tcCol14.ColumnSpan = 2;
            tcCol14.Width = Unit.Percentage(15);
            tcCol14.HorizontalAlign = HorizontalAlign.Center;
            tcCol14.Text = "Designation";
            tcCol14.BorderWidth = 1;
            tcCol14.BorderColor = System.Drawing.Color.White;
            th2.Cells.Add(tcCol14);

            TableHeaderCell tcCol15a = new TableHeaderCell();
            // tcCol15.ColumnSpan = 2;
            tcCol15a.Width = Unit.Percentage(15);
            tcCol15a.HorizontalAlign = HorizontalAlign.Center;
            tcCol15a.Text = "Organisation Name";
            tcCol15a.BorderWidth = 1;
            tcCol15a.BorderColor = System.Drawing.Color.White;
            th2.Cells.Add(tcCol15a);

            TableHeaderCell tcCol15 = new TableHeaderCell();
            // tcCol15.ColumnSpan = 2;
            tcCol15.Width = Unit.Percentage(15);
            tcCol15.HorizontalAlign = HorizontalAlign.Center;
            tcCol15.Text = "DOB";
            tcCol15.BorderWidth = 1;
            tcCol15.BorderColor = System.Drawing.Color.White;
            th2.Cells.Add(tcCol15);

            TableHeaderCell tcCol16 = new TableHeaderCell();
            //tcCol16.ColumnSpan = 2;
            tcCol16.Width = Unit.Percentage(15);
            tcCol16.HorizontalAlign = HorizontalAlign.Center;
            tcCol16.Text = "Mobile No";
            tcCol16.BorderWidth = 1;
            tcCol16.BorderColor = System.Drawing.Color.White;
            th2.Cells.Add(tcCol16);

            TableHeaderCell tcCol17 = new TableHeaderCell();
           // tcCol17.ColumnSpan = 2;
            tcCol17.Width = Unit.Percentage(15);
            tcCol17.HorizontalAlign = HorizontalAlign.Center;
            tcCol17.Text = "Email Id";
            tcCol17.BorderWidth = 1;
            tcCol17.BorderColor = System.Drawing.Color.White;
            th2.Cells.Add(tcCol17);

            TableHeaderCell tcCol18a = new TableHeaderCell();
            //tcCol18a.ColumnSpan = 2;
            tcCol18a.Width = Unit.Percentage(15);
            tcCol18a.HorizontalAlign = HorizontalAlign.Center;
            tcCol18a.Text = "Payee Organisation Name";
            tcCol18a.BorderWidth = 1;
            tcCol18a.BorderColor = System.Drawing.Color.White;
            th2.Cells.Add(tcCol18a);

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

            SqlCommand scCommand = new SqlCommand("repNominatedCandidates", new SqlConnection(con.ConnectionString));
            scCommand.CommandType = CommandType.StoredProcedure;

            if (lblOrg.Text != "0")
                scCommand.Parameters.Add("@Org", SqlDbType.BigInt).Value = Convert.ToInt32(lblOrg.Text);
            else
            scCommand.Parameters.Add("@Org", SqlDbType.BigInt).Value = Convert.ToInt32(ddlOrganisation.SelectedValue);
            //scCommand.Parameters.Add("@Org", SqlDbType.BigInt).Value = Convert.ToInt32(ddlOrganisation.SelectedValue);


            scCommand.Parameters.Add("@Schedule", SqlDbType.BigInt).Value = Convert.ToInt32(ddlSchedule.SelectedValue);

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

                TableCell tdRow1 = new TableCell();
                tdRow1.Width = Unit.Percentage(17);
                tdRow1.Text = ds.Tables[0].Rows[j]["Registration_Number"].ToString();
                tdRow1.HorizontalAlign = HorizontalAlign.Center;
                tdRow1.BorderWidth = 1;
                tdRow1.BorderColor = System.Drawing.Color.White;
                tr.Cells.Add(tdRow1);

                TableCell tdRow2 = new TableCell();
                tdRow2.Width = Unit.Percentage(8);
                tdRow2.Text = ds.Tables[0].Rows[j]["Candidate_Name"].ToString();
                tdRow2.HorizontalAlign = HorizontalAlign.Left;
                tdRow2.BorderWidth = 1;
                tdRow2.BorderColor = System.Drawing.Color.White;
                tr.Cells.Add(tdRow2);

                TableCell tdRow3 = new TableCell();
                tdRow3.Width = Unit.Percentage(8);
                tdRow3.Text = ds.Tables[0].Rows[j]["Designation"].ToString();
                tdRow3.HorizontalAlign = HorizontalAlign.Left;
                tdRow3.BorderWidth = 1;
                tdRow3.BorderColor = System.Drawing.Color.White;
                tr.Cells.Add(tdRow3);


                TableCell tdRow15 = new TableCell();
                tdRow15.Width = Unit.Percentage(8);
                tdRow15.Text = ds.Tables[0].Rows[j]["Organisation_Name"].ToString();
                tdRow15.HorizontalAlign = HorizontalAlign.Left;
                tdRow15.BorderWidth = 1;
                tdRow15.BorderColor = System.Drawing.Color.White;
                tr.Cells.Add(tdRow15);

                TableCell tdRow14 = new TableCell();
                tdRow14.Width = Unit.Percentage(8);
                tdRow14.Text = ds.Tables[0].Rows[j]["DOB"].ToString();
                tdRow14.HorizontalAlign = HorizontalAlign.Center;
                tdRow14.BorderWidth = 1;
                tdRow14.BorderColor = System.Drawing.Color.White;
                tr.Cells.Add(tdRow14);

                TableCell tdRow16 = new TableCell();
                tdRow16.Width = Unit.Percentage(8);
                tdRow16.Text = ds.Tables[0].Rows[j]["Mobile_No"].ToString();
                tdRow16.HorizontalAlign = HorizontalAlign.Center;
                tdRow16.BorderWidth = 1;
                tdRow16.BorderColor = System.Drawing.Color.White;
                tr.Cells.Add(tdRow16);

                TableCell tdRow4 = new TableCell();
                tdRow4.Width = Unit.Percentage(8);
                tdRow4.Text = ds.Tables[0].Rows[j]["Email_Id"].ToString();
                tdRow4.HorizontalAlign = HorizontalAlign.Left;
                tdRow4.BorderWidth = 1;
                tdRow4.BorderColor = System.Drawing.Color.White;
                tr.Cells.Add(tdRow4);

               TableCell tdRow21 = new TableCell();
               tdRow21.Width = Unit.Percentage(8);
               tdRow21.Text = ds.Tables[0].Rows[j]["payee_organisation_name"].ToString();
               tdRow21.HorizontalAlign = HorizontalAlign.Left;
               tdRow21.BorderWidth = 1;
               tdRow21.BorderColor = System.Drawing.Color.White;
               tr.Cells.Add(tdRow21);

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

            SqlCommand scCommand = new SqlCommand("repNominatedCandidates", new SqlConnection(con.ConnectionString));
            scCommand.CommandType = CommandType.StoredProcedure;


            scCommand.Parameters.Add("@Org", SqlDbType.BigInt).Value = Convert.ToInt32(ddlOrganisation.SelectedValue);

            scCommand.Parameters.Add("@Schedule", SqlDbType.BigInt).Value = Convert.ToInt32(ddlSchedule.SelectedValue);

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

                TableCell tdRow1 = new TableCell();
                tdRow1.Width = Unit.Percentage(17);
                tdRow1.Text = ds.Tables[0].Rows[j]["Registration_Number"].ToString();
                tdRow1.HorizontalAlign = HorizontalAlign.Left;
                tdRow1.BorderWidth = 1;
                tdRow1.BorderColor = System.Drawing.Color.White;
                tr.Cells.Add(tdRow1);

                TableCell tdRow2 = new TableCell();
                tdRow2.Width = Unit.Percentage(8);
                tdRow2.Text = ds.Tables[0].Rows[j]["Candidate_Name"].ToString();
                tdRow2.HorizontalAlign = HorizontalAlign.Right;
                tdRow2.BorderWidth = 1;
                tdRow2.BorderColor = System.Drawing.Color.White;
                tr.Cells.Add(tdRow2);

                TableCell tdRow3 = new TableCell();
                tdRow3.Width = Unit.Percentage(8);
                tdRow3.Text = ds.Tables[0].Rows[j]["Designation"].ToString();
                tdRow3.HorizontalAlign = HorizontalAlign.Right;
                tdRow3.BorderWidth = 1;
                tdRow3.BorderColor = System.Drawing.Color.White;
                tr.Cells.Add(tdRow3);


                TableCell tdRow15 = new TableCell();
                tdRow15.Width = Unit.Percentage(8);
                tdRow15.Text = ds.Tables[0].Rows[j]["Organisation_Name"].ToString();
                tdRow15.HorizontalAlign = HorizontalAlign.Right;
                tdRow15.BorderWidth = 1;
                tdRow15.BorderColor = System.Drawing.Color.White;
                tr.Cells.Add(tdRow15);

                TableCell tdRow14 = new TableCell();
                tdRow14.Width = Unit.Percentage(8);
                tdRow14.Text = ds.Tables[0].Rows[j]["DOB"].ToString();
                tdRow14.HorizontalAlign = HorizontalAlign.Right;
                tdRow14.BorderWidth = 1;
                tdRow14.BorderColor = System.Drawing.Color.White;
                tr.Cells.Add(tdRow14);

                TableCell tdRow16 = new TableCell();
                tdRow16.Width = Unit.Percentage(8);
                tdRow16.Text = ds.Tables[0].Rows[j]["Mobile_No"].ToString();
                tdRow16.HorizontalAlign = HorizontalAlign.Right;
                tdRow16.BorderWidth = 1;
                tdRow16.BorderColor = System.Drawing.Color.White;
                tr.Cells.Add(tdRow16);

                TableCell tdRow4 = new TableCell();
                tdRow4.Width = Unit.Percentage(8);
                
                tdRow4.Text = ds.Tables[0].Rows[j]["Email_Id"].ToString();
                tdRow4.HorizontalAlign = HorizontalAlign.Right;
                tdRow4.BorderWidth = 1;
                tdRow4.BorderColor = System.Drawing.Color.White;
                tr.Cells.Add(tdRow4);

                TableCell tdRow21 = new TableCell();
                tdRow21.Width = Unit.Percentage(8);
                tdRow21.Text = ds.Tables[0].Rows[j]["payee_organisation_name"].ToString();
                tdRow21.HorizontalAlign = HorizontalAlign.Right;
                tdRow21.BorderWidth = 1;
                tdRow21.ColumnSpan = 2;
                tdRow21.BorderColor = System.Drawing.Color.White;
                tr.Cells.Add(tdRow21);

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

            tbl.Width = Unit.Percentage(400);

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
            Response.AddHeader("content-disposition", "attachment;filename=NominatedCandidates.xls");
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
