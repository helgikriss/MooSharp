using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MooSharp.Models.ViewModels
{
	public class CreateAssignmentViewModel
	{
		public string Title { get; set; }

		public int CourseID { get; set; }

		public string Description { get; set; }
	}
}