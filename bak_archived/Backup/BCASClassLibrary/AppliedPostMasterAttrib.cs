using System;

namespace BCASClassLibrary
{
    public abstract class AppliedPostMasterAttrib
    {
        // Private variables
        Int32 _n_Post_Applied;
        string _v_Description;
        string _v_Enter_By;
        DateTime _d_Enter_Date;

        // Boolean properties
        public bool b_Post_Applied { get; set; }
        public bool b_Description { get; set; }
        public bool b_Enter_By { get; set; }
        public bool b_Enter_Date { get; set; }

        // Public Property
        public Int32 n_Post_Applied { get { return _n_Post_Applied; } set { _n_Post_Applied = value; b_Post_Applied = true; } }
        public string v_Description { get { return _v_Description; } set { _v_Description = value; b_Description = true; } }
        public string v_Enter_By { get { return _v_Enter_By; } set { _v_Enter_By = value; b_Enter_By = true; } }
        public DateTime d_Enter_Date { get { return _d_Enter_Date; } set { _d_Enter_Date = value; b_Enter_Date = true; } }
    }
}
