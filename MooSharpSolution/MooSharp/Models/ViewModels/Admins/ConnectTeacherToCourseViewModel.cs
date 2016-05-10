using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MooSharp.Models.Entities;

namespace MooSharp.Models.ViewModels.Admins
{
	public class ConnectTeacherToCourseViewModel
	{
		[Required]
		[Display(Name = "CourseID")]
		public int CourseID { get; set; }

		[Required]
		[Display(Name = "UserID")]
		public string UserID { get; set; }

		public string CourseTitle { get; set; }

		public List<UserViewModel> AllTeachers { get; set; }
	}
}