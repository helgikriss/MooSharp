using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MooSharp.Models;
using MooSharp.Models.Entities;
using MooSharp.Models.ViewModels.Students;

namespace MooSharp.Services
{
	public class TestCasesService
	{
		private ApplicationDbContext _db;

		public TestCasesService() {
			_db = new ApplicationDbContext();
		}

		/// <summary>
		/// Returns all Test Cases associated with Milestone id.
		/// </summary>
		public List<TestCase> GetTestCasesByMilestoneId(int id) {
			// TODO: Check if test case exists with milestone id.
			var testCases = (from testCase in _db.TestCases
							 where testCase.MilestoneID == id
							 select testCase).ToList();

			return testCases;
		}

		public List<string> GetInputsByMilestoneId(int id) {
			var testCases = GetTestCasesByMilestoneId(id);

			var inputs = (from input in testCases
						  select input.Input).ToList();

			return inputs;
		}

		public bool TestCasesByMilestoneIdHaveInput(int id) {
			bool hasInput = (from i in _db.TestCases
							where i.MilestoneID == id
							select i.HasInput).FirstOrDefault();

			return hasInput;
		}

		public void CreateSubmissionTestCases(ref List<bool> timeLimitExceeded, /*ref List<bool> memoryError,*/ ref List<bool> wrongOutputs, ref List<string> outputs, /*ref List<string> memoryErrorOutput,*/ int submissionID, int numberOfTestCases, int milestoneID) {
			var testCases = GetTestCasesByMilestoneId(milestoneID);
			for(int i = 0; i < numberOfTestCases; i++) {
				var submissionTestCase = new SubmissionTestCase() {
					TimeLimitExceeded = timeLimitExceeded[i],
					/*MemoryError = memoryError[i],*/
					WrongOutput = wrongOutputs[i],
					Output = outputs[i],
					/*MemoryErrorOutput = memoryErrorOutput[i],*/
					SubmissionID = submissionID,
					ExpectedOutput = testCases[i].Output
			};
				_db.SubmissionTestCases.Add(submissionTestCase);
				_db.SaveChanges();
			}
		}

		public List<SubmissionTestCaseViewModel> GetSubmissionTestCasesBySubmissionId(int submissionID) {
			var submissionTestCases = (from test in _db.SubmissionTestCases
										 where test.SubmissionID == submissionID
										 select test).ToList();

			List<SubmissionTestCaseViewModel> submissionTestCaseViewModels = new List<SubmissionTestCaseViewModel>();

			foreach(SubmissionTestCase submissionTestCase in submissionTestCases) {
				var viewModel = new SubmissionTestCaseViewModel() {
					ID = submissionTestCase.ID,
					SubmissionID = submissionTestCase.SubmissionID,
					MemoryError = submissionTestCase.MemoryError,
					MemoryErrorOutput = submissionTestCase.MemoryErrorOutput,
					Output = submissionTestCase.Output,
					TimeLimitExceeded = submissionTestCase.TimeLimitExceeded,
					WrongOutput = submissionTestCase.WrongOutput,
					ExpectedOutput = submissionTestCase.ExpectedOutput
				};
				submissionTestCaseViewModels.Add(viewModel);
			}
			return submissionTestCaseViewModels;
		}
	}
}