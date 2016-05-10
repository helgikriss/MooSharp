using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MooSharp.Models.ViewModels
{
	public class SubmissionViewModel
	{
		public int ID { get; set; }

		public int MilestoneID { get; set; }

		public string UserID { get; set; }

		public DateTime SubmissionDateTime { get; set; }

		public string SubmissionPath { get; set; }

		public string Status { get; set; }

		public List<string> Outputs { get; set; }

	}
}