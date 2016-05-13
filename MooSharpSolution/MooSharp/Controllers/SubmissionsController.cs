using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using MooSharp.Models.ViewModels;
using MooSharp.Models.ViewModels.Students;
using MooSharp.Services;


namespace MooSharp.Controllers
{
    public class SubmissionsController : Controller
    {
		CoursesService _coursesService = new CoursesService();
		MilestonesService _milestonesService = new MilestonesService();
		AssignmentsService _assignmentsService = new AssignmentsService();
		SubmissionsService _submissionsService = new SubmissionsService();

		// GET: Submissions
		public ActionResult Index(){
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
			var extension = Path.GetExtension(file.FileName);

			string path = "";

			if (file.ContentLength > 0) {
				var fileName = Path.GetFileName(file.FileName);
				var mapPath = Server.MapPath(String.Format("~/App_Data/Submissions/{0}/{1}/{2}/{3}", 
															userName, 
															courseTitle + "_" + courseID, 
															assignmentID + "_" + assignmentTitle, 
															milestoneID + "_" + milestoneTitle));
				path = Path.Combine(mapPath, fileName);
				if (!Directory.Exists(mapPath)) {
					Directory.CreateDirectory(mapPath);
				}
				
				int i = 0;
				while (System.IO.File.Exists(path)) {
					if(i == 0) {
						path = path.Replace(extension, "(" + ++i + ")" + extension);
					}
					else {
						path = path.Replace("(" + i + ")" + extension, "(" + ++i + ")" + extension);
					}
				}
				file.SaveAs(path);
			}

			var submissionViewModel = new CreateSubmissionViewModel() {
				UserID = User.Identity.GetUserId(),
				MilestoneID = milestoneID,
				SubmissionDateTime = DateTime.Now,
				SubmissionPath = path
			};

			int submissionID = _submissionsService.CreateSubmission(submissionViewModel, extension);

			string exeFileFullPath = path.Replace(".cpp", ".exe");
			string objFileFullPath = path.Replace(".cpp", ".obj");
			
			if (System.IO.File.Exists(exeFileFullPath)) {
				System.IO.File.Delete(exeFileFullPath);
			}
			if (System.IO.File.Exists(objFileFullPath)) {
				System.IO.File.Delete(objFileFullPath);
			}

			return RedirectToAction("AssignmentDetails", "Students", new { assignmentID = assignmentID });
		}
	}
}