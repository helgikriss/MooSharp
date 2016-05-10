using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MooSharp.Models.Entities {
	public class CourseUsers {
		[Key, ForeignKey("Course"), Column(Order = 0)]
		public int CourseID { get; set; }

		[Key, ForeignKey("User"), Column(Order = 1)]
		public string UserID { get; set; }
		
		public virtual Course Course { get; set; }

		public virtual ApplicationUser User { get; set; }
	}
}