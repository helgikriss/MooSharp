using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MooSharp.Models.ViewModels.Admins
{
	public class CreateCourseViewModel
	{
		/// <summary>
		/// Contains the course number which can be both alpha and numerical characters.
		/// Example: "T-220-VLN2".
		/// </summary>
		[Required]
		[Display(Name = "CourseNumber")]
		public string CourseNumber { get; set; }

		/// <summary>
		/// Contains the title of the course.
		/// Example: "Verklegt námskeið 2"
		/// </summary>
		[Required]
		[Display(Name = "Name")]
		public string Name { get; set; }
	}
}