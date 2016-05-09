using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MooSharp.Handlers
{
	public class CustomHandleErrorAttribute : HandleErrorAttribute
	{
		public override void OnException(ExceptionContext filterContext) {
			//Get the exception
			Exception ex = filterContext.Exception;

			//TODO: Log the exception

			//Set the view name to be returned, maybe return different error view for different exception types
			string viewName = "";

			if(ex.Message == "Not found") {
				viewName = "NotFound";
			}
			else {
				viewName = "Error";
			}
			
			//Get current controller and action
			string currentController = (string)filterContext.RouteData.Values["controller"];
			string currentActionName = (string)filterContext.RouteData.Values["action"];

			//Create the error model information
			HandleErrorInfo model = new HandleErrorInfo(filterContext.Exception, currentController, currentActionName);
			ViewResult result = new ViewResult {
				ViewName = viewName,
				ViewData = new ViewDataDictionary<HandleErrorInfo>(model),
				TempData = filterContext.Controller.TempData
			};

			filterContext.Result = result;
			filterContext.ExceptionHandled = true;

			// Call the base class implementation:
			base.OnException(filterContext);
		}
	}
}