using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MooSharp.Models.ViewModels
{
	public class CreateAssignmentViewModel {
		[Required]
		[Display(Name = "ID")]
		public int ID { get; set; }
		[Required]
		[Display(Name = "Title")]
		public string Title { get; set; }

		[Required]
		[Display(Name = "CourseID")]
		public int CourseID { get; set; }

		[Display(Name = "Description")]
		public string Description { get; set; }

		[Required]
		[Display(Name = "Opening Date")]
		public string OpeningDate { get; set; }

		[RegularExpression("([01]?[0-9]|2[0-3]):[0-5][0-9]", ErrorMessage = "The time has to be in 24 hour format, f.e. 13:37")]
		[Required]
		[Display(Name = "Opening time")]
		public string OpeningTime { get; set; }

		[Required]
		[Display(Name = "Closing Date")]
		public string ClosingDate { get; set; }

		[RegularExpression("([01]?[0-9]|2[0-3]):[0-5][0-9]", ErrorMessage = "The time has to be in 24 hour format, f.e. 13:37")]
		[Required]
		[Display(Name = "Closing time")]
		public string ClosingTime { get; set; }
	}
}