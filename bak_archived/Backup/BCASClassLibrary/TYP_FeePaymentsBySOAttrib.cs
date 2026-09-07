using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BCASClassLibrary
{

    public class TYP_FeePaymentsBySOAttrib
    {
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
        float _n_Service_Tax;
        float _n_Total_Fee_With_Service_Tax;
        string _v_Fee_Payment_By;
        DateTime _d_Fee_Payment_Date;

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
        public bool b_Service_Tax { get; set; }
        public bool b_Total_Fee_With_Service_Tax { get; set; }
        public bool b_Fee_Payment_By { get; set; }
        public bool b_Fee_Payment_Date { get; set; }


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
        public float n_Service_Tax { get { return _n_Service_Tax; } set { _n_Service_Tax = value; b_Service_Tax = true; } }
        public float n_Total_Fee_With_Service_Tax { get { return _n_Total_Fee_With_Service_Tax; } set { _n_Total_Fee_With_Service_Tax = value; b_Total_Fee_With_Service_Tax = true; } }
        public string v_Fee_Payment_By { get { return _v_Fee_Payment_By; } set { _v_Fee_Payment_By = value; b_Fee_Payment_By = true; } }
        public DateTime d_Fee_Payment_Date { get { return _d_Fee_Payment_Date; } set { _d_Fee_Payment_Date = value; b_Fee_Payment_Date = true; } }


        //**********Payment_Balance Table******

        Int32 _i_Sponsoring_Organisation_Code;
        Int32 _i_Payee_Organisation_Code;
        float _n_Total_Amount;
        float _n_Balance_Amount;

        public bool b_Sponsoring_Organisation_Code { get; set; }
        public bool b_Payee_Organisation_Code { get; set; }
        public bool b_Total_Amount { get; set; }
        public bool b_Balance_Amount { get; set; }

        public Int32 i_Sponsoring_Organisation_Code { get { return _i_Sponsoring_Organisation_Code; } set { _i_Sponsoring_Organisation_Code = value; b_Sponsoring_Organisation_Code = true; } }
        public Int32 i_Payee_Organisation_Code { get { return _i_Payee_Organisation_Code; } set { _i_Payee_Organisation_Code = value; b_Payee_Organisation_Code = true; } }
        public float n_Total_Amount { get { return _n_Total_Amount; } set { _n_Total_Amount = value; b_Total_Amount = true; } }
        public float n_Balance_Amount { get { return _n_Balance_Amount; } set { _n_Balance_Amount = value; b_Balance_Amount = true; } }




    }

}
