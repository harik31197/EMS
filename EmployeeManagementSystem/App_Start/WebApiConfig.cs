
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Net.Http.Formatting;
using System.Web.Http.Cors;

namespace EmployeeManagementSystem
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {

           // Web API configuration and services
            config.Formatters.JsonFormatter.AddUriPathExtensionMapping("json", "application/json");
            var json = config.Formatters.JsonFormatter;
            json.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects;
            config.Formatters.Remove(config.Formatters.XmlFormatter);
            json.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            // Web API routes
            config.MapHttpAttributeRoutes();

                config.EnableCors();
                config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }


            );


        }
    }
}
