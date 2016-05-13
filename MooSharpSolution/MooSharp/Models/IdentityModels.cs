using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MooSharp.Models.Entities;

namespace MooSharp.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
		/// <summary>
		/// Navigation property.
		/// </summary>
		public ICollection<CourseUser> CourseUsers { get; set; }

		/// <summary>
		/// Navigation property.
		/// </summary>
		public ICollection<Submission> Submissions { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
		
	}
	public interface IAppDataContext
	{
		IDbSet<Assignment> Assignments { get; set; }
		IDbSet<Milestone> Milestones { get; set; }
		IDbSet<Course> Courses { get; set; }
		IDbSet<Submission> Submissions { get; set; }
		IDbSet<CourseUser> CourseUsers { get; set; }
		IDbSet<TestCase> TestCases { get; set; }
		IDbSet<SubmissionTestCase> SubmissionTestCases { get; set; }
		IDbSet<ApplicationUser> Users { get; set; }
		IDbSet<IdentityRole> Roles { get; set; }
		IDbSet<IdentityUserRole> UserRoles { get; set; }
		int SaveChanges();

	}
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IAppDataContext
    {
		public IDbSet<Assignment> Assignments { get; set; }
		public IDbSet<Milestone>  Milestones  { get; set; }
		public IDbSet<Course>     Courses     { get; set; }
		public IDbSet<Submission> Submissions { get; set; }
		public IDbSet<CourseUser> CourseUsers { get; set; }
		public IDbSet<TestCase> TestCases { get; set; }
		public IDbSet<SubmissionTestCase> SubmissionTestCases { get; set; }
		public IDbSet<IdentityUserRole> UserRoles { get; set; }

		public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}