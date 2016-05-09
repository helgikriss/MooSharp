using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MooSharp.Models.ViewModels
{
	public class AssignmentViewModel
	{
		public int ID { get; set; }
		public string Title { get; set; }

		public string CourseID { get; set; }

		public string Description { get; set; }

		public List<MilestoneViewModel> Milestones { get; set; }
	}
}
