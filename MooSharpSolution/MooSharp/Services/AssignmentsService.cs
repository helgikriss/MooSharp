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
            var assignmentsInCourse = _db.Assignments.Where(x => x.CourseID == courseID).ToList();

            var viewModels = new List<AssignmentViewModel>();

            foreach (Assignment a in assignmentsInCourse) {
                var viewModel = new AssignmentViewModel()
                {
                    CourseID = a.CourseID,
                    Description = a.Description,
                    Title = a.Title,
                    ID = a.ID
                };
                viewModels.Add(viewModel);   
            }
            return viewModels;
        }

		public AssignmentViewModel GetAssignmentByID(int assignmentID)
		{
			var assignment = _db.Assignments.SingleOrDefault(x => x.ID == assignmentID);
			if (assignment == null)
			{
                throw new Exception("Not found");
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
				ID = assignment.ID,
				Title = assignment.Title,
				CourseID = assignment.CourseID,
				Description = assignment.Description
				
				//TODO: Milestones = milestones,
			};

			return viewModel;
		}

		public int CreateAssignment(CreateAssignmentViewModel model) {
			var assignment = new Assignment() {
				Title = model.Title,
				CourseID = model.CourseID,
				Description = model.Description
			};
			_db.Assignments.Add(assignment);
			_db.SaveChanges();

			var id = Convert.ToInt32(_db.Assignments.ToList().LastOrDefault().ID);
			return id;
		}

		public bool AssignmentIsInDbById(int id) {
			var assignment = _db.Assignments.Find(id);
			if (assignment == null) {
				return false;
			}
			return true;
		}
	}
}
