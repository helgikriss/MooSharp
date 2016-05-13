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
				Name = "Students",
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

			//Creating courses
			var course1 = new Course() {
				ID = 1,
				Title = "Gagnaskipan",
				CourseNumber = "GSK-2016"
			};
			fakeDb.Courses.Add(course1);

			var course2 = new Course() {
				ID = 2,
				Title = "Forritun",
				CourseNumber = "FOR-2016"
			};
			fakeDb.Courses.Add(course2);

			var course3 = new Course() {
				ID = 3,
				Title = "Reiknirit",
				CourseNumber = "Ri-2016"
			};
			fakeDb.Courses.Add(course3);

			//Adding users to course
			var courseUsers1 = new CourseUser() {
				CourseID = 3,
				UserID = "1"
			};
			fakeDb.CourseUsers.Add(courseUsers1);

			var courseUsers2 = new CourseUser() {
				CourseID = 2,
				UserID = "1"
			};
			fakeDb.CourseUsers.Add(courseUsers2);

			var courseUsers3 = new CourseUser() {
				CourseID = 2,
				UserID = "2"
			};
			fakeDb.CourseUsers.Add(courseUsers3);

			// Creating assignments
			var assignment1 = new Assignment() {
				ID = 1,
				Title = "Lab 1",
				CourseID = 3,
				Description = "Desc",
				OpeningTime = new DateTime(2016, 5, 4, 15, 30, 0),
				ClosingTime = new DateTime(2016, 6, 4, 15, 30, 0)
			};
			fakeDb.Assignments.Add(assignment1);

			var assignment2 = new Assignment() {
				ID = 2,
				Title = "Lab 2",
				CourseID = 3,
				Description = "Desc",
				OpeningTime = new DateTime(2015, 5, 4, 15, 30, 0),
				ClosingTime = new DateTime(2015, 6, 4, 15, 30, 0)
			};
			fakeDb.Assignments.Add(assignment2);

			//	Creating milestones
			var milestone1 = new Milestone() {
				ID = 1,
				AssignmentID = 1,
				Title = "Milestone 1",
				Description = "Desc",
				Weight = 30
			};
			fakeDb.Milestones.Add(milestone1);

			var milestone2 = new Milestone() {
				ID = 2,
				AssignmentID = 1,
				Title = "Milestone 2",
				Description = "Desc",
				Weight = 40
			};
			fakeDb.Milestones.Add(milestone2);

			var milestone3 = new Milestone() {
				ID = 3,
				AssignmentID = 2,
				Title = "Milestone 3",
				Description = "Desc",
				Weight = 20
			};
			fakeDb.Milestones.Add(milestone3);
			fakeDb.SaveChanges();

			_service = new UsersService(fakeDb);
			_manager = new IdentityManager();
		}
		[TestMethod]
		public void TestGetAllUsers() {
			// Arrange:
			// Nothing to arrange

			// Act:
			var result = _service.GetAllUsers();

			// Assert:
			Assert.AreEqual(3, result.Count);
		}

		[TestMethod]
		public void TestGetStudentsByCourse() {
			// Arrange
			var courseID = 2;

			// Act
			var result = _service.GetStudentsByCourse(courseID);

			// Assert
			Assert.AreEqual(2, result.Count);
		}
	}
}
