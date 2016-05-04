using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MooSharp.Services;

namespace MooSharp.Controllers
{
	private AssignmentsService _service = new AssignmentsService();
    public class AssignmentsController : Controller
    {
        // GET: Assignments
        public ActionResult Index()
        {
            return View();
        }
    }
}