using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MooSharp.Models.Entities
{
	/// <summary>
	/// A Milestone represents a single part of an assignment.
	/// Each assignment can contain one or more milestones and each milestone
	/// weighs a certain percentage of the final grade of the assignment.
	/// </summary>
	public class Milestone
	{
		/// <summary>
		/// The database-generated unique ID of the milestone.
		/// Primary key.
		/// </summary>
		[Key]
		public int ID { get; set; }

		/// <summary>
		/// The foreign key to the assignment that this milestone is a part of.
		/// Foreign key.
		/// </summary>
		public int AssignmentID { get; set; }

		/// <summary>
		/// Contains the name of the milestone.
		/// Example: "Part 1"
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// Determines how much this milestone weighs of the entire assignment.
		/// Example: If the milestone is 15% of the grade of the assignment then
		/// this property contains the value 15.
		/// </summary>
		public int Weight { get; set; }
		
        /// <summary>
		/// Describes the milestone.
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// Navigation property.
		/// </summary>
		[ForeignKey("AssignmentID")]
		public virtual Assignment Assignment { get; set; }

		/// <summary>
		/// Navigation property.
		/// </summary>
		public virtual ICollection<TestCase> TestCases { get; set; }

		/// <summary>
		/// Navigation property.
		/// </summary>
		public virtual ICollection<Submission> Submissions { get; set; }
	}
}