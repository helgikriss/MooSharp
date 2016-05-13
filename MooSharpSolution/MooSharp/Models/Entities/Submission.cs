using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MooSharp.Models.Entities
{
	/// <summary>
	/// A Submission represents a single submission to a milestone.
	/// Submission contains the date and time of submission, where the
	/// submitted file is located on the server, the status of the submission
	/// and a list of strings with the obtained output.
	/// </summary>
	public class Submission
	{
		/// <summary>
		/// The database-generated unique ID of the submission.
		/// Primary key.
		/// </summary>
		[Key]
		public int ID { get; set; }

		/// <summary>
		/// Foreign key.
		/// </summary>
		public int MilestoneID { get; set; }

		/// <summary>
		/// Foreign key.
		/// </summary>
		public string UserID { get; set; }

		/// <summary>
		/// Contains the date and time of submission.
		/// Example: 1/1/1970 00:00:00
		/// </summary>
		public DateTime SubmissionDateTime { get; set; }

		/// <summary>
		/// Contains the path to the submitted file.
		/// Example: "~\submissions\user\hello.cpp"
		/// </summary>
		public string SubmissionPath { get; set; }

		/// <summary>
		/// Contains the status of the submission after it has been compiled, run and checked.
		/// status can contain for example "Accepted", "Wrong Answer", "Memory Error", etc.
		/// </summary>
		public string Status { get; set; }

		/// <summary>
		/// True if submission was able to be compiled, false otherwise.
		/// </summary>
		public bool Compiled { get; set; }

		/// <summary>
		/// The output from the compiler.
		/// </summary>
		public string CompilerOutput { get; set; }

		/// <summary>
		/// Navigation property.
		/// </summary>
		[ForeignKey("MilestoneID")]
		public virtual Milestone Milestone { get; set; }

		/// <summary>
		/// Navigation property.
		/// </summary>
		[ForeignKey("UserID")]
		public virtual ApplicationUser User { get; set; }

		/// <summary>
		/// Navigation property.
		/// </summary>
		public ICollection<SubmissionTestCase> SubmissionTestCases { get; set; }
	}
}