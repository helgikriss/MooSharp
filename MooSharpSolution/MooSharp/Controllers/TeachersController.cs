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
using MooSharp.Models.ViewModels.Teachers;

namespace MooSharp.Controllers
{
	[AccessDeniedAttribute(Roles = "Teachers")]
	[Authorize(Roles = "Teachers")]
	public class TeachersController : Controller
    {
		private AssignmentsService _assignmentsService = new AssignmentsService();
		private CoursesService _coursesService = new CoursesService();
		private MilestonesService _milestoneService = new MilestonesService();
		
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
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			if (!_coursesService.CourseIsInDbById(Convert.ToInt32(courseID))) {
				throw new HttpException(404, "Not found");
			}

			var CreateAssignmentViewModel = new CreateAssignmentViewModel {
				CourseID = Convert.ToInt32(courseID)
			};
			return View(CreateAssignmentViewModel);
		}

		[HttpPost]
		public ActionResult CreateAssignment(CreateAssignmentViewModel viewModel) {
			var id = _assignmentsService.CreateAssignment(viewModel);
			return RedirectToAction("AssignmentDetails", new { assignmentID = id });
		}

		public ActionResult AssignmentDetails(int? assignmentID) {
			if (!assignmentID.HasValue) {
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			if (!_assignmentsService.AssignmentIsInDbById(Convert.ToInt32(assignmentID))) {
				throw new HttpException(404, "Not found");
			}
			var assignment = _assignmentsService.GetAssignmentByID(Convert.ToInt32(assignmentID));

			return View(assignment);
		}
		
		public ActionResult CreateMilestone(int? assignmentID) {
			if (!assignmentID.HasValue) {
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			if (!_milestoneService.MilestoneIsInDbById(Convert.ToInt32(assignmentID))) {
				throw new HttpException(404, "Not found");
			}

			var CreateMilestoneViewModel = new CreateMilestoneViewModel {
				AssignmentID = Convert.ToInt32(assignmentID)
			};
			return View(CreateMilestoneViewModel);
		}
		// TODO: Eftir að klara utfæra CreateMilestone POST fallið í MilestoneService
		[HttpPost]
		public ActionResult CreateMilestone(CreateMilestoneViewModel viewModel) {
			var id = _milestoneService.CreateAssignment(viewModel);
			return RedirectToAction("AssignmentDetails", new { assignmentID = viewModel.AssignmentID });
		}
	}
}