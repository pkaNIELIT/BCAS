using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Threading;
using BCASClassLibrary;
using System.Data.SqlClient;
using System.Text;

using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;

public partial class Forms_DetailsForms_BCAS_DwnProformaInvoice : System.Web.UI.Page
{
    TYP_BCAS_Approval objBCAS = new TYP_BCAS_Approval();
    Int64 DemandNoteId = 0;
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

                using (DataTable dt = objBCAS.getBCASCourseSchedule())
                {
                    if (dt.Rows.Count > 0)
                    {
                        ddlSchedule.DataSource = dt;
                        ddlSchedule.DataTextField = "Course_Schedule"; // This is text displayed
                        ddlSchedule.DataValueField = "Schedule_ID"; // This is the value returned
                        ddlSchedule.DataBind();
                    }

                }
              
                using (DataTable dtOrg = objBCAS.getGetAllOrganisation())
                {
                    if (dtOrg.Rows.Count > 0)
                    {
                        ddlOrganisation.DataSource = dtOrg;
                        ddlOrganisation.DataValueField = "Organisation_Code";
                        ddlOrganisation.DataTextField = "Organisation_Name";
                        ddlOrganisation.DataBind();

                        ddlOrganisation.SelectedValue = Session["UserOrg"].ToString();
                    }

                }
                //Added for biller Organisation
                using (DataSet dtOrg = TYP_PAYEE_ORGANISATION_MASTER.GetAllPayee_Organisation_Master())
                {
                    if (dtOrg.Tables[0].Rows.Count > 0)
                    {
                        ddlBillerOrganisation.DataSource = dtOrg;
                        ddlBillerOrganisation.DataValueField = "Payee_Organisation_Code";
                        ddlBillerOrganisation.DataTextField = "Payee_Organisation_Name";
                        ddlBillerOrganisation.DataBind();


                        ddlBillerOrganisation.SelectedValue = Session["UserOrg"].ToString();
                    }

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

        scheduleID = Convert.ToInt32(ddlSchedule.SelectedValue);

        Organisation = Convert.ToInt32(ddlBillerOrganisation.SelectedValue);

        gvAll.Visible = true;

        using (DataTable dt = objBCAS.getBCASApporvedCandidates(scheduleID, Organisation))
        {
            
            gvAll.DataSource = dt;
            gvAll.DataBind();
            
        }


    }
   
    protected void OnPaging(object sender, GridViewPageEventArgs e)
    {

        gvAll.PageIndex = e.NewPageIndex;        
        BindGrid();       
      
    }


    protected void btnUpdateStatus_Click(object sender, EventArgs e)
    {
        try
        {
            int sponsor, scheduleID, Organisation, demandnoteid;
             string createdby = "";
            scheduleID = Convert.ToInt32(ddlSchedule.SelectedValue);
            sponsor = Convert.ToInt32(ddlOrganisation.SelectedValue);
            Organisation = Convert.ToInt32(ddlBillerOrganisation.SelectedValue);

            if(sponsor<=0 )
            {
                ShowAlertMessage("Please select a valid sponsoring organisation.");
                return;
            }
            if (Organisation <= 0)
            {
                ShowAlertMessage("Please select a valid Payee organisation.");
                return;
            }

            demandnoteid = Convert.ToInt32(ddlDemandNote.SelectedValue);
            createdby = Session["UserId"].ToString();
            using (DataTable dt = objBCAS.GetProformaInvoiceData(sponsor,scheduleID, Organisation, demandnoteid, createdby))
            {
                if (dt.Rows.Count > 0)
                {
                    for (int k = 0; k < dt.Rows.Count; k++)
                    {
                        using (StringWriter sw = new StringWriter())
                        {
                            using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                            {
                                string code = dt.Rows[k]["smGSTIN"].ToString().Length>2? dt.Rows[k]["smGSTIN"].ToString().Substring(0, 2): dt.Rows[k]["smGSTIN"].ToString();
                                StringBuilder sb = new StringBuilder();
                                //Generate Proforma Invoice
                                sb.Append("<br/><br/><table width='95%' cellspacing='1' cellpadding='2' border = '1'>");
                                sb.Append("<tr><td align='center' style='background-color: #18B5F0' colspan = '2'><b>PERFORMA INVOICE</b></td></tr>");
                                sb.Append("<tr><td colspan = '2'></td></tr>");
                                //Row 1 start initial
                                sb.Append("<tr><td><b>" + dt.Rows[k]["Name"].ToString() + " </b><br/>");
                                sb.Append("" + dt.Rows[k]["Address1"].ToString() + " <br/>" + dt.Rows[k]["Address2"].ToString() + ",<br/>" + dt.Rows[k]["Address3"].ToString() + ",<br/>" + dt.Rows[k]["NState"].ToString() + "-" + dt.Rows[k]["Pin"].ToString() + "<br/>GSTIN : " + dt.Rows[k]["GSTIN"].ToString() + "");
                                sb.Append("</td><td ><table width='100%' cellspacing='0' cellpadding='2' border = '1'> ");
                                //Row 1 start
                                sb.Append("<tr><td>Invoice No. :<br/>");
                                sb.Append(dt.Rows[k]["ProformaInvoiceNo"].ToString());//DB VALUE
                                sb.Append("</td>");

                                sb.Append("<td>Dated:<br/>");
                                sb.Append(DateTime.Now.ToString("dd/MM/yyyy"));
                                sb.Append("</td>");
                                sb.Append("</td></tr>");
                                //Row 1 end

                                //Row 2 start
                                sb.Append("<tr><td >Delivery Note :<br/>");
                                sb.Append("<font color='white'>NA </font>");//DB VALUE
                                sb.Append("</td>");

                                sb.Append("<td>Mode/terms of payment<br/>");
                                sb.Append("Online");//DB VALUE
                                sb.Append("</td>");
                                sb.Append("</td></tr>");
                                //Row 2 end

                                //Row 3 start
                                sb.Append("<tr><td>Supplier Ref :<br/>email Ref.");
                                sb.Append("");//DB VALUE
                                sb.Append("</td>");

                                sb.Append("<td>Other Ref.<br/>");
                                sb.Append("<font color='white'>NA </font>");//DB VALUE
                                sb.Append("</td>");
                                sb.Append("</td></tr>");
                                //Row 3 end

                                sb.Append(" </table></td></tr>");

                                // Row 1 intital End

                                // Row 2 intial start ****************************************
                                //Row 1 start initial
                                sb.Append("<tr><td>Party :<br/>");
                                sb.Append("<font size='2'>" +  dt.Rows[k]["Organisation_Address"].ToString() + "</p>GSTIN : " + dt.Rows[k]["smGSTIN"].ToString() + "");//DB VALUE
                                //sb.Append("<br/>State : " + dt.Rows[k]["smState"].ToString() + "  Code: " + dt.Rows[k]["SCODE"].ToString() + "</font>");//DB VALUE
                                sb.Append("<br/>State : " + dt.Rows[k]["smState"].ToString() + "  Code: " + code + "</font>");//DB VALUE

                                //sb.Append("</td><td ><table style='height:100%; width:100%;'  cellspacing='0' cellpadding='8' border = '1'> ");
                                sb.Append("</td><td ><table height='100%'   cellspacing='0' cellpadding='1' border = '1'> ");
                                //Row 1 start
                                sb.Append("<tr><td>Buyer's Order No. :<br/>");
                                sb.Append("<font color='white'>NA </font>");//DB VALUE
                                sb.Append("</td>");

                                sb.Append("<td>Dated:<br/>");
                                //sb.Append(DateTime.Now);
                                sb.Append("<font color='white'>NA </font>");
                                sb.Append("</td>");
                                sb.Append("</td></tr>");
                                //Row 1 end

                                //Row 2 start
                                sb.Append("<tr><td>Dispatch Through<br/>");
                                sb.Append("<font color='white'>NA </font>");//DB VALUE
                                sb.Append("</td>");

                                sb.Append("<td>Destination :<br/>");
                                sb.Append("<font color='white'>NA </font>");//DB VALUE
                                sb.Append("</td>");
                                sb.Append("</td></tr>");
                                //Row 2 end

                                //Row 3 start
                                sb.Append("<tr><td>Reverse Charge :<br/>");
                                sb.Append("NO");//DB VALUE
                                sb.Append("</td>");

                                sb.Append("<td>Place of Supply :<br/>");
                                sb.Append("" + dt.Rows[k]["smState"].ToString() + "");//DB VALUE
                                sb.Append("</td>");
                                sb.Append("</td></tr>");
                                //Row 3 end

                                sb.Append(" </table></td></tr>");

                                //Row2 intial end *******************************

                                //sb.Append("<tr><td colspan = '2'><b>Company Name: </b>");
                                //sb.Append(companyName);
                                //sb.Append("</td></tr>");
                                // part 2 start 
                                sb.Append("<tr><td colspan = '2'>");
                                sb.Append("<table width='100%' cellspacing='0' cellpadding='0' border = '1'> ");

                                sb.Append("<tr align='center'>");
                                sb.Append("<td style='width:5px'>");
                                sb.Append("Sl No.");
                                sb.Append("</td>");
                                sb.Append("<td colspan = '3'>");
                                sb.Append("Particulars");
                                sb.Append("</td>");
                                sb.Append("<td>");
                                sb.Append("SAC Code");
                                sb.Append("</td>");
                                sb.Append("<td><font size='2'>");
                                sb.Append("No. of Candidate");
                                sb.Append("</font></td>");
                                sb.Append("<td colspan = '2'>");
                                sb.Append("Examination fees (per candidate)");
                                sb.Append("</td>");
                                sb.Append("<td colspan = '2'>");
                                sb.Append("Amount (Rs.)");
                                sb.Append("</td>");
                                sb.Append("</tr>");

                                //sb.Append("<tr align='center'>");
                                sb.Append("<tr align='center' valign='top'>");
                                sb.Append("<td style='width:5px'>");
                                sb.Append("1");
                                sb.Append("</td>");
                                sb.Append("<td colspan = '3' valign='top'>");
                                sb.Append("BCAS " + dt.Rows[k]["Course_Name"].ToString() + " Examination (Online) -<br/>");
                                sb.Append(" " + dt.Rows[k]["Date_of_Exam"].ToString() + "");
                                sb.Append("</td>");
                                sb.Append("<td>");
                                sb.Append("" + dt.Rows[k]["SAC_CODE"].ToString() + "");
                                sb.Append("</td>");
                                sb.Append("<td>");
                                sb.Append("" + dt.Rows[k]["NoOfCandidates"].ToString() + "");
                                sb.Append("</td>");
                                sb.Append("<td colspan = '2'>");
                                sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;" + dt.Rows[k]["PerCanFeeAmt"].ToString() + "");
                                string IgstCheck = dt.Rows[k]["IGST_Others"].ToString();

                                if (IgstCheck == "")
                                {
                                    sb.Append("<br/><br/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br/>&nbsp;" + dt.Rows[k]["CGST_Delhi"].ToString() + "<br/>");
                                    sb.Append("&nbsp;&nbsp;&nbsp;" + dt.Rows[k]["SGST_Delhi"].ToString() + "");
                                }
                                else
                                {
                                    //sb.Append("<br/><br/><br/><br/> Add:<br/>IGST @ 18%");
                                    sb.Append("<br/><br/>&nbsp;&nbsp;&nbsp;&nbsp;<br/>&nbsp;&nbsp;&nbsp;&nbsp;" + dt.Rows[k]["IGST_Others"].ToString() + "");
                                }
                                sb.Append("</td>");
                                sb.Append("<td colspan = '2'>");
                                sb.Append("&nbsp;" + dt.Rows[k]["Fee_Amount"].ToString() + "");
                                if (IgstCheck == "")
                                {
                                    //Double servicetaxamount = Convert.ToInt32(dt.Rows[k]["Service_Tax"].ToString());
                                    Double servicetaxamount = Convert.ToDouble(dt.Rows[k]["Service_Tax"].ToString());
                                    sb.Append("<br/><br/><br/>&nbsp;&nbsp;&nbsp;&nbsp;" + (servicetaxamount / 2).ToString() + "<br/>");
                                    sb.Append("&nbsp;&nbsp;&nbsp;" + (servicetaxamount / 2).ToString() + "");
                                }
                                else
                                {
                                    sb.Append("<br/><br/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + dt.Rows[k]["Service_Tax"].ToString() + "");
                                }
                                sb.Append("</td>");
                                sb.Append("</tr>");

                                sb.Append("<tr align='center'>");
                                sb.Append("<td style='width:5px'>");
                                sb.Append("</td>");
                                sb.Append("<td colspan = '3'>");
                                sb.Append("</td>");
                                sb.Append("<td>");
                                sb.Append("<b>Total</b>");
                                sb.Append("</td>");

                                sb.Append("<td>");
                                sb.Append("" + dt.Rows[k]["NoOfCandidates"].ToString() + "");
                                sb.Append("</td>");

                                sb.Append("<td colspan = '2'>");
                                sb.Append("</td>");
                                sb.Append("<td colspan = '2'>");
                                sb.Append("<b>" + dt.Rows[k]["Total_Fee_With_Service_Tax"].ToString() + "</b>");
                                sb.Append("</td>");
                                sb.Append("</tr>");

                                sb.Append(" </table>");
                                sb.Append("</td></tr>");
                                // part 2 End

                                sb.Append("<tr><td colspan = '2'>(Amount in Words)<br/>");
                                Double Totalamount = Convert.ToDouble(dt.Rows[k]["Total_Fee_With_Service_Tax"].ToString());
                                //sb.Append("(Rupees " + ConvertNumbertoWords(Totalamount) + " Only) <br/>"); ConvertAmount
                                sb.Append("( " + ConvertAmount(Totalamount) + ") <br/>");
                                //sb.Append("<p align='right'><u>Company's Bank details:</u> <br/>");
                                //sb.Append("" + dt.Rows[k]["BankHolderName"].ToString() + " <br/>");
                                //sb.Append("" + dt.Rows[k]["BankName"].ToString() + ", " + dt.Rows[k]["BankAddress1"].ToString() + ", " + dt.Rows[k]["BankAddress2"].ToString() + ", " + dt.Rows[k]["bnkState"].ToString() + "  <br/>");
                                //sb.Append("A/c no. : " + dt.Rows[k]["AccountNo"].ToString() + " <br/>");
                                //sb.Append("Account Type : " + dt.Rows[k]["AccountType"].ToString() + " <br/>");
                                //sb.Append("IFSC Code: " + dt.Rows[k]["IFSC_CODE"].ToString() + "</p>");
                                //sb.Append("</td></tr>");

                                //sb.Append("<tr><td colspan = '2'> <br/><u>Declaration</u><br/>");
                                //sb.Append("We declare that all particulars are true and correct.<br/><br/>");                
                                //sb.Append("</td></tr>");

                                sb.Append("<tr align='center'><td colspan = '2'>");
                                sb.Append(" <font size='2'>This receipt is electronically generated and no signature is required.<br/>");
                                sb.Append("SUBJECT TO DELHI JURIDICTION</font>");
                                sb.Append("</td></tr>");

                                sb.Append("</table>");
                                sb.Append("<br />");

                                //Added by Kismat
                                string htmlContent = sb.ToString(); // Store the raw HTML string
                                StringReader sr = new StringReader(htmlContent);
                                Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                                HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                                pdfDoc.Open();
                                htmlparser.Parse(sr);
                                pdfDoc.Close();
                                //Response.ContentType = "text/html";
                                Response.ContentType = "application/pdf";
                                Response.AddHeader("content-disposition", "attachment;filename=ProformaInvoice.pdf");
                                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                                Response.Write(pdfDoc);
                                Response.End();
                            }
                        }

                    }
                }
                else
                {
                    ShowAlertMessage("Records not found!");
                }

            }
        }
        catch (Exception ex)
        {
            ShowAlertMessage("Something is wrong, Please contact administrator!!");
        }
    }
    public static String ConvertAmount(double amount)
    {
        try
        {
            Int64 amount_int = (Int64)amount;
            Int64 amount_dec = (Int64)Math.Round((amount - (double)(amount_int)) * 100);
            if (amount_dec == 0)
            {
                return "Rs. " + ConvertNumbertoWords(amount_int) + " Only.";
            }
            else
            {
                return "Rs. "+ ConvertNumbertoWords(amount_int) + " and " + ConvertNumbertoWords(amount_dec) + " Paisa Only.";
            }
        }
        catch (Exception e)
        {
            // TODO: handle exception  
        }
        return "";
    }  

    public static string ConvertNumbertoWords(Int64 number)
    {
        if (number == 0)
            return "Zero";
        if (number < 0)
            return "minus " + ConvertNumbertoWords(Math.Abs(number));
        string words = "";

        if ((number / 1000000000) > 0)
        {
            words += ConvertNumbertoWords(number / 1000000000) + " Billion ";
            number %= 1000000000;
        }

        if ((number / 10000000) > 0)
        {
            words += ConvertNumbertoWords(number / 10000000) + " Crore ";
            number %= 10000000;
        }

        if ((number / 1000000) > 0)
        {
            words += ConvertNumbertoWords(number / 1000000) + " MILLION ";
            number %= 1000000;
        }
        if ((number / 1000) > 0)
        {
            words += ConvertNumbertoWords(number / 1000) + " Thousand ";
            number %= 1000;
        }
        if ((number / 100) > 0)
        {
            words += ConvertNumbertoWords(number / 100) + " Hundred ";
            number %= 100;
        }
        if (number > 0)
        {
            if (words != "")
                words += "and ";
            var unitsMap = new[] { "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
            var tensMap = new[] { "Zero", "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };

            if (number < 20)
                words += unitsMap[number];
            else
            {
                words += tensMap[number / 10];
                if ((number % 10) > 0)
                    words += " " + unitsMap[number % 10];
            }
        }
        return words;
    }

    public void GenerateDemandNote(int Schedule_ID, int countRecord, string allRegNo, string ServiceID)
    {
        try
        {

            string constr = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            using (SqlConnection Conn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("InsertDemandNote", Conn))
                {
                    Conn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Schedule_ID", Schedule_ID);
                    cmd.Parameters.AddWithValue("@countRecord", countRecord);
                    //cmd.Parameters.AddWithValue("@allRegNo", allRegNo);
                    cmd.Parameters.AddWithValue("@ServiceID", ServiceID);
                    cmd.Parameters.AddWithValue("@Created_By", Session["user_id"].ToString());
                    cmd.Parameters.Add("@DemandNoteid", SqlDbType.Int).Direction = ParameterDirection.Output; 
                    cmd.ExecuteNonQuery();

                    string id = cmd.Parameters["@DemandNoteid"].Value.ToString();

                    DemandNoteId = Convert.ToInt64(id);
                }
            }
        }
        catch (Exception ex)
        {           
        }
    }
    private void UpdateCanData(int Registration_Number, int Schedule_ID, Int64 demandNoteId)
    {
        string constr = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
        using (SqlConnection Conn = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("UpdateCandDemandNote", Conn))
            {
                Conn.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Schedule_ID", Schedule_ID);
                cmd.Parameters.AddWithValue("@Registration_Number", Registration_Number);
                cmd.Parameters.AddWithValue("@DemandNoteid", demandNoteId);
                cmd.ExecuteNonQuery();              
            }
        }

    }

    private void UpdateRecord(int Registration_Number, int Schedule_ID)
    {

        objBCAS.i_Schedule_ID = Schedule_ID;
        objBCAS.i_Registration_No = Registration_Number;
        objBCAS.v_BCAS_Approval_By = Session["user_id"].ToString();

        using (DataTable dt_Update = objBCAS.UpdateNominationApproval())
        {
            if (Convert.ToInt32(dt_Update.Rows[0][0]) != 0)
            {
                lbl_error.Text = "Transaction Failed";
                lbl_error.Visible = true;

            }
            else
            {
                //*******Display Payment Reciept
                lbl_error.Visible = false;
            }
        }     
        
    }


    protected void ddlSchedule_SelectedIndexChanged(object sender, EventArgs e)
    {
        //BindGrid();
        ddlDemandNote.Items.Clear();
        if (ddlBillerOrganisation.SelectedValue != "0")
        {

            ddlDemandNote.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
            Int64 OrgID = Convert.ToInt64(ddlBillerOrganisation.SelectedValue);
            Int64 ScheduleID = Convert.ToInt64(ddlSchedule.SelectedValue);
            string constr = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    //cmd.CommandText = "SELECT ID, 'DemandNoteId - '+ cast ( ID as varchar) + '; ( Date - ' +  CONVERT(varchar,date,105) +'; #Candidates - '+ CAST(NoOfCandidates as varchar)+'; FeeAmount - '+ CAST(Amount as varchar) +' )' as IDDateCountAmt   FROM Demand_Note where Schedule_ID=@ScheduleID and Online_Transaction_ID is null and ID in (select Demand_Note_ID from NOMINATIONS_FOR_COURSES where Biller_OrgCode =@OrgID)";
                    cmd.CommandText = "SELECT ID, 'DemandNoteId - '+ cast ( ID as varchar) + '; ( Date - ' +  CONVERT(varchar,date,105) +'; #Candidates - '+  ";
                    cmd.CommandText +="CAST(NoOfCandidates as varchar)+'; FeeAmount - '+ CAST(Amount as varchar) +' )' as IDDateCountAmt   FROM Demand_Note ";
                    cmd.CommandText += "where Schedule_ID=@ScheduleID and Online_Transaction_ID is null and ID in (select Demand_Note_ID from NOMINATIONS_FOR_COURSES n, ";
                    cmd.CommandText += "Candidate_Registration_Screeners c, Course_Schedule cs where c.BillerOrganisation_Code =@OrgID ";
                    cmd.CommandText +="and n.Registration_No =c.Registration_Number and cs.Course_Code =c.course_code  and cs.Schedule_ID =n.Schedule_ID) ";
                    cmd.CommandText += " union SELECT ID, 'DemandNoteId - '+ cast ( ID as varchar) + '; ( Date - ' +  CONVERT(varchar,date,105) +'; #Candidates - '+  ";
                    cmd.CommandText += "CAST(NoOfCandidates as varchar)+'; FeeAmount - '+ CAST(Amount as varchar) +' )' as IDDateCountAmt   FROM Demand_Note ";
                    cmd.CommandText += "where Schedule_ID=@ScheduleID and Online_Transaction_ID is null and ID in (select Demand_Note_ID from NOMINATIONS_FOR_COURSES n, ";
                    cmd.CommandText += "Candidate_Registration c, Course_Schedule cs where c.BillerOrganisation_Code =@OrgID ";
                    cmd.CommandText += "and n.Registration_No =c.Registration_Number  and cs.Schedule_ID =n.Schedule_ID) ";

                    cmd.Parameters.AddWithValue("@OrgID", OrgID);
                    cmd.Parameters.AddWithValue("@ScheduleID", ScheduleID);
                    cmd.Connection = con;
                    con.Open();
                    SqlDataAdapter adpt = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adpt.Fill(dt);
                    ddlDemandNote.DataSource = dt;                
                    ddlDemandNote.DataTextField = "IDDateCountAmt";
                    ddlDemandNote.DataValueField = "ID";

                    ddlDemandNote.DataBind();
                    con.Close();
                }
            }
        }
    }

    protected void ddlOrganisation_SelectedIndexChanged(object sender, EventArgs e)
    {
       // BindGrid();
        ddlDemandNote.Items.Clear();
        if (ddlOrganisation.SelectedValue != "0")
        {
           
            ddlDemandNote.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
            Int64 OrgID = Convert.ToInt64(ddlOrganisation.SelectedValue);
            Int64 ScheduleID = Convert.ToInt64(ddlSchedule.SelectedValue);
            string constr = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT ID   FROM Demand_Note where Schedule_ID=@ScheduleID and ID in (select Demand_Note_ID from NOMINATIONS_FOR_COURSES where Nominating_OrgCode =@OrgID)";

                    cmd.Parameters.AddWithValue("@OrgID", OrgID);
                    cmd.Parameters.AddWithValue("@ScheduleID", ScheduleID);
                    cmd.Connection = con;
                    con.Open();
                    SqlDataAdapter adpt = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adpt.Fill(dt);
                    ddlDemandNote.DataSource = dt;
                   // ddlDemandNote.DataBind();
                    ddlDemandNote.DataTextField = "ID";
                    ddlDemandNote.DataValueField = "ID";
                    
                    ddlDemandNote.DataBind();
                    con.Close();
                }
            }
        }
    }
    protected void ddlBillerOrganisation_SelectedIndexChanged(object sender, EventArgs e)
    {
        // BindGrid();
        ddlDemandNote.Items.Clear();
        if (ddlOrganisation.SelectedValue != "0")
        {

            ddlDemandNote.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
            Int64 OrgID = Convert.ToInt64(ddlOrganisation.SelectedValue);
            Int64 ScheduleID = Convert.ToInt64(ddlSchedule.SelectedValue);
            string constr = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                   // cmd.CommandText = "SELECT ID   FROM Demand_Note where Schedule_ID=@ScheduleID and ID in (select Demand_Note_ID from NOMINATIONS_FOR_COURSES where Nominating_OrgCode =@OrgID)";
                    cmd.CommandText = "SELECT ID   FROM Demand_Note where Schedule_ID=@ScheduleID and ID in ";
                    cmd.CommandText +="(select Demand_Note_ID from NOMINATIONS_FOR_COURSES n,Candidate_Registration_Screeners c,Course_Schedule s ";
                    cmd.CommandText+="where n.Registration_No =c.Registration_Number and n.Schedule_ID =s.Schedule_ID and c.course_code =s.Course_Code ";
                    cmd.CommandText+=" and c.BillerOrganisation_Code =@BillerOrgID)";


                    cmd.Parameters.AddWithValue("@OrgID", OrgID);
                    cmd.Parameters.AddWithValue("@ScheduleID", ScheduleID);
                    cmd.Connection = con;
                    con.Open();
                    SqlDataAdapter adpt = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adpt.Fill(dt);
                    ddlDemandNote.DataSource = dt;
                    // ddlDemandNote.DataBind();
                    ddlDemandNote.DataTextField = "ID";
                    ddlDemandNote.DataValueField = "ID";

                    ddlDemandNote.DataBind();
                    con.Close();
                }
            }
        }
    }
   
}


