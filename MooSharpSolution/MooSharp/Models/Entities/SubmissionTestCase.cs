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
		/// Navigation propery.
		/// </summary>
		[ForeignKey("SubmissionID")]
		public virtual Submission Submission { get; set; }
	}
}