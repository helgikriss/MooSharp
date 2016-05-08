using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MooSharp.Models.ViewModels.Teachers
{
	public class CreateMilestoneViewModel
	{
		[Required]
		[Display(Name = "AssignmentID")]
		public int AssignmentID { get; set; }

		[Required]
		[Display(Name = "Title")]
		public string Title { get; set; }

		[Required]
		[Display(Name = "Weight")]
		public int Weight { get; set; }
	}
}