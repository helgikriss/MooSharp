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
		private ApplicationDbContext _db;
		private IdentityManager _manager;

		public UsersService() {
			_db = new ApplicationDbContext();
			_manager = new IdentityManager();
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
					roles = _manager.GetUserRoles(user.Id).ToList()
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
	}
}