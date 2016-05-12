using System;
using System.Web;
using System.Web.Mvc;
using MooSharp.Models.ViewModels;
using MooSharp.Services;
using MooSharp.Utilities;
using MooSharp.Models.ViewModels.Teachers;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;

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
				MyCourses = _coursesService.GetCoursesByUser(User.Identity.GetUserId()),
				ActiveAssignments = _assignmentsService.GetActiveAssignments(User.Identity.GetUserId()),
				FinishedAssignments = _assignmentsService.GetFinishedAssignments(User.Identity.GetUserId())

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
			course.Assignments.Sort((y, x) => DateTime.Compare(x.DueDate, y.DueDate));

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

			var detailsViewModel = new AssignmentDetailsCreateMilestoneViewModel() {
				ID = assignmentViewModel.ID,
				AssignmentTitle = assignmentViewModel.Title,
				CourseTitle = _coursesService.GetCourseById(assignmentViewModel.CourseID).Title,
				CourseID = assignmentViewModel.CourseID,
				AssignmentDescription = assignmentViewModel.Description,
				Milestones = assignmentViewModel.Milestones,
				TotalWeightOfMilestones = _assignmentsService.GetTotalWeightOfMilestonesInAssignment(Convert.ToInt32(assignmentID))
			};

			return View(detailsViewModel);
		}
		/*
		public ActionResult CreateMilestone(int? assignmentID) {
			if (!assignmentID.HasValue) {
				throw new HttpException(400, "Bad Request");
			}
			if (!_assignmentsService.AssignmentIsInDbById(Convert.ToInt32(assignmentID))) {
				throw new HttpException(404, "Not found");
			}

			var CreateMilestoneViewModel = new CreateMilestoneViewModel {
				AssignmentID = Convert.ToInt32(assignmentID),
				TotalWeightOfMilestones = _assignmentsService.GetTotalWeightOfMilestonesInAssignment(Convert.ToInt32(assignmentID)),
				AssignmentTitle = _assignmentsService.GetAssignmentByID(Convert.ToInt32(assignmentID)).Title
			};
			return View(CreateMilestoneViewModel);
		}
		*/
		[HttpPost]
		public ActionResult CreateMilestone(AssignmentDetailsCreateMilestoneViewModel viewModel) {

			if (!ModelState.IsValid) {
				return View(viewModel);
			}
			
			var newViewModel = new CreateMilestoneViewModel() {
				AssignmentID = viewModel.ID,
				AssignmentTitle = viewModel.AssignmentTitle,
				Description = viewModel.MilestoneDescription,
				Title = viewModel.MilestoneTitle,
				Weight = viewModel.Weight,
				InputFile = viewModel.InputFile,
				OutputFile = viewModel.OutputFile
			};

			_milestoneService.CreateMilestone(newViewModel);
			return RedirectToAction("AssignmentDetails", new { assignmentID = viewModel.ID });
		}
	}
}