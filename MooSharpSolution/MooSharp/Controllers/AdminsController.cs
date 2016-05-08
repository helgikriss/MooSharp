using MooSharp.Models.ViewModels;
using MooSharp.Models.ViewModels.Admins;
using MooSharp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MooSharp.Controllers
{
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

		public ActionResult CreateCourse() {
			return View();
		}

		[HttpPost]
		public ActionResult CreateCourse(FormCollection collection) {

			var course = new CreateCourseViewModel() {
				CourseNumber = collection["coursenumber"],
				Name = collection["coursename"]
			};
			_coursesService.Create(course);

			return RedirectToAction("Index");
		}
	}
}