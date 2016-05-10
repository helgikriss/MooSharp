using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MooSharp.Models.Entities {
	public class CourseUsers {
		public int ID { get; set; }

		public int CourseID { get; set; }

		public string UserID { get; set; }

		public string role { get; set; }

		public virtual Course Course { get; set; }

		public virtual ApplicationUser ApplicationUser { get; set; }
	}
}