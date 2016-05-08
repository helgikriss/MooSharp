using MooSharp.Models;
using MooSharp.Models.ViewModels;
using MooSharp.Models.ViewModels.Admins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MooSharp.Services
{
    public class UsersService
	{
        private ApplicationDbContext _db;
		private IdentityManager _manager;

		public UsersService() {
			_db = new ApplicationDbContext();
			_manager = new IdentityManager();
		}
 
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

		public bool CreateUser(CreateUserViewModel viewModel) {
			// get the values from viewModel and write it down to DB.
			if (_manager.UserExists(viewModel.UserName)) {
				return false;
			}

			CreateUserViewModel testModel = new CreateUserViewModel {
				UserName = "TestUser",
				Email = "test@test.is",
				Password = "asdfg",
				ConfirmPassword = "asdfg",
				Roles = "Students"
			};
			ApplicationUser newUser = new ApplicationUser();
			newUser.UserName = testModel.UserName;
			newUser.Email = testModel.Email;

			if(_manager.CreateUser(newUser, testModel.Password)) {
				var User = _manager.GetUser(testModel.UserName);
				if (!_manager.UserIsInRole(User.Id, testModel.Roles)) {
					_manager.AddUserToRole(User.Id, testModel.Roles);
				}
				return true;
			}
			return false;
			
			/*
			ApplicationUser newUser = new ApplicationUser();
			newUser.UserName = viewModel.UserName;
			newUser.Email = viewModel.Email;

			_manager.CreateUser(newUser, viewModel.Password);

			var User = _manager.GetUser(viewModel.UserName);
			if (!_manager.UserIsInRole(User.Id, viewModel.Roles)) {
				_manager.AddUserToRole(User.Id, viewModel.Roles);
			}*/
		}
	}
}