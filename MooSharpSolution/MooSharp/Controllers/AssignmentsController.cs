using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MooSharp.Services;

namespace MooSharp.Controllers
{
    public class AssignmentsController : Controller
    {
        private AssignmentsService _service = new AssignmentsService();
        // GET: Assignments
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details(int id){

            var viewModel = _service.GetAssignmentById(id);

            return View(viewModel);
        }
    }
}