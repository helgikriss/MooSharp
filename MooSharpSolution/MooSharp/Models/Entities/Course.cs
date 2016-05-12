using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MooSharp.Models.Entities
{
	/// <summary>
	/// A Course represents a single academic course.
	/// A Course can have multiple assignments and multiple users 
	/// associated with it.
	/// </summary>
    public class Course
    {
		/// <summary>
		/// The database-generated unique ID of the course.
		/// Primary key.
		/// </summary>
		[Key]
		public int ID { get; set; }

		/// <summary>
		/// Contains the course number which can be both alpha and numerical characters.
		/// Example: "T-220-VLN2".
		/// </summary>
		public string CourseNumber { get; set; }

		/// <summary>
		/// Contains the title of the course.
		/// Example: "Verklegt námskeið 2"
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// Navigation property.
		/// </summary>
		public ICollection<CourseUser> CourseUsers { get; set; }

		/// <summary>
		/// Navigation property.
		/// </summary>
		public ICollection<Assignment> Assignments { get; set; }
	}
}