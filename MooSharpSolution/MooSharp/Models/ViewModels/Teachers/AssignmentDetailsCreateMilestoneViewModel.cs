using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MooSharp.Models.ViewModels.Teachers
{
	public class AssignmentDetailsCreateMilestoneViewModel
	{
		public int ID { get; set; }

		public string AssignmentTitle { get; set; }

		public int CourseID { get; set; }

		public string AssignmentDescription { get; set; }

		public List<MilestoneViewModel> Milestones { get; set; }

		public DateTime DueDate { get; set; }

		public string CourseTitle { get; set; }

		[Required]
		[Display(Name = "AssignmentID")]
		public int AssignmentID { get; set; }

		[Required]
		[Display(Name = "MilestoneTitle")]
		public string MilestoneTitle { get; set; }

		[Required]
		[Display(Name = "Weight")]
		[Range(0, 100)]
		public int Weight { get; set; }

		[Required]
		[Display(Name = "Description")]
		public string MilestoneDescription { get; set; }

		[Display(Name = "TotalWeightOfMilestones")]
		public int TotalWeightOfMilestones { get; set; }

		[Display(Name = "InputFile")]
		public HttpPostedFileBase InputFile { get; set; }

		[Required]
		[Display(Name = "OutputFile")]
		public HttpPostedFileBase OutputFile { get; set; }
	}
}