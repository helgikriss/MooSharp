using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MooSharp.Models.ViewModels.Students
{
	public class CreateSubmissionViewModel
	{
		[Required]
		[Display(Name = "ID")]
		public int ID { get; set; }

		[Required]
		[Display(Name = "MilestoneID")]
		public int MilestoneID { get; set; }

		[Required]
		[Display(Name = "UserID")]
		public string UserID { get; set; }

		[Required]
		[Display(Name = "Submission Date/Time")]
		public DateTime SubmissionDateTime { get; set; }

		[Required]
		[Display(Name = "Submission path")]
		public string SubmissionPath { get; set; }

		[Required]
		[Display(Name = "Status")]
		public string Status { get; set; }
	}
}