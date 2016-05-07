using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MooSharp.Models.ViewModels
{
	public class UserViewModel
	{
		public string userId { get; set; }
		public string username { get; set; }
		public string email { get; set; }
		public string role { get; set; }
	}
}