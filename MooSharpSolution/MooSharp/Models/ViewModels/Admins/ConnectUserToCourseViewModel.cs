using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MooSharp.Models.ViewModels.Admins {
	public class ConnectUserToCourseViewModel {
		public int CourseID { get; set; }

		public string UserID { get; set; }

		public string role { get; set; }
	}
}