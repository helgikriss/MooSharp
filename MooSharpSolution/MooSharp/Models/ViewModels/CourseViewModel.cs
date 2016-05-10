using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MooSharp.Models.ViewModels
{
	public class CourseViewModel
	{
		public int ID { get; set; }
		public string CourseNumber { get; set; }
		/// <summary>
		/// Contains the title of the course.
		/// Example: "Verklegt námskeið 2"
		/// </summary>

		public string Title { get; set; }
		public List<AssignmentInCourseViewModel> Assignments { get; set; }

		public List<UserViewModel> Students { get; set; }

		public List<UserViewModel> Teachers { get; set; }
	}
}