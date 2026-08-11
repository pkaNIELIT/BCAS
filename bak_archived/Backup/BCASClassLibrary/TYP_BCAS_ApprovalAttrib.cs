using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BCASClassLibrary;

namespace BCASClassLibrary
{
    public class TYP_BCAS_ApprovalAttrib
    {

        //Int32 _i_Schedule_ID;
        //Int32 _i_Registration_No;
        //Int32 _n_Fee_Amount;
        //string _c_BCAS_Approval_Status;

        //public bool b_Schedule_ID { get; set; }
        //public bool b_Registration_No { get; set; }
        //public bool b_Fee_Amount { get; set; }
        //public bool b_BCAS_Approval_Status { get; set; }

        //public Int32 i_Schedule_ID { get { return _i_Schedule_ID; } set { _i_Schedule_ID = value; b_Schedule_ID = true; } }
        //public Int32 i_Registration_No { get { return _i_Registration_No; } set { _i_Registration_No = value; b_Registration_No = true; } }
        //public Int32 n_Fee_Amount { get { return _n_Fee_Amount; } set { _n_Fee_Amount = value; b_Fee_Amount = true; } }
        //public string c_BCAS_Approval_Status { get { return _c_BCAS_Approval_Status; } set { _c_BCAS_Approval_Status = value; b_BCAS_Approval_Status = true; } }

        //*********

        Int32 _i_Schedule_ID;
        Int32 _i_Registration_No;
        Int32 _n_Fee_Amount;
        string _c_BCAS_Approval_Status;
        Int32 _i_Nominating_OrgCode;
        string _v_Nominated_By;
        DateTime _d_Nominating_Date;
        string _v_BCAS_Approval_By;
        DateTime _d_BCAS_Approval_Date;
        Int32 _i_Exam_Location_Code;

        public bool b_Schedule_ID { get; set; }
        public bool b_Registration_No { get; set; }
        public bool b_Fee_Amount { get; set; }
        public bool b_BCAS_Approval_Status { get; set; }
        public bool b_Nominating_OrgCode { get; set; }
        public bool b_Nominated_By { get; set; }
        public bool b_Nominating_Date { get; set; }
        public bool b_BCAS_Approval_By { get; set; }
        public bool b_BCAS_Approval_Date { get; set; }
        public bool b_Exam_Location_Code { get; set; }

        public Int32 i_Schedule_ID { get { return _i_Schedule_ID; } set { _i_Schedule_ID = value; b_Schedule_ID = true; } }
        public Int32 i_Registration_No { get { return _i_Registration_No; } set { _i_Registration_No = value; b_Registration_No = true; } }
        public Int32 n_Fee_Amount { get { return _n_Fee_Amount; } set { _n_Fee_Amount = value; b_Fee_Amount = true; } }
        public string c_BCAS_Approval_Status { get { return _c_BCAS_Approval_Status; } set { _c_BCAS_Approval_Status = value; b_BCAS_Approval_Status = true; } }
        public Int32 i_Nominating_OrgCode { get { return _i_Nominating_OrgCode; } set { _i_Nominating_OrgCode = value; b_Nominating_OrgCode = true; } }
        public string v_Nominated_By { get { return _v_Nominated_By; } set { _v_Nominated_By = value; b_Nominated_By = true; } }
        public DateTime d_Nominating_Date { get { return _d_Nominating_Date; } set { _d_Nominating_Date = value; b_Nominating_Date = true; } }
        public string v_BCAS_Approval_By { get { return _v_BCAS_Approval_By; } set { _v_BCAS_Approval_By = value; b_BCAS_Approval_By = true; } }
        public DateTime d_BCAS_Approval_Date { get { return _d_BCAS_Approval_Date; } set { _d_BCAS_Approval_Date = value; b_BCAS_Approval_Date = true; } }
        public Int32 i_Exam_Location_Code { get { return _i_Exam_Location_Code; } set { _i_Exam_Location_Code = value; b_Exam_Location_Code = true; } }

    }
}
