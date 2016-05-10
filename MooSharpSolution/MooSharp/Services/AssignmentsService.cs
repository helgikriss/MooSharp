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
		/// <summary>
		/// CreateAssignment takes in a viewmodel that has string value for date and time.
		/// The function splits donw the date to format it the way SQL Server wants it's DATETIME
		/// input to have.
		/// Parses all the information, create's a new assignment and writes it down to the DB
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		public int CreateAssignment(CreateAssignmentViewModel model) {

			char[] delimiters = { '-', ':' };
			string[] OpeningDateSplit = model.OpeningDate.Split(delimiters[0]);
			string[] ClosingDateSplit = model.ClosingDate.Split(delimiters[0]);

			string Opening = OpeningDateSplit[0] + "/" + OpeningDateSplit[1] + "/" + OpeningDateSplit[2] + " " + model.OpeningTime + ":00.00";
			string Closing = ClosingDateSplit[0] + "/" + ClosingDateSplit[1] + "/" + ClosingDateSplit[2] + " " + model.OpeningTime + ":00.00";

			var assignment = new Assignment() {
				Title = model.Title,
				CourseID = model.CourseID,
				Description = model.Description,
				OpeningTime = Convert.ToDateTime(Opening),
				ClosingTime = Convert.ToDateTime(Closing)
			};
			_db.Assignments.Add(assignment);
			_db.SaveChanges();

			var id = Convert.ToInt32(_db.Assignments.ToList().LastOrDefault().ID);
			return id;
		}

		/// <summary>
		/// Takes in the ID of the user, checks which courses he is connected to and returns
		/// all active assignments in those courses.
		/// </summary>
		public List<AssignmentViewModel> GetActiveAssignments(int userID) {
			//var userAssignments = _db.Assignments.Where(JOIN USERID WITH COURSES HERE).ToList();
			var userAssignments = new List<Assignment>();
			var userAssignmentViewModels = new List<AssignmentViewModel>();

			foreach(Assignment a in userAssignments) {
				//Creates a viewmodel class for each assignment
				var viewModel = new AssignmentViewModel() {
					CourseID = a.CourseID,
					Description = a.Description,
					Title = a.Title,
					ID = a.ID
				};

				//Compares current date with the closing date of each assignment and adds current assignments to list.
				var result = DateTime.Compare(a.ClosingTime, DateTime.Today);

				if(result == 0) {
					userAssignmentViewModels.Add(viewModel);
				}
				else if(result > 0){
					userAssignmentViewModels.Add(viewModel);
				}
			}

			return userAssignmentViewModels;
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
