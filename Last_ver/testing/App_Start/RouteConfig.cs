using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace testing
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Home",
                url: "",
                defaults: new { controller = "News", action = "Index"}
            );

            routes.MapRoute(
                name: "Registration",
                url: "registration",
                defaults: new { controller = "Account", action = "Register" }
            );

            routes.MapRoute(
                name: "Login",
                url: "login",
                defaults: new { controller = "Account", action = "Login" }
            );

            routes.MapRoute(
                name: "LogOff",
                url: "logoff",
                defaults: new { controller = "Account", action = "LogOff" }
            );

            routes.MapRoute(
                name: "NewsAdd",
                url: "Add/{id}",
                defaults: new { controller = "News", action = "Add", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "NewsArticle",
                url: "Article/{id}-{translit}",
                defaults: new { controller = "News", action = "Article", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "HomeDel",
                url: "Del/{id}",
                defaults: new { controller = "News", action = "Del", id = UrlParameter.Optional }
            );
            routes.MapRoute(
               name: "RoleAddToUser",
               url: "RoleManage",
               defaults: new { controller = "Account", action = "RoleAddToUser"}
           );
            routes.MapRoute(
               name: "DeleteRoleForUser",
               url: "delete",
               defaults: new { controller = "Account", action = "DeleteRoleForUser" }
           );





        }
    }
}
