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

		public SubmissionsService() {
			_db = new ApplicationDbContext();
		}

		public void CreateSubmission(CreateSubmissionViewModel viewModel) {
			//Compile and run.
			var submission = new Submission() {
				ID = viewModel.ID,
				MilestoneID = viewModel.MilestoneID,
				UserID = viewModel.UserID,
				SubmissionDateTime = viewModel.SubmissionDateTime,
				Outputs = viewModel.Outputs,
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

		public void CompileSubmission(CreateSubmissionViewModel viewModel) {
			string submissionPath = viewModel.SubmissionPath;
			System.IO.StreamReader myFile = new System.IO.StreamReader(submissionPath);
			string code = myFile.ReadToEnd();
			myFile.Close();

			// Set up our working folder, and the file names/paths.
			// In this example, this is all hardcoded, but in a
			// real life scenario, there should probably be individual
			// folders for each user/assignment/milestone.

			/*var workingFolder = "C:\\Temp\\Mooshak2Code\\";
			var cppFileName = "Hello.cpp";
			var exeFilePath = workingFolder + "Hello.exe";*/
			
			int lastIndexOfSlash = submissionPath.LastIndexOf('\\');
			int indexOfLast = submissionPath.Length - 1;

			var cppFileName = submissionPath.Substring(lastIndexOfSlash+1, indexOfLast-lastIndexOfSlash);
			var exeFilePath = submissionPath.Replace(".cpp", ".exe");
			var workingFolder = submissionPath.Substring(0, lastIndexOfSlash+1);

			// Write the code to a file, such that the compiler
			// can find it:
			System.IO.File.WriteAllText(workingFolder + cppFileName, code);

			// In this case, we use the C++ compiler (cl.exe) which ships
			// with Visual Studio. It is located in this folder:
			var compilerFolder = "C:\\Program Files (x86)\\Microsoft Visual Studio 14.0\\VC\\bin\\";
			// There is a bit more to executing the compiler than
			// just calling cl.exe. In order for it to be able to know
			// where to find #include-d files (such as <iostream>),
			// we need to add certain folders to the PATH.
			// There is a .bat file which does that, and it is
			// located in the same folder as cl.exe, so we need to execute
			// that .bat file first.

			// Using this approach means that:
			// * the computer running our web application must have
			//   Visual Studio installed. This is an assumption we can
			//   make in this project.
			// * Hardcoding the path to the compiler is not an optimal
			//   solution. A better approach is to store the path in
			//   web.config, and access that value using ConfigurationManager.AppSettings.

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
			string output = compiler.StandardOutput.ReadToEnd();
			compiler.WaitForExit();
			compiler.Close();

			// Check if the compile succeeded, and if it did,
			// we try to execute the code:
			if (System.IO.File.Exists(exeFilePath)) {
				var processInfoExe = new ProcessStartInfo(exeFilePath, "");
				processInfoExe.UseShellExecute = false;
				processInfoExe.RedirectStandardOutput = true;
				processInfoExe.RedirectStandardError = true;
				processInfoExe.CreateNoWindow = true;
				using (var processExe = new Process()) {
					processExe.StartInfo = processInfoExe;
					processExe.Start();
					// In this example, we don't try to pass any input
					// to the program, but that is of course also
					// necessary. We would do that here, using
					// processExe.StandardInput.WriteLine(), similar
					// to above.

					// TODO: Add check if there is input and if so then pass it to the program

					// We then read the output of the program:
					//var lines = new List<string>();
					string programOutput = "";
					while (!processExe.StandardOutput.EndOfStream) {
						//lines.Add(processExe.StandardOutput.ReadLine());
						programOutput += processExe.StandardOutput.ReadLine() + '\n';
					}

					//ViewBag.Output = lines;
					//Debug.WriteLine(programOutput);
					/*foreach (String s in lines) {
						Debug.WriteLine(s);
					}*/
				}
			}

			// TODO: We might want to clean up after the process, there
			// may be files we should delete etc.
		}
	}
}