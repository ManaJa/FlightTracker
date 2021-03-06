﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using ServiceStack.Text;

namespace FlightTracker
{
	// Note: For instructions on enabling IIS6 or IIS7 classic mode, 
	// visit http://go.microsoft.com/?LinkId=9394801

	public class MvcApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();
        

			WebApiConfig.Register(GlobalConfiguration.Configuration);
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);
			
			JsConfig.EmitCamelCaseNames = true;

            Bootstrapper.Initialise();


		}

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            if (!HttpContext.Current.Request.Path.Contains("Bootstrap"))
                if (!FlightTracker.Controllers.Bootstrap.IsCompleted)
                {
                    Response.Redirect("~/Bootstrap?ref=" + HttpContext.Current.Request.Path);
                }

        }
	}



}