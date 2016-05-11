using MooSharp.Models;
using MooSharp.Models.Entities;
using MooSharp.Models.ViewModels;
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
				Description = model.Description,
				AssignmentID = model.AssignmentID,
				Title = model.Title,
				Weight = model.Weight
			};
			_db.Milestones.Add(milestone);
			_db.SaveChanges();

			return model.AssignmentID;
		}

		public bool MilestoneIsInDbById(int id) {
			var milestone = _db.Milestones.Find(id);
			if (milestone == null) {
				return false;
			}
			return true;
		}

        public MilestoneViewModel GetMilestonetByID(int milestoneID)
        {
            var milestone = _db.Milestones.SingleOrDefault(x => x.ID == milestoneID);
            if (milestone == null)
            {
                throw new Exception("Not found");
            }

            var viewModel = new MilestoneViewModel {
                //ID, Title, description etc...

                Title = milestone.Title,
                Description = milestone.Description,
                Weight = milestone.Weight,
                ID = milestone.ID
            };

            return viewModel;
        }

		public List<Milestone> GetAllMilestonesByAssignmentId(int id) {
			List<Milestone> milestones = (from milestone in _db.Milestones
										  where milestone.AssignmentID == id
										  select milestone).ToList();

			if(!milestones.Any()) {
				return null;
			}

			return milestones;
		}
	}
}