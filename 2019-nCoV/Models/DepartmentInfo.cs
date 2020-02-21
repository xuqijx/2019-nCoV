using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _2019_nCoV.Models
{
    public class DepartmentInfo
    {
        public int ID { get; set; }
        public string onelevel { get; set; }
        public string twolevel { get; set; }
        public string threelevel { get; set; }
    }
    public class DepartmentData
    {
        public List<DepartmentInfo> data { get; set; }
        public string success { get; set; }
        public string msg { get; set; }
    }
}