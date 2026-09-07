using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BCASClassLibrary
{
    public class TYP_PayeeOrganisationPaymentAttrib
    {

        Int32 _i_Transaction_id;
        Int32 _i_Payee_Organisation_Code;
        Int32 _i_Sponsoring_Organisation_Code;
        Int32 _n_Amount_To_Pay;
        DateTime _d_Date_of_Payment;
        string _v_User_ID;

        public bool b_Transaction_id { get; set; }
        public bool b_Payee_Organisation_Code { get; set; }
        public bool b_Sponsoring_Organisation_Code { get; set; }
        public bool b_Amount_To_Pay { get; set; }
        public bool b_Date_of_Payment { get; set; }
        public bool b_User_ID { get; set; }

        public Int32 i_Transaction_id { get { return _i_Transaction_id; } set { _i_Transaction_id = value; b_Transaction_id = true; } }
        public Int32 i_Payee_Organisation_Code { get { return _i_Payee_Organisation_Code; } set { _i_Payee_Organisation_Code = value; b_Payee_Organisation_Code = true; } }
        public Int32 i_Sponsoring_Organisation_Code { get { return _i_Sponsoring_Organisation_Code; } set { _i_Sponsoring_Organisation_Code = value; b_Sponsoring_Organisation_Code = true; } }
        public Int32 n_Amount_To_Pay { get { return _n_Amount_To_Pay; } set { _n_Amount_To_Pay = value; b_Amount_To_Pay = true; } }
        public DateTime d_Date_of_Payment { get { return _d_Date_of_Payment; } set { _d_Date_of_Payment = value; b_Date_of_Payment = true; } }
        public string v_User_ID { get { return _v_User_ID; } set { _v_User_ID = value; b_User_ID = true; } }


    }
}
