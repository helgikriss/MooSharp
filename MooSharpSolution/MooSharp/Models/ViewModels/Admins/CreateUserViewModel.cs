using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MooSharp.Models.ViewModels.Admins
{
    /// <summary>
    /// This class creates a new user
    /// </summary>
    public class CreateUserViewModel
	{
        /// <summary>
        /// Contains a username
        /// </summary>
        [Required]
		[Display(Name = "Username")]
		public string UserName { get; set; }

        /// <summary>
        /// Contains the email address of the user
        /// Example: user@example.com
        /// </summary>
        [Required]
		[EmailAddress]
		[Display(Name = "Email")]
		public string Email { get; set; }

        /// <summary>
        /// Contains a password that has to 
        /// be at least 6 characters long
        /// </summary>
        [Required]
		[StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
		[DataType(DataType.Password)]
		[Display(Name = "Password")]
		public string Password { get; set; }

        /// <summary>
        /// A user must confirm his password by 
        /// entering it again
        /// </summary>
        [DataType(DataType.Password)]
		[Display(Name = "Confirm password")]
		[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
		public string ConfirmPassword { get; set; }

        /// <summary>
        /// A role must be assigned to each user.
        /// Example: Student, Teacher, Admin
        /// </summary>
        public string Roles { get; set; }
	}
}