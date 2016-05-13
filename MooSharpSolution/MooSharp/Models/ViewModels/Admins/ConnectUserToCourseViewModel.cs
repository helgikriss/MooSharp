using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MooSharp.Models.Entities;

namespace MooSharp.Models.ViewModels.Admins {

    /// <summary>
    /// This class connects a certain user to a certain course
    /// </summary>
    public class ConnectUserToCourseViewModel {

        /// <summary>
        /// Contains a CourseID which is required to find a certain course
        /// </summary>
        [Required]
		[Display(Name = "CourseID")]
		public int CourseID { get; set; }

        /// <summary>
        /// Contains UserID which is required
        /// </summary>
        [Required]
		[Display(Name = "UserID")]
		public string UserID { get; set; }

        /// <summary>
        /// A list that holds all courses
        /// </summary>
        public List<CourseViewModel> AllCourses { get; set; }

        /// <summary>
        /// A list that holds all the users
        /// </summary>
        public List<UserViewModel> AllUsers { get; set; }
	}
}