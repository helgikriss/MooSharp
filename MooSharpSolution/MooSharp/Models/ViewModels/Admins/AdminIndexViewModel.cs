using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MooSharp.Models.ViewModels.Admins
{
	public class AdminIndexViewModel
	{
		public List<CourseViewModel> AllCourses { get; set; }
		public List<UserViewModel> AllUsers { get; set; }
	}
}