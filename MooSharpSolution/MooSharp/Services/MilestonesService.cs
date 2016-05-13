using MooSharp.Models;
using MooSharp.Models.Entities;
using MooSharp.Models.ViewModels;
using MooSharp.Models.ViewModels.Teachers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace MooSharp.Services
{
    public class MilestonesService
    {
		private IAppDataContext _db;

		public MilestonesService() {
			_db = new ApplicationDbContext();
		}
	
		public MilestonesService(IAppDataContext context) {
			_db = context;
		}

		public void CreateMilestone(CreateMilestoneViewModel model) {

			string input = "";
			string output = "";

			HttpPostedFileBase inputFile = model.InputFile;
			HttpPostedFileBase outputFile = model.OutputFile;

			if (inputFile.ContentLength > 0 || !inputFile.Equals(null)) {
				BinaryReader b = new BinaryReader(inputFile.InputStream);
				byte[] binData = b.ReadBytes(Convert.ToInt32(inputFile.InputStream.Length));
				input = System.Text.Encoding.UTF8.GetString(binData);
			}
			if (outputFile.ContentLength > 0 || outputFile.Equals(null)) {
				BinaryReader b = new BinaryReader(outputFile.InputStream);
				byte[] binData = b.ReadBytes(Convert.ToInt32(outputFile.InputStream.Length));
				output = System.Text.Encoding.UTF8.GetString(binData);
			}

			var milestone = new Milestone() {
				Description = model.Description,
				AssignmentID = model.AssignmentID,
				Title = model.Title,
				Weight = model.Weight
			};
			_db.Milestones.Add(milestone);
			_db.SaveChanges();

			var testCase = new TestCase() {
				MilestoneID = _db.Milestones.Max(item => item.ID),
				Input = input,
				Output = output
			};

			_db.TestCases.Add(testCase);
			_db.SaveChanges();
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