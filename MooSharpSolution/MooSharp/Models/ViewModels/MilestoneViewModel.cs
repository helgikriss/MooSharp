using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MooSharp.Models.ViewModels
{
	public class MilestoneViewModel
	{
		public string Title { get; set; }

        public string Description { get; set; }

        public int ID { get; set; }

        public int Weight { get; set; }

		public List<SubmissionViewModel> Submissions { get; set; }
    }
}