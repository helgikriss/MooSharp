using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using MooSharp.Utilities;
using MooSharp.Controllers;

namespace MooSharp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
			AccountInitializer.Init();
        }
		public void Application_Error(Object sender, EventArgs e) {
			Exception exception = Server.GetLastError();
			Server.ClearError();

			var routeData = new RouteData();
			routeData.Values.Add("controller", "ErrorPages");
			routeData.Values.Add("action", "Error");
			routeData.Values.Add("exception", exception);

			if (exception.GetType() == typeof(HttpException)) {
				routeData.Values.Add("statusCode", ((HttpException)exception).GetHttpCode());
			}
			else {
				routeData.Values.Add("statusCode", 500);
			}

			Response.TrySkipIisCustomErrors = true;
			IController controller = new ErrorPagesController();
			controller.Execute(new RequestContext(new HttpContextWrapper(Context), routeData));
			Response.End();
		}
	}
}
