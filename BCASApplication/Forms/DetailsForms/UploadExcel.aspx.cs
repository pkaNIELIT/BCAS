using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Drawing;
using System.Xml.Linq;
using Ionic.Zip;

public partial class Forms_DetailsForms_UploadExcel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        lblError.Text = "";
        SuccessLabel.Text = "";
        grdFailed.Visible = false;

        if (!fileUploadExcel.HasFile)
        {
            lblError.Text = "Please select a file to upload.";
            return;
        }

        string extension = Path.GetExtension(fileUploadExcel.FileName).ToLower();
        if (extension != ".xlsx")
        {
            lblError.Text = "Only .xlsx files are supported (Excel 2007 or later). Please save your file as .xlsx.";
            return;
        }

        // Allow up to 10 minutes for large files
        Server.ScriptTimeout = 600;

        string filePath = Server.MapPath("~/Upload/EBCASFiles/" + fileUploadExcel.FileName);

        try
        {
            fileUploadExcel.SaveAs(filePath);
            lblfilename.Text = fileUploadExcel.FileName;

            // Read the xlsx directly — no OleDb/Office required
            DataTable dt = ReadXlsxFile(filePath);

            // Validate required columns exist in header row
            string[] required = { "BCASID", "APPLICANTNAME", "DOB" };
            foreach (string col in required)
            {
                if (!dt.Columns.Contains(col))
                {
                    lblError.Text = "Excel is missing required column: '" + col
                        + "'. Required columns in row 1: BCASID, APPLICANTNAME, DOB.";
                    DeleteFile(filePath);
                    return;
                }
            }

            using (SqlConnection connection = new SqlConnection(
                ConfigurationManager.ConnectionStrings["BCASConnectionString"].ConnectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("ExcelDataImport", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 60;

                    foreach (DataRow row in dt.Rows)
                    {
                        string bcasId = row["BCASID"].ToString().Trim();
                        if (string.IsNullOrEmpty(bcasId))
                            continue;

                        // Excel stores dates as OA serial numbers (e.g. "44927.0")
                        DateTime dob;
                        string dobRaw = row["DOB"].ToString().Trim();
                        double oaDate;
                        if (double.TryParse(dobRaw, out oaDate))
                            dob = DateTime.FromOADate(oaDate);
                        else if (!DateTime.TryParse(dobRaw, out dob))
                        {
                            lblError.Text = "Invalid date in DOB column for BCASID: " + bcasId
                                + ". Value was: " + dobRaw;
                            return;
                        }

                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@EBCASNo", bcasId);
                        command.Parameters.AddWithValue("@name", row["APPLICANTNAME"].ToString().Trim());
                        command.Parameters.AddWithValue("@Dateofbirth", dob);
                        command.Parameters.AddWithValue("@uploadedFileName", fileUploadExcel.FileName);
                        command.Parameters.Add("@pRemarks", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;

                        command.ExecuteNonQuery();

                        string remarks = command.Parameters["@pRemarks"].Value == DBNull.Value
                            ? "" : command.Parameters["@pRemarks"].Value.ToString();
                        if (!string.IsNullOrEmpty(remarks))
                        {
                            lblError.Text = remarks;
                            lblError.Visible = true;
                        }
                    }
                }

                using (SqlCommand cmdCheck = new SqlCommand("ExcelDataImportCheck", connection))
                {
                    cmdCheck.CommandType = CommandType.StoredProcedure;
                    cmdCheck.CommandTimeout = 60;
                    cmdCheck.Parameters.Add("@SuccessCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmdCheck.Parameters.Add("@FailureCount", SqlDbType.Int).Direction = ParameterDirection.Output;

                    cmdCheck.ExecuteNonQuery();

                    int success = Convert.ToInt32(cmdCheck.Parameters["@SuccessCount"].Value);
                    int failure = Convert.ToInt32(cmdCheck.Parameters["@FailureCount"].Value);

                    SuccessLabel.Text = "Saved Records: " + success + "<br/>Duplicate Records: " + failure;
                    grdFailed.DataBind();
                    grdFailed.Visible = (failure > 0);
                }
            }
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
            DeleteFile(filePath);
        }
    }

    // Reads an .xlsx file using Ionic.Zip (already in Bin) — no OleDb/Office/COM required
    private DataTable ReadXlsxFile(string filePath)
    {
        DataTable dt = new DataTable();
        List<string> sharedStrings = new List<string>();

        using (ZipFile zip = ZipFile.Read(filePath))
        {
            // 1. Load shared strings table (Excel stores all text in this lookup)
            if (zip.ContainsEntry("xl/sharedStrings.xml"))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    zip["xl/sharedStrings.xml"].Extract(ms);
                    ms.Position = 0;
                    XDocument xss = XDocument.Load(ms);
                    XNamespace ns = "http://schemas.openxmlformats.org/spreadsheetml/2006/main";
                    foreach (XElement si in xss.Descendants(ns + "si"))
                    {
                        sharedStrings.Add(string.Concat(si.Descendants(ns + "t").Select(t => t.Value)));
                    }
                }
            }

            // 2. Load Sheet1 worksheet
            if (!zip.ContainsEntry("xl/worksheets/sheet1.xml"))
                throw new Exception("'Sheet1' worksheet not found in the Excel file. "
                    + "Please ensure your first sheet is named 'Sheet1'.");

            using (MemoryStream ms = new MemoryStream())
            {
                zip["xl/worksheets/sheet1.xml"].Extract(ms);
                ms.Position = 0;

                XDocument xdoc = XDocument.Load(ms);
                XNamespace ns = "http://schemas.openxmlformats.org/spreadsheetml/2006/main";

                bool isHeader = true;

                foreach (XElement rowEl in xdoc.Descendants(ns + "row"))
                {
                    List<XElement> cells = rowEl.Elements(ns + "c").ToList();

                    if (isHeader)
                    {
                        // Row 1 = column headers
                        foreach (XElement cell in cells)
                        {
                            string header = GetCellText(cell, sharedStrings, ns).Trim().ToUpper();
                            dt.Columns.Add(header);
                        }
                        isHeader = false;
                    }
                    else
                    {
                        // Skip completely blank rows
                        if (cells.All(c => string.IsNullOrWhiteSpace(GetCellText(c, sharedStrings, ns))))
                            continue;

                        DataRow dr = dt.NewRow();
                        int colIdx = 0;

                        foreach (XElement cell in cells)
                        {
                            // Cell ref like "B5" tells us which column this belongs to
                            string cellRef = cell.Attribute("r") != null ? cell.Attribute("r").Value : "";
                            int targetCol = GetColumnNumber(cellRef) - 1; // 0-based

                            // Advance to correct column (handles sparse/empty cells)
                            while (colIdx < targetCol && colIdx < dt.Columns.Count)
                                colIdx++;

                            if (colIdx < dt.Columns.Count)
                            {
                                dr[colIdx] = GetCellText(cell, sharedStrings, ns);
                                colIdx++;
                            }
                        }

                        dt.Rows.Add(dr);
                    }
                }
            }
        }

        return dt;
    }

    // Returns the text value of a cell element
    private string GetCellText(XElement cell, List<string> sharedStrings, XNamespace ns)
    {
        string type = cell.Attribute("t") != null ? cell.Attribute("t").Value : "";
        XElement vEl = cell.Element(ns + "v");
        string raw = vEl != null ? vEl.Value : "";

        if (type == "s") // shared string — raw is an index into sharedStrings
        {
            int idx;
            if (int.TryParse(raw, out idx) && idx < sharedStrings.Count)
                return sharedStrings[idx];
            return "";
        }
        return raw; // number, date serial, or empty
    }

    // Converts column letters in cell ref to 1-based column number (A=1, B=2, AA=27 …)
    private int GetColumnNumber(string cellRef)
    {
        int result = 0;
        foreach (char c in cellRef)
        {
            if (!char.IsLetter(c)) break;
            result = result * 26 + (c - 'A' + 1);
        }
        return result == 0 ? 1 : result;
    }

    private void DeleteFile(string filePath)
    {
        try { if (File.Exists(filePath)) File.Delete(filePath); } catch { }
    }
}
