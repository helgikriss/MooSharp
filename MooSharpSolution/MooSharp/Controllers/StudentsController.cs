using MooSharp.Services;
using MooSharp.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace MooSharp.Controllers
{
	[AccessDeniedAttribute(Roles = "Students")]
	[Authorize(Roles = "Students")]
	public class StudentsController : Controller
    {
		private AssignmentsService _assignmentsService = new AssignmentsService();
        // GET: Students
        public ActionResult Index()
        {
            return View();
        }

    }
}