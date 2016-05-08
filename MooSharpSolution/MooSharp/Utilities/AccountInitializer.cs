using MooSharp.Models;
using MooSharp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MooSharp.Utilities
{
	/// <summary>
	/// A class for initializing user roles and one admin account.
	/// </summary>
	public class AccountInitializer
	{
		/// <summary>
		/// This method was tought my Patrekur Patreksson in the course Web Programming
		/// </summary>
		public static void Init() {
		
			IdentityManager manager = new IdentityManager();

			if (!manager.RoleExists("Administrators")){
				manager.CreateRole("Administrators");
			}

			if (!manager.RoleExists("Teachers")) {
				manager.CreateRole("Teachers");
			}

			if (!manager.RoleExists("Students")) {
				manager.CreateRole("Students");
			}

			if (!manager.UserExists("admin")) {
				ApplicationUser admin = new ApplicationUser();
				admin.UserName = "admin";
				manager.CreateUser(admin, "admin");
			}

			var userAdmin = manager.GetUser("admin");

			if (!manager.UserIsInRole(userAdmin.Id, "Administrators")) {
				manager.AddUserToRole(userAdmin.Id, "Administrators");
			}

			if (!manager.UserExists("teacher")) {
				ApplicationUser teacher = new ApplicationUser();
				teacher.UserName = "teacher";
				teacher.Email = "teacher@moosharp.is";
				manager.CreateUser(teacher, "teacher");
			}

			var userTeacher = manager.GetUser("teacher");

			if (!manager.UserIsInRole(userTeacher.Id, "Teachers")) {
				manager.AddUserToRole(userTeacher.Id, "Teachers");
			}

			if (!manager.UserExists("student")) {
				ApplicationUser student = new ApplicationUser();
				student.UserName = "student";
				student.Email = "student@moosharp.is";
				manager.CreateUser(student, "student");
			}

			var userStudent = manager.GetUser("student");

			if (!manager.UserIsInRole(userStudent.Id, "Students")) {
				manager.AddUserToRole(userStudent.Id, "Students");
			}

			/// <summary>
			/// Gandalf SuperUser created for testing, user can access all controllers
			/// </summary>

			if (!manager.UserExists("gandalf")) {
				ApplicationUser gandalf = new ApplicationUser();
				gandalf.UserName = "gandalf";
				gandalf.Email = "gandalf@moosharp.is";
				manager.CreateUser(gandalf, "gandalf");
			}

			var userGandalf = manager.GetUser("gandalf");

			if (!manager.UserIsInRole(userGandalf.Id, "Students")) {
				manager.AddUserToRole(userGandalf.Id, "Students");
			}
			if (!manager.UserIsInRole(userGandalf.Id, "Teachers")) {
				manager.AddUserToRole(userGandalf.Id, "Teachers");
			}
			if (!manager.UserIsInRole(userGandalf.Id, "Administrators")) {
				manager.AddUserToRole(userGandalf.Id, "Administrators");
			}
		}
	}
}