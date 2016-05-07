using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MooSharp.Models.ViewModels
{
	public class CourseViewModel
	{
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
<<<<<<< HEAD
		public string Title { get; set; }
=======
		public string Title { get; set; }.
>>>>>>> 3ea9d6a313145681425b15cb282b258ecbbc64cc
	}
}