using System;
using System.Collections.Generic;
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
		/// </summary>
		public int _ID { get; set; }

		/// <summary>
		/// Contains the course number which can be both alpha and numerical characters.
		/// Example: "T-220-VLN2".
		/// </summary>
		public string _courseNumber { get; set; }

		/// <summary>
		/// Contains the title of the course.
		/// Example: "Verklegt námskeið 2"
		/// </summary>
		public string _title { get; set; }
	}
}