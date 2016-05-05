using System;
using System.Collections.Generic;
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
		/// </summary>
		public int _ID { get; set; }

		/// <summary>
		/// The foreign key to the milestone that this submission is a part of.
		/// </summary>
		public int _milestoneID { get; set; }

		/// <summary>
		/// Contains the date and time of submission.
		/// Example: 1/1/1970 00:00:00
		/// </summary>
		public DateTime _submissionDateTime { get; set; }

		/// <summary>
		/// Contains the path to the submitted file.
		/// Example: "~\submissions\user\hello.cpp"
		/// </summary>
		public string _submissionPath { get; set; }

		/// <summary>
		/// Contains the status of the submission after it has been compiled, run and checked.
		/// status can contain for example "Accepted", "Wrong Answer", "Memory Error", etc.
		/// </summary>
		public string _status { get; set; }

		/// <summary>
		/// Contains a list of all the outputs that were obtained from running some input on it.
		/// Each item in the list is an output from a single time the program was run.
		/// </summary>
		public List<string> _outputs { get; set; }
	}
}