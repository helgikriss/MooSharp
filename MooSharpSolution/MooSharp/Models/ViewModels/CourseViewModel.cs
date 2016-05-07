﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MooSharp.Models.ViewModels
{
	public class CourseViewModel
	{
		public int ID { get; set; }
		public string CourseNumber { get; set; }
		public string Title { get; set; }
		public List<AssignmentInCourseViewModel> Assignments { get; set; }
	}
}