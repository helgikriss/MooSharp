using MooSharp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MooSharp.Controllers
{
	/// <summary>
	/// This class handles all controls for assignments.
	/// </summary>
    public class AssignmentsController : Controller
    {
		private AssignmentsService _service = new AssignmentsService();
		
		public ActionResult Index() {
            return View();
        }

		/// <summary>
		/// Returns AssignmentViewModel.
		/// </summary>
		public ActionResult Details(int id) {
			var viewModel = _service.GetAssignmentByID(id);

			return View(viewModel);
		}

    }
}