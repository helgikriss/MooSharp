using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MooSharp.Models.Entities
{
	public class TestCase
	{
		/// <summary>
		/// Primary key.
		/// </summary>
		[Key]
		public int ID { get; set; }

		/// <summary>
		/// Foreign key.
		/// </summary>
		public int MilestoneID { get; set; }

		/// <summary>
		/// Input string to be read into the program for this test case.
		/// </summary>
		public string Input { get; set; }

		/// <summary>
		/// Output string that corresponds with the input string.
		/// </summary>
		public string Output { get; set; }

		/// <summary>
		/// Navigation property.
		/// </summary>
		[ForeignKey("MilestoneID")]
		public virtual Milestone Milestone { get; set; }
	}
}