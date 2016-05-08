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
	[AccessDeniedAttribute(Roles = "Administrators")]
	[Authorize(Roles = "Administrators")]
	public class AdminsController : Controller
    {
		private CoursesService _coursesService = new CoursesService();
		private UsersService _usersService = new UsersService();

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
		/// <returns></returns>
		public ActionResult CreateCourse() {
			return View();
		}
		/// <summary>
		/// Createcourse gets a viewmodel with information about a new course and 
		/// sends that info to CourseService to be added
		/// </summary>
		[HttpPost]
		public ActionResult CreateCourse(FormCollection collection) {

			var course = new CreateCourseViewModel() {
				CourseNumber = collection["coursenumber"],
				Name = collection["coursename"]
			};
			_coursesService.Create(course);

			return RedirectToAction("Index");
		}

		public ActionResult CreateUser() {
			return View();
		}

		[HttpPost]
		public ActionResult CreateUser(CreateUserViewModel viewModel) {
			// TODO: taka a móti forminu úr CreatUser og senda ViewModel í UserService CreateUser fallið
			if (ModelState.IsValid) {
				if (_usersService.CreateUser(viewModel)) {
					return RedirectToAction("Index");
				}
			}
			return View(viewModel);
		}
	}
}