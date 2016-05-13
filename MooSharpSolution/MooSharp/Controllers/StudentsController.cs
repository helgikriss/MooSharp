using Microsoft.AspNet.Identity;
using MooSharp.Models.ViewModels.Students;
using MooSharp.Services;
using MooSharp.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MooSharp.Models.ViewModels;

namespace MooSharp.Controllers
{
	[AccessDeniedAttribute(Roles = "Students")]
	[Authorize(Roles = "Students")]
	public class StudentsController : Controller
    {
		private CoursesService _coursesService = new CoursesService();
		private AssignmentsService _assignmentsService = new AssignmentsService();
		private SubmissionsService _submissionsService = new SubmissionsService();
		private MilestonesService _milestonesService = new MilestonesService();
        // GET: Students
        public ActionResult Index()
        {
			var viewModel = new StudentIndexViewModel() {
				ActiveAssignments = _assignmentsService.GetActiveAssignments(User.Identity.GetUserId())
									.Where(x => DateTime.Compare(x.OpeningDate,DateTime.Today) <= 0).ToList(),
				FinishedAssignments = _assignmentsService.GetFinishedAssignments(User.Identity.GetUserId()),
				MyCourses = _coursesService.GetCoursesByUser(User.Identity.GetUserId())
				
			};
            return View(viewModel);
        }

		public ActionResult CreateSubmission(int? milestoneID) {
			if(!milestoneID.HasValue) {
				throw new HttpException(400, "Bad Request");
			}
			if (!_milestonesService.MilestoneIsInDbById(Convert.ToInt32(milestoneID))) {
				throw new HttpException(404, "Not Found");
			}
			var viewModel = new CreateSubmissionViewModel() {
				MilestoneID = Convert.ToInt32(milestoneID),
				UserID = User.Identity.GetUserId().ToString()
			};
			return View(viewModel);
		}

		[HttpPost]
		public ActionResult CreateSubmission(CreateSubmissionViewModel viewModel) {
			//Hugsanlega compile-a og fara yfir verkefni og skila því
			//Kalla í CreateSubmission í submissionService
			return null;
		}

		public ActionResult CourseDetails(int? id) {
			if (!id.HasValue) {
				throw new HttpException(400, "Bad Request");
			}
			if (!_coursesService.CourseIsInDbById(Convert.ToInt32(id))) {
				throw new HttpException(404, "Not Found");
			}

			var course = _coursesService.GetCourseById(Convert.ToInt32(id));
			course.Assignments.Sort((y, x) => DateTime.Compare(x.DueDate, y.DueDate));
			course.Assignments = course.Assignments
								.Where(x => DateTime.Compare(x.OpeningDate, DateTime.Today) <= 0).ToList();
			return View(course);
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

			var viewModel = new AssignmentDetailsViewModel() {
				CourseID = assignmentViewModel.CourseID,
				CourseTitle = assignmentViewModel.CourseTitle,
				Description = assignmentViewModel.Description,
				DueDate = assignmentViewModel.DueDate,
				ID = assignmentViewModel.ID,
				Milestones = assignmentViewModel.Milestones,
				Title = assignmentViewModel.Title
			};

			return View(viewModel);
		}

		public ActionResult SubmissionResults(int submissionID) {
			var viewModel = _submissionsService.GetSubmissionById(submissionID);

			return View(viewModel);
		}
	}
}