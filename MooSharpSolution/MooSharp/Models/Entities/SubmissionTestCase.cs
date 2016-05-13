using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MooSharp.Models.Entities
{
	public class SubmissionTestCase
	{
		/// <summary>
		/// Primary Key.
		/// </summary>
		[Key]
		public int ID { get; set; }

		/// <summary>
		/// Foreign key.
		/// </summary>
		public int SubmissionID { get; set; }

		/// <summary>
		/// String that contains the obtained output of this submission.
		/// </summary>
		public string Output { get; set; }

		/// <summary>
		/// True if timelimit exceeded during runtime false otherwise.
		/// </summary>
		public bool TimeLimitExceeded { get; set; }

		/// <summary>
		/// True if there were any memory errors during runtime, false otherwise.
		/// </summary>
		public bool MemoryError { get; set; }

		/// <summary>
		/// Output of the memory error, if there was any.
		/// </summary>
		public string MemoryErrorOutput { get; set; }

		/// <summary>
		/// True if output was correct, false otherwise.
		/// </summary>
		public bool WrongOutput { get; set; }

		/// <summary>
		/// Navigation propery.
		/// </summary>
		[ForeignKey("SubmissionID")]
		public virtual Submission Submission { get; set; }
	}
}