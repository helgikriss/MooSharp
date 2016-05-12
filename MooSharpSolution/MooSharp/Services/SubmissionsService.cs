﻿using MooSharp.Models;
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

		public SubmissionsService() {
			_db = new ApplicationDbContext();
			_testCasesService = new TestCasesService();
		}

		public void CreateSubmission(CreateSubmissionViewModel viewModel) {
			// Compile program.
			string compileOutput = "";
			string exeFilePath = "";
			bool compileSuccess = CompileSubmission(viewModel, ref compileOutput, ref exeFilePath);

			if (compileSuccess) {
				// Run program.
				List<string> outputs = new List<string>();
				bool runSuccess = RunSubmission(viewModel.MilestoneID, exeFilePath, ref outputs);
			}
			


			var submission = new Submission() {
				ID = viewModel.ID,
				MilestoneID = viewModel.MilestoneID,
				UserID = viewModel.UserID,
				SubmissionDateTime = viewModel.SubmissionDateTime,
				Status = viewModel.Status,
				SubmissionPath = viewModel.SubmissionPath
			};
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
			var cppFileName = submissionPath.Substring(lastIndexOfSlash+1, indexOfLast-lastIndexOfSlash);
			exeFilePath = submissionPath.Replace(".cpp", ".exe");
			var workingFolder = submissionPath.Substring(0, lastIndexOfSlash+1);
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

		public bool RunSubmission(int milestoneID, string exeFilePath, ref List<string> outputs) {

			var processInfoExe = new ProcessStartInfo(exeFilePath, "");
			processInfoExe.UseShellExecute = false;
			processInfoExe.RedirectStandardOutput = true;
			processInfoExe.RedirectStandardError = true;
			processInfoExe.CreateNoWindow = true;

			using (var processExe = new Process()) {
				processExe.StartInfo = processInfoExe;
				processExe.Start();

				
				List<bool> timeLimitExceeded = new List<bool>();
				
				// Check if there are any inputs to be read in and if so reads them in and
				// captures the output and puts it into a list.
				if (_testCasesService.TestCasesByMilestoneIdHaveInput(milestoneID)) {
					var inputs = _testCasesService.GetInputsByMilestoneId(milestoneID);
					
					foreach (string input in inputs) {
						Stopwatch sw = new Stopwatch();
						sw.Start();
						while(sw.ElapsedMilliseconds < 200) {

							processExe.StandardInput.WriteLine(input);
							string output = "";
							while (!processExe.StandardOutput.EndOfStream) {
								output += processExe.StandardOutput.ReadLine() + '\n';
							}
							outputs.Add(output);

						}
					}
				}
				else {
					// If there are no inputs to be read we simply read the output of the program.
					string programOutput = "";
					while (!processExe.StandardOutput.EndOfStream) {
						programOutput += processExe.StandardOutput.ReadLine() + '\n';
					}
					outputs.Add(programOutput);
				}

				return true;
			}
		}
}