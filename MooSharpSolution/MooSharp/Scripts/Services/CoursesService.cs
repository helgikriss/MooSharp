using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MooSharp.Models.ViewModels;
using MooSharp.Models;
using MooSharp.Models.Entities;

namespace MooSharp.Services
{
	public class CoursesService
	{
		private ApplicationDbContext _db;

		public CoursesService() {
			_db = new ApplicationDbContext();
		}

		//TODO: Write code here
		public void Create(CreateCourseViewModel course) {
			var cour = new Course() {
				CourseNumber = course.CourseNumber,
				Title = course.Name
			};

			_db.Courses.Add(cour);
			_db.SaveChanges();
		}
	}
}