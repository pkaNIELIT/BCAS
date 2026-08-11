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
using BCASClassLibrary;
using System.Data.SqlClient;
using System.Text;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;

public partial class Reports_ScheduleOrgWiseCandidateDetails : System.Web.UI.Page
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

        }

    }

    protected void rdoOption_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdoOption.SelectedValue == "S")
        {
            trSpons.Visible = true;
            trLocation.Visible = false;
            using (DataTable dtOrg = objBCAS.getGetAllOrganisation())
            {
                if (dtOrg.Rows.Count > 0)
                {
                    ddlOrganisation.DataSource = dtOrg;
                    ddlOrganisation.DataBind();
                }

            }
        }
        if (rdoOption.SelectedValue == "L")
        {
            trSpons.Visible = false;
            trLocation.Visible = true;
            if (ddlSchedule.SelectedValue != "0")
                ddlSchedule_SelectedIndexChanged(sender, e);
        }

    }
    protected void ddlSchedule_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlExamLocation.Items.Clear();
        ddlExamLocation.Items.Add(new System.Web.UI.WebControls.ListItem("Select Exam Location", "0"));
        ddlExamLocation.Items.Add(new System.Web.UI.WebControls.ListItem("All Exam Location", "999999"));
        ddlExamLocation.AppendDataBoundItems = true;
        SqlParameter p = new SqlParameter("@Schedule", ddlSchedule.SelectedValue);
        DataSet ds = GetAllExamLocation("GetAllExamLocationName", p);
        ddlExamLocation.DataSource = ds;
        ddlExamLocation.DataBind();
    }
    private DataSet GetAllExamLocation(String spname, SqlParameter p)
    {
        string ConnectionString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(ConnectionString))
        {
            con.Open();
            using (SqlDataAdapter sda = new SqlDataAdapter(spname, con))
            {
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.Parameters.Add(p);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                return ds;
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
            tcCol141.Text = "<b> Report of candidates Generated on:</b> " + DateTime.Now.ToString("dd-MMM-yyyy");
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
            tcCol12.Text = "Course Name";
            tcCol12.BorderWidth = 1;
            tcCol12.BorderColor = System.Drawing.Color.White;
            th2.Cells.Add(tcCol12);

            TableHeaderCell tcCol13 = new TableHeaderCell();
            //tcCol13.ColumnSpan = 2;
            tcCol13.Width = Unit.Percentage(15);
            tcCol13.HorizontalAlign = HorizontalAlign.Center;
            tcCol13.Text = "Payee Organisation";
            tcCol13.BorderWidth = 1;
            tcCol13.BorderColor = System.Drawing.Color.White;
            th2.Cells.Add(tcCol13);

            TableHeaderCell tcCol14 = new TableHeaderCell();
            // tcCol14.ColumnSpan = 2;
            tcCol14.Width = Unit.Percentage(15);
            tcCol14.HorizontalAlign = HorizontalAlign.Center;
            tcCol14.Text = "Sponsoring Organisation";
            tcCol14.BorderWidth = 1;
            tcCol14.BorderColor = System.Drawing.Color.White;
            th2.Cells.Add(tcCol14);

            TableHeaderCell tcCol15a = new TableHeaderCell();
            // tcCol15.ColumnSpan = 2;
            tcCol15a.Width = Unit.Percentage(15);
            tcCol15a.HorizontalAlign = HorizontalAlign.Center;
            tcCol15a.Text = "Employee ID";
            tcCol15a.BorderWidth = 1;
            tcCol15a.BorderColor = System.Drawing.Color.White;
            th2.Cells.Add(tcCol15a);

            TableHeaderCell tcCol15 = new TableHeaderCell();
            // tcCol15.ColumnSpan = 2;
            tcCol15.Width = Unit.Percentage(15);
            tcCol15.HorizontalAlign = HorizontalAlign.Center;
            tcCol15.Text = "Registration No.";
            tcCol15.BorderWidth = 1;
            tcCol15.BorderColor = System.Drawing.Color.White;
            th2.Cells.Add(tcCol15);

            TableHeaderCell tcCol16 = new TableHeaderCell();
            //tcCol16.ColumnSpan = 2;
            tcCol16.Width = Unit.Percentage(15);
            tcCol16.HorizontalAlign = HorizontalAlign.Center;
            tcCol16.Text = "Name";
            tcCol16.BorderWidth = 1;
            tcCol16.BorderColor = System.Drawing.Color.White;
            th2.Cells.Add(tcCol16);

            TableHeaderCell tcCol17 = new TableHeaderCell();
            //tcCol16.ColumnSpan = 2;
            tcCol17.Width = Unit.Percentage(15);
            tcCol17.HorizontalAlign = HorizontalAlign.Center;
            tcCol17.Text = "Gender";
            tcCol17.BorderWidth = 1;
            tcCol17.BorderColor = System.Drawing.Color.White;
            th2.Cells.Add(tcCol17);

            TableHeaderCell tcCol18a = new TableHeaderCell();
            //tcCol16.ColumnSpan = 2;
            tcCol18a.Width = Unit.Percentage(15);
            tcCol18a.HorizontalAlign = HorizontalAlign.Center;
            tcCol18a.Text = "Designation";
            tcCol18a.BorderWidth = 1;
            tcCol18a.BorderColor = System.Drawing.Color.White;
            th2.Cells.Add(tcCol18a);

            TableHeaderCell tcCol18 = new TableHeaderCell();
            //tcCol16.ColumnSpan = 2;
            tcCol18.Width = Unit.Percentage(15);
            tcCol18.HorizontalAlign = HorizontalAlign.Center;
            tcCol18.Text = "Date Of Birth";
            tcCol18.BorderWidth = 1;
            tcCol18.BorderColor = System.Drawing.Color.White;
            th2.Cells.Add(tcCol18);

            TableHeaderCell tcCol3 = new TableHeaderCell();
            tcCol3.Width = Unit.Percentage(8);
            tcCol3.HorizontalAlign = HorizontalAlign.Center;
            tcCol3.Text = "Email ";
            tcCol3.BorderWidth = 1;
            tcCol3.BorderColor = System.Drawing.Color.White;
            th2.Cells.Add(tcCol3);

            TableHeaderCell tcCol6 = new TableHeaderCell();
            tcCol6.Width = Unit.Percentage(8);
            tcCol6.HorizontalAlign = HorizontalAlign.Center;
            tcCol6.BorderWidth = 1;
            tcCol6.BorderColor = System.Drawing.Color.White;
            tcCol6.Text = "Mobile";
            th2.Cells.Add(tcCol6);

            TableHeaderCell tcCol7 = new TableHeaderCell();
            tcCol7.Width = Unit.Percentage(8);
            tcCol7.HorizontalAlign = HorizontalAlign.Center;
            tcCol7.BorderWidth = 1;
            tcCol7.BorderColor = System.Drawing.Color.White;
            tcCol7.Text = "Educational Qualification";
            th2.Cells.Add(tcCol7);

            TableHeaderCell tcCol9 = new TableHeaderCell();
            tcCol9.Width = Unit.Percentage(8);
            tcCol9.HorizontalAlign = HorizontalAlign.Center;
            tcCol9.BorderWidth = 1;
            tcCol9.BorderColor = System.Drawing.Color.White;
            tcCol9.Text = "Posting at City";
            th2.Cells.Add(tcCol9);

            TableHeaderCell tcCol10 = new TableHeaderCell();
            tcCol10.Width = Unit.Percentage(8);
            tcCol10.HorizontalAlign = HorizontalAlign.Center;
            tcCol10.BorderWidth = 1;
            tcCol10.BorderColor = System.Drawing.Color.White;
            tcCol10.Text = "Posting at State";
            th2.Cells.Add(tcCol10);

            TableHeaderCell tcCol19 = new TableHeaderCell();
            tcCol19.Width = Unit.Percentage(8);
            tcCol19.HorizontalAlign = HorizontalAlign.Center;
            tcCol19.BorderWidth = 1;
            tcCol19.BorderColor = System.Drawing.Color.White;
            tcCol19.Text = "EBCAS Id";
            th2.Cells.Add(tcCol19);


            //TableHeaderCell tcCol20 = new TableHeaderCell();
            //tcCol20.Width = Unit.Percentage(8);
            //tcCol20.HorizontalAlign = HorizontalAlign.Center;
            //tcCol20.BorderWidth = 1;
            //tcCol20.BorderColor = System.Drawing.Color.White;
            //tcCol20.Text = "BCAS Id";
            //th2.Cells.Add(tcCol20);

            TableHeaderCell tcCol5 = new TableHeaderCell();
            tcCol5.Width = Unit.Percentage(8);
            tcCol5.HorizontalAlign = HorizontalAlign.Center;
            tcCol5.BorderWidth = 1;
            tcCol5.BorderColor = System.Drawing.Color.White;
            tcCol5.Text = "Demand Note No.";
            th2.Cells.Add(tcCol5);

            TableHeaderCell tcCol21 = new TableHeaderCell();
            tcCol21.Width = Unit.Percentage(8);
            tcCol21.HorizontalAlign = HorizontalAlign.Center;
            tcCol21.BorderWidth = 1;
            tcCol21.BorderColor = System.Drawing.Color.White;
            tcCol21.Text = "Fee Amount";
            th2.Cells.Add(tcCol21);

            TableHeaderCell tcCol22 = new TableHeaderCell();
            tcCol22.Width = Unit.Percentage(8);
            tcCol22.HorizontalAlign = HorizontalAlign.Center;
            tcCol22.BorderWidth = 1;
            tcCol22.BorderColor = System.Drawing.Color.White;
            tcCol22.Text = "Service Tax";
            th2.Cells.Add(tcCol22);

            TableHeaderCell tcCol23 = new TableHeaderCell();
            tcCol23.Width = Unit.Percentage(8);
            tcCol23.HorizontalAlign = HorizontalAlign.Center;
            tcCol23.BorderWidth = 1;
            tcCol23.BorderColor = System.Drawing.Color.White;
            tcCol23.Text = "Total Fee Amount";
            th2.Cells.Add(tcCol23);

            TableHeaderCell tcCol24 = new TableHeaderCell();
            tcCol24.Width = Unit.Percentage(8);
            tcCol24.HorizontalAlign = HorizontalAlign.Center;
            tcCol24.BorderWidth = 1;
            tcCol24.BorderColor = System.Drawing.Color.White;
            tcCol24.Text = "Payment Date";
            th2.Cells.Add(tcCol24);

            TableHeaderCell tcCol25 = new TableHeaderCell();
            tcCol25.Width = Unit.Percentage(8);
            tcCol25.HorizontalAlign = HorizontalAlign.Center;
            tcCol25.BorderWidth = 1;
            tcCol25.BorderColor = System.Drawing.Color.White;
            tcCol25.Text = "Exam location";
            th2.Cells.Add(tcCol25);

            TableHeaderCell tcCol25a = new TableHeaderCell();
            tcCol25a.Width = Unit.Percentage(8);
            tcCol25a.HorizontalAlign = HorizontalAlign.Center;
            tcCol25a.BorderWidth = 1;
            tcCol25a.BorderColor = System.Drawing.Color.White;
            tcCol25a.Text = "";
            tcCol25a.ColumnSpan = 8;
            th2.Cells.Add(tcCol25a);

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
            tcCol12.Text = "Course Name";
            tcCol12.BorderWidth = 1;
            tcCol12.BorderColor = System.Drawing.Color.White;
            th2.Cells.Add(tcCol12);

            TableHeaderCell tcCol13 = new TableHeaderCell();
            //tcCol13.ColumnSpan = 2;
            tcCol13.Width = Unit.Percentage(15);
            tcCol13.HorizontalAlign = HorizontalAlign.Center;
            tcCol13.Text = "Payee Organisation";
            tcCol13.BorderWidth = 1;
            tcCol13.BorderColor = System.Drawing.Color.White;
            th2.Cells.Add(tcCol13);

            TableHeaderCell tcCol14 = new TableHeaderCell();
            // tcCol14.ColumnSpan = 2;
            tcCol14.Width = Unit.Percentage(15);
            tcCol14.HorizontalAlign = HorizontalAlign.Center;
            tcCol14.Text = "Sponsoring Organisation";
            tcCol14.BorderWidth = 1;
            tcCol14.BorderColor = System.Drawing.Color.White;
            th2.Cells.Add(tcCol14);

            TableHeaderCell tcCol15a = new TableHeaderCell();
            // tcCol15.ColumnSpan = 2;
            tcCol15a.Width = Unit.Percentage(15);
            tcCol15a.HorizontalAlign = HorizontalAlign.Center;
            tcCol15a.Text = "Employee ID";
            tcCol15a.BorderWidth = 1;
            tcCol15a.BorderColor = System.Drawing.Color.White;
            th2.Cells.Add(tcCol15a);

            TableHeaderCell tcCol15 = new TableHeaderCell();
            // tcCol15.ColumnSpan = 2;
            tcCol15.Width = Unit.Percentage(15);
            tcCol15.HorizontalAlign = HorizontalAlign.Center;
            tcCol15.Text = "Registration No.";
            tcCol15.BorderWidth = 1;
            tcCol15.BorderColor = System.Drawing.Color.White;
            th2.Cells.Add(tcCol15);

            TableHeaderCell tcCol16 = new TableHeaderCell();
            //tcCol16.ColumnSpan = 2;
            tcCol16.Width = Unit.Percentage(15);
            tcCol16.HorizontalAlign = HorizontalAlign.Center;
            tcCol16.Text = "Name";
            tcCol16.BorderWidth = 1;
            tcCol16.BorderColor = System.Drawing.Color.White;
            th2.Cells.Add(tcCol16);

            TableHeaderCell tcCol17 = new TableHeaderCell();
            //tcCol16.ColumnSpan = 2;
            tcCol17.Width = Unit.Percentage(15);
            tcCol17.HorizontalAlign = HorizontalAlign.Center;
            tcCol17.Text = "Gender";
            tcCol17.BorderWidth = 1;
            tcCol17.BorderColor = System.Drawing.Color.White;
            th2.Cells.Add(tcCol17);

            TableHeaderCell tcCol18a = new TableHeaderCell();
            //tcCol16.ColumnSpan = 2;
            tcCol18a.Width = Unit.Percentage(15);
            tcCol18a.HorizontalAlign = HorizontalAlign.Center;
            tcCol18a.Text = "Designation";
            tcCol18a.BorderWidth = 1;
            tcCol18a.BorderColor = System.Drawing.Color.White;
            th2.Cells.Add(tcCol18a);

            TableHeaderCell tcCol18 = new TableHeaderCell();
            //tcCol16.ColumnSpan = 2;
            tcCol18.Width = Unit.Percentage(15);
            tcCol18.HorizontalAlign = HorizontalAlign.Center;
            tcCol18.Text = "Date Of Birth";
            tcCol18.BorderWidth = 1;
            tcCol18.BorderColor = System.Drawing.Color.White;
            th2.Cells.Add(tcCol18);

            TableHeaderCell tcCol3 = new TableHeaderCell();
            tcCol3.Width = Unit.Percentage(8);
            tcCol3.HorizontalAlign = HorizontalAlign.Center;
            tcCol3.Text = "Email ";
            tcCol3.BorderWidth = 1;
            tcCol3.BorderColor = System.Drawing.Color.White;
            th2.Cells.Add(tcCol3);

            TableHeaderCell tcCol6 = new TableHeaderCell();
            tcCol6.Width = Unit.Percentage(8);
            tcCol6.HorizontalAlign = HorizontalAlign.Center;
            tcCol6.BorderWidth = 1;
            tcCol6.BorderColor = System.Drawing.Color.White;
            tcCol6.Text = "Mobile";
            th2.Cells.Add(tcCol6);

            TableHeaderCell tcCol7 = new TableHeaderCell();
            tcCol7.Width = Unit.Percentage(8);
            tcCol7.HorizontalAlign = HorizontalAlign.Center;
            tcCol7.BorderWidth = 1;
            tcCol7.BorderColor = System.Drawing.Color.White;
            tcCol7.Text = "Educational Qualification";
            th2.Cells.Add(tcCol7);

            TableHeaderCell tcCol9 = new TableHeaderCell();
            tcCol9.Width = Unit.Percentage(8);
            tcCol9.HorizontalAlign = HorizontalAlign.Center;
            tcCol9.BorderWidth = 1;
            tcCol9.BorderColor = System.Drawing.Color.White;
            tcCol9.Text = "Posting at City";
            th2.Cells.Add(tcCol9);

            TableHeaderCell tcCol10 = new TableHeaderCell();
            tcCol10.Width = Unit.Percentage(8);
            tcCol10.HorizontalAlign = HorizontalAlign.Center;
            tcCol10.BorderWidth = 1;
            tcCol10.BorderColor = System.Drawing.Color.White;
            tcCol10.Text = "Posting at State";
            th2.Cells.Add(tcCol10);

            TableHeaderCell tcCol19 = new TableHeaderCell();
            tcCol19.Width = Unit.Percentage(8);
            tcCol19.HorizontalAlign = HorizontalAlign.Center;
            tcCol19.BorderWidth = 1;
            tcCol19.BorderColor = System.Drawing.Color.White;
            tcCol19.Text = "EBCAS Id";
            th2.Cells.Add(tcCol19);


            //TableHeaderCell tcCol20 = new TableHeaderCell();
            //tcCol20.Width = Unit.Percentage(8);
            //tcCol20.HorizontalAlign = HorizontalAlign.Center;
            //tcCol20.BorderWidth = 1;
            //tcCol20.BorderColor = System.Drawing.Color.White;
            //tcCol20.Text = "BCAS Id";
            //th2.Cells.Add(tcCol20);

            TableHeaderCell tcCol5 = new TableHeaderCell();
            tcCol5.Width = Unit.Percentage(8);
            tcCol5.HorizontalAlign = HorizontalAlign.Center;
            tcCol5.BorderWidth = 1;
            tcCol5.BorderColor = System.Drawing.Color.White;
            tcCol5.Text = "Demand Note No.";
            th2.Cells.Add(tcCol5);

            TableHeaderCell tcCol21 = new TableHeaderCell();
            tcCol21.Width = Unit.Percentage(8);
            tcCol21.HorizontalAlign = HorizontalAlign.Center;
            tcCol21.BorderWidth = 1;
            tcCol21.BorderColor = System.Drawing.Color.White;
            tcCol21.Text = "Fee Amount";
            th2.Cells.Add(tcCol21);

            TableHeaderCell tcCol22 = new TableHeaderCell();
            tcCol22.Width = Unit.Percentage(8);
            tcCol22.HorizontalAlign = HorizontalAlign.Center;
            tcCol22.BorderWidth = 1;
            tcCol22.BorderColor = System.Drawing.Color.White;
            tcCol22.Text = "Service Tax";
            th2.Cells.Add(tcCol22);

            TableHeaderCell tcCol23 = new TableHeaderCell();
            tcCol23.Width = Unit.Percentage(8);
            tcCol23.HorizontalAlign = HorizontalAlign.Center;
            tcCol23.BorderWidth = 1;
            tcCol23.BorderColor = System.Drawing.Color.White;
            tcCol23.Text = "Total Fee Amount";
            th2.Cells.Add(tcCol23);

            TableHeaderCell tcCol24 = new TableHeaderCell();
            tcCol24.Width = Unit.Percentage(8);
            tcCol24.HorizontalAlign = HorizontalAlign.Center;
            tcCol24.BorderWidth = 1;
            tcCol24.BorderColor = System.Drawing.Color.White;
            tcCol24.Text = "Payment Date";
            th2.Cells.Add(tcCol24);

            TableHeaderCell tcCol25 = new TableHeaderCell();
            tcCol25.Width = Unit.Percentage(8);
            tcCol25.HorizontalAlign = HorizontalAlign.Center;
            tcCol25.BorderWidth = 1;
            tcCol25.BorderColor = System.Drawing.Color.White;
            tcCol25.Text = "Exam location";
            th2.Cells.Add(tcCol25);

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

            SqlCommand scCommand = new SqlCommand("repCandidatesDataWithPayment", new SqlConnection(con.ConnectionString));
            scCommand.CommandType = CommandType.StoredProcedure;
            string choice = rdoOption.SelectedValue.ToString();


            if (rdoOption.SelectedValue == "S")
                scCommand.Parameters.Add("@OrgLoc", SqlDbType.BigInt).Value = Convert.ToInt32(ddlOrganisation.SelectedValue);
            if (rdoOption.SelectedValue == "L")
                scCommand.Parameters.Add("@OrgLoc", SqlDbType.BigInt).Value = Convert.ToInt32(ddlExamLocation.SelectedValue);

            scCommand.Parameters.Add("@ReportChoice", SqlDbType.VarChar, 1).Value = choice;
            scCommand.Parameters.Add("@ScheduleID", SqlDbType.BigInt).Value = Convert.ToInt32(ddlSchedule.SelectedValue);

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
                tdRow1.Text = ds.Tables[0].Rows[j]["Course"].ToString();
                tdRow1.HorizontalAlign = HorizontalAlign.Left;
                tdRow1.BorderWidth = 1;
                tdRow1.BorderColor = System.Drawing.Color.White;
                tr.Cells.Add(tdRow1);

                TableCell tdRow2 = new TableCell();
                tdRow2.Width = Unit.Percentage(8);
                tdRow2.Text = ds.Tables[0].Rows[j]["Payee Org Name"].ToString();
                tdRow2.HorizontalAlign = HorizontalAlign.Center;
                tdRow2.BorderWidth = 1;
                tdRow2.BorderColor = System.Drawing.Color.White;
                tr.Cells.Add(tdRow2);

                TableCell tdRow3 = new TableCell();
                tdRow3.Width = Unit.Percentage(8);
                tdRow3.Text = ds.Tables[0].Rows[j]["Sponsoring Org Name"].ToString();
                tdRow3.HorizontalAlign = HorizontalAlign.Center;
                tdRow3.BorderWidth = 1;
                tdRow3.BorderColor = System.Drawing.Color.White;
                tr.Cells.Add(tdRow3);


                TableCell tdRow15 = new TableCell();
                tdRow15.Width = Unit.Percentage(8);
                tdRow15.Text = ds.Tables[0].Rows[j]["Employee_id"].ToString();
                tdRow15.HorizontalAlign = HorizontalAlign.Center;
                tdRow15.BorderWidth = 1;
                tdRow15.BorderColor = System.Drawing.Color.White;
                tr.Cells.Add(tdRow15);

                TableCell tdRow14 = new TableCell();
                tdRow14.Width = Unit.Percentage(8);
                tdRow14.Text = ds.Tables[0].Rows[j]["Regn No."].ToString();
                tdRow14.HorizontalAlign = HorizontalAlign.Center;
                tdRow14.BorderWidth = 1;
                tdRow14.BorderColor = System.Drawing.Color.White;
                tr.Cells.Add(tdRow14);

                TableCell tdRow16 = new TableCell();
                tdRow16.Width = Unit.Percentage(8);
                tdRow16.Text = ds.Tables[0].Rows[j]["Candidate_Name"].ToString();
                tdRow16.HorizontalAlign = HorizontalAlign.Center;
                tdRow16.BorderWidth = 1;
                tdRow16.BorderColor = System.Drawing.Color.White;
                tr.Cells.Add(tdRow16);

                TableCell tdRow4 = new TableCell();
                tdRow4.Width = Unit.Percentage(8);
                tdRow4.Text = ds.Tables[0].Rows[j]["Gender"].ToString();
                tdRow4.HorizontalAlign = HorizontalAlign.Center;
                tdRow4.BorderWidth = 1;
                tdRow4.BorderColor = System.Drawing.Color.White;
                tr.Cells.Add(tdRow4);

                TableCell tdRow21 = new TableCell();
                tdRow21.Width = Unit.Percentage(8);
                tdRow21.Text = ds.Tables[0].Rows[j]["Designation"].ToString();
                tdRow21.HorizontalAlign = HorizontalAlign.Center;
                tdRow21.BorderWidth = 1;
                tdRow21.BorderColor = System.Drawing.Color.White;
                tr.Cells.Add(tdRow21);


                TableCell tdRow22 = new TableCell();
                tdRow22.Width = Unit.Percentage(8);
                tdRow22.Text = ds.Tables[0].Rows[j]["Date Of Birth"].ToString();
                tdRow22.HorizontalAlign = HorizontalAlign.Center;
                tdRow22.BorderWidth = 1;
                tdRow22.BorderColor = System.Drawing.Color.White;
                tr.Cells.Add(tdRow22);

                TableCell tdRow23 = new TableCell();
                tdRow23.Width = Unit.Percentage(8);
                tdRow23.Text = ds.Tables[0].Rows[j]["Email"].ToString();
                tdRow23.HorizontalAlign = HorizontalAlign.Center;
                tdRow23.BorderWidth = 1;
                tdRow23.BorderColor = System.Drawing.Color.White;
                tr.Cells.Add(tdRow23);

                TableCell tdRow24 = new TableCell();
                tdRow24.Width = Unit.Percentage(8);
                tdRow24.Text = ds.Tables[0].Rows[j]["Mobile"].ToString();
                tdRow24.HorizontalAlign = HorizontalAlign.Center;
                tdRow24.BorderWidth = 1;
                tdRow24.BorderColor = System.Drawing.Color.White;
                tr.Cells.Add(tdRow24);

                TableCell tdRow25 = new TableCell();
                tdRow25.Width = Unit.Percentage(8);
                tdRow25.Text = ds.Tables[0].Rows[j]["Qualification"].ToString();
                tdRow25.HorizontalAlign = HorizontalAlign.Center;
                tdRow25.BorderWidth = 1;
                tdRow25.BorderColor = System.Drawing.Color.White;
                tr.Cells.Add(tdRow25);

                TableCell tdRow26 = new TableCell();
                tdRow26.Width = Unit.Percentage(8);
                tdRow26.Text = ds.Tables[0].Rows[j]["Posting City"].ToString();
                tdRow26.HorizontalAlign = HorizontalAlign.Center;
                tdRow26.BorderWidth = 1;
                tdRow26.BorderColor = System.Drawing.Color.White;
                tr.Cells.Add(tdRow26);

                TableCell tdRow27 = new TableCell();
                tdRow27.Width = Unit.Percentage(8);
                tdRow27.Text = ds.Tables[0].Rows[j]["Posting State"].ToString();
                tdRow27.HorizontalAlign = HorizontalAlign.Center;
                tdRow27.BorderWidth = 1;
                tdRow27.BorderColor = System.Drawing.Color.White;
                tr.Cells.Add(tdRow27);

                TableCell tdRow28 = new TableCell();
                tdRow28.Width = Unit.Percentage(8);
                tdRow28.Text = "" + ds.Tables[0].Rows[j]["EBCASId"].ToString();
                tdRow28.HorizontalAlign = HorizontalAlign.Center;
                tdRow28.BorderWidth = 1;
                tdRow28.BorderColor = System.Drawing.Color.White;
                tr.Cells.Add(tdRow28);

                //TableCell tdRow29 = new TableCell();
                //tdRow29.Width = Unit.Percentage(8);
                //tdRow29.Text = ds.Tables[0].Rows[j]["BCASId"].ToString();
                //tdRow29.HorizontalAlign = HorizontalAlign.Center;
                //tdRow29.BorderWidth = 1;
                //tdRow29.BorderColor = System.Drawing.Color.White;
                //tr.Cells.Add(tdRow29);

                TableCell tdRow30 = new TableCell();
                tdRow30.Width = Unit.Percentage(8);
                tdRow30.Text = ds.Tables[0].Rows[j]["Demand Note No."].ToString();
                tdRow30.HorizontalAlign = HorizontalAlign.Center;
                tdRow30.BorderWidth = 1;
                tdRow30.BorderColor = System.Drawing.Color.White;
                tr.Cells.Add(tdRow30);

                TableCell tdRow31 = new TableCell();
                tdRow31.Width = Unit.Percentage(8);
                tdRow31.Text = ds.Tables[0].Rows[j]["Amount"].ToString();
                tdRow31.HorizontalAlign = HorizontalAlign.Center;
                tdRow31.BorderWidth = 1;
                tdRow31.BorderColor = System.Drawing.Color.White;
                tr.Cells.Add(tdRow31);

                TableCell tdRow32 = new TableCell();
                tdRow32.Width = Unit.Percentage(8);
                tdRow32.Text = ds.Tables[0].Rows[j]["Service Tax"].ToString();
                tdRow32.HorizontalAlign = HorizontalAlign.Center;
                tdRow32.BorderWidth = 1;
                tdRow32.BorderColor = System.Drawing.Color.White;
                tr.Cells.Add(tdRow32);

                TableCell tdRow33 = new TableCell();
                tdRow33.Width = Unit.Percentage(8);
                tdRow33.Text = ds.Tables[0].Rows[j]["Total Fees"].ToString();
                tdRow33.HorizontalAlign = HorizontalAlign.Center;
                tdRow33.BorderWidth = 1;
                tdRow33.BorderColor = System.Drawing.Color.White;
                tr.Cells.Add(tdRow33);

                TableCell tdRow34 = new TableCell();
                tdRow34.Width = Unit.Percentage(8);
                tdRow34.Text = ds.Tables[0].Rows[j]["Payment Date"].ToString();
                tdRow34.HorizontalAlign = HorizontalAlign.Center;
                tdRow34.BorderWidth = 1;
                tdRow34.BorderColor = System.Drawing.Color.White;
                tr.Cells.Add(tdRow34);

                TableCell tdRow35 = new TableCell();
                tdRow35.Width = Unit.Percentage(8);
                tdRow35.Text = ds.Tables[0].Rows[j]["Exam location"].ToString();
                tdRow35.HorizontalAlign = HorizontalAlign.Center;
                tdRow35.BorderWidth = 1;
                tdRow35.BorderColor = System.Drawing.Color.White;
                tr.Cells.Add(tdRow35);
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

            SqlCommand scCommand = new SqlCommand("repCandidatesDataWithPayment", new SqlConnection(con.ConnectionString));
            scCommand.CommandType = CommandType.StoredProcedure;
            string choice = rdoOption.SelectedValue.ToString();


            if (rdoOption.SelectedValue == "S")
                scCommand.Parameters.Add("@OrgLoc", SqlDbType.BigInt).Value = Convert.ToInt32(ddlOrganisation.SelectedValue);
            if (rdoOption.SelectedValue == "L")
                scCommand.Parameters.Add("@OrgLoc", SqlDbType.BigInt).Value = Convert.ToInt32(ddlExamLocation.SelectedValue);

            scCommand.Parameters.Add("@ReportChoice", SqlDbType.VarChar, 1).Value = choice;
            scCommand.Parameters.Add("@ScheduleID", SqlDbType.BigInt).Value = Convert.ToInt32(ddlSchedule.SelectedValue);

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
                tdRow1.Text = ds.Tables[0].Rows[j]["Course"].ToString();
                tdRow1.HorizontalAlign = HorizontalAlign.Left;
                tdRow1.BorderWidth = 1;
                tdRow1.BorderColor = System.Drawing.Color.White;
                tr.Cells.Add(tdRow1);

                TableCell tdRow2 = new TableCell();
                tdRow2.Width = Unit.Percentage(8);
                tdRow2.Text = ds.Tables[0].Rows[j]["Payee Org Name"].ToString();
                tdRow2.HorizontalAlign = HorizontalAlign.Center;
                tdRow2.BorderWidth = 1;
                tdRow2.BorderColor = System.Drawing.Color.White;
                tr.Cells.Add(tdRow2);

                TableCell tdRow3 = new TableCell();
                tdRow3.Width = Unit.Percentage(8);
                tdRow3.Text = ds.Tables[0].Rows[j]["Sponsoring Org Name"].ToString();
                tdRow3.HorizontalAlign = HorizontalAlign.Center;
                tdRow3.BorderWidth = 1;
                tdRow3.BorderColor = System.Drawing.Color.White;
                tr.Cells.Add(tdRow3);


                TableCell tdRow15 = new TableCell();
                tdRow15.Width = Unit.Percentage(8);
                tdRow15.Text = ds.Tables[0].Rows[j]["Employee_id"].ToString();
                tdRow15.HorizontalAlign = HorizontalAlign.Center;
                tdRow15.BorderWidth = 1;
                tdRow15.BorderColor = System.Drawing.Color.White;
                tr.Cells.Add(tdRow15);

                TableCell tdRow14 = new TableCell();
                tdRow14.Width = Unit.Percentage(8);
                tdRow14.Text = ds.Tables[0].Rows[j]["Regn No."].ToString();
                tdRow14.HorizontalAlign = HorizontalAlign.Center;
                tdRow14.BorderWidth = 1;
                tdRow14.BorderColor = System.Drawing.Color.White;
                tr.Cells.Add(tdRow14);

                TableCell tdRow16 = new TableCell();
                tdRow16.Width = Unit.Percentage(8);
                tdRow16.Text = ds.Tables[0].Rows[j]["Candidate_Name"].ToString();
                tdRow16.HorizontalAlign = HorizontalAlign.Center;
                tdRow16.BorderWidth = 1;
                tdRow16.BorderColor = System.Drawing.Color.White;
                tr.Cells.Add(tdRow16);

                TableCell tdRow4 = new TableCell();
                tdRow4.Width = Unit.Percentage(8);
                tdRow4.Text = ds.Tables[0].Rows[j]["Gender"].ToString();
                tdRow4.HorizontalAlign = HorizontalAlign.Center;
                tdRow4.BorderWidth = 1;
                tdRow4.BorderColor = System.Drawing.Color.White;
                tr.Cells.Add(tdRow4);

                TableCell tdRow21 = new TableCell();
                tdRow21.Width = Unit.Percentage(8);
                tdRow21.Text = ds.Tables[0].Rows[j]["Designation"].ToString();
                tdRow21.HorizontalAlign = HorizontalAlign.Center;
                tdRow21.BorderWidth = 1;
                tdRow21.BorderColor = System.Drawing.Color.White;
                tr.Cells.Add(tdRow21);


                TableCell tdRow22 = new TableCell();
                tdRow22.Width = Unit.Percentage(8);
                tdRow22.Text = ds.Tables[0].Rows[j]["Date Of Birth"].ToString();
                tdRow22.HorizontalAlign = HorizontalAlign.Center;
                tdRow22.BorderWidth = 1;
                tdRow22.BorderColor = System.Drawing.Color.White;
                tr.Cells.Add(tdRow22);

                TableCell tdRow23 = new TableCell();
                tdRow23.Width = Unit.Percentage(8);
                tdRow23.Text = ds.Tables[0].Rows[j]["Email"].ToString();
                tdRow23.HorizontalAlign = HorizontalAlign.Center;
                tdRow23.BorderWidth = 1;
                tdRow23.BorderColor = System.Drawing.Color.White;
                tr.Cells.Add(tdRow23);

                TableCell tdRow24 = new TableCell();
                tdRow24.Width = Unit.Percentage(8);
                tdRow24.Text = ds.Tables[0].Rows[j]["Mobile"].ToString();
                tdRow24.HorizontalAlign = HorizontalAlign.Center;
                tdRow24.BorderWidth = 1;
                tdRow24.BorderColor = System.Drawing.Color.White;
                tr.Cells.Add(tdRow24);

                TableCell tdRow25 = new TableCell();
                tdRow25.Width = Unit.Percentage(8);
                tdRow25.Text = ds.Tables[0].Rows[j]["Qualification"].ToString();
                tdRow25.HorizontalAlign = HorizontalAlign.Center;
                tdRow25.BorderWidth = 1;
                tdRow25.BorderColor = System.Drawing.Color.White;
                tr.Cells.Add(tdRow25);

                TableCell tdRow26 = new TableCell();
                tdRow26.Width = Unit.Percentage(8);
                tdRow26.Text = ds.Tables[0].Rows[j]["Posting City"].ToString();
                tdRow26.HorizontalAlign = HorizontalAlign.Center;
                tdRow26.BorderWidth = 1;
                tdRow26.BorderColor = System.Drawing.Color.White;
                tr.Cells.Add(tdRow26);

                TableCell tdRow27 = new TableCell();
                tdRow27.Width = Unit.Percentage(8);
                tdRow27.Text = ds.Tables[0].Rows[j]["Posting State"].ToString();
                tdRow27.HorizontalAlign = HorizontalAlign.Center;
                tdRow27.BorderWidth = 1;
                tdRow27.BorderColor = System.Drawing.Color.White;
                tr.Cells.Add(tdRow27);

                TableCell tdRow28 = new TableCell();
                tdRow28.Width = Unit.Percentage(8);
                tdRow28.Attributes.CssStyle.Add("mso-number-format", "\\@");
                tdRow28.Text = ds.Tables[0].Rows[j]["EBCASId"].ToString();
                tdRow28.HorizontalAlign = HorizontalAlign.Center;
                tdRow28.BorderWidth = 1;
                tdRow28.BorderColor = System.Drawing.Color.White;
                // tdRow28.CssClass = "text";
                tr.Cells.Add(tdRow28);

                //TableCell tdRow29 = new TableCell();
                //tdRow29.Width = Unit.Percentage(8);
                //tdRow29.Text = ds.Tables[0].Rows[j]["BCASId"].ToString();
                //tdRow29.Attributes.CssStyle.Add("mso-number-format", "\\@");
                //tdRow29.BorderWidth = 1;
                //tdRow29.BorderColor = System.Drawing.Color.White;
                //tr.Cells.Add(tdRow29);

                TableCell tdRow30 = new TableCell();
                tdRow30.Width = Unit.Percentage(8);
                tdRow30.Text = ds.Tables[0].Rows[j]["Demand Note No."].ToString();
                tdRow30.HorizontalAlign = HorizontalAlign.Center;
                tdRow30.BorderWidth = 1;
                tdRow30.BorderColor = System.Drawing.Color.White;
                tr.Cells.Add(tdRow30);

                TableCell tdRow31 = new TableCell();
                tdRow31.Width = Unit.Percentage(8);
                tdRow31.Text = ds.Tables[0].Rows[j]["Amount"].ToString();
                tdRow31.HorizontalAlign = HorizontalAlign.Center;
                tdRow31.BorderWidth = 1;
                tdRow31.BorderColor = System.Drawing.Color.White;
                tr.Cells.Add(tdRow31);

                TableCell tdRow32 = new TableCell();
                tdRow32.Width = Unit.Percentage(8);
                tdRow32.Text = ds.Tables[0].Rows[j]["Service Tax"].ToString();
                tdRow32.HorizontalAlign = HorizontalAlign.Center;
                tdRow32.BorderWidth = 1;
                tdRow32.BorderColor = System.Drawing.Color.White;
                tr.Cells.Add(tdRow32);

                TableCell tdRow33 = new TableCell();
                tdRow33.Width = Unit.Percentage(8);
                tdRow33.Text = ds.Tables[0].Rows[j]["Total Fees"].ToString();
                tdRow33.HorizontalAlign = HorizontalAlign.Center;
                tdRow33.BorderWidth = 1;
                tdRow33.BorderColor = System.Drawing.Color.White;
                tr.Cells.Add(tdRow33);

                TableCell tdRow34 = new TableCell();
                tdRow34.Width = Unit.Percentage(8);
                tdRow34.Text = ds.Tables[0].Rows[j]["Payment Date"].ToString();
                tdRow34.HorizontalAlign = HorizontalAlign.Center;
                tdRow34.BorderWidth = 1;
                tdRow34.BorderColor = System.Drawing.Color.White;
                tr.Cells.Add(tdRow34);

                TableCell tdRow35 = new TableCell();
                tdRow35.Width = Unit.Percentage(8);
                tdRow35.Text = ds.Tables[0].Rows[j]["Exam location"].ToString();
                tdRow35.HorizontalAlign = HorizontalAlign.Center;
                tdRow35.BorderWidth = 1;
                tdRow35.BorderColor = System.Drawing.Color.White;
                tr.Cells.Add(tdRow35);

                TableCell tdRow35a = new TableCell();
                tdRow35a.Width = Unit.Percentage(8);
                tdRow35a.ColumnSpan = 7;
                // tdRow35a.Text = ds.Tables[0].Rows[j]["Exam location"].ToString();
                tdRow35a.HorizontalAlign = HorizontalAlign.Center;
                tdRow35a.BorderWidth = 1;
                tdRow35a.BorderColor = System.Drawing.Color.White;
                tr.Cells.Add(tdRow35a);

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
            Response.AddHeader("content-disposition", "attachment;filename=CandidateDetails.pdf");
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
            Response.AddHeader("content-disposition", "attachment;filename=CandidateDetails.xls");
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

