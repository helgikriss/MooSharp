using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MooSharp.Models.ViewModels.Students
{
    /// <summary>
    /// This class creates a submission
    /// </summary>
    public class CreateSubmissionViewModel
	{
        /// <summary>
        /// Contains an id of the submission
        /// </summary>
        [Required]
		[Display(Name = "ID")]
		public int ID { get; set; }

        /// <summary>
        /// Contains an id of the milstone which
        /// the submission is being submitted to
        /// </summary>
        [Required]
		[Display(Name = "MilestoneID")]
		public int MilestoneID { get; set; }

        /// <summary>
        /// Contains an user id which is submitting
        /// the result
        /// </summary>
        [Required]
		[Display(Name = "UserID")]
		public string UserID { get; set; }

        /// <summary>
        /// The date and time of the submission
        /// </summary>
        [Required]
		[Display(Name = "Submission Date/Time")]
		public DateTime SubmissionDateTime { get; set; }

        /// <summary>
        /// A directory path that the submission goes to,
        /// once subitted
        /// </summary>
        [Required]
		[Display(Name = "Submission path")]
		public string SubmissionPath { get; set; }

        /// <summary>
        /// A general status of the submission
        /// Example: Time limit exceeded, wrong answer etc.
        /// </summary>
        [Required]
		[Display(Name = "Status")]
		public string Status { get; set; }
	}
}