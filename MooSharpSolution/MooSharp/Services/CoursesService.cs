using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MooSharp.Models.ViewModels;
using MooSharp.Models;
using MooSharp.Models.Entities;
using MooSharp.Models.ViewModels.Admins;
using System.Diagnostics;

namespace MooSharp.Services
{
	/// <summary>
	/// This class handles all service regarding Courses.
	/// For example to create and get courses.
	/// </summary>
	public class CoursesService
	{
		private ApplicationDbContext _db;
		private IdentityManager _manager;
		private UsersService _usersService;

		public CoursesService() {
			_db = new ApplicationDbContext();
			_manager = new IdentityManager();
			_usersService = new UsersService();
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
					Description = x.Description,
					DueDate = x.ClosingTime
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
		/// Returns a list of courses connected to the specific user
		/// </summary>
		/// <param name="userId"></param>
		/// <returns></returns>
		public List<CourseViewModel> GetCoursesByUser(string userId) {
			if(_db.Users.Find(userId) == null) {
				throw new HttpException(400, "Bad Request");
			}

			var courses = (from course in _db.Courses
						   join connection in _db.CourseUsers on course.ID equals	connection.CourseID
						   where userId == connection.UserID
						   select course).ToList();

			var viewModels = new List<CourseViewModel>();

			foreach (Course c in courses) {
				var viewmodel = new CourseViewModel() {
					CourseNumber = c.CourseNumber,
					Title = c.Title,
					ID = c.ID
				};
				viewModels.Add(viewmodel);
			}
			return viewModels;
		}

		/// <summary>
		/// Returns a list of CourseViewModel that contains all Courses
		/// in the database.
		/// </summary>
		public List<CourseViewModel> GetAllCourses() {
			var courses = _db.Courses.ToList();

			var viewModels = new List<CourseViewModel>();

			foreach (Course c in courses) {
				var viewmodel = new CourseViewModel() {
					CourseNumber = c.CourseNumber,
					Title = c.Title,
					ID = c.ID,
					Students = _usersService.GetStudentsByCourse(c.ID),
					Teachers = _usersService.GetTeachersByCourse(c.ID)
				};
				viewModels.Add(viewmodel);
			}
			return viewModels;
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

		/// <summary>
		/// Returns true if connecting user to course was successful, false otherwise.
		/// </summary>
		public bool ConnectUserToCourse(string userID, int courseID) {
			if (UserConnectedToCourse(userID, courseID)) {
				return false;
			}

			var newConnection = new CourseUser() {
				CourseID = courseID,
				UserID = userID
			};

			_db.CourseUsers.Add(newConnection);
			_db.SaveChanges();
			return true;
		}

		/// <summary>
		/// Returns true if user is connected to course, false otherwise.
		/// </summary>
		public bool UserConnectedToCourse(string userID, int courseID) {
			var result = _db.CourseUsers.Find(courseID, userID);

			if (result != null) {
				return true;
			}
			return false;
		}
	}
}