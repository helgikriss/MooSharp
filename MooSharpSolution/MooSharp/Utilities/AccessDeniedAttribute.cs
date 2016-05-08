﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MooSharp.Utilities
{
	public class AccessDeniedAttribute : AuthorizeAttribute
	{
		protected override void HandleUnauthorizedRequest(AuthorizationContext context) {

			if (!context.HttpContext.User.Identity.IsAuthenticated) {
				base.HandleUnauthorizedRequest(context);
			}
			else {
				context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Home", action = "AccessDenied" }));
			}
		}
	}
}