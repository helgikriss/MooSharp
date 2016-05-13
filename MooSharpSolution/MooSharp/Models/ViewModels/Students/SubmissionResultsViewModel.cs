using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MooSharp.Models.ViewModels.Students
{
    /// <summary>
    /// This class contains the information displayed
    /// in the Submission result page
    /// </summary>
	public class SubmissionResultsViewModel
	{
        /// <summary>
        /// Contains the id of the submission
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Contains the id of the milestone that the
        /// submission is submitted to
        /// </summary>
        public int MilestoneID { get; set; }

        /// <summary>
        /// Contains the title of the main assignment 
        /// </summary>
        public string AssignmentTitle { get; set; }

        /// <summary>
        /// Contains the tite of the milestone
        /// </summary>
        public string MilestoneTitle { get; set; }

        /// <summary>
        /// Contains the user id which is submitting 
        /// the result
        /// </summary>
        public string UserID { get; set; }

        /// <summary>
        /// Contains the date and time of the submission
        /// </summary>
        public DateTime SubmissionDateTime { get; set; }
        
        /// <summary>
        /// A general status of the submission.
        /// Example: Wrong answer, compile error etc.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// A bool function that determines if the
        /// submission compiled or not
        /// </summary>
        public bool Compiled { get; set; }

        /// <summary>
        /// The output of the compilation if it failed
        /// </summary>
        public string CompilerOutput { get; set; }

        /// <summary>
        /// A list that contains all the test cases of the submission
        /// </summary>
        public List<SubmissionTestCaseViewModel> SubmissionTestCases { get; set; }


	}
}