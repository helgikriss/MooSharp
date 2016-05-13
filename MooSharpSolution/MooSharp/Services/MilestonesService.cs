using MooSharp.Models;
using MooSharp.Models.Entities;
using MooSharp.Models.ViewModels;
using MooSharp.Models.ViewModels.Teachers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Diagnostics;

namespace MooSharp.Services
{
    public class MilestonesService
    {
		private TestCasesService _testCasesService;
		private IAppDataContext _db;

		public MilestonesService() {
			_db = new ApplicationDbContext();
			_testCasesService = new TestCasesService();
		}
	
		public MilestonesService(IAppDataContext context) {
			_db = context;
		}

		public void CreateMilestone(CreateMilestoneViewModel model) {

			string input = "";
			string output = "";
			bool inputFileIncluded = false;
			
			if (model.InputFile != null && model.InputFile.ContentLength > 0) {
				BinaryReader b = new BinaryReader(model.InputFile.InputStream);
				byte[] binData = b.ReadBytes(Convert.ToInt32(model.InputFile.InputStream.Length));
				input = System.Text.Encoding.UTF8.GetString(binData);
				input = input.Replace(System.Environment.NewLine, "\n");
				inputFileIncluded = true;
			}
			if (model.OutputFile != null && model.OutputFile.ContentLength > 0) {
				BinaryReader b = new BinaryReader(model.OutputFile.InputStream);
				byte[] binData = b.ReadBytes(Convert.ToInt32(model.OutputFile.InputStream.Length));
				output = System.Text.Encoding.UTF8.GetString(binData);
				output = output.Replace(System.Environment.NewLine, "\n");
			}

			var milestone = new Milestone() {
				Description = model.Description,
				AssignmentID = model.AssignmentID,
				Title = model.Title,
				Weight = model.Weight
			};
			_db.Milestones.Add(milestone);
			_db.SaveChanges();

			if (inputFileIncluded) {
				var testCase = new TestCase() {
					MilestoneID = _db.Milestones.Max(item => item.ID),
					Input = input,
					Output = output,
					HasInput = true
				};
				_db.TestCases.Add(testCase);
				_db.SaveChanges();
			}
			else {
				var testCase = new TestCase() {
					MilestoneID = _db.Milestones.Max(item => item.ID),
					Output = output,
					HasInput = false
				};
				_db.TestCases.Add(testCase);
				_db.SaveChanges();
			}
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

		public int DeleteMilestone(int milestoneID) {
			var viewModel = GetMilestonetByID(milestoneID);
			var milestone = new Milestone {
				ID = viewModel.ID
			};
			var assignmentID = GetAssignmentIdForMilestone(milestoneID);
			var result = _db.Milestones.Remove(milestone);
			_db.SaveChanges();

			return assignmentID;
		}

		public int GetAssignmentIdForMilestone(int milestoneID) {
			return _db.Milestones.Where(x => x.ID == milestoneID).Select(x => x.AssignmentID).FirstOrDefault();
		}
	}
}