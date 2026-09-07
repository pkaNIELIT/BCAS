using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BCASClassLibrary
{
   public class TYP_CREATE_USERAttrib
    {

        string _v_UserName; 
        Int32 _i_OrganisationCode; 
        string _c_Organisation_Type; 
        string _v_Name; 
        string _v_Email; 
        string _v_Created_By; 
        DateTime _d_Created_On;

        string _v_Mobile_No;

        public bool  b_UserName{ get; set; }
        public bool  b_OrganisationCode{ get; set; }
        public bool  b_Organisation_Type{ get; set; }
        public bool  b_Name{ get; set; }
        public bool  b_Email{ get; set; }
        public bool  b_Created_By{ get; set; }
        public bool  b_Created_On{ get; set; }
       
       public bool b_Mobile_No { get; set; }


        public string v_UserName { get { return _v_UserName; } set { _v_UserName = value; b_UserName = true; } }
        public Int32 i_OrganisationCode { get { return _i_OrganisationCode; } set { _i_OrganisationCode = value; b_OrganisationCode = true; } }
        public string c_Organisation_Type { get { return _c_Organisation_Type; } set { _c_Organisation_Type = value; b_Organisation_Type = true; } }
        public string v_Name { get { return _v_Name; } set { _v_Name = value; b_Name = true; } }
        public string v_Email { get { return _v_Email; } set { _v_Email = value; b_Email = true; } }
        public string v_Created_By { get { return _v_Created_By; } set { _v_Created_By = value; b_Created_By = true; } }
        public DateTime d_Created_On { get { return _d_Created_On; } set { _d_Created_On = value; b_Created_On = true; } }
        
       public string v_Mobile_No { get { return _v_Mobile_No; } set { _v_Mobile_No = value; b_Mobile_No = true; } }

    }
}
