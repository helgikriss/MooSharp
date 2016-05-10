using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MooSharp.Models.Entities;

namespace MooSharp.Models.ViewModels.Admins {
	public class ConnectUserToCourseViewModel {
		[Required]
		[Display(Name = "CourseID")]
		public int CourseID { get; set; }

		[Required]
		[Display(Name = "UserID")]
		public string UserID { get; set; }

		[Required]
		[Display(Name = "Role")]
		public string Role { get; set; }

		public List<CourseViewModel> AllCourses { get; set; }

		public List<UserViewModel> AllUsers { get; set; }
	}
}