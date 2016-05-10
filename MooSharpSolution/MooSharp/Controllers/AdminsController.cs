using Microsoft.AspNet.Identity;
using MooSharp.Models;
using MooSharp.Models.ViewModels;
using MooSharp.Models.ViewModels.Admins;
using MooSharp.Services;
using MooSharp.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MooSharp.Controllers
{
	/// <summary>
	/// This class handles all control for Admins.
	/// Example: Create course, create user.
	/// </summary>
	[AccessDeniedAttribute(Roles = "Administrators")]
	[Authorize(Roles = "Administrators")]
	public class AdminsController : Controller
    {
		/// <summary>
		/// Instance of CoursesService for Admin to be able to work with Courses.
		/// </summary>
		private CoursesService _coursesService = new CoursesService();
		/// <summary>
		/// Instance of UsersService for Admin to be able to work with Users.
		/// </summary>
		private UsersService _usersService = new UsersService();

		/// <summary>
		/// Returns Index view with AdminIndexViewModel.
		/// </summary>
		public ActionResult Index() {
			var viewmodel = new AdminIndexViewModel() {
				AllCourses = _coursesService.GetAllCourses(),
				AllUsers = _usersService.GetAllUsers()
			};
            return View(viewmodel);
        }

		/// <summary>
		/// Returns a view with a form to be filled out to create a course
		/// </summary>
		public ActionResult CreateCourse() {
			return View();
		}

		/// <summary>
		/// Createcourse gets a viewmodel with information about a new course and 
		/// sends that info to CourseService to be added
		/// </summary>
		[HttpPost]
		public ActionResult CreateCourse(CreateCourseViewModel viewModel) {

			if (ModelState.IsValid) {
				_coursesService.Create(viewModel);
				return RedirectToAction("Index");
			}
			return View(viewModel);
		}

		/// <summary>
		/// Returns CreateUser view.
		/// </summary>
		public ActionResult CreateUser() {
			return View();
		}

		/// <summary>
		/// Takes in CreateUserViewModel that has been filled in by Admin and creates
		/// a new user with that information.
		/// </summary>
		[HttpPost]
		public ActionResult CreateUser(CreateUserViewModel viewModel) {
			// TODO: taka a móti forminu úr CreatUser og senda ViewModel í UserService CreateUser fallið
			if (ModelState.IsValid) {
				if (_usersService.CreateUser(viewModel)) {
					return RedirectToAction("Index");
				}
				else {
					ModelState.AddModelError("", "Creating User has failed, please try again.");
				}
			}
			return View(viewModel);
		}

		public ActionResult ConnectUserToCourse() {

			return View();
		}
		[HttpPost]
		public ActionResult ConnectUserToCourse(ConnectUserToCourseViewModel viewModel) {
			var newViewModel = new ConnectUserToCourseViewModel {
				CourseID = 2,
				UserID = "0b3e3e67-62b0-431f-8d34-30c76c8573af",
				Role = "Teacher"
			};
			if (!_coursesService.ConnectUserToCourse(newViewModel)) {
				ModelState.AddModelError("", "User is already connected to this course");
			}

			return View();
		}
	}
}