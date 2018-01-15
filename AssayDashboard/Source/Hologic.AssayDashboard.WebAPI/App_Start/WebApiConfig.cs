using Hologic.AssayDashboard.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Hologic.AssayDashboard.WebAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Initialize DB upon startup. This will automatically crate and migrate to
            // its latest version
            //
            var assayDbContext = new AssayDashboardContext();
            assayDbContext.InitializeDatabase();

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
