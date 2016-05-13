using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MooSharp.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using MooSharp.Services;
using MooSharp.Models.Entities;
using MooSharp.Models.ViewModels;
using MooSharp.Models.ViewModels.Admins;

namespace MooSharpTests.Services
{
	[TestClass]
	public class CoursesServiceTest
	{
		private CoursesService _service;
		[TestInitialize]
		public void Initialize() {

			var fakeDb = new FakeDatabase();

			// Creating users
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

			// Creating role student
			var role = new IdentityRole() {
				Id = "1",
				Name = "Student",
			};
			fakeDb.Roles.Add(role);

			// Adding all users to a role (this is required)
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

			// Creating courses
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

			// Adding users to course
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
			fakeDb.SaveChanges();

			_service = new CoursesService(fakeDb);
		}

		[TestMethod]
		public void TestGetCourseById() {

			// Arrange
			var id = 3;

			// Act
			var result = _service.GetCourseById(id);

			// Assert
			Assert.AreEqual("Reiknirit", result.Title);
		}

		[TestMethod]
		public void TestGetCoursesByUser() {

			// Arrange
			var userId = "1";

			// Act
			var result1 = _service.GetCoursesByUser(userId);

			// Assert
			Assert.AreEqual(2, result1.Count);
			Assert.AreEqual("Forritun" , result1[0].Title);
		}


		[TestMethod]
		public void TestCreate() {

			// Arrange
			var course = new CreateCourseViewModel() {
				Name = "Greining og honnun",
				CourseNumber = "GHOH"
			};

			// Act
			bool result = _service.Create(course);

			// Assert
			Assert.IsTrue(result);
		}

	}
}
