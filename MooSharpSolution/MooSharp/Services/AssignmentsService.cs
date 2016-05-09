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
				ID = assignment.ID,
				Title      = assignment.Title,
				CourseID = assignment.CourseID.ToString(),
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

		public bool AssignmentIsInDbById(int id) {
			var assignment = _db.Assignments.Find(id);
			if (assignment == null) {
				return false;
			}
			return true;
		}
	}
}
