using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MooSharp.Models.Entities
{
	/// <summary>
	/// An Assignment represents a single assignment in a course.
	/// </summary>
    public class Assignment
    {
		/// <summary>
		/// The database-generated unique ID of the assignment.
		/// Primary key.
		/// </summary>
		[Key]
		public int ID { get; set; }

		/// <summary>
		/// Foreign key.
		/// </summary>
		public int CourseID { get; set; }

		/// <summary>
		/// Contains the name of the assignment.
		/// Example: "Lab 1"
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// Contains the description for the assignment.
		/// This description is the value 
		/// Example: "This assignment contains two milestones, each weighing 50% of the grade of the assignment".
		/// </summary>
		public string Description { get; set; }
		
		/// <summary>
		/// Declares when the Assignment should start
		/// </summary>
		public DateTime OpeningTime { get; set; }

		/// <summary>
		/// Declares when the Assignment should end
		/// </summary>
		public DateTime ClosingTime { get; set; }

		/// <summary>
		/// Navigation property.
		/// </summary>
		public virtual ICollection<Milestone> Milestones { get; set; }

		/// <summary>
		/// Navigation property.
		/// </summary>
		[ForeignKey("CourseID")]
		public virtual Course Course { get; set; }
	}
}