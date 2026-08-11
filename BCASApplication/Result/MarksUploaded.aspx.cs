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



public partial class Result_MarksUploaded : System.Web.UI.Page
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
        //Upload and save the file
        string excelPath = Server.MapPath("~/Files/") + Path.GetFileName(FileUpload1.PostedFile.FileName);
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
            //Added by Kismat on 25-08-2025
            if (sheet1 != "Sheet1$") //Checking sheet name
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "Please Change your Excel Sheet Name. It should be 'Sheet1' ";
                return;
            }

            DataTable dtExcelData = new DataTable();

            //[OPTIONAL]: It is recommended as otherwise the data will be considered as String by default.
            dtExcelData.Columns.AddRange(new DataColumn[3] { new DataColumn("Id", typeof(int)),
                new DataColumn("Name", typeof(string)),
                new DataColumn("Salary",typeof(DateTime)) });

            using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT * FROM [Sheet1$]", excel_con))
            {
                oda.Fill(dtExcelData);
            }
            excel_con.Close();
 


            string consString = ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(consString))
            {
                using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                {
                    //Set the database table name
                    sqlBulkCopy.DestinationTableName = "dbo.tblPersons";

                    //[OPTIONAL]: Map the Excel columns with that of the database table
                    sqlBulkCopy.ColumnMappings.Add("Id", "PersonId");
                    sqlBulkCopy.ColumnMappings.Add("Name", "Name");
                    sqlBulkCopy.ColumnMappings.Add("ROLL NO", "Salary");

                    //test Convert(varchar(10),'" + columns[3] + "',103)

                    con.Open();
                    sqlBulkCopy.WriteToServer(dtExcelData);
                    con.Close();
                    
                }
            }
            
            lblMessage.Text = "Marks is Uploaded Successfully.";
        }
    }
   
        
}
