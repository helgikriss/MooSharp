using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MooSharp.Models.Entities
{
    public class Assignment
    {
		public int _ID { get; set; }
		public int _courseID { get; set; }
		public string _title { get; set; }
		public string _description { get; set; }
    }
}