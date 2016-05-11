using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using MooSharp.Models.ViewModels;
using MooSharp.Services;
using MooSharp.Models.ViewModels.Students;

namespace MooSharp.Controllers
{
    public class SubmissionsController : Controller
    {
		// TODO: Finish SubmissionsController.
		CoursesService _coursesService = new CoursesService();
		MilestonesService _milestonesService = new MilestonesService();
		AssignmentsService _assignmentsService = new AssignmentsService();
		SubmissionsService _submissionsService = new SubmissionsService();

		// GET: Submissions
		public ActionResult Index()
        {
            return View();
        }

		public ActionResult UploadSubmission(AssignmentDetailsViewModel viewModel, HttpPostedFileBase file) {
			var courseID = viewModel.CourseID;
			var assignmentID = viewModel.ID;
			var milestoneID = viewModel.MilestoneID;
			var userName = User.Identity.GetUserName();
			var courseTitle = _coursesService.GetCourseById(courseID).Title;
			var assignmentTitle = _assignmentsService.GetAssignmentByID(assignmentID).Title;
			var milestoneTitle = _milestonesService.GetMilestonetByID(milestoneID).Title;

			if (file.ContentLength > 0) {
				var fileName = Path.GetFileName(file.FileName);
				var mapPath = Server.MapPath(String.Format("~/App_Data/Submissions/{0}/{1}/{2}/{3}", userName, courseID + "-" + courseTitle, assignmentID + "-" + assignmentTitle, milestoneID + "-" + milestoneTitle));
				var path = Path.Combine(mapPath, fileName);
				if (!Directory.Exists(mapPath)) {
					Directory.CreateDirectory(mapPath);
				}
				file.SaveAs(path);

				var submissionViewModel = new CreateSubmissionViewModel() {
					MilestoneID = milestoneID,
					UserID = User.Identity.GetUserId(),
					SubmissionDateTime = DateTime.Now,
					SubmissionPath = path
				};
				_submissionsService.CreateSubmission(submissionViewModel);
			}
			// TODO: Compile and check submission and return results
			return RedirectToAction("Index", "Home");
		}
	}
}