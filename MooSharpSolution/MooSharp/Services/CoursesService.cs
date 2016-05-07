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
		public List<CourseViewModel> GetAllCourses() {
			var courses = _db.Courses.ToList();
			var viewmodels = new List<CourseViewModel>();

<<<<<<< HEAD
			foreach(Course c in courses) {
=======
			foreach (Course c in courses) {
>>>>>>> 3ea9d6a313145681425b15cb282b258ecbbc64cc
				var viewmodel = new CourseViewModel() {
					CourseNumber = c.CourseNumber,
					Title = c.Title,
					ID = c.ID
				};
				viewmodels.Add(viewmodel);
			}
			return viewmodels;
		}
	}
}