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
				manager.CreateRole("Administators");
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
				manager.CreateUser(admin, "hopur24");
			}

			var user = manager.GetUser("admin@moosharp.is");

			if (!manager.UserIsInRole(user.Id, "Administrators")) {
				manager.AddUserToRole(user.Id, "Administrators");
			}
		}
	}
}