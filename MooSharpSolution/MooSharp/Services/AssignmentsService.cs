using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MooSharp.Models;
using MooSharp.Models.ViewModels;

namespace MooSharp.Services
{
    public class AssignmentsService
    {
		private ApplicationDbContext _db;

		public AssignmentsService() {
			_db = new ApplicationDbContext();
		}

        public list<AssignmentViewModel> GetAssignmentsByCourseId(int courseID) {
			// TODO: Finish GetAssignmentsByCourseId() method.
			return null;
        }

		public AssignmentViewModel GetAssignmentById(int assignmentID) {
			var assignment = _db._assignments.SingleOrDefault(x => x._ID == assignmentID);
			if(assignment == null) {
				// TODO: Throw exception.
			}

			var milestones = _db._milestones
				.Where(x => x._assignmentID == assignmentID)
				.Select(x => new MilestoneViewModel {
					_title = x._title;
				})
				.ToList();

			var viewModel = new AssignmentViewModel {
				_title = assignment._title;
				_milestones = milestones;
			};

			return viewModel;
		}
    }
}