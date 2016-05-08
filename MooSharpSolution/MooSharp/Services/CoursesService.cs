using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MooSharp.Models.ViewModels;
using MooSharp.Models;
using MooSharp.Models.Entities;
using MooSharp.Models.ViewModels.Admins;

namespace MooSharp.Services
{
	/// <summary>
	/// This class handles all service regarding Courses.
	/// For example to create and get courses.
	/// </summary>
	public class CoursesService
	{
		private ApplicationDbContext _db;

		public CoursesService() {
			_db = new ApplicationDbContext();
		}
		
		/// <summary>
		/// Takes in CreateCourseViewModel and saves it to the database.
		/// </summary>
		public bool Create(CreateCourseViewModel course) {
			var cour = new Course() {
				CourseNumber = course.CourseNumber,
				Title = course.Name
			};

			_db.Courses.Add(cour);
			_db.SaveChanges();
			return true;
		}

		/// <summary>
		/// Returns a CourseViewModel by ID. Returns null if Course is not in database.
		/// </summary>
		public CourseViewModel GetCourseById(int id) {
			var course = _db.Courses.Find(id);

			if(course == null) {
				return null;
			}

			var assignments = _db.Assignments
				.Where(x => x.CourseID == id)
				.Select(x => new AssignmentInCourseViewModel {
					ID = x.ID,
					Title = x.Title,
					Description = x.Description
				})
				.ToList();

			var courseViewModel = new CourseViewModel() {
				ID = course.ID,
				Title = course.Title,
				CourseNumber = course.CourseNumber,
				Assignments = assignments
			};

			return courseViewModel;
		}
		
		/// <summary>
		/// Returns a list of CourseViewModel that contains all Courses
		/// in the database.
		/// </summary>
		public List<CourseViewModel> GetAllCourses() {
			var courses = _db.Courses.ToList();

			var viewmodels = new List<CourseViewModel>();

			foreach (Course c in courses) {
				var viewmodel = new CourseViewModel() {
					CourseNumber = c.CourseNumber,
					Title = c.Title,
					ID = c.ID
				};
				viewmodels.Add(viewmodel);
			}
			return viewmodels;
		}

		/// <summary>
		/// Returns true if Course is in database based, false otherwise.
		/// </summary>
		public bool CourseIsInDbById(int id) {
			var course = _db.Courses.Find(id);
			if (course == null) {
				return false;
			}
			return true;
		}
	}
}