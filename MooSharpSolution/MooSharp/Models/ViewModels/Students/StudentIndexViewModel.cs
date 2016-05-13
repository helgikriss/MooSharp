using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MooSharp.Models.ViewModels.Students
{
    /// <summary>
    /// This class contains the information displayed
    /// on the student index page
    /// </summary>
	public class StudentIndexViewModel
	{
        /// <summary>
        /// A list of the students active assignments
        /// which are not due yet
        /// </summary>
        public List<AssignmentViewModel> ActiveAssignments { get; set; }

        /// <summary>
        /// A list of all the assignments that have been 
        /// </summary>
        public List<AssignmentViewModel> FinishedAssignments { get; set; }

		public List<CourseViewModel> MyCourses { get; set; }
	}
}