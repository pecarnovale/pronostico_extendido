using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace PronosticoExtendido
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "Default",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { controller = "Pronostico", action = "Home", id = RouteParameter.Optional }
            );
        }
    }
}
