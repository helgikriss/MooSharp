using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MooSharp.Models.ViewModels.Students
{
	public class SubmissionResultsViewModel
	{
		public int ID { get; set; }

		public int MilestoneID { get; set; }

		public int UserID { get; set; }
		
		public DateTime SubmissionDateTime { get; set; }

		public string Status { get; set; }

		public bool Compiled { get; set; }

		public string CompilerOutput { get; set; }

		List<SubmissionTestCaseViewModel> SubmissionTestCases { get; set; }


	}
}