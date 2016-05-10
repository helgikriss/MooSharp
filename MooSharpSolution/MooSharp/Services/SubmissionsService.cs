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
			//Búa til entity og save-a í database
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