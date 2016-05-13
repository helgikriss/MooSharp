using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MooSharp.Models.ViewModels.Admins
{
    /// <summary>
    /// This class holds lists of all users and all courses
    /// that appear on the admin index page
    /// </summary>
    public class AdminIndexViewModel
	{
        /// <summary>
        /// A list that holds all courses
        /// </summary>
        public List<CourseViewModel> AllCourses { get; set; }

        /// <summary>
        /// a list that holds all users
        /// </summary>
        public List<UserViewModel> AllUsers { get; set; }
	}
}