using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MooSharp.Controllers
{
    public class NavigationController : Controller
    {
		[ChildActionOnly]
		public ActionResult SideBar() {
			if (User.IsInRole("Administrators")) {
				return PartialView("AdminSideBar");
			}
			if (User.IsInRole("Teachers")) {
				return PartialView("TeacherSideBar");
			}
			if (User.IsInRole("Students")) {
				return PartialView("StudentSideBar");
			}

			return new EmptyResult();
		}
	}
}