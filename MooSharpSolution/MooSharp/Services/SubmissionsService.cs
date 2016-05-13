using MooSharp.Models;
using MooSharp.Models.Entities;
using MooSharp.Models.ViewModels.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;

namespace MooSharp.Services
{
	public class SubmissionsService
	{
		private ApplicationDbContext _db;
		private TestCasesService _testCasesService;
		const string ACCEPTED			= "Accepted";
		const string WRONG_OUTPUT		= "Wrong Output";
		const string COMPILE_ERROR		= "Compile Error";
		const string TIMELIMIT_EXCEEDED = "Timelimit Exceeded";
		const string MEMORY_ERROR		= "Memory Error";

		public SubmissionsService() {
			_db = new ApplicationDbContext();
			_testCasesService = new TestCasesService();
		}

		public void CreateSubmission(CreateSubmissionViewModel viewModel) {
			// Compile program.
			string compileOutput = "";
			string exeFilePath = "";
			

			List<string> outputs = new List<string>();
			List<bool> timeLimitExceeded = new List<bool>();
			List<bool> wrongOutput = new List<bool>();

			// TODO: Implement memory error check.
			List<bool> memoryError = new List<bool>();
			List<string> memoryErrorOutputs = new List<string>();

			string status = "";

			bool compileSuccess = CompileSubmission(viewModel, ref compileOutput, ref exeFilePath);
			if (compileSuccess) {
				// Run program.
				RunSubmission(viewModel.MilestoneID, exeFilePath, ref outputs, ref timeLimitExceeded);

				if (sameNumberOfOutputsAsTestCases(viewModel.MilestoneID, ref outputs)) {
					// Check output.
					CheckOutput(viewModel.MilestoneID, ref outputs, ref wrongOutput);
				}
			}
			else {
				// Program did not compile.
				var sub = new Submission() {
					Compiled = false,
					CompilerOutput = compileOutput,
					Status = COMPILE_ERROR,
					MilestoneID = viewModel.MilestoneID,
					SubmissionDateTime = viewModel.SubmissionDateTime,
					SubmissionPath = viewModel.SubmissionPath,
					UserID = viewModel.UserID
				};
			}

			var submission = new Submission() {
				Compiled = true,
				CompilerOutput = compileOutput,
				Status = GetStatusOfSubmission(ref timeLimitExceeded, ref memoryError, ref wrongOutput, _testCasesService.GetTestCasesByMilestoneId(viewModel.MilestoneID).Count),
				MilestoneID = viewModel.MilestoneID,
				SubmissionDateTime = viewModel.SubmissionDateTime,
				SubmissionPath = viewModel.SubmissionPath,
				UserID = viewModel.UserID
			};

			_testCasesService.CreateSubmissionTestCases(outputs, wrongOutput, timeLimitExceeded);

			
			_db.Submissions.Add(submission);
			_db.SaveChanges();
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

		public bool sameNumberOfOutputsAsTestCases(int milestoneID, ref List<string> outputs) {
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

		public string GetStatusOfSubmission(ref List<bool> timeLimitExceeded, ref List<bool> memoryError, ref List<bool> wrongOutput, int numberOfTestCases) {
			if(timeLimitExceeded.Count == numberOfTestCases &&
				memoryError.Count == numberOfTestCases &&
				wrongOutput.Count == numberOfTestCases) {
				for(int i = 0; i < numberOfTestCases; i++) {
					if(timeLimitExceeded[i] == true) {
						return TIMELIMIT_EXCEEDED;
					}
					else if(memoryError[i] == true) {
						return MEMORY_ERROR;
					}
					else if(wrongOutput[i] == true) {
						return WRONG_OUTPUT;
					}
				}
			}
			return ACCEPTED;
		}
	}
}