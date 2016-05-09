using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MooSharp.Models.ViewModels
{
	public class CreateAssignmentViewModel
	{
		[Required]
		[Display(Name ="ID")]
		public int ID { get; set; }
		[Required]
		[Display(Name = "Title")]
		public string Title { get; set; }

		[Required]
		[Display(Name = "CourseID")]
		public int CourseID { get; set; }

		[Display(Name = "Description")]
		public string Description { get; set; }
	}
}