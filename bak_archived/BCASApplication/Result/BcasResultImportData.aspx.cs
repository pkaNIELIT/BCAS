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
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;
using System.IO;

public partial class Result_BcasResultImportData : System.Web.UI.Page
{
        TYP_ApplicantDetails objApplicant = new TYP_ApplicantDetails();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Convert.ToString(HttpContext.Current.Session["UserId"]).Trim().Length == 0))
        {
            HttpContext.Current.Response.Redirect("~/Home.aspx");
        }
            
    }
    protected void Upload(object sender, EventArgs e)
    {
        Panel1.Visible = false;
        //Upload and save the file
        Clear();
        string excelPath = Server.MapPath("~/UploadResultFile/") + Path.GetFileName(FileUpload1.PostedFile.FileName);
        if (FileUpload1.PostedFile.FileName != "")
        {
            FileUpload1.SaveAs(excelPath);
            string conString = string.Empty;
            string extension = Path.GetExtension(FileUpload1.PostedFile.FileName);
            switch (extension)
            {
                case ".xls": //Excel 97-03
                    conString = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                    break;
                case ".xlsx": //Excel 07 or higher
                    conString = ConfigurationManager.ConnectionStrings["Excel07+ConString"].ConnectionString;
                    break;
            }
            conString = string.Format(conString, excelPath);
            using (OleDbConnection excel_con = new OleDbConnection(conString))
            {
                excel_con.Open();
                string sheet1 = excel_con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows[0]["TABLE_NAME"].ToString();
                DataTable dtExcelData = new DataTable();

                //Added by Kismat on 25-08-2025
                if (sheet1 == "Sheet1$") //Checking sheet name
                {
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    lblMessage.Text = "Please Change your Excel Sheet Name. It should be 'Sheet1' ";
                    return;
                }

                //[OPTIONAL]: It is recommended as otherwise the data will be considered as String by default.
                dtExcelData.Columns.AddRange(new DataColumn[11] { 
                new DataColumn("FirstName", typeof(string)),new DataColumn("ROLL NO", typeof(string)),new DataColumn("Reg", typeof(string)),
                new DataColumn("BcasRoll", typeof(string)),new DataColumn("Centre_Allot", typeof(string)),new DataColumn("ExamDate", typeof(string)),
               new DataColumn("Attendance", typeof(string)), new DataColumn("attemped", typeof(string)),new DataColumn("CORRECT", typeof(string)),
               new DataColumn("NotCorrect", typeof(string)),new DataColumn("marks", typeof(string))
                //new DataColumn("Salary",typeof(DateTime)) 
            });
                //Changes the sheet man by Kismat on 25-08-2025
                using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT * FROM [Sheet1]", excel_con))
                {
                    oda.Fill(dtExcelData);
                }
                excel_con.Close();

                string consString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
                using (SqlConnection con = new SqlConnection(consString))
                {
                    string qry = "DELETE FROM dbo.temp_Bcas_Result";
                    con.Open();
                    SqlCommand com = new SqlCommand(qry, con);
                    com.ExecuteNonQuery();
                    con.Close();
                    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                    {
                        try
                        {
                            //Set the database table name
                            sqlBulkCopy.DestinationTableName = "dbo.temp_Bcas_Result";

                            //[OPTIONAL]: Map the Excel columns with that of the database table  Attendance                 
                            sqlBulkCopy.ColumnMappings.Add("FirstName", "Candidate_Name");
                            sqlBulkCopy.ColumnMappings.Add("ROLL NO", "Roll_Number");
                            sqlBulkCopy.ColumnMappings.Add("Reg", "Registration_Number");
                            sqlBulkCopy.ColumnMappings.Add("BcasRoll", "Bcas_Roll_Number");
                            sqlBulkCopy.ColumnMappings.Add("Centre_Allot", "Centre_Allot");
                            sqlBulkCopy.ColumnMappings.Add("ExamDate", "ExamDate");
                            sqlBulkCopy.ColumnMappings.Add("Attendance", "Attendance");
                            sqlBulkCopy.ColumnMappings.Add("attemped", "Attemped");
                            sqlBulkCopy.ColumnMappings.Add("CORRECT", "Correct");
                            sqlBulkCopy.ColumnMappings.Add("NotCorrect", "Not_Correct");
                            sqlBulkCopy.ColumnMappings.Add("marks", "Marks");
                            con.Open();
                            sqlBulkCopy.WriteToServer(dtExcelData);
                            con.Close();
                            lblMessage.ForeColor = System.Drawing.Color.Green;
                            lblMessage.Text = "Marks is Uploaded Successfully.";
                            Panel1.Visible = true;
                            Finalize.Enabled = true;
                        }
                        catch
                        {
                            lblMessage.ForeColor = System.Drawing.Color.Red;
                            lblMessage.Text = "Please Check Excel Sheet Data.";
                        }
                    }
                }
            }
        }
        else
        {
            lblMessage.ForeColor = System.Drawing.Color.Red;
            lblMessage.Text = "Please select the file.";
        }
    }
    protected void ddlSchedule_SelectedIndexChanged(object sender, EventArgs e)
    {
        Clear();
        //Finalize.Enabled = true;
    }
    protected void Finalize_Click(object sender, EventArgs e)
    {
        checkdata();       
    }
    public void  checkdata()
    {
        try
        {
            string str = " ";

            str = "select count(*) from e_Candidate_Marks where Schedule_ID=@Schedule_ID";
            string consString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(consString))
            {
                using (SqlCommand cmd = new SqlCommand(str, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = str;
                    cmd.Parameters.AddWithValue("@Schedule_ID", ddlSchedule.SelectedValue);
                    con.Open();
                    int str1 = (int)cmd.ExecuteScalar();
                    con.Close();
                   string HiddenCountCandiate = str1.ToString();
                    int record = Convert.ToInt32(HiddenCountCandiate);
                    if (record == 0)
                    {
                        RecordCount();
                        ImportData();
                    }
                    else
                    {
                        lblMessage1.Text = "";
                        lblMessage0.ForeColor = System.Drawing.Color.Red;
                        lblMessage0.Text = "Data is Already  Ported for  this ExamDate.";
                    }
                }
            }
        }
        catch(Exception ex)
        {
            lblMessage.Text = ex.Message.ToString();
        }
    }
    public void ImportData()
    {        
         string consString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(consString))
            {
                using (SqlCommand cmd = new SqlCommand("ImportData_e_Candidate_Marks", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Schedule_ID", ddlSchedule.SelectedValue);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    lblMessage0.Visible = true;
                    lblMessage0.ForeColor = System.Drawing.Color.Green;
                    lblMessage0.Text = " Candidate Data is Ported Successfully.";
                    Finalize.Enabled = false;
                }
            }
    }
    public void RecordCount()
    {
        try
        {
            string str = " ";

            str = "select count(*) from temp_Bcas_Result ";
            string consString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(consString))
            {
                using (SqlCommand cmd = new SqlCommand(str, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = str;                  
                    con.Open();
                    int str1 = (int)cmd.ExecuteScalar();
                    con.Close();
                    string HiddenCountCandiate0 = str1.ToString();
                    int RecordCnt = Convert.ToInt32(HiddenCountCandiate0);
                    lblMessage1.Text = RecordCnt.ToString();
                 }
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message.ToString();
        }
    }
    public void Clear()
    {
        lblMessage.Text = "";
        lblMessage0.Text = "";
        lblMessage1.Text = "";
    }
}

 