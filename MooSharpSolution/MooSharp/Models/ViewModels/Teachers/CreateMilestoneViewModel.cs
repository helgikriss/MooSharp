using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MooSharp.Models.ViewModels.Teachers
{
	public class CreateMilestoneViewModel
	{
		public int AssignmentID { get; set; }
		
		public string Title { get; set; }
		
		public int Weight { get; set; }
		
		public string Description { get; set; }
		
		public int TotalWeightOfMilestones { get; set; }

		public string AssignmentTitle { get; set; }

		public HttpPostedFileBase InputFile { get; set; }

		public HttpPostedFileBase OutputFile { get; set; }
	}
}