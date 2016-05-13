using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MooSharp.Controllers
{
	/// <summary>
	/// Controller for Error Pages.
	/// </summary>
    public class ErrorPagesController : Controller
    {
		/// <summary>
		/// Returns Error view with error message.
		/// </summary>
		public ActionResult Error(int statusCode, Exception exception) {
			Response.StatusCode = statusCode;
			ViewBag.StatusCode = statusCode + " Error";
			return View();
		}
	}
}