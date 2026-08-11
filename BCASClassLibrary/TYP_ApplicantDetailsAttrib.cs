using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BCASClassLibrary
{
    public class TYP_ApplicantDetailsAttrib
    {

        Int32 _i_Organisation_Code;
        Int32 _i_bOrganisation_Code;
        string _v_Employee_ID;
        string _v_Candidate_Name;
        //deep
        string _v_OrganisationNameOf_Candidate;
        string _v_Aadhaar_Number;


        string _v_PostedAtStateId;
        string _v_PostedAtCityId;

        string _v_EbasId;
        string _v_BcasId;
        string _v_oldNomination;
        //deep

        string _c_Sex_Code;
        Int32 _i_Designation_Code;
        DateTime _d_DOB;
        string _v_Email_Id;
        string _v_Mobile_No;
        Int32 _i_Nationality_Code;
        Int32 _i_Educational_Qualification_Code;
        Int32 _i_Registration_Number;
        string _v_Photo_Filename;
        string _v_Signature_Filename;
        string _v_PhotoIcard_Filename;
        string _v_Enter_by;
        DateTime _d_Enter_Date;
        Int32 _i_option;

        string _c_Is_Relieved;
        string _v_Relieve_Remarks;
        string _v_Relieved_By;
        DateTime _d_Relieving_Entry_Date;

        string _c_Is_Application_Complete; 



        public bool b_Organisation_Code { get; set; }
        public bool b_bOrganisation_Code { get; set; }
        public bool b_Employee_ID { get; set; }
        public bool b_Candidate_Name { get; set; }
        //deep
        public bool b_OrganisationNameOf_Candidate { get; set; }
        public bool b_Aadhaar_Number { get; set; }


        public bool b_PostedAtStateId { get; set; }
        public bool b_PostedAtCityId { get; set; }
        public bool b_EbasId { get; set; }
        public bool b_BcasId { get; set; }
        public bool b_oldNomination { get; set; }
       
        //deep
        public bool b_Sex_Code { get; set; }
        public bool b_Designation_Code { get; set; }
        public bool b_DOB { get; set; }
        public bool b_Email_Id { get; set; }
        public bool b_Mobile_No { get; set; }
        public bool b_Nationality_Code { get; set; }
        public bool b_Educational_Qualification_Code { get; set; }
        public bool b_Registration_Number { get; set; }
        public bool b_Photo_Filename { get; set; }
        public bool b_Signature_Filename { get; set; }

        public bool b_PhotoIcard_Filename { get; set; }

        public bool b_Enter_by { get; set; }
        public bool b_Enter_Date { get; set; }
        public bool b_option { get; set; }


        public bool b_Is_Relieved { get; set; }
        public bool b_Relieve_Remarks { get; set; }
        public bool b_Relieved_By { get; set; }
        public bool b_Relieving_Entry_Date { get; set; }
       
        public bool b_Is_Application_Complete { get; set; }



        public Int32 i_bOrganisation_Code { get { return _i_bOrganisation_Code; } set { _i_bOrganisation_Code = value; b_Organisation_Code = true; } }
        public Int32 i_Organisation_Code { get { return _i_Organisation_Code; } set { _i_Organisation_Code = value; b_Organisation_Code = true; } }
        public string v_Employee_ID { get { return _v_Employee_ID; } set { _v_Employee_ID = value; b_Employee_ID = true; } }
        public string v_Candidate_Name { get { return _v_Candidate_Name; } set { _v_Candidate_Name = value; b_Candidate_Name = true; } }
        //deep
        public string v_OrganisationNameOf_Candidate { get { return _v_OrganisationNameOf_Candidate; } set { _v_OrganisationNameOf_Candidate = value; b_OrganisationNameOf_Candidate = true; } }
        public string v_Aadhaar_Number { get { return _v_Aadhaar_Number; } set { _v_Aadhaar_Number = value; b_Aadhaar_Number = true; } }

        public string v_PostedAtStateId { get { return _v_PostedAtStateId; } set { _v_PostedAtStateId = value; b_PostedAtStateId = true; } }
        public string v_PostedAtCityId { get { return _v_PostedAtCityId; } set { _v_PostedAtCityId = value; b_PostedAtCityId = true; } }

        public string v_EbasId { get { return _v_EbasId; } set { _v_EbasId = value; b_EbasId = true; } }
        public string v_BcasId { get { return _v_BcasId; } set { _v_BcasId = value; b_BcasId = true; } }


        //deep
        public string c_Sex_Code { get { return _c_Sex_Code; } set { _c_Sex_Code = value; b_Sex_Code = true; } }
        public Int32 i_Designation_Code { get { return _i_Designation_Code; } set { _i_Designation_Code = value; b_Designation_Code = true; } }
        public DateTime d_DOB { get { return _d_DOB; } set { _d_DOB = value; b_DOB = true; } }
        public string v_Email_Id { get { return _v_Email_Id; } set { _v_Email_Id = value; b_Email_Id = true; } }
        public string v_Mobile_No { get { return _v_Mobile_No; } set { _v_Mobile_No = value; b_Mobile_No = true; } }
        public Int32 i_Nationality_Code { get { return _i_Nationality_Code; } set { _i_Nationality_Code = value; b_Nationality_Code = true; } }
        public Int32 i_Educational_Qualification_Code { get { return _i_Educational_Qualification_Code; } set { _i_Educational_Qualification_Code = value; b_Educational_Qualification_Code = true; } }
        public Int32 i_Registration_Number { get { return _i_Registration_Number; } set { _i_Registration_Number = value; b_Registration_Number = true; } }
        public string v_Photo_Filename { get { return _v_Photo_Filename; } set { _v_Photo_Filename = value; b_Photo_Filename = true; } }
        public string v_Signature_Filename { get { return _v_Signature_Filename; } set { _v_Signature_Filename = value; b_Signature_Filename = true; } }

        public string v_PhotoIcard_Filename { get { return _v_PhotoIcard_Filename; } set { _v_PhotoIcard_Filename = value; b_PhotoIcard_Filename = true; } }
        
        
        public string v_Enter_by { get { return _v_Enter_by; } set { _v_Enter_by = value; b_Enter_by = true; } }
        public DateTime d_Enter_Date { get { return _d_Enter_Date; } set { _d_Enter_Date = value; b_Enter_Date = true; } }
        public Int32 i_option { get { return _i_option; } set { _i_option = value; b_option = true; } }


        public string c_Is_Relieved { get { return _c_Is_Relieved; } set { _c_Is_Relieved = value; b_Is_Relieved = true; } }
        public string v_Relieve_Remarks { get { return _v_Relieve_Remarks; } set { _v_Relieve_Remarks = value; b_Relieve_Remarks = true; } }
        public string v_Relieved_By { get { return _v_Relieved_By; } set { _v_Relieved_By = value; b_Relieved_By = true; } }
        public DateTime d_Relieving_Entry_Date { get { return _d_Relieving_Entry_Date; } set { _d_Relieving_Entry_Date = value; b_Relieving_Entry_Date = true; } }

        public string c_Is_Application_Complete { get { return _c_Is_Application_Complete; } set { _c_Is_Application_Complete = value; b_Is_Application_Complete = true; } }

    }
}
