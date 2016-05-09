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
		/// </summary>
		public int ID { get; set; }

		/// <summary>
		/// The foreign key to the assignment that this milestone is a part of.
		/// </summary>
		public virtual int AssignmentID { get; set; }

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

		// TODO: Add input/output property eða a það heima i TestCases töflu ?.
	}
}