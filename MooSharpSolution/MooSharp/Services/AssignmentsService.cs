using MooSharp.Models;
using MooSharp.Models.Entities;
using MooSharp.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MooSharp.Services
{
	public class AssignmentsService
	{
		private ApplicationDbContext _db;

		public AssignmentsService()
		{
			_db = new ApplicationDbContext();
		}

		public List<AssignmentViewModel> GetAssignmentsInCourse(int courseID)
		{
			// TODO:
			return null;
		}

		public AssignmentViewModel GetAssignmentByID(int assignmentID)
		{
			var assignment = _db.Assignments.SingleOrDefault(x => x.ID == assignmentID);
			if (assignment == null)
			{
				// TODO: kasta villu!
			}

			var milestones = _db.Milestones
				.Where(x => x.AssignmentID == assignmentID)
				.Select(x => new MilestoneViewModel
				{
					Title = x.Title
				})
				.ToList();

			var viewModel = new AssignmentViewModel
			{
				Title      = assignment.Title,
				Milestones = milestones
			};

			return viewModel;
		}

		public int CreateAssignment(AssignmentViewModel model) {
			var assignment = new Assignment() {
				CourseID = model.CourseID,
				Title = model.Title,
				Description = model.Description
				
			};
			_db.Assignments.Add(assignment);

			return assignment.ID;
		}
	}
}
