﻿using MooSharp.Models;
using MooSharp.Models.Entities;
using MooSharp.Models.ViewModels.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;
using System.IO;
using MooSharp.Models.ViewModels;
using Microsoft.AspNet.Identity;

namespace MooSharp.Services
{
	public class SubmissionsService
	{
		private ApplicationDbContext _db;
		private TestCasesService _testCasesService;
		private MilestonesService _milestonesService;
		private AssignmentsService _assignmentsService;
		const string ACCEPTED			= "Accepted";
		const string WRONG_OUTPUT		= "Wrong Output";
		const string COMPILE_ERROR		= "Compile Error";
		const string TIMELIMIT_EXCEEDED = "Timelimit Exceeded";
		const string MEMORY_ERROR		= "Memory Error";

		public SubmissionsService() {
			_db = new ApplicationDbContext();
			_testCasesService = new TestCasesService();
			_milestonesService = new MilestonesService();
			_assignmentsService = new AssignmentsService();
		}

		public int CreateSubmission(CreateSubmissionViewModel viewModel, string extension) {
			// Compile program.
			string compileOutput = "";
			string exeFilePath = "";
			bool compileSuccess;
			int numberOfTestCases;
			int submissionID = 0;
			
			List<string> outputs = new List<string>();
			List<bool> timeLimitExceeded = new List<bool>();
			List<bool> wrongOutput = new List<bool>();

			// TODO: Implement memory error check and fill these lists.
			List<bool> memoryError = new List<bool>();
			List<string> memoryErrorOutputs = new List<string>();

			// Check extension.
			if(extension != ".cpp") {
				// File extension is correct.
				compileSuccess = false;
				compileOutput = "File with incorrect file extension submitted, submissions should end with extension .cpp";

				var submission = new Submission() {
					Compiled = compileSuccess,
					CompilerOutput = compileOutput,
					Status = COMPILE_ERROR,
					MilestoneID = viewModel.MilestoneID,
					SubmissionDateTime = viewModel.SubmissionDateTime,
					SubmissionPath = viewModel.SubmissionPath,
					UserID = viewModel.UserID
				};

				_db.Submissions.Add(submission);
				_db.SaveChanges();
				return _db.Submissions.Max(item => item.ID);
			}
			else {
				// File extension is correct.
				// Compile program.
				compileSuccess = CompileSubmission(viewModel, ref compileOutput, ref exeFilePath);
				if (compileSuccess) {
					// Program compiled.
					// Run program.
					RunSubmission(viewModel.MilestoneID, exeFilePath, ref outputs, ref timeLimitExceeded);
					// Check output.
					CheckOutput(viewModel.MilestoneID, ref outputs, ref wrongOutput);
				}
				else {
					// Program did not compile.
					var compileErrorSubmission = new Submission() {
						Compiled = compileSuccess,
						CompilerOutput = compileOutput,
						Status = COMPILE_ERROR,
						MilestoneID = viewModel.MilestoneID,
						SubmissionDateTime = viewModel.SubmissionDateTime,
						SubmissionPath = viewModel.SubmissionPath,
						UserID = viewModel.UserID
					};

					_db.Submissions.Add(compileErrorSubmission);
					_db.SaveChanges();
					return _db.Submissions.Max(item => item.ID);
				}

				numberOfTestCases = _testCasesService.GetTestCasesByMilestoneId(viewModel.MilestoneID).Count;

				var submission = new Submission() {
					Compiled = compileSuccess,
					CompilerOutput = compileOutput,
					Status = GetStatusOfSubmission(ref timeLimitExceeded, /*ref memoryError,*/ ref wrongOutput, numberOfTestCases),
					MilestoneID = viewModel.MilestoneID,
					SubmissionDateTime = viewModel.SubmissionDateTime,
					SubmissionPath = viewModel.SubmissionPath,
					UserID = viewModel.UserID
				};

				_db.Submissions.Add(submission);
				_db.SaveChanges();

				submissionID = _db.Submissions.Max(item => item.ID);

				_testCasesService.CreateSubmissionTestCases(ref timeLimitExceeded, /*ref memoryError,*/ ref wrongOutput, ref outputs, /*ref memoryErrorOutputs,*/ submissionID, numberOfTestCases, viewModel.MilestoneID);

				return submissionID;
			}
		}

		public bool SubmissionIsInDbById(int id) {
			var submission = _db.Submissions.Find(id);
			if (submission == null) {
				return false;
			}
			return true;
		}

		public bool CompileSubmission(CreateSubmissionViewModel viewModel, ref string output, ref string exeFilePath) {
			
			
			// Write submission code into a string variable.
			string submissionPath = viewModel.SubmissionPath;
			System.IO.StreamReader myFile = new System.IO.StreamReader(submissionPath);
			string code = myFile.ReadToEnd();
			myFile.Close();

			// Get Filenames and path to working folder and path to compiler.
			int lastIndexOfSlash = submissionPath.LastIndexOf('\\');
			int indexOfLast = submissionPath.Length - 1;
			var cppFileName = submissionPath.Substring(lastIndexOfSlash + 1, indexOfLast - lastIndexOfSlash);
			exeFilePath = submissionPath.Replace(".cpp", ".exe");
			var workingFolder = submissionPath.Substring(0, lastIndexOfSlash + 1);
			var compilerFolder = "C:\\Program Files (x86)\\Microsoft Visual Studio 14.0\\VC\\bin\\";

			// Write the code to a file, such that the compiler can find it:
			System.IO.File.WriteAllText(workingFolder + cppFileName, code);

			// Execute the compiler:
			Process compiler = new Process();
			compiler.StartInfo.FileName = "cmd.exe";
			compiler.StartInfo.WorkingDirectory = workingFolder;
			compiler.StartInfo.RedirectStandardInput = true;
			compiler.StartInfo.RedirectStandardOutput = true;
			compiler.StartInfo.UseShellExecute = false;

			compiler.Start();
			compiler.StandardInput.WriteLine("\"" + compilerFolder + "vcvars32.bat" + "\"");
			compiler.StandardInput.WriteLine("cl.exe /nologo /EHsc " + cppFileName);
			compiler.StandardInput.WriteLine("exit");
			output = compiler.StandardOutput.ReadToEnd();
			// TODO: Check compile time here.
			compiler.WaitForExit();
			compiler.Close();

			// Return true if the compile succeeded, false otherwise.
			return System.IO.File.Exists(exeFilePath);
		}

		public void RunSubmission(int milestoneID, string exeFilePath, ref List<string> outputs, ref List<bool> timeLimitExceeded) {
			var processInfoExe = new ProcessStartInfo(exeFilePath, "");
			processInfoExe.UseShellExecute = false;
			processInfoExe.RedirectStandardOutput = true;
			processInfoExe.RedirectStandardError = true;
			processInfoExe.CreateNoWindow = true;

			using (var processExe = new Process()) {
				processExe.StartInfo = processInfoExe;
				processExe.Start();

				// Check if there are any inputs to be read in and if so reads them in and
				// captures the output and puts it into a list.
				if (_testCasesService.TestCasesByMilestoneIdHaveInput(milestoneID)) {
					var inputs = _testCasesService.GetInputsByMilestoneId(milestoneID);

					foreach (string input in inputs) {
						Stopwatch sw = new Stopwatch();
						sw.Start();
						bool finishedRunning = false;

						while (sw.ElapsedMilliseconds < 20000 && finishedRunning == false) {
							processExe.StandardInput.WriteLine(input);
							string output = "";
							while (!processExe.StandardOutput.EndOfStream) {
								output += processExe.StandardOutput.ReadLine() + '\n';
							}
							outputs.Add(output);
							finishedRunning = true;
						}
						timeLimitExceeded.Add(!finishedRunning);
					}
				}
				else {
					// If there are no inputs to be read we simply read the output of the program.
					Stopwatch sw = new Stopwatch();
					sw.Start();
					bool finishedRunning = false;
					while (sw.ElapsedMilliseconds < 20000 && finishedRunning == false) {
						string programOutput = "";
						while (!processExe.StandardOutput.EndOfStream) {
							programOutput += processExe.StandardOutput.ReadLine() + '\n';
						}
						outputs.Add(programOutput);
						finishedRunning = true;
					}
					timeLimitExceeded.Add(!finishedRunning);
				}
			}
		}

		public bool SameNumberOfOutputsAsTestCases(int milestoneID, ref List<string> outputs) {
			if(outputs.Count == _testCasesService.GetTestCasesByMilestoneId(milestoneID).Count) {
				return true;
			}
			return false;
		}

		public void CheckOutput(int milestoneID, ref List<string>outputs, ref List<bool> wrongOutput) {
			var testCases = _testCasesService.GetTestCasesByMilestoneId(milestoneID);
			
			for (int i = 0; i < testCases.Count; i++) {
				if(outputs[i] != testCases[i].Output) {
					wrongOutput.Add(true);
				}
				else {
					wrongOutput.Add(false);
				}
			}
		}

		public string GetStatusOfSubmission(ref List<bool> timeLimitExceeded, /*ref List<bool> memoryError,*/ ref List<bool> wrongOutput, int numberOfTestCases) {
			for(int i = 0; i < numberOfTestCases; i++) {
				if(timeLimitExceeded[i] == true) {
					return TIMELIMIT_EXCEEDED;
				}
				/*else if(memoryError[i] == true) {
					return MEMORY_ERROR;
				}*/
				else if(wrongOutput[i] == true) {
					return WRONG_OUTPUT;
				}
			}
			return ACCEPTED;
		}

		public SubmissionResultsViewModel GetSubmissionById(int submissionID) {
			var submission = (from sub in _db.Submissions
							  where sub.ID == submissionID
							  select sub).FirstOrDefault();
			var milestone = _milestonesService.GetMilestonetByID(submission.MilestoneID);

			var submissionResultsViewModel = new SubmissionResultsViewModel() {
				ID = submission.ID,
				MilestoneID = submission.MilestoneID,
				Status = submission.Status,
				Compiled = submission.Compiled,
				CompilerOutput = submission.CompilerOutput,
				SubmissionDateTime = submission.SubmissionDateTime,
				UserID = submission.UserID,
				SubmissionTestCases = _testCasesService.GetSubmissionTestCasesBySubmissionId(submissionID),
				MilestoneTitle = milestone.Title,
				AssignmentTitle = _assignmentsService.GetAssignmentByID(milestone.AssignmentId).Title
			};
			return submissionResultsViewModel;
		}
		public List<SubmissionViewModel> GetSubmissionsByUser(string userID) {
			var submissions = _db.Submissions.Where(x => x.UserID == userID).ToList();
			submissions.Sort((y, x) => DateTime.Compare(x.SubmissionDateTime, y.SubmissionDateTime));

			var viewModels = new List<SubmissionViewModel>();
			foreach (Submission a in submissions) {
				var viewModel = new SubmissionViewModel() {
					ID = a.ID,
					MilestoneID = a.MilestoneID,
					UserID = a.UserID,
					Status = a.Status,
					SubmissionDateTime = a.SubmissionDateTime,
					SubmissionPath = a.SubmissionPath
				};
				viewModels.Add(viewModel);
			}
			return viewModels;
		}
	}
}