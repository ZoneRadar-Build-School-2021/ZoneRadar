using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ZoneRadar
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //會員
            routes.MapRoute(
                name: "Member",
                url: "Member/{id}",
                defaults: new { controller = "MemberCenter", action = "Member", id = UrlParameter.Optional }
            );

            //場地主
            routes.MapRoute(
                name: "Host",
                url: "Host/{id}",
                defaults: new { controller = "MemberCenter", action = "Host", id = UrlParameter.Optional }
            );

            //收藏
            routes.MapRoute(
                name: "Collection",
                url: "Collection/{id}",
                defaults: new { controller = "MemberCenter", action = "Collection", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Home",
                url: "",
                defaults: new { controller = "Home", action = "Index" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
