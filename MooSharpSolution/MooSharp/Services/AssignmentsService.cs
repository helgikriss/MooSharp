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

        public List<AssignmentViewModel> GetAssignmentsByCourseId(int courseID) {
			// TODO: Finish GetAssignmentsByCourseId() method.
			return null;
        }

        public AssignmentViewModel GetAssignmentById(int assignmentID) {
            var assignment = _db.Assignments.SingleOrDefault(x => x.ID == assignmentID);
            if (assignment == null) {
                // TODO: Throw exception.
            }

            var milestones = _db.Milestones
                .Where(x => x.AssignmentID == assignmentID)
                .Select(x => new MilestoneViewModel {
                    Title = x.Title
                })
                .ToList();

            var viewModel = new AssignmentViewModel {
                Title = assignment.Title,
                Milestones = milestones
            };

			return viewModel;
		}
    }
}