using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MooSharp.Models.ViewModels.Teachers
{
	public class TeacherIndexViewModel
	{
		public List<CourseViewModel> MyCourses { get; set; }

		public List<AssignmentViewModel> ActiveAssignments { get; set; }

		public List<AssignmentViewModel> FinishedAssignments { get; set; }
	}
}