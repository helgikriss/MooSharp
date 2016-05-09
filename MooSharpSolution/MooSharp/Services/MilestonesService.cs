using MooSharp.Models;
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

		public int CreateMilestone(CreateMilestoneViewModel viewModel) {
			return 0;
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