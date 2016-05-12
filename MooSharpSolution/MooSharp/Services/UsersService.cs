using MooSharp.Models;
using MooSharp.Models.ViewModels;
using MooSharp.Models.ViewModels.Admins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MooSharp.Services
{
	public class UsersService {
		private readonly IAppDataContext _db;
		private IdentityManager _manager;

		public UsersService() {
			_db = new ApplicationDbContext();
			_manager = new IdentityManager();
		}
		
		public UsersService(IAppDataContext context) {
			_db = context;
		}
		/// <summary>
		/// GetAllUsers() gets a list of all users in the database and their roles
		/// and returns it in a viewmodel for the view to use.
		/// </summary>
		public List<UserViewModel> GetAllUsers() {
			var users = _db.Users.ToList();
			var viewmodels = new List<UserViewModel>();
			foreach (ApplicationUser user in users) {
				var viewmodel = new UserViewModel() {
					userId = user.Id,
					email = user.Email,
					username = user.UserName,
					roles = (from roles in _db.Roles
							join connection in _db.UserRoles on roles.Id equals connection.RoleId
							where user.Id == connection.UserId
							select roles.Name.ToString()).ToList()
			};
				viewmodels.Add(viewmodel);
			}
			return viewmodels;
		}
		

		public List<UserViewModel> GetAllTeachers() {
			var users = _db.Users.ToList();
			var viewModels = new List<UserViewModel>();

			foreach (ApplicationUser user in users) {
				var role = _manager.GetUserRoles(user.Id).ToList();
				foreach (string r in role) {
					if (r == "Teachers") {
						var viewmodel = new UserViewModel() {

							userId = user.Id,
							email = user.Email,
							username = user.UserName,
							roles = _manager.GetUserRoles(user.Id).ToList()
						};
						viewModels.Add(viewmodel);
					}
				}
			}
			return viewModels;
		}

		/// <summary>
		///  This function takes in a viewmodel from a form, filled out with user information.
		///  username, email, password, and role is used .
		///  username, email and password is used to create the user in the database, then the role is added to the user.
		/// </summary>
		public bool CreateUser(CreateUserViewModel viewModel) {
			// get the values from viewModel and write it down to DB.
			if (_manager.UserExists(viewModel.UserName)) {
				return false;
			}
			ApplicationUser user = new ApplicationUser();
			ApplicationUser newUser = new ApplicationUser();
			newUser.UserName = viewModel.UserName;
			newUser.Email = viewModel.Email;

			if(_manager.CreateUser(newUser, viewModel.Password)) {
				var User = _manager.GetUser(viewModel.UserName);
				if (!_manager.UserIsInRole(User.Id, viewModel.Roles)) {
					_manager.AddUserToRole(User.Id, viewModel.Roles);
				}
				return true;
			}
			return false;
		}

		public List<UserViewModel> GetUsersByCourse(int courseID) {
			if (_db.Courses.Find(courseID) == null) {
				throw new HttpException(400, "Bad Request");
			}

			var users = (from user in _db.Users
						 join connection in _db.CourseUsers on user.Id equals connection.UserID
						 where courseID == connection.CourseID
						 select user).ToList();

			var viewModels = new List<UserViewModel>();

			foreach (ApplicationUser u in users) {
				var role = _manager.GetUserRoles(u.Id).ToList();
				var viewmodel = new UserViewModel() {
					username = u.UserName,
					email = u.Email,
					roles = role,
					userId = u.Id
				};
				viewModels.Add(viewmodel);
			}
			return viewModels;
		}

		public List<UserViewModel> GetTeachersByCourse(int courseID) {
			if (_db.Courses.Find(courseID) == null) {
				throw new HttpException(400, "Bad Request");
			}

			var users = (from user in _db.Users
						 join connection in _db.CourseUsers on user.Id equals connection.UserID
						 where courseID == connection.CourseID
						 select user).ToList();

			var viewModels = new List<UserViewModel>();

			foreach (ApplicationUser u in users) {
				var role = _manager.GetUserRoles(u.Id).ToList();
				foreach (string r in role) {
					if (r == "Teachers") {
						var viewmodel = new UserViewModel() {
							username = u.UserName,
							email = u.Email,
							roles = role,
							userId = u.Id
						};
						viewModels.Add(viewmodel);
					}
				}
			}
			return viewModels;
		}

		public List<UserViewModel> GetStudentsByCourse(int courseID) {
			if (_db.Courses.Find(courseID) == null) {
				throw new HttpException(400, "Bad Request");
			}

			var users = (from user in _db.Users
						 join connection in _db.CourseUsers on user.Id equals connection.UserID
						 where courseID == connection.CourseID
						 select user).ToList();

			var viewModels = new List<UserViewModel>();

			foreach (ApplicationUser u in users) {
				var role = _manager.GetUserRoles(u.Id).ToList();
				foreach (string r in role) {
					if (r == "Students") {
						var viewmodel = new UserViewModel() {
							username = u.UserName,
							email = u.Email,
							roles = role,
							userId = u.Id
						};
						viewModels.Add(viewmodel);
					}
				}
			}
			return viewModels;
		}
	}
}