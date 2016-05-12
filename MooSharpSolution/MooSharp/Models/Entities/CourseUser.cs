using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MooSharp.Models.Entities
{
	public class CourseUser
	{
		/// <summary>
		/// Foreign key.
		/// </summary>
		[Key, Column(Order = 0)]
		public int CourseID { get; set; }

		/// <summary>
		/// Foreign key.
		/// </summary>
		[Key, Column(Order = 1)]
		public string UserID { get; set; }

		/// <summary>
		/// Navigation property.
		/// </summary>
		[ForeignKey("CourseID")]
		public virtual Course Course { get; set; }

		/// <summary>
		/// Navigation property.
		/// </summary>
		[ForeignKey("UserID")]
		public virtual ApplicationUser User { get; set; }
	}
}