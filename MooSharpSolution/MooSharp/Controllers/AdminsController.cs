using MooSharp.Models.ViewModels;
using MooSharp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MooSharp.Controllers
{
    public class AdminsController : Controller
    {
		private CoursesService _coursesService = new CoursesService();
		// GET: Admins
		public ActionResult Index()
        {
            return View();
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