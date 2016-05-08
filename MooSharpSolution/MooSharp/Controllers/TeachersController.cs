using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MooSharp.Models.ViewModels;
using MooSharp.Services;
using MooSharp.Utilities;

namespace MooSharp.Controllers
{
	[AccessDeniedAttribute(Roles = "Teachers")]
	[Authorize(Roles = "Teachers")]
	public class TeachersController : Controller
    {
		private AssignmentsService _assignmentsService = new AssignmentsService();
		private CoursesService _coursesService = new CoursesService();
		
        // GET: Teachers
        public ActionResult Index()
        {
            return View();
        }

		public ActionResult CourseDetails(int? id) {
			if (!id.HasValue) {
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			if (!_coursesService.CourseIsInDbById(Convert.ToInt32(id))) {
				throw new HttpException(404, "Not found");
			}
			var course = _coursesService.GetCourseById(Convert.ToInt32(id));

			return View(course);
		}

		public ActionResult CreateAssignment(int? courseID) {
			if (!courseID.HasValue) {
				Debug.WriteLine(courseID);
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			if (!_coursesService.CourseIsInDbById(Convert.ToInt32(courseID))) {
				throw new HttpException(404, "Not found");
			}
			var courseIdViewModel = new CourseIdViewModel {
				ID = Convert.ToInt32(courseID)
			};
			return View(courseIdViewModel);
		}

		[HttpPost]
		public ActionResult CreateAssignment(FormCollection collection) {

			var assignment = new CreateAssignmentViewModel() {
				Title = collection["assignmentname"],
				CourseID = Convert.ToInt32(collection["courseid"]),
				Description = collection["assignmentdescription"]
			};
			_assignmentsService.CreateAssignment(assignment);

			return RedirectToAction("Index");
		}
	}
}