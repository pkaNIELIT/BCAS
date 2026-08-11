using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BCASClassLibrary
{
    public class TYP_PaymentResponseAttrib
    {
        Int32 _i_Transaction_id;
        float _n_Amount_Recieved;
        Int32 _i_Payee_Organisation_Code;
        Int32 _i_Sponsoring_Organisation_Code; //ResponseDescription
        string _v_Reference_No;
        string _v_ResponseDescription;
        DateTime _d_Date_of_Payment;
        DateTime _d_Enter_Date;

        public bool b_Transaction_id { get; set; }
        public bool b_Amount_Recieved { get; set; }
        public bool b_Payee_Organisation_Code { get; set; }
        public bool b_Sponsoring_Organisation_Code { get; set; }
        public bool b_Reference_No { get; set; }
        public bool b_ResponseDescription { get; set; }
        public bool b_Date_of_Payment { get; set; }
        public bool b_Enter_Date { get; set; }




        public Int32 i_Transaction_id { get { return _i_Transaction_id; } set { _i_Transaction_id = value; b_Transaction_id = true; } }
        public float n_Amount_Recieved { get { return _n_Amount_Recieved; } set { _n_Amount_Recieved = value; b_Amount_Recieved = true; } }
        public Int32 i_Payee_Organisation_Code { get { return _i_Payee_Organisation_Code; } set { _i_Payee_Organisation_Code = value; b_Payee_Organisation_Code = true; } }
        public Int32 i_Sponsoring_Organisation_Code { get { return _i_Sponsoring_Organisation_Code; } set { _i_Sponsoring_Organisation_Code = value; b_Sponsoring_Organisation_Code = true; } }
        public string v_Reference_No { get { return _v_Reference_No; } set { _v_Reference_No = value; b_Reference_No = true; } }
        public string v_ResponseDescription { get { return _v_ResponseDescription; } set { _v_ResponseDescription = value; b_ResponseDescription = true; } }
        public DateTime d_Date_of_Payment { get { return _d_Date_of_Payment; } set { _d_Date_of_Payment = value; b_Date_of_Payment = true; } }
        public DateTime d_Enter_Date { get { return _d_Enter_Date; } set { _d_Enter_Date = value; b_Enter_Date = true; } }

        //*********Transaction Log Table
               
        Int32 _n_Amount_To_Pay;     
        
        public bool b_Amount_To_Pay { get; set; }   
               
        public Int32 n_Amount_To_Pay { get { return _n_Amount_To_Pay; } set { _n_Amount_To_Pay = value; b_Amount_To_Pay = true; } }
        
        //*********Payment Response Table
        
        string _v_Transaction_Reference_No;
        float _n_Fee_Amount_Deducted;
        string _v_Payment_Status;
        DateTime _d_Transaction_Date;
        string _v_Response_String;            
       
        public bool b_Transaction_Reference_No { get; set; }
        public bool b_Fee_Amount_Deducted { get; set; }
        public bool b_Payment_Status { get; set; }
        public bool b_Transaction_Date { get; set; }
        public bool b_Response_String { get; set; }
               
        public string v_Transaction_Reference_No { get { return _v_Transaction_Reference_No; } set { _v_Transaction_Reference_No = value; b_Transaction_Reference_No = true; } }
        public float n_Fee_Amount_Deducted { get { return _n_Fee_Amount_Deducted; } set { _n_Fee_Amount_Deducted = value; b_Fee_Amount_Deducted = true; } }
        public string v_Payment_Status { get { return _v_Payment_Status; } set { _v_Payment_Status = value; b_Payment_Status = true; } }
        public DateTime d_Transaction_Date { get { return _d_Transaction_Date; } set { _d_Transaction_Date = value; b_Transaction_Date = true; } }
        public string v_Response_String { get { return _v_Response_String; } set { _v_Response_String = value; b_Response_String = true; } }
        



    }
}
