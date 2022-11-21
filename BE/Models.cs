using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Models
    {
        //jason hebcals

    }
    public class HeDateParts
    {
        public string y { get; set; }
        public string m { get; set; }
        public string d { get; set; }
    }
    public class RootHeb
    {
        public int gy { get; set; }
        public int gm { get; set; }
        public int gd { get; set; }
        public bool afterSunset { get; set; }
        public int hy { get; set; }
        public string hm { get; set; }
        public int hd { get; set; }
        public string hebrew { get; set; }
        public HeDateParts heDateParts { get; set; }
        public List<string> events { get; set; }


    }

    public class Calendar
    {
        public List<RootHeb> days { get; set; }

            
            public Calendar() { days = new List<RootHeb>(); }
        }
    }



