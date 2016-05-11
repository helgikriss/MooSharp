using MooSharp.Models;
using MooSharp.Models.Entities;
using MooSharp.Models.ViewModels.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MooSharp.Services
{
    public class SubmissionsService
    {
		private ApplicationDbContext _db;

		public SubmissionsService() {
			_db = new ApplicationDbContext();
		}

		public void CreateSubmission(CreateSubmissionViewModel viewModel) {
			//Compile and run.
			var submission = new Submission() {
				ID = viewModel.ID,
				MilestoneID = viewModel.MilestoneID,
				UserID = viewModel.UserID,
				SubmissionDateTime = viewModel.SubmissionDateTime,
				Outputs = viewModel.Outputs,
				Status = viewModel.Status,
				SubmissionPath = viewModel.SubmissionPath
			};
			_db.Submissions.Add(submission);
			_db.SaveChanges();
		}
		public bool SubmissionIsInDbById(int id) {
			var submission = _db.Submissions.Find(id);
			if (submission == null) {
				return false;
			}
			return true;
		}
	}
}