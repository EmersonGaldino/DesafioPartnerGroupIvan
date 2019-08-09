using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace DesafioPartnerGroup
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // "força" o uso de JSON
            config.Formatters.Remove(GlobalConfiguration.Configuration.Formatters.XmlFormatter);
            config.Formatters.Add(GlobalConfiguration.Configuration.Formatters.JsonFormatter);

        }
    }
}
