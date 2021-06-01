using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace RESTContact
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            //config.MapHttpAttributeRoutes();

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);
            config.Routes.MapHttpRoute(
           name: "Contacts",
               routeTemplate: "api/contacts",
               defaults: new { controller = "contacts" }
               );

            config.Routes.MapHttpRoute(
            name: "Home",
                routeTemplate: "api/home",
                defaults: new { controller = "home" }
                );


            config.Routes.MapHttpRoute(
            name: "Values",
                routeTemplate: "api/values",
                defaults: new { controller = "values" }
                );


            config.Routes.MapHttpRoute(
            name: "Contact",
                routeTemplate: "api/contacts/{id}",
                defaults: new { controller = "contacts", id = RouteParameter.Optional }
                );

        }
    }
}
