using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MooSharp.Models.ViewModels.Students
{
	public class StudentIndexViewModel
	{
		public List<AssignmentViewModel> ActiveAssignments { get; set; }

		public List<AssignmentViewModel> FinishedAssignments { get; set; }

		public List<CourseViewModel> MyCourses { get; set; }
	}
}