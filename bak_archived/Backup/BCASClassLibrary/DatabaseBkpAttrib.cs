using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BCASClassLibrary;

namespace BCASClassLibrary
{
    public class DatabaseBkpAttrib
    {
        string _v_filePath;

        public bool b_filePath { get; set; }

        public string v_filePath { get { return _v_filePath; } set { _v_filePath = value; b_filePath = true; } }

    }
}
