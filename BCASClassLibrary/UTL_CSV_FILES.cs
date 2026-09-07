using System;
using System.Collections.Generic;
using System.IO;
using System.Data;

namespace BCASClassLibrary
{
   public class UTL_CSV_FILES
    {
        

        public DataTable ReadCsvFile(string file_name, string file_path)
        {
            DataTable dtError = new DataTable();
           
                DataTable insDataTable = new DataTable();
                System.Data.Odbc.OdbcConnection conn;
                System.Data.Odbc.OdbcDataAdapter da;
                conn = new System.Data.Odbc.OdbcConnection(@"Driver={Microsoft Text Driver (*.txt; *.csv)};Dbq=" + file_path + ";Extensions=asc,csv,tab,txt;Persist Security Info=False");
                da = new System.Data.Odbc.OdbcDataAdapter("select * from [" + file_name + "]", conn);
                da.Fill(insDataTable);
                return insDataTable;
        }
           
       
        public void WriteSchemaFile(string file_name, string file_path)
        {
           
                using (StreamWriter WriteFile = new StreamWriter(file_path + "schema.ini"))
                {
                    WriteFile.WriteLine('[' + file_name + ']');
                    WriteFile.WriteLine("ColNameHeader=True ");
                    WriteFile.WriteLine("Format=CSVDelimited");
                    WriteFile.WriteLine("DateTimeFormat=yyyy-MM-dd");

                    int ColCount = 1;
                    string[] strColMain;

                    strColMain = ReadHeaderOfCSVFile(file_path + file_name);

                    foreach (string strCol in strColMain)
                    {
                        WriteFile.WriteLine("Col" + ColCount.ToString() + "=" + strCol + " Text Width 500");
                        ColCount++;
                    }
                }           

        }


        public string[] ReadHeaderOfCSVFile(string path)
        {
            string stmLine;
            string[] row;
            using (StreamReader Readfile = new StreamReader(path))
            {
                stmLine = Readfile.ReadLine();
                row = stmLine.Split(',');
                return row;
            }
        }


    }
}
