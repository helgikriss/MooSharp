using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MooSharp.Models;
using MooSharp.Models.Entities;

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
			var inputs = GetInputsByMilestoneId(id);

			return inputs.Any();
		}
	}
}