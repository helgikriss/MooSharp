using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MooSharp.Services;
using MooSharp.Models;
using MooSharp.Models.ViewModels.Teachers;
using Microsoft.AspNet.Identity.EntityFramework;
using MooSharp.Models.Entities;
using MooSharp.Models.ViewModels;

namespace MooSharpTests.Services
{
	[TestClass]
	public class AssignmentsServiceTest {
		private AssignmentsService _service;
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
			fakeDb.SaveChanges();

			_service = new AssignmentsService(fakeDb);
		}

		[TestMethod]
		public void TestGetAssignmentsInCourse() {
			// Arrange
			var courseID = 3;

			// Act
			var result = _service.GetAssignmentsInCourse(courseID);

			// Assert
			Assert.AreEqual(2, result.Count);
			Assert.AreEqual("Lab 1", result[0].Title);
		}

		[TestMethod]
		public void TestCreateAssignment() {
			// Arrange
			var viewModel = new CreateAssignmentViewModel(){
				Title = "Lab 3",
				CourseID = 2,
				Description = "Desc",
				OpeningTime = "23:00",
				OpeningDate = "2016-4-13",
				ClosingTime = "23:00",
				ClosingDate = "2016-5-15"
			};

			// Act
			var result = _service.CreateAssignment(viewModel);

			// Assert

			Assert.AreEqual("Lab 3", _service.GetAssignmentsInCourse(2)[0].Title);
			Assert.IsTrue(_service.AssignmentIsInDbById(result));
		}

		[TestMethod]
		public void TestGetActiveAssignments() {
			// Arrange
			var userID1 = "1";

			var userID2 = "2";

			// Act
			var result1 = _service.GetActiveAssignments(userID1);

			var result2 = _service.GetActiveAssignments(userID2);

			// Assert
			Assert.AreEqual(1, result1.Count);
			Assert.AreEqual("Lab 1", result1[0].Title);

			Assert.AreEqual(0, result2.Count);
		}
	}
}

