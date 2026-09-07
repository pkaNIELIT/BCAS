using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using BCASClassLibrary;

namespace BCASClassLibrary
{
    public class TYP_NominationEntryBySOScreenersAttrib
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


        //**************
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


        string _c_Whether_Reappearing;
        public bool b_Whether_Reappearing { get; set; }
        public string c_Whether_Reappearing { get { return _c_Whether_Reappearing; } set { _c_Whether_Reappearing = value; b_Whether_Reappearing = true; } }



        //*******For Candidate Registration Table

        Int32 _i_Organisation_Code;
        string _v_Employee_ID;
        string _v_Candidate_Name;
        string _c_Sex_Code;
        Int32 _i_Designation_Code;
        DateTime _d_DOB;
        string _v_Email_Id;
        string _v_Mobile_No;
        Int32 _i_Nationality_Code;
        Int32 _i_Educational_Qualification_Code;
        Int32 _i_Registration_Number;
        string _v_Enter_by;
        DateTime _d_Enter_Date;

        public bool b_Organisation_Code { get; set; }
        public bool b_Employee_ID { get; set; }
        public bool b_Candidate_Name { get; set; }
        public bool b_Sex_Code { get; set; }
        public bool b_Designation_Code { get; set; }
        public bool b_DOB { get; set; }
        public bool b_Email_Id { get; set; }
        public bool b_Mobile_No { get; set; }
        public bool b_Nationality_Code { get; set; }
        public bool b_Educational_Qualification_Code { get; set; }
        public bool b_Registration_Number { get; set; }
        public bool b_Enter_by { get; set; }
        public bool b_Enter_Date { get; set; }

        public Int32 i_Organisation_Code { get { return _i_Organisation_Code; } set { _i_Organisation_Code = value; b_Organisation_Code = true; } }
        public string v_Employee_ID { get { return _v_Employee_ID; } set { _v_Employee_ID = value; b_Employee_ID = true; } }
        public string v_Candidate_Name { get { return _v_Candidate_Name; } set { _v_Candidate_Name = value; b_Candidate_Name = true; } }
        public string c_Sex_Code { get { return _c_Sex_Code; } set { _c_Sex_Code = value; b_Sex_Code = true; } }
        public Int32 i_Designation_Code { get { return _i_Designation_Code; } set { _i_Designation_Code = value; b_Designation_Code = true; } }
        public DateTime d_DOB { get { return _d_DOB; } set { _d_DOB = value; b_DOB = true; } }
        public string v_Email_Id { get { return _v_Email_Id; } set { _v_Email_Id = value; b_Email_Id = true; } }
        public string v_Mobile_No { get { return _v_Mobile_No; } set { _v_Mobile_No = value; b_Mobile_No = true; } }
        public Int32 i_Nationality_Code { get { return _i_Nationality_Code; } set { _i_Nationality_Code = value; b_Nationality_Code = true; } }
        public Int32 i_Educational_Qualification_Code { get { return _i_Educational_Qualification_Code; } set { _i_Educational_Qualification_Code = value; b_Educational_Qualification_Code = true; } }
        public Int32 i_Registration_Number { get { return _i_Registration_Number; } set { _i_Registration_Number = value; b_Registration_Number = true; } }
        public string v_Enter_by { get { return _v_Enter_by; } set { _v_Enter_by = value; b_Enter_by = true; } }
        public DateTime d_Enter_Date { get { return _d_Enter_Date; } set { _d_Enter_Date = value; b_Enter_Date = true; } }

        
    }
}
