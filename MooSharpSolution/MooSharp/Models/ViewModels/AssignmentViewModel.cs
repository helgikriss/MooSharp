using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MooSharp.Models.ViewModels
{
    public class AssignmentViewModel
    {
		public string Title { get; set; }
		public List<MilestoneViewModel> Milestones { get; set; }

		// TODO: Finish all AssignmentViewModel properties and add summary to them.
	}
}