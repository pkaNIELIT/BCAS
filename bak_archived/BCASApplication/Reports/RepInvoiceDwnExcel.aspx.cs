using System;

using System.Configuration;
using System.Data;
using System.IO;
using System.Web;

using System.Web.UI;

using System.Web.UI.WebControls;

using BCASClassLibrary;
using System.Data.SqlClient;
using System.Text;

using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;

using System.Globalization;
using System.IO.Compression;
using Org.BouncyCastle.Asn1.Pkcs;
using System.Runtime.InteropServices.ComTypes;

public partial class Reports_RepInvoiceDwnExcel : System.Web.UI.Page
{
    TYP_BCAS_Approval objBCAS = new TYP_BCAS_Approval();
    Int64 DemandNoteId = 0;
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

                if (Session["OrgName"] != null)
                {
                    lblHeading.Visible = true;
                    //lblHeading.Text = " Successful Payment Of :  " + Session["OrgName"].ToString();
                    //string str = Session["UserId"].ToString();

                    string s = Session["OrgName"].ToString();
                    //ddlPayeeOrganisation.Items.FindByText(Session["OrgName"].ToString()).Selected = true;
                    ddlPayeeOrganisation.SelectedItem.Text = Session["OrgName"].ToString();
                    //lblOrg.Text = TYP_PAYEE_ORGANISATION_MASTER.GetPayee_OrganisationCode(Session["OrgName"].ToString()).ToString();
                    lblOrg.Text = TYP_PAYEE_ORGANISATION_MASTER.GetPayee_OrganisationCode(s.ToString()).ToString();
                    ddlPayeeOrganisation.Enabled = false;
                }
                else
                {
                    using (DataTable dtOrg = objBCAS.getPayeeOrganisation())
                    {
                        if (dtOrg.Rows.Count > 0)
                        {
                            ddlPayeeOrganisation.DataSource = dtOrg;
                            ddlPayeeOrganisation.DataBind();

                        }
                    }
                    ddlPayeeOrganisation.Enabled = true;
                }

            }

        }


    }

    private void ShowMessage(int count)
    {

        StringBuilder sb = new StringBuilder();

        sb.Append("<script type = 'text/javascript'>");

        sb.Append("alert('");

        sb.Append(count.ToString());

        sb.Append(" records Updated.');");

        sb.Append("</script>");

        ClientScript.RegisterStartupScript(this.GetType(),

                        "script", sb.ToString());

    }
    private void ShowAlertMessage(string msg)
    {

        StringBuilder sb = new StringBuilder();

        sb.Append("<script type = 'text/javascript'>");

        sb.Append("alert('");

        sb.Append(msg);

        sb.Append("</script>");

        ClientScript.RegisterStartupScript(this.GetType(),

                        "script", sb.ToString());

    }
    private void BindGrid()
    {

        int scheduleID, Organisation;

        //scheduleID = Convert.ToInt32(ddlSchedule.SelectedValue);

        Organisation = Convert.ToInt32(ddlPayeeOrganisation.SelectedValue);
    }

    protected void OnPaging(object sender, GridViewPageEventArgs e)
    {
        BindGrid();

    }

    //public int GetMonthDifference(DateTime startDate, DateTime endDate)
    //{

    //    int monthDiff = (endDate.Year - startDate.Year) * 12 + (endDate.Month - startDate.Month);
    //    int dateDiff = (endDate.Day - startDate.Day);

    //        if (monthDiff >= 1)
    //        {
    //        if (dateDiff >= 0)
    //        {
                
    //        }
    //        else
    //            return true;

    //    }
    //    else
    //        return true;

    //    int monthDifference = (endDate.Year - startDate.Year) * 12 + endDate.Month - startDate.Month;
    //    return monthDifference;
    //}

    protected void btnDisplay_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            DateTime maxDate = DateTime.Now;
            
            
            DateTime fromDate = DateTime.ParseExact(txt_from_date.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime toDate = DateTime.ParseExact(txt_to_date.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            int monthDiff = (toDate.Year - fromDate.Year) * 12 + (toDate.Month - fromDate.Month);
            int dateDiff = (toDate.Day - fromDate.Day);

            int result = DateTime.Compare(fromDate, toDate);

            if (toDate > maxDate)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "TestFunction", "alert('To Date Must Be before Current Date');", true);
            }
            else if (fromDate > maxDate)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "TestFunction", "alert('From Date Must Be before Current Date');", true);
            }
            else if (result > 0)
            {
                imgExcel.Visible = false;
                //imgpdf.Visible = false;
                divReportData.Visible = false;
                ShowTableHeader();
                ShowTableData();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "TestFunction", "alert('From Date Must be Before To Date');", true);
            }
            else if (monthDiff > 1)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "TestFunction", "alert('Date Range must be less than one month');", true);
            }
            else if ((monthDiff == 1) && (dateDiff > 0))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "TestFunction", "alert('Date Range must be less than one month');", true);
            }
            else
            {
                imgExcel.Visible = true;
                //imgpdf.Visible = true;
                divReportData.Visible = true;
                ShowTableHeader();
                ShowTableData();
            }
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
            tcCol141.Text = "<b> Excel Report of BCAS TAX Invoice For Finance Section Generated on:</b> " + DateTime.Now.ToString("dd-MMM-yyyy");
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
            //tcCol161.Text = "<b> Successfull Payment of :</b> " + ddlPayeeOrganisation.SelectedItem.Text;
            tcCol161.Text = "<b> BCAS TAX Invoice For Finance Section Between Date :" + txt_from_date.Text + "-" + txt_to_date.Text + " </b> ";
            tcCol161.ColumnSpan = 10;
            th6.Cells.Add(tcCol161);

            tbl.Rows.Add(th6);


            //TableHeaderRow th7 = new TableHeaderRow();
            //th7.CssClass = "head1";
            //TableHeaderCell tcCol171 = new TableHeaderCell();
            //tcCol171.Width = Unit.Percentage(19);
            //tcCol171.HorizontalAlign = HorizontalAlign.Center;
            //tcCol171.BorderWidth = 1;
            //tcCol171.BorderColor = System.Drawing.Color.Chocolate;
            //tcCol171.Text = "<b> Course Name :</b> " + ddlSchedule.SelectedItem.Text;
            //tcCol171.ColumnSpan = 10;
            //th7.Cells.Add(tcCol171);

            //tbl.Rows.Add(th7);


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
            tcCol11.BorderColor = System.Drawing.Color.Black; 
            th2.Cells.Add(tcCol11);

            TableHeaderCell tcColCourse_Name = new TableHeaderCell();
            tcColCourse_Name.Width = Unit.Percentage(1);
            // tcCol11.RowSpan = 2;
            tcColCourse_Name.HorizontalAlign = HorizontalAlign.Center;
            tcColCourse_Name.Text = "Course_Name";
            tcColCourse_Name.BorderWidth = 1;
            tcColCourse_Name.BorderColor = System.Drawing.Color.Black;
            th2.Cells.Add(tcColCourse_Name); 

            TableHeaderCell tcCol17 = new TableHeaderCell();
            //tcCol17.ColumnSpan = 2;
            tcCol17.Width = Unit.Percentage(10);
            tcCol17.HorizontalAlign = HorizontalAlign.Center;
            tcCol17.Text = "GSTIN/UIN of Recipient";
            tcCol17.BorderWidth = 1;
            tcCol17.BorderColor = System.Drawing.Color.Black;
            th2.Cells.Add(tcCol17);

            TableHeaderCell tcCol12 = new TableHeaderCell();
            tcCol12.Width = Unit.Percentage(5);
            tcCol12.ColumnSpan = 1;
            tcCol12.HorizontalAlign = HorizontalAlign.Center;
            tcCol12.Text = "Receiver Name";
            tcCol12.BorderWidth = 1;
            tcCol12.BorderColor = System.Drawing.Color.Black;
            th2.Cells.Add(tcCol12);

            TableHeaderCell tcAddress = new TableHeaderCell();
            tcAddress.Width = Unit.Percentage(5);
            tcAddress.ColumnSpan = 1;
            tcAddress.HorizontalAlign = HorizontalAlign.Center;
            tcAddress.Text = "Receiver Address";
            tcAddress.BorderWidth = 1;
            tcAddress.BorderColor = System.Drawing.Color.Black;
            th2.Cells.Add(tcAddress);

            TableHeaderCell tcState = new TableHeaderCell();
            tcState.Width = Unit.Percentage(5);
            tcState.ColumnSpan = 1;
            tcState.HorizontalAlign = HorizontalAlign.Center;
            tcState.Text = "Receiver State";
            tcState.BorderWidth = 1;
            tcState.BorderColor = System.Drawing.Color.Black;
            th2.Cells.Add(tcState);

            TableHeaderCell tcCol121 = new TableHeaderCell();
            tcCol121.Width = Unit.Percentage(8);
            tcCol121.ColumnSpan = 1;
            tcCol121.HorizontalAlign = HorizontalAlign.Center;
            tcCol121.Text = "Pin Code";
            tcCol121.BorderWidth = 1;
            tcCol121.BorderColor = System.Drawing.Color.Black;
            th2.Cells.Add(tcCol121);

            TableHeaderCell tcCol13 = new TableHeaderCell();
            //tcCol13.ColumnSpan = 2;
            tcCol13.Width = Unit.Percentage(8);
            tcCol13.HorizontalAlign = HorizontalAlign.Center;
            tcCol13.Text = "Invoice Number";
            tcCol13.BorderWidth = 1;
            tcCol13.BorderColor = System.Drawing.Color.Black;
            th2.Cells.Add(tcCol13);

            TableHeaderCell tcCol14 = new TableHeaderCell();
            //tcCol14.ColumnSpan = 2;
            tcCol14.Width = Unit.Percentage(10);
            tcCol14.HorizontalAlign = HorizontalAlign.Center;
            tcCol14.Text = "Invoice date";
            tcCol14.BorderWidth = 1;
            tcCol14.BorderColor = System.Drawing.Color.Black;
            th2.Cells.Add(tcCol14);

            TableHeaderCell tcCol14a = new TableHeaderCell();
            //tcCol14.ColumnSpan = 2;
            tcCol14a.Width = Unit.Percentage(10);
            tcCol14a.HorizontalAlign = HorizontalAlign.Center;
            tcCol14a.Text = "Invoice Value";
            tcCol14a.BorderWidth = 1;
            tcCol14a.BorderColor = System.Drawing.Color.Black;
            th2.Cells.Add(tcCol14a);

            TableHeaderCell tcCol15a = new TableHeaderCell();
            tcCol15a.ColumnSpan = 1;
            tcCol15a.Width = Unit.Percentage(10);
            tcCol15a.HorizontalAlign = HorizontalAlign.Center;
            tcCol15a.Text = "Place Of Supply";
            tcCol15a.BorderWidth = 1;
            tcCol15a.BorderColor = System.Drawing.Color.Black;
            th2.Cells.Add(tcCol15a);


            //TableHeaderCell tcCol15b = new TableHeaderCell();
            //tcCol15b.ColumnSpan = 1;
            //tcCol15b.Width = Unit.Percentage(5);
            //tcCol15b.HorizontalAlign = HorizontalAlign.Center;
            //tcCol15b.Text = "Reverse Charge\r\n";
            //tcCol15b.BorderWidth = 1;
            //tcCol15b.BorderColor = System.Drawing.Color.White;
            //th2.Cells.Add(tcCol15b);

            TableHeaderCell tcCol15c = new TableHeaderCell();
            tcCol15c.ColumnSpan = 1;
            tcCol15c.Width = Unit.Percentage(5);
            tcCol15c.HorizontalAlign = HorizontalAlign.Center;
            tcCol15c.Text = "Reverse Charge";
            tcCol15c.BorderWidth = 1;
            tcCol15c.BorderColor = System.Drawing.Color.Black;
            th2.Cells.Add(tcCol15c);

            TableHeaderCell tcCol15d = new TableHeaderCell();
            tcCol15d.ColumnSpan = 1;
            tcCol15d.Width = Unit.Percentage(3);
            tcCol15d.HorizontalAlign = HorizontalAlign.Center;
            tcCol15d.Text = "Invoice Type";
            tcCol15d.BorderWidth = 1;
            tcCol15d.BorderColor = System.Drawing.Color.Black;
            th2.Cells.Add(tcCol15d);

            TableHeaderCell tcCol15e = new TableHeaderCell();
            tcCol15e.ColumnSpan = 1;
            tcCol15e.Width = Unit.Percentage(5);
            tcCol15e.HorizontalAlign = HorizontalAlign.Center;
            tcCol15e.Text = "E-Commerce GSTIN";
            tcCol15e.BorderWidth = 1;
            tcCol15e.BorderColor = System.Drawing.Color.Black;
            th2.Cells.Add(tcCol15e);

            TableHeaderCell tcCol15 = new TableHeaderCell(); 
            tcCol15.Width = Unit.Percentage(5);
            tcCol15.HorizontalAlign = HorizontalAlign.Center;
            tcCol15.Text = "Rate";
            tcCol15.BorderWidth = 1;
            tcCol15.BorderColor = System.Drawing.Color.Black;
            th2.Cells.Add(tcCol15);

            TableHeaderCell tcColCGST_Delhi = new TableHeaderCell();
            tcColCGST_Delhi.Width = Unit.Percentage(5);
            tcColCGST_Delhi.HorizontalAlign = HorizontalAlign.Center;
            tcColCGST_Delhi.Text = "CGST";
            tcColCGST_Delhi.BorderWidth = 1;
            tcColCGST_Delhi.BorderColor = System.Drawing.Color.Black;
            th2.Cells.Add(tcColCGST_Delhi);

            TableHeaderCell tcColSGST_Delhi = new TableHeaderCell();
            tcColSGST_Delhi.Width = Unit.Percentage(5);
            tcColSGST_Delhi.HorizontalAlign = HorizontalAlign.Center;
            tcColSGST_Delhi.Text = "SGST";
            tcColSGST_Delhi.BorderWidth = 1;
            tcColSGST_Delhi.BorderColor = System.Drawing.Color.Black;
            th2.Cells.Add(tcColSGST_Delhi);

            TableHeaderCell tcColIGST_Others = new TableHeaderCell();
            tcColIGST_Others.Width = Unit.Percentage(5);
            tcColIGST_Others.HorizontalAlign = HorizontalAlign.Center;
            tcColIGST_Others.Text = "IGST";
            tcColIGST_Others.BorderWidth = 1;
            tcColIGST_Others.BorderColor = System.Drawing.Color.Black;
            th2.Cells.Add(tcColIGST_Others);

            TableHeaderCell tcColNoOfCandidates = new TableHeaderCell();
            tcColNoOfCandidates.Width = Unit.Percentage(5);
            tcColNoOfCandidates.HorizontalAlign = HorizontalAlign.Center;
            tcColNoOfCandidates.Text = "No Of Candidates";
            tcColNoOfCandidates.BorderWidth = 1;
            tcColNoOfCandidates.BorderColor = System.Drawing.Color.Black;
            th2.Cells.Add(tcColNoOfCandidates);

            TableHeaderCell tcCol16 = new TableHeaderCell();
            tcCol16.ColumnSpan = 1;
            tcCol16.Width = Unit.Percentage(5);
            tcCol16.HorizontalAlign = HorizontalAlign.Center;
            tcCol16.Text = "Taxable Value";
            tcCol16.BorderWidth = 1;
            tcCol16.BorderColor = System.Drawing.Color.Black;
            th2.Cells.Add(tcCol16);

            TableHeaderCell tcCol18a = new TableHeaderCell();
            tcCol18a.ColumnSpan = 1;
            tcCol18a.Width = Unit.Percentage(5);
            tcCol18a.HorizontalAlign = HorizontalAlign.Center;
            tcCol18a.Text = "Cess Amount";
            tcCol18a.BorderWidth = 1;
            tcCol18a.BorderColor = System.Drawing.Color.Black;
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
            tcCol11.BorderColor = System.Drawing.Color.Black;
            th2.Cells.Add(tcCol11);

            TableHeaderCell tcCol17 = new TableHeaderCell();
            tcCol17.ColumnSpan = 1;
            tcCol17.Width = Unit.Percentage(15);
            tcCol17.HorizontalAlign = HorizontalAlign.Center;
            tcCol17.Text = "GSTIN/UIN of Recipient";
            tcCol17.BorderWidth = 1;
            tcCol17.BorderColor = System.Drawing.Color.Black;
            th2.Cells.Add(tcCol17);


            TableHeaderCell tcCol12 = new TableHeaderCell();
            tcCol12.Width = Unit.Percentage(5);
            tcCol12.ColumnSpan = 2;
            tcCol12.HorizontalAlign = HorizontalAlign.Center;
            tcCol12.Text = "Receiver Name";
            tcCol12.BorderWidth = 1;
            tcCol12.BorderColor = System.Drawing.Color.Black;
            th2.Cells.Add(tcCol12);

            TableHeaderCell tcColAddress = new TableHeaderCell();
            tcCol12.Width = Unit.Percentage(5);
            tcCol12.ColumnSpan = 2;
            tcCol12.HorizontalAlign = HorizontalAlign.Center;
            tcCol12.Text = "Receiver Address";
            tcCol12.BorderWidth = 1;
            tcCol12.BorderColor = System.Drawing.Color.Black;
            th2.Cells.Add(tcColAddress);

            TableHeaderCell tcColState = new TableHeaderCell();
            tcCol12.Width = Unit.Percentage(5);
            tcCol12.ColumnSpan = 2;
            tcCol12.HorizontalAlign = HorizontalAlign.Center;
            tcCol12.Text = "Receiver State";
            tcCol12.BorderWidth = 1;
            tcCol12.BorderColor = System.Drawing.Color.Black;
            th2.Cells.Add(tcColState);


            TableHeaderCell tcCol121 = new TableHeaderCell();
            tcCol121.Width = Unit.Percentage(15);
            tcCol121.ColumnSpan = 1;
            tcCol121.HorizontalAlign = HorizontalAlign.Center;
            tcCol121.Text = "Pin Code";
            tcCol121.BorderWidth = 1;
            tcCol121.BorderColor = System.Drawing.Color.Black;
            th2.Cells.Add(tcCol121);


            TableHeaderCell tcCol13 = new TableHeaderCell();
            //tcCol13.ColumnSpan = 2;
            tcCol13.Width = Unit.Percentage(15);
            tcCol13.HorizontalAlign = HorizontalAlign.Center;
            tcCol13.Text = "Invoice Number";
            tcCol13.BorderWidth = 1;
            tcCol13.BorderColor = System.Drawing.Color.Black;
            th2.Cells.Add(tcCol13);

            TableHeaderCell tcCol14 = new TableHeaderCell();
            tcCol14.ColumnSpan = 1;
            tcCol14.Width = Unit.Percentage(15);
            tcCol14.HorizontalAlign = HorizontalAlign.Center;
            tcCol14.Text = "Invoice Date";
            tcCol14.BorderWidth = 1;
            tcCol14.BorderColor = System.Drawing.Color.Black;
            th2.Cells.Add(tcCol14);

            TableHeaderCell tcCol14a = new TableHeaderCell();
            //tcCol14.ColumnSpan = 2;
            tcCol14a.Width = Unit.Percentage(15);
            tcCol14a.HorizontalAlign = HorizontalAlign.Center;
            tcCol14a.Text = "Invoice Value";
            tcCol14a.BorderWidth = 1;
            tcCol14a.BorderColor = System.Drawing.Color.Black;
            th2.Cells.Add(tcCol14a);

            TableHeaderCell tcCol15b = new TableHeaderCell();
            tcCol15b.ColumnSpan = 1;
            tcCol15b.Width = Unit.Percentage(15);
            tcCol15b.HorizontalAlign = HorizontalAlign.Center;
            tcCol15b.Text = "Place Of Supply";
            tcCol15b.BorderWidth = 1;
            tcCol15b.BorderColor = System.Drawing.Color.Black;
            th2.Cells.Add(tcCol15b);

            TableHeaderCell tcCol15c = new TableHeaderCell();
            tcCol15c.ColumnSpan = 1;
            tcCol15c.Width = Unit.Percentage(15);
            tcCol15c.HorizontalAlign = HorizontalAlign.Center;
            tcCol15c.Text = "Reverse Charge";
            tcCol15c.BorderWidth = 1;
            tcCol15c.BorderColor = System.Drawing.Color.Black;
            th2.Cells.Add(tcCol15c);

            TableHeaderCell tcCol15d = new TableHeaderCell();
            tcCol15d.ColumnSpan = 1;
            tcCol15d.Width = Unit.Percentage(15);
            tcCol15d.HorizontalAlign = HorizontalAlign.Center;
            tcCol15d.Text = "Invoice Type";
            tcCol15d.BorderWidth = 1;
            tcCol15d.BorderColor = System.Drawing.Color.Black;
            th2.Cells.Add(tcCol15d);

            TableHeaderCell tcCol15e = new TableHeaderCell();
            tcCol15e.ColumnSpan = 1;
            tcCol15e.Width = Unit.Percentage(15);
            tcCol15e.HorizontalAlign = HorizontalAlign.Center;
            tcCol15e.Text = "E-Commerce GSTIN";
            tcCol15e.BorderWidth = 1;
            tcCol15e.BorderColor = System.Drawing.Color.Black;
            th2.Cells.Add(tcCol15e);



            TableHeaderCell tcCol15 = new TableHeaderCell();
            // tcCol15.ColumnSpan = 2;
            tcCol15.Width = Unit.Percentage(15);
            tcCol15.HorizontalAlign = HorizontalAlign.Center;
            tcCol15.Text = "Rate";
            tcCol15.BorderWidth = 1;
            tcCol15.BorderColor = System.Drawing.Color.Black;
            th2.Cells.Add(tcCol15);

            TableHeaderCell tcCol16 = new TableHeaderCell();
            tcCol16.ColumnSpan = 1;
            tcCol16.Width = Unit.Percentage(15);
            tcCol16.HorizontalAlign = HorizontalAlign.Center;
            tcCol16.Text = "Taxable Value";
            tcCol16.BorderWidth = 1;
            tcCol16.BorderColor = System.Drawing.Color.Black;
            th2.Cells.Add(tcCol16);

            TableHeaderCell tcCol18a = new TableHeaderCell();
            tcCol18a.ColumnSpan = 1;
            tcCol18a.Width = Unit.Percentage(15);
            tcCol18a.HorizontalAlign = HorizontalAlign.Center;
            tcCol18a.Text = "Cess Amount";
            tcCol18a.BorderWidth = 1;
            tcCol18a.BorderColor = System.Drawing.Color.Black;
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
            //Int64 totUnderTrained = 0, totCertified = 0;
            string constr = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(constr);

            SqlCommand scCommand = new SqlCommand("repInvoiceDwnExcel", new SqlConnection(con.ConnectionString));
            scCommand.CommandType = CommandType.StoredProcedure;



            if (Session["OrgName"] != null)
            {
                //scCommand.Parameters.Add("@Org", SqlDbType.BigInt).Value = Convert.ToInt32(lblOrg.Text.ToString());
            }

            else
            {
                scCommand.Parameters.Add("@Org", SqlDbType.BigInt).Value = Convert.ToInt32(ddlPayeeOrganisation.SelectedValue);
            }

            string sqlCompatibleFromDate = DateTime.ParseExact(txt_from_date.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
            string sqlCompatibleToDate = DateTime.ParseExact(txt_to_date.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");

            scCommand.Parameters.Add("@frmDate", SqlDbType.Date).Value = sqlCompatibleFromDate.ToString();
            scCommand.Parameters.Add("@toDate", SqlDbType.Date).Value = sqlCompatibleToDate.ToString();



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
                tdRow.BorderColor = System.Drawing.Color.Black;
                tr.Cells.Add(tdRow);


                TableCell tdRow4 = new TableCell();
                tdRow4.Width = Unit.Percentage(8);
                tdRow4.ColumnSpan = 1;
                tdRow4.Text = ds.Tables[0].Rows[j]["GSTIN"].ToString();
                tdRow4.HorizontalAlign = HorizontalAlign.Right;
                tdRow4.BorderWidth = 1;
                tdRow4.BorderColor = System.Drawing.Color.Black;
                tr.Cells.Add(tdRow4);

                TableCell tdRow1 = new TableCell();
                tdRow1.Width = Unit.Percentage(5);
                tdRow1.ColumnSpan = 2;
                tdRow1.Text = ds.Tables[0].Rows[j]["Payee_Organisation_Name"].ToString();
                tdRow1.HorizontalAlign = HorizontalAlign.Center;
                tdRow1.BorderWidth = 1;
                tdRow1.BorderColor = System.Drawing.Color.Black;
                tr.Cells.Add(tdRow1);

                TableCell tdRowAddress = new TableCell();
                tdRow1.Width = Unit.Percentage(12);
                tdRow1.ColumnSpan = 2;
                tdRow1.Text = ds.Tables[0].Rows[j]["Payee_Address"].ToString();
                tdRow1.HorizontalAlign = HorizontalAlign.Center;
                tdRow1.BorderWidth = 1;
                tdRow1.BorderColor = System.Drawing.Color.Black;
                tr.Cells.Add(tdRowAddress);

                TableCell tdRowSTATES = new TableCell();
                tdRow1.Width = Unit.Percentage(12);
                tdRow1.ColumnSpan = 2;
                tdRow1.Text = ds.Tables[0].Rows[j]["STATES"].ToString();
                tdRow1.HorizontalAlign = HorizontalAlign.Center;
                tdRow1.BorderWidth = 1;
                tdRow1.BorderColor = System.Drawing.Color.Black;
                tr.Cells.Add(tdRowSTATES);

                TableCell tdRow11 = new TableCell();
                tdRow11.Width = Unit.Percentage(17);
                tdRow11.ColumnSpan = 1;
                tdRow11.Text = ds.Tables[0].Rows[j]["Payee_Location_Pincode"].ToString();
                tdRow11.HorizontalAlign = HorizontalAlign.Center;
                tdRow11.BorderWidth = 1;
                tdRow11.BorderColor = System.Drawing.Color.Black;
                tr.Cells.Add(tdRow11);

                TableCell tdRow2 = new TableCell();
                tdRow2.Width = Unit.Percentage(8);
                tdRow2.Text = ds.Tables[0].Rows[j]["ProformaInvoiceNo"].ToString();
                tdRow2.HorizontalAlign = HorizontalAlign.Center;
                tdRow2.BorderWidth = 1;
                tdRow2.BorderColor = System.Drawing.Color.Black;
                tr.Cells.Add(tdRow2);

                TableCell tdRow3 = new TableCell();
                tdRow3.Width = Unit.Percentage(8);
                tdRow3.ColumnSpan = 1;
                tdRow3.Text = ds.Tables[0].Rows[j]["ProformaInvoice_Date"].ToString();
                tdRow3.HorizontalAlign = HorizontalAlign.Center;
                tdRow3.BorderWidth = 1;
                tdRow3.BorderColor = System.Drawing.Color.Black;
                tr.Cells.Add(tdRow3);


                TableCell tdRow15 = new TableCell();
                tdRow15.Width = Unit.Percentage(8);
                tdRow15.ColumnSpan = 1;
                tdRow15.Text = ds.Tables[0].Rows[j]["Total_Fee_With_Service_Tax"].ToString();
                tdRow15.HorizontalAlign = HorizontalAlign.Center;
                tdRow15.BorderWidth = 1;
                tdRow15.BorderColor = System.Drawing.Color.Black;
                tr.Cells.Add(tdRow15);

                TableCell tdRow15a = new TableCell();
                tdRow15a.Width = Unit.Percentage(8);
                tdRow15a.ColumnSpan = 1;
                tdRow15a.Text = ds.Tables[0].Rows[j]["Place_Of_Supply"].ToString();
                tdRow15a.HorizontalAlign = HorizontalAlign.Center;
                tdRow15a.BorderWidth = 1;
                tdRow15a.BorderColor = System.Drawing.Color.Black;
                tr.Cells.Add(tdRow15a);


                TableCell tdRow15b = new TableCell();
                tdRow15b.Width = Unit.Percentage(8);
                tdRow15b.ColumnSpan = 1;
                tdRow15b.Text = "N";
                tdRow15b.HorizontalAlign = HorizontalAlign.Center;
                tdRow15b.BorderWidth = 1;
                tdRow15b.BorderColor = System.Drawing.Color.Black;
                tr.Cells.Add(tdRow15b);

                TableCell tdRow15c = new TableCell();
                tdRow15c.Width = Unit.Percentage(8);
                tdRow15c.ColumnSpan = 1;
                tdRow15c.Text = "Regular";
                tdRow15c.HorizontalAlign = HorizontalAlign.Center;
                tdRow15c.BorderWidth = 1;
                tdRow15c.BorderColor = System.Drawing.Color.Black;
                tr.Cells.Add(tdRow15c);

                TableCell tdRow15d = new TableCell();
                tdRow15d.Width = Unit.Percentage(8);
                tdRow15d.ColumnSpan = 1;
                tdRow15d.Text = "" ;
                tdRow15d.HorizontalAlign = HorizontalAlign.Center;
                tdRow15d.BorderWidth = 1;
                tdRow15d.BorderColor = System.Drawing.Color.Black;
                tr.Cells.Add(tdRow15d);


                TableCell tdRow14 = new TableCell();
                tdRow14.Width = Unit.Percentage(8);
                //tdRow14.Text = ds.Tables[0].Rows[j]["IGST_Others"].ToString();
                tdRow14.Text = ds.Tables[0].Rows[j]["Rate"].ToString();
                tdRow14.HorizontalAlign = HorizontalAlign.Center;
                tdRow14.BorderWidth = 1;
                tdRow14.BorderColor = System.Drawing.Color.Black;
                tr.Cells.Add(tdRow14);


                TableCell tdRow21 = new TableCell();
                tdRow21.Width = Unit.Percentage(8);
                tdRow21.ColumnSpan = 1;
                tdRow21.Text = ds.Tables[0].Rows[j]["Fee_Amount"].ToString(); ;
                tdRow21.HorizontalAlign = HorizontalAlign.Center;
                tdRow21.BorderWidth = 1;
                tdRow21.BorderColor = System.Drawing.Color.Black;
                tr.Cells.Add(tdRow21);

                TableCell tdRow22 = new TableCell();
                tdRow22.Width = Unit.Percentage(8);
                tdRow22.ColumnSpan = 1;
                tdRow22.Text = "-----";
                tdRow22.HorizontalAlign = HorizontalAlign.Center;
                tdRow22.BorderWidth = 1;
                tdRow22.BorderColor = System.Drawing.Color.Black;
                tr.Cells.Add(tdRow22);

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
            //Int64 totUnderTrained = 0, totCertified = 0;
            string constr = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(constr);

            SqlCommand scCommand = new SqlCommand("repInvoiceDwnExcel", new SqlConnection(con.ConnectionString));
            scCommand.CommandType = CommandType.StoredProcedure;



            if (Session["OrgName"] != null)
            {
                //scCommand.Parameters.Add("@Org", SqlDbType.BigInt).Value = Convert.ToInt32(lblOrg.Text.ToString());
            }

            else
            {
                scCommand.Parameters.Add("@Org", SqlDbType.BigInt).Value = Convert.ToInt32(ddlPayeeOrganisation.SelectedValue);
            }


            string sqlCompatibleFromDate = DateTime.ParseExact(txt_from_date.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
            string sqlCompatibleToDate = DateTime.ParseExact(txt_to_date.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");

            scCommand.Parameters.Add("@frmDate", SqlDbType.Date).Value = sqlCompatibleFromDate.ToString();
            scCommand.Parameters.Add("@toDate", SqlDbType.Date).Value = sqlCompatibleToDate.ToString();

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
                tdRow.BorderColor = System.Drawing.Color.Black;
                tr.Cells.Add(tdRow);

                TableCell tdCellCourse_Name = new TableCell();
                tdCellCourse_Name.Width = Unit.Percentage(5);
                tdCellCourse_Name.ColumnSpan = 1;
                tdCellCourse_Name.Text = ds.Tables[0].Rows[j]["Course_Name"].ToString();
                tdCellCourse_Name.HorizontalAlign = HorizontalAlign.Right;
                tdCellCourse_Name.BorderWidth = 1;
                tdCellCourse_Name.BorderColor = System.Drawing.Color.Black;
                tr.Cells.Add(tdCellCourse_Name);

                TableCell tdRow4 = new TableCell();
                tdRow4.Width = Unit.Percentage(8);
                tdRow4.ColumnSpan = 1;
                tdRow4.Text = ds.Tables[0].Rows[j]["GSTIN"].ToString();
                tdRow4.HorizontalAlign = HorizontalAlign.Right;
                tdRow4.BorderWidth = 1;
                tdRow4.BorderColor = System.Drawing.Color.Black;
                tr.Cells.Add(tdRow4);


                TableCell tdRow1 = new TableCell();
                tdRow1.Width = Unit.Percentage(5);
                tdRow1.ColumnSpan = 1;
                tdRow1.Text = ds.Tables[0].Rows[j]["Payee_Organisation_Name"].ToString();
                tdRow1.HorizontalAlign = HorizontalAlign.Center;
                tdRow1.BorderWidth = 1;
                tdRow1.BorderColor = System.Drawing.Color.Black;
                tr.Cells.Add(tdRow1);

                TableCell tdAddress = new TableCell();
                tdAddress.Width = Unit.Percentage(7);
                tdAddress.ColumnSpan = 1;
                tdAddress.Text = ds.Tables[0].Rows[j]["Payee_Address"].ToString();
                tdAddress.HorizontalAlign = HorizontalAlign.Center;
                tdAddress.BorderWidth = 1;
                tdAddress.BorderColor = System.Drawing.Color.Black;
                tr.Cells.Add(tdAddress);

                TableCell tdSTATES = new TableCell();
                tdSTATES.Width = Unit.Percentage(5);
                tdSTATES.ColumnSpan = 1;
                tdSTATES.Text = ds.Tables[0].Rows[j]["STATES"].ToString();
                tdSTATES.HorizontalAlign = HorizontalAlign.Center;
                tdSTATES.BorderWidth = 1;
                tdSTATES.BorderColor = System.Drawing.Color.Black;
                tr.Cells.Add(tdSTATES);

                TableCell tdRow11 = new TableCell();
                tdRow11.Width = Unit.Percentage(17);
                tdRow11.ColumnSpan = 1;
                tdRow11.Text = ds.Tables[0].Rows[j]["Payee_Location_Pincode"].ToString();
                tdRow11.HorizontalAlign = HorizontalAlign.Center;
                tdRow11.BorderWidth = 1;
                tdRow11.BorderColor = System.Drawing.Color.Black;
                tr.Cells.Add(tdRow11);

                TableCell tdRow2 = new TableCell();
                tdRow2.Width = Unit.Percentage(8);
                tdRow2.Text = ds.Tables[0].Rows[j]["ProformaInvoiceNo"].ToString();
                tdRow2.HorizontalAlign = HorizontalAlign.Center;
                tdRow2.BorderWidth = 1;
                tdRow2.BorderColor = System.Drawing.Color.Black;
                tr.Cells.Add(tdRow2);

                TableCell tdRow3 = new TableCell();
                tdRow3.Width = Unit.Percentage(8);
                // tdRow3.ColumnSpan = 1;
                tdRow3.Text = ds.Tables[0].Rows[j]["ProformaInvoice_Date"].ToString();
                tdRow3.HorizontalAlign = HorizontalAlign.Center;
                tdRow3.BorderWidth = 1;
                tdRow3.BorderColor = System.Drawing.Color.Black;
                tr.Cells.Add(tdRow3);


                TableCell tdRow15 = new TableCell();
                tdRow15.Width = Unit.Percentage(8);
                tdRow15.ColumnSpan = 1;
                tdRow15.Text = ds.Tables[0].Rows[j]["Total_Fee_With_Service_Tax"].ToString();
                tdRow15.HorizontalAlign = HorizontalAlign.Center;
                tdRow15.BorderWidth = 1;
                tdRow15.BorderColor = System.Drawing.Color.Black;
                tr.Cells.Add(tdRow15);


                TableCell tdRow15a = new TableCell();
                tdRow15a.Width = Unit.Percentage(8);
                tdRow15a.ColumnSpan = 1;
                tdRow15a.Text = ds.Tables[0].Rows[j]["Place_Of_Supply"].ToString();
                tdRow15a.HorizontalAlign = HorizontalAlign.Center;
                tdRow15a.BorderWidth = 1;
                tdRow15a.BorderColor = System.Drawing.Color.Black;
                tr.Cells.Add(tdRow15a);


                TableCell tdRow15b = new TableCell();
                tdRow15b.Width = Unit.Percentage(8);
                tdRow15b.ColumnSpan = 1;
                tdRow15b.Text = "N";
                tdRow15b.HorizontalAlign = HorizontalAlign.Center;
                tdRow15b.BorderWidth = 1;
                tdRow15b.BorderColor = System.Drawing.Color.Black;
                tr.Cells.Add(tdRow15b);

                TableCell tdRow15c = new TableCell();
                tdRow15c.Width = Unit.Percentage(8);
                tdRow15c.ColumnSpan = 1;
                tdRow15c.Text = "Regular";
                tdRow15c.HorizontalAlign = HorizontalAlign.Center;
                tdRow15c.BorderWidth = 1;
                tdRow15c.BorderColor = System.Drawing.Color.Black;
                tr.Cells.Add(tdRow15c);

                TableCell tdRow15d = new TableCell();
                tdRow15d.Width = Unit.Percentage(8);
                tdRow15d.ColumnSpan = 1;
                tdRow15d.Text = "";
                tdRow15d.HorizontalAlign = HorizontalAlign.Center;
                tdRow15d.BorderWidth = 1;
                tdRow15d.BorderColor = System.Drawing.Color.Black;
                tr.Cells.Add(tdRow15d);


                TableCell tdRow14 = new TableCell();
                tdRow14.Width = Unit.Percentage(8);
                //tdRow14.Text = ds.Tables[0].Rows[j]["IGST_Others"].ToString();
                tdRow14.Text = ds.Tables[0].Rows[j]["Rate"].ToString();
                tdRow14.HorizontalAlign = HorizontalAlign.Center;
                tdRow14.BorderWidth = 1;
                tdRow14.BorderColor = System.Drawing.Color.Black;
                tr.Cells.Add(tdRow14);

                TableCell tdCellCGST_Delhi = new TableCell();
                tdCellCGST_Delhi.Width = Unit.Percentage(5);
                //tdRow14.Text = ds.Tables[0].Rows[j]["IGST_Others"].ToString();
                tdCellCGST_Delhi.Text = ds.Tables[0].Rows[j]["CGST_Delhi"].ToString();
                tdCellCGST_Delhi.HorizontalAlign = HorizontalAlign.Center;
                tdCellCGST_Delhi.BorderWidth = 1;
                tdCellCGST_Delhi.BorderColor = System.Drawing.Color.Black;
                tr.Cells.Add(tdCellCGST_Delhi);

                TableCell tdCellSGST_Delhi = new TableCell();
                tdCellSGST_Delhi.Width = Unit.Percentage(5);
                //tdRow14.Text = ds.Tables[0].Rows[j]["IGST_Others"].ToString();
                tdCellSGST_Delhi.Text = ds.Tables[0].Rows[j]["SGST_Delhi"].ToString();
                tdCellSGST_Delhi.HorizontalAlign = HorizontalAlign.Center;
                tdCellSGST_Delhi.BorderWidth = 1;
                tdCellSGST_Delhi.BorderColor = System.Drawing.Color.Black;
                tr.Cells.Add(tdCellSGST_Delhi);

                TableCell tdCellIGST_Others = new TableCell();
                tdCellIGST_Others.Width = Unit.Percentage(5);
                //tdRow14.Text = ds.Tables[0].Rows[j]["IGST_Others"].ToString();
                tdCellIGST_Others.Text = ds.Tables[0].Rows[j]["IGST_Others"].ToString();
                tdCellIGST_Others.HorizontalAlign = HorizontalAlign.Center;
                tdCellIGST_Others.BorderWidth = 1;
                tdCellIGST_Others.BorderColor = System.Drawing.Color.Black;
                tr.Cells.Add(tdCellIGST_Others);

                TableCell tdCellNoOfCandidates = new TableCell();
                tdCellNoOfCandidates.Width = Unit.Percentage(5);
                //tdRow14.Text = ds.Tables[0].Rows[j]["IGST_Others"].ToString();
                tdCellNoOfCandidates.Text = ds.Tables[0].Rows[j]["NoOfCandidates"].ToString();
                tdCellNoOfCandidates.HorizontalAlign = HorizontalAlign.Center;
                tdCellNoOfCandidates.BorderWidth = 1;
                tdCellNoOfCandidates.BorderColor = System.Drawing.Color.Black;
                tr.Cells.Add(tdCellNoOfCandidates);


                TableCell tdRow21 = new TableCell();
                tdRow21.Width = Unit.Percentage(8);
                tdRow21.ColumnSpan = 1;
                tdRow21.Text = ds.Tables[0].Rows[j]["Fee_Amount"].ToString();
                tdRow21.HorizontalAlign = HorizontalAlign.Center;
                tdRow21.BorderWidth = 1;
                tdRow21.BorderColor = System.Drawing.Color.Black;
                tr.Cells.Add(tdRow21);

                TableCell tdRow22 = new TableCell();
                tdRow22.Width = Unit.Percentage(8);
                tdRow22.ColumnSpan = 1;
                tdRow22.Text = "-----";
                tdRow22.HorizontalAlign = HorizontalAlign.Center;
                tdRow22.BorderWidth = 1;
                tdRow22.BorderColor = System.Drawing.Color.Black;
                tr.Cells.Add(tdRow22);

                //TableCell tdRow16 = new TableCell();
                //tdRow16.Width = Unit.Percentage(8);
                //tdRow16.ColumnSpan = 2;
                //tdRow16.Text = ds.Tables[0].Rows[j]["Settled_FileName"].ToString();
                //tdRow16.HorizontalAlign = HorizontalAlign.Center;
                //tdRow16.BorderWidth = 1;
                //tdRow16.BorderColor = System.Drawing.Color.White;
                //tr.Cells.Add(tdRow16);


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
            Response.AddHeader("content-disposition", "attachment;filename=RepInvoiceDwnExcel.pdf");
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
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename=RepInvoiceDwnExcel.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.xls";
            htmlWrite = new Html32TextWriter(StringWrite);
            divReportData.RenderControl(htmlWrite);
            Response.Write(StringWrite.ToString());
            Response.End();

        }
        catch (Exception ex)
        {
            //ShowAlert(ex.Message);
        }
    }
    //protected void txt_from_date_TextChanged(object sender, EventArgs e)
    //{
    //    DateTime dt1 = DateTime.Now;

    //    DateTime date2 = DateTime.ParseExact(txt_from_date.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
    //    int r1 = DateTime.Compare(dt1, date2);
    //    if (r1 < 0)
    //    {
    //        Page.ClientScript.RegisterStartupScript(this.GetType(), "TestFunction", "alert('From Date Must Not Be later than the Current Date');", true);
    //    }
    //}

    //protected void txt_to_date_TextChanged(object sender, EventArgs e)
    //{
    //    DateTime dt1 = DateTime.Now;

    //    DateTime date1 = DateTime.ParseExact(txt_to_date.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
    //    // Compare the DateTime objects
    //    int r2 = DateTime.Compare(dt1, date1);
    //    if (r2 < 0)
    //    {
    //        Page.ClientScript.RegisterStartupScript(this.GetType(), "TestFunction", "alert('To Date Must Not Be later than the Current Date');", true);
    //    }
    //}
}
