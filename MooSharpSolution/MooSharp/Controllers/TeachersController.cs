using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MooSharp.Models.ViewModels;
using MooSharp.Services;

namespace MooSharp.Controllers
{
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
			var course = _coursesService.GetCourseById(Convert.ToInt32(id));
			if (course == null) {
				// return HttpNotFound();
				throw new HttpException(404, "Not found");
			}
			return View(course);
		}

		public ActionResult CreateAssignment() {
			// TODO: Create AssignmentIdViewModel with courseID to send into CreateAssignment view
			return View();
		}

		[HttpPost]
		public ActionResult CreateAssignment(FormCollection collection) {
			var assignment = new CreateAssignmentViewModel() {
				Title = collection["assignmentname"],
				CourseID = Convert.ToInt32(collection["courseid"]),
				Description = collection["assignmentdescription"]
			};
			int assignmentID = _assignmentsService.InitialCreateAssignment(assignment);

			return RedirectToAction("Index");
		}
	}
}