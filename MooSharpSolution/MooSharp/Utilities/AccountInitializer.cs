using MooSharp.Models;
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

			if (!manager.UserExists("admin@moosharp.is")) {
				ApplicationUser admin = new ApplicationUser();
				admin.UserName = "admin@moosharp.is";
				manager.CreateUser(admin, "admin");
			}

			var userAdmin = manager.GetUser("admin@moosharp.is");

			if (!manager.UserIsInRole(userAdmin.Id, "Administrators")) {
				manager.AddUserToRole(userAdmin.Id, "Administrators");
			}

			if (!manager.UserExists("teacher@moosharp.is")) {
				ApplicationUser teacher = new ApplicationUser();
				teacher.UserName = "teacher@moosharp.is";
				manager.CreateUser(teacher, "teacher");
			}

			var userTeacher = manager.GetUser("teacher@moosharp.is");

			if (!manager.UserIsInRole(userTeacher.Id, "Teachers")) {
				manager.AddUserToRole(userTeacher.Id, "Teachers");
			}

			if (!manager.UserExists("student@moosharp.is")) {
				ApplicationUser student = new ApplicationUser();
				student.UserName = "student@moosharp.is";
				manager.CreateUser(student, "student");
			}

			var userStudent = manager.GetUser("student@moosharp.is");

			if (!manager.UserIsInRole(userStudent.Id, "Students")) {
				manager.AddUserToRole(userStudent.Id, "Students");
			}

		}
	}
}