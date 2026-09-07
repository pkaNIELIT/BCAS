using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BCASClassLibrary
{
    public class TYP_CourseMasterAttrib 
    {

        Int32 _i_Course_Code;
        string _v_Course_Name;
        string _v_Enter_By;
        DateTime _d_Enter_Date;

        public bool b_Course_Code { get; set; }
        public bool b_Course_Name { get; set; }
        public bool b_Enter_By { get; set; }
        public bool b_Enter_Date { get; set; }

        public Int32 i_Course_Code { get { return _i_Course_Code; } set { _i_Course_Code = value; b_Course_Code = true; } }
        public string v_Course_Name { get { return _v_Course_Name; } set { _v_Course_Name = value; b_Course_Name = true; } }
        public string v_Enter_By { get { return _v_Enter_By; } set { _v_Enter_By = value; b_Enter_By = true; } }
        public DateTime d_Enter_Date { get { return _d_Enter_Date; } set { _d_Enter_Date = value; b_Enter_Date = true; } }

    }
}
