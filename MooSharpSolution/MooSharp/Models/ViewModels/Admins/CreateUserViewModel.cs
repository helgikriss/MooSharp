using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MooSharp.Models.ViewModels.Admins
{
	public class CreateUserViewModel
	{
		[Required]
		[Display(Name = "Username")]
		public string userName { get; set; }

		[Required]
		[EmailAddress]
		[Display(Name = "Email")]
		public string email { get; set; }

		[Required]
		[StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 5)]
		[DataType(DataType.Password)]
		[Display(Name = "Password")]
		public string password { get; set; }

		[DataType(DataType.Password)]
		[Display(Name = "Confirm password")]
		[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
		public string confirmPassword { get; set; }

		public List<string> roles { get; set; }
	}
}