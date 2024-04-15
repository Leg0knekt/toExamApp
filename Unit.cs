using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace toExamApp
{
    public class Unit
    {
        public Unit(string unitname)
        {
            Unitname = unitname;
        }
        public string Unitname { get; set; }
    }
}
