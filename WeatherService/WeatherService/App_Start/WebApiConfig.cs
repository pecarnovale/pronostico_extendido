using WeatherService.Controllers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace WeatherService
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            
            // Configuración y servicios de API web
            config.MapHttpAttributeRoutes();
            config.MessageHandlers.Add(new TokenValidationHandler());

            var cors = new EnableCorsAttribute(ConfigurationManager.AppSettings["CORS_URL"].ToString(), "*", "*");
            config.EnableCors(cors);
            // Rutas de API web

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
