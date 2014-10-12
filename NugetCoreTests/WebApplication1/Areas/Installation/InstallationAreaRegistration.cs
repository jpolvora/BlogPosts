﻿using System.Web.Mvc;
using WebApplication1.AutoUpdate;

namespace WebApplication1.Areas.Installation {
    public class InstallationAreaRegistration : AreaRegistration {
        public override string AreaName {
            get {
                return "Installation";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) {
            context.MapRoute(
                "Installation_default",
                "Installation/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );

            // To override package id based on convention, change it here...
            GlobalFilters.Filters.Add(new PackageIdFilter(null /* PackageId */));
        }
    }
}
