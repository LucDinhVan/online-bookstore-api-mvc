using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TriThucOnline_TTN.Models
{
    public class Publisher
    {
        public string name { get; set; }
        public int id { get; set;}
    }

    public class Publishers
    {
        public List<Publisher> publishers { get; set; }
    }
}