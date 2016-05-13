using MooSharp.Models;
using MooSharp.Models.Entities;
using MooSharp.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace MooSharp.Services
{
	public class AssignmentsService
	{
		private IAppDataContext _db;
		private MilestonesService _milestonesService;

		public AssignmentsService()
		{
			_db = new ApplicationDbContext();
			_milestonesService = new MilestonesService();
		}
		public AssignmentsService(IAppDataContext context) {
			_db = context;
			_milestonesService = new MilestonesService();
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
                    ID = a.ID,
					OpeningDate = a.OpeningTime,
					DueDate = a.ClosingTime

                };
                viewModels.Add(viewModel);   
            }
            return viewModels;
        }

		public AssignmentViewModel GetAssignmentByID(int assignmentID) {
			var assignment = _db.Assignments.SingleOrDefault(x => x.ID == assignmentID);
			if (assignment == null)
			{
                throw new Exception("Not found");
            }

			var milestones = _db.Milestones
				.Where(x => x.AssignmentID == assignmentID)
				.Select(x => new MilestoneViewModel
				{
					Title = x.Title,
					Description = x.Description,
					Weight = x.Weight,
					ID = x.ID,
					Submissions =	_db.Submissions
									.Where(y => y.MilestoneID == x.ID)
									.Select(z => new SubmissionViewModel {
										ID  = z.ID,
										MilestoneID = z.ID,
										Status = z.Status,
										SubmissionDateTime = z.SubmissionDateTime,
										SubmissionPath = z.SubmissionPath,
										UserID = z.UserID,
										UserName =	_db.Users.Where(a => a.Id == z.UserID).Select(b => b.UserName).FirstOrDefault().ToString()
									}).ToList()

				})
				.ToList();

			var viewModel = new AssignmentViewModel
			{
				ID = assignment.ID,
				Title = assignment.Title,
				CourseID = assignment.CourseID,
				Description = assignment.Description,
				CourseTitle = assignment.Title,
				DueDate = assignment.ClosingTime,
				OpeningDate = assignment.OpeningTime,
				Milestones = milestones
				
			};

			return viewModel;
		}
		/// <summary>
		/// CreateAssignment takes in a viewmodel that has string value for date and time.
		/// The function splits donw the date to format it the way SQL Server wants it's DATETIME
		/// input to have.
		/// Parses all the information, create's a new assignment and writes it down to the DB
		/// </summary>
		public int CreateAssignment(CreateAssignmentViewModel model) {

			char delimiter = '-';
			string[] OpeningDateSplit = model.OpeningDate.Split(delimiter);
			string[] ClosingDateSplit = model.ClosingDate.Split(delimiter);

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
		public List<AssignmentViewModel> GetActiveAssignments(string userID) {
			var userAssignments = (from assignment in _db.Assignments
						   join connection in _db.CourseUsers on assignment.CourseID equals connection.CourseID
						   where userID == connection.UserID
						   select assignment).ToList();

            var userAssignmentViewModels = new List<AssignmentViewModel>();
			foreach(Assignment a in userAssignments) {
				var viewModel = new AssignmentViewModel() {
					CourseID = a.CourseID,
					Description = a.Description,
					Title = a.Title,
					ID = a.ID,
					OpeningDate = a.OpeningTime,
                    DueDate = a.ClosingTime
				};
				var result = DateTime.Compare(a.ClosingTime, DateTime.Today);
				// DateTime.Compare: If less than 0 the left parameter is earlier 
				// If equal to 0 it's the same time. If greater than 0 the right parameter is earlier 
				if(result >= 0){
					userAssignmentViewModels.Add(viewModel);
				}
			}
			userAssignmentViewModels.Sort((x, y) => DateTime.Compare(x.DueDate, y.DueDate));
			return userAssignmentViewModels;
		}

		public List<AssignmentViewModel> GetFinishedAssignments(string userID) {
			var userAssignments = (from assignment in _db.Assignments
								   join connection in _db.CourseUsers on assignment.CourseID equals connection.CourseID
								   where userID == connection.UserID
								   select assignment).ToList();

			var userAssignmentViewModels = new List<AssignmentViewModel>();

			foreach (Assignment a in userAssignments) {
				//Creates a viewmodel class for each assignment
				var viewModel = new AssignmentViewModel() {
					CourseID = a.CourseID,
					Description = a.Description,
					Title = a.Title,
					ID = a.ID
				};

				//Compares current date with the closing date of each assignment and adds current assignments to list.
				var result = DateTime.Compare(a.ClosingTime, DateTime.Today);

				if (result < 0) {
					userAssignmentViewModels.Add(viewModel);
				}
			}
			userAssignmentViewModels.Sort((x, y) => DateTime.Compare(y.DueDate, x.DueDate));
			return userAssignmentViewModels;
		}

		public bool AssignmentIsInDbById(int id) {
			var assignment = _db.Assignments.Where(x => x.ID == id).FirstOrDefault();
			if (assignment == null) {
				return false;
			}
			return true;
		}

		public int GetTotalWeightOfMilestonesInAssignment(int id) {
			List<Milestone> milestones = _milestonesService.GetAllMilestonesByAssignmentId(id);

			int totalWeight = 0;
			if (milestones != null) {
				foreach (Milestone m in milestones) {
					totalWeight += m.Weight;
				}
			}

			return totalWeight;
		}
	}
}
