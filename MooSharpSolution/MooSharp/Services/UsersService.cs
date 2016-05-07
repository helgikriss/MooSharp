using MooSharp.Models;
using MooSharp.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MooSharp.Services
{
    public class UsersService
	{
        private ApplicationDbContext _db;
 
 		public UsersService() {
			_db = new ApplicationDbContext();
		}
 
 		public List<UserViewModel> GetAllUsers() {
			var users = _db.Users.ToList();
			
			var viewmodels = new List<UserViewModel>();
			
			foreach (ApplicationUser user in users) {
				var viewmodel = new UserViewModel() {
					userId = user.Id,
					email = user.Email,
					username = user.UserName,
					role = _db.Roles.Where(x => x.Id == user.Id).Select(X => X.Name).ToString()
				};
				viewmodels.Add(viewmodel);
			}
			return viewmodels;
		}
	}
}