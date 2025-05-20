using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebApplicationYeni
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                //defaults: new { controller = "Home", action = "About", id = UrlParameter.Optional }
                //defaults: new { controller = "Security", action = "Login", id = UrlParameter.Optional }
                defaults: new { controller = "Kitap", action = "GetAllKitaps", id = UrlParameter.Optional }

            );
        }
    }
}
