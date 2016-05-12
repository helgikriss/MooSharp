using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FakeDbSet;
using System.Data.Entity;
using MooSharp.Models;
using MooSharp.Models.Entities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MooSharpTests
{
	/// <summary>
	/// This is an example of how we'd create a fake database by implementing the 
	/// same interface that the BookeStoreEntities class implements.
	/// </summary>
	public class FakeDatabase : IAppDataContext
	{
		/// <summary>
		/// Sets up the fake database.
		/// </summary>
		public FakeDatabase() {
			// We're setting our DbSets to be InMemoryDbSets rather than using SQL Server.
			this.Assignments = new InMemoryDbSet<Assignment>();
			this.Milestones = new InMemoryDbSet<Milestone>();
			this.Courses = new InMemoryDbSet<Course>();
			this.Submissions = new InMemoryDbSet<Submission>();
			this.CourseUsers = new InMemoryDbSet<CourseUser>();
			this.TestCases = new InMemoryDbSet<TestCase>();
			this.SubmissionTestCases = new InMemoryDbSet<SubmissionTestCase>();
			this.Users = new InMemoryDbSet<ApplicationUser>();
			this.Roles = new InMemoryDbSet<IdentityRole>();
			this.UserRoles = new InMemoryDbSet<IdentityUserRole>();
		}

		public IDbSet<Assignment> Assignments { get; set; }
		public IDbSet<Milestone> Milestones { get; set; }
		public IDbSet<Course> Courses { get; set; }
		public IDbSet<Submission> Submissions { get; set; }
		public IDbSet<CourseUser> CourseUsers { get; set; }
		public IDbSet<TestCase> TestCases { get; set; }
		public IDbSet<SubmissionTestCase> SubmissionTestCases { get; set; }
		public IDbSet<ApplicationUser> Users { get; set; }
		public IDbSet<IdentityRole> Roles { get; set; }
		public IDbSet<IdentityUserRole> UserRoles { get; set; }
		public int SaveChanges() {
			// Pretend that each entity gets a database id when we hit save.
			int changes = 0;
//			changes += DbSetHelper.IncrementPrimaryKey<Author>(x => x.AuthorId, this.Authors);
//			changes += DbSetHelper.IncrementPrimaryKey<Book>(x => x.BookId, this.Books);

			return changes;
		}

		public void Dispose() {
			// Do nothing!
		}
	}
}