using System;
using System.Web;
using System.Web.Mvc;
using MooSharp.Models.ViewModels;
using MooSharp.Services;
using MooSharp.Utilities;
using MooSharp.Models.ViewModels.Teachers;
using Microsoft.AspNet.Identity;

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
			var viewModel = new TeacherIndexViewModel() {
				MyCourses = _coursesService.GetCoursesByUser(User.Identity.GetUserId())
			};
            return View(viewModel);
        }

		public ActionResult CourseDetails(int? id) {
			if (!id.HasValue) {
				throw new HttpException(400, "Bad Request");
			}
			if (!_coursesService.CourseIsInDbById(Convert.ToInt32(id))) {
				throw new HttpException(404, "Not Found");
			}

			var course = _coursesService.GetCourseById(Convert.ToInt32(id));

			return View(course);
		}

		public ActionResult CreateAssignment(int? courseID) {
			if (!courseID.HasValue) {
				throw new HttpException(400, "Bad Request");
			}
			if (!_coursesService.CourseIsInDbById(Convert.ToInt32(courseID))) {
				throw new HttpException(404, "Not Found");
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
				//return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
				throw new HttpException(400, "Bad Request");
			}
			if (!_assignmentsService.AssignmentIsInDbById(Convert.ToInt32(assignmentID))) {
				//throw new Exception("Not found");
				throw new HttpException(404, "Not Found");
			}

				var assignmentViewModel = _assignmentsService.GetAssignmentByID(Convert.ToInt32(assignmentID));

			return View(assignmentViewModel);
		}
		
		public ActionResult CreateMilestone(int? assignmentID) {
			if (!assignmentID.HasValue) {
				throw new HttpException(400, "Bad Request");
			}
			if (!_assignmentsService.AssignmentIsInDbById(Convert.ToInt32(assignmentID))) {
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
			// TODO: gera IsValid check á viewmodel
			var id = _milestoneService.CreateMilestone(viewModel);
			return RedirectToAction("AssignmentDetails", new { assignmentID = id });
		}
	}
}