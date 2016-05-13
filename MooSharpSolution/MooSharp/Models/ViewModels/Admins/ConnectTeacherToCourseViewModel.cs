using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MooSharp.Models.Entities;

namespace MooSharp.Models.ViewModels.Admins
{
    /// <summary>
    /// This class connects teachers by username to a certain course
    /// </summary>
    public class ConnectTeacherToCourseViewModel
	{
        /// <summary>
        /// CourseID is required to find a certain course
        /// </summary>
        [Required]
		[Display(Name = "CourseID")]
		public int CourseID { get; set; }

        /// <summary>
        /// UserID is required to find a certain teacher
        /// </summary>
        [Required]
		[Display(Name = "UserID")]
		public string UserID { get; set; }

        /// <summary>
        /// The title of the course that the teacher will be assigned to
        /// </summary>
        public string CourseTitle { get; set; }

        /// <summary>
        /// A list that holds all the teachers 
        /// </summary>
        public List<UserViewModel> AllTeachers { get; set; }
	}
}