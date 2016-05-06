using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MooSharp.Models.ViewModels
{
	public class AssignmentViewModel
	{
		public string Title { get; set; }

		public int CourseID { get; set; }

		public string Description { get; set; }

		public List<MilestoneViewModel> Milestones { get; set; }
	}
}
