using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BCASClassLibrary
{

    public class TYP_NEFT_RTGS_File_UploadAttrib
    {
        Int32 _i_Serial_No;
        string _v_UTR_Number;
        string _v_Sender_IFSC;
        string _v_Sender_Name;
        DateTime _d_Transaction_date;
        float _n_Amount;
        string _v_Enter_By;
        DateTime _d_Enter_date;

        public bool b_Serial_No { get; set; }
        public bool b_UTR_Number { get; set; }
        public bool b_Sender_IFSC { get; set; }
        public bool b_Sender_Name { get; set; }
        public bool b_Transaction_date { get; set; }
        public bool b_Amount { get; set; }
        public bool b_Enter_By { get; set; }
        public bool b_Enter_date { get; set; }

        public Int32 i_Serial_No { get { return _i_Serial_No; } set { _i_Serial_No = value; b_Serial_No = true; } }
        public string v_UTR_Number { get { return _v_UTR_Number; } set { _v_UTR_Number = value; b_UTR_Number = true; } }
        public string v_Sender_IFSC { get { return _v_Sender_IFSC; } set { _v_Sender_IFSC = value; b_Sender_IFSC = true; } }
        public string v_Sender_Name { get { return _v_Sender_Name; } set { _v_Sender_Name = value; b_Sender_Name = true; } }
        public DateTime d_Transaction_date { get { return _d_Transaction_date; } set { _d_Transaction_date = value; b_Transaction_date = true; } }
        public float n_Amount { get { return _n_Amount; } set { _n_Amount = value; b_Amount = true; } }
        public string v_Enter_By { get { return _v_Enter_By; } set { _v_Enter_By = value; b_Enter_By = true; } }
        public DateTime d_Enter_date { get { return _d_Enter_date; } set { _d_Enter_date = value; b_Enter_date = true; } }


        string _v_filePath;

        public bool b_filePath { get; set; }

        public string v_filePath { get { return _v_filePath; } set { _v_filePath = value; b_filePath = true; } }

    }

}
