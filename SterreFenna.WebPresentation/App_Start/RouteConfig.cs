using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SterreFenna.WebPresentation
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "SerieOnly",
                url: "{serie}",
                defaults: new { controller = "Home", action = "SerieOnly" },
                namespaces: new string[] { "SterreFenna.WebPresentation.Controllers" }
            );

            routes.MapRoute(
                name: "ProjectWithSerie",
                url: "{project}/{serie}",
                defaults: new { controller = "Home", action = "Show" },
                namespaces: new string[] { "SterreFenna.WebPresentation.Controllers" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}",
                defaults: new { controller = "Home", action = "Index" },
                namespaces: new string[] { "SterreFenna.WebPresentation.Controllers" }
            );
        }
    }
}
