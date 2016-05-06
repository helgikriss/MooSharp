using Microsoft.AspNet.Identity;
using MooSharp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MooSharp.Controllers
{
	public class HomeController : Controller
	{
		[Authorize]
		public ActionResult Index()
		{
			var manager = new IdentityManager();

			string id = User.Identity.GetUserId().ToString();

			if (manager.UserIsInRole(id, "Administrators")) {
				return RedirectToAction("Index", "Admins");
			}
			if (manager.UserIsInRole(id, "Students")) {
				return RedirectToAction("Index", "Students");
			}
			if(manager.UserIsInRole(id, "Teachers")) {
				return RedirectToAction("Index", "Teachers");
			}
			return View();
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		[Authorize(Roles = "Students")]
		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}