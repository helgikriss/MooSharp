using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MooSharp.Models.ViewModels.Students
{
	public class SubmissionTestCaseViewModel
	{
		public int ID { get; set; }

		public int SubmissionID { get; set; }

		public bool WrongOutput { get; set; }

		public string Output { get; set; }

		public string ExpectedOutput { get; set; }

		public bool TimeLimitExceeded { get; set; }

		public bool MemoryError { get; set; }

		public string MemoryErrorOutput { get; set; }
	}
}