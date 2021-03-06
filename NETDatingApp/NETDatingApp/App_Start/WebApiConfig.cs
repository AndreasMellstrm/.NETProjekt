﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace NETDatingApp
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            /*config.Routes.MapHttpRoute(
                name: "PostSend",
                routeTemplate: "api/posts/{message}/{recieverID}/{senderID}",
                );*/

            config.Routes.MapHttpRoute(
                name: "PostApiController",
                routeTemplate: "api/posts/{id}",
                defaults: new { id = RouteParameter.Optional }
                );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
