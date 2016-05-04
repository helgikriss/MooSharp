﻿namespace MooSharp.Models.Entities
{
	/// <summary>
	/// An Assignment represents a single assignment in a course.
	/// </summary>
    public class Assignment
    {
		/// <summary>
		/// The database-generated unique ID of the assignment.
		/// </summary>
		public int _ID { get; set; }

		/// <summary>
		/// The foreign key to the course that this assignment is a part of.
		/// </summary>
		public int _courseID { get; set; }

		/// <summary>
		/// Contains the name of the assignment.
		/// Example: "Lab 1"
		/// </summary>
		public string _title { get; set; }

		/// <summary>
		/// Contains the description for the assignment.
		/// This description is the value 
		/// Example: "This assignment contains two milestones, each weighing 50% of the grade of the assignment".
		/// </summary>
		public string _description { get; set; }
    }
}