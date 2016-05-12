using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MooSharp.Services;
using MooSharp.Models.Entities;
using MooSharp.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MooSharpTests.Services
{
	[TestClass]
	public class UserServicesTest
	{
		private UsersService _service;
		private IdentityManager _manager;

		[TestInitialize]
		public void Initialize() {

			var fakeDb = new FakeDatabase();

			//Creating users
			var user1 = new ApplicationUser() {
				UserName = "John",
				Id = "1",
				Email = "john@stuff.com"
			};
			fakeDb.Users.Add(user1);

			var user2 = new ApplicationUser() {
				UserName = "William",
				Id = "2",
				Email = "william@stuff.com"

			};
			fakeDb.Users.Add(user2);

			var user3 = new ApplicationUser() {
				UserName = "Bill",
				Id = "3",
				Email = "bill@stuff.com"

			};
			fakeDb.Users.Add(user3);

			//Creating role student
			var role = new IdentityRole() {
				Id = "1",
				Name = "Student",
			};
			fakeDb.Roles.Add(role);

			//Adding all users to a role (this is required)
			var roleConn1 = new IdentityUserRole() {
				RoleId = "1",
				UserId = "1"
			};
			fakeDb.UserRoles.Add(roleConn1);

			var roleConn2 = new IdentityUserRole() {
				RoleId = "1",
				UserId = "2"
			};
			fakeDb.UserRoles.Add(roleConn2);

			var roleConn3 = new IdentityUserRole() {
				RoleId = "1",
				UserId = "3"
			};
			fakeDb.UserRoles.Add(roleConn3);
			fakeDb.SaveChanges();

			_service = new UsersService(fakeDb);
			_manager = new IdentityManager();
		}
		[TestMethod]
		public void TestGetAllUsers() {
			//Arrange:

			//Nothing to arrange

			//Act:

			var result = _service.GetAllUsers();

			//Assert:

			Assert.AreEqual(3, result.Count);
		}
	}
}
