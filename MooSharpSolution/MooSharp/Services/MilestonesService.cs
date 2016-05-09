using MooSharp.Models;
using MooSharp.Models.Entities;
using MooSharp.Models.ViewModels.Teachers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MooSharp.Services
{
    public class MilestonesService
    {
		private ApplicationDbContext _db;

		public MilestonesService() {
			_db = new ApplicationDbContext();
		}

		public int CreateMilestone(CreateMilestoneViewModel model) {
			var milestone = new Milestone() {
				AssignmentID = model.AssignmentID,
				Title = model.Title,
				Weight = model.Weight
			};
			_db.Milestones.Add(milestone);
			_db.SaveChanges();

			var id = Convert.ToInt32(_db.Assignments.ToList().LastOrDefault().ID);
			return id;
		}
		public bool MilestoneIsInDbById(int id) {
			var milestone = _db.Milestones.Find(id);
			if (milestone == null) {
				return false;
			}
			return true;
		}
	}
}