using SterreFenna.WebPresentation.Menus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace SterreFenna.WebPresentation
{
    public static class MvcHtmlStringExtensions
    {
        public static MvcHtmlString AdminMenuLink(this HtmlHelper helper, string text, string action, string controller)
        {
            var htmlString = GetAdminHtmlLink(helper, text, action, controller);

            return htmlString;
        }

        private static MvcHtmlString GetAdminHtmlLink(HtmlHelper helper, string text, string action, string controller)
        {
            var isLinkActive = IsAdminLinkActive(helper, action, controller) ? "active" : "";
            var link = helper.ActionLink(text, action, controller);
            var liItem = new MvcHtmlString($"<li class=\"{isLinkActive}\">{link}</li>");

            return liItem;
        }

        private static bool IsAdminLinkActive(HtmlHelper helper, string action, string controller)
        {
            var routeData = helper.ViewContext.RouteData.Values;
            var currentController = routeData["controller"].ToString();
            var currentAction = routeData["action"].ToString();

            return
                string.Equals(controller, currentController, StringComparison.OrdinalIgnoreCase);
        }

        public static MvcHtmlString SiteSerieLink(
            this HtmlHelper helper, 
            string uniqueProjectName, 
            string uniqueSerieName, 
            string serieName)
        {
            var htmlString = GetSiteHtmlLink(helper, uniqueProjectName, uniqueSerieName, serieName);

            return htmlString;
        }
        public static MvcHtmlString SiteSerieLink(
            this HtmlHelper helper, 
            string uniqueSerieName, 
            string serieName)
        {
            var htmlString = GetSiteHtmlLink(helper, "", uniqueSerieName, serieName);

            return htmlString;
        }

        private static MvcHtmlString GetSiteHtmlLink(HtmlHelper helper, string uniqueProjectName, string uniqueSerieName, string serieName)
        {
            var isLinkActive = IsSiteLinkActive(helper, uniqueProjectName, uniqueSerieName) ? "active" : "";
            var htmlAttrs = new { @class = $"{isLinkActive}"};
            MvcHtmlString link = null;

            if(uniqueProjectName.HasValue())
                link = helper.ActionLink(serieName, uniqueSerieName, uniqueProjectName, null, htmlAttrs);
            else
                link = helper.ActionLink(serieName, "Show", "Home", new { serie = uniqueSerieName}, htmlAttrs);

            var liItem = new MvcHtmlString($"<li class=\"{isLinkActive}\">{link}</li>");

            return liItem;
        }

        private static bool IsSiteLinkActive(HtmlHelper helper, string uniqueProjectName, string uniqueSerieName)
        {
            var routeData = helper.ViewContext.RouteData.Values;
            var currentProject = routeData["project"]?.ToString();
            var currentSerie = routeData["serie"]?.ToString();
            var active = string.Equals(uniqueSerieName, currentSerie, StringComparison.OrdinalIgnoreCase);

            if (uniqueProjectName.HasValue())
                active = active && string.Equals(uniqueProjectName, currentProject, StringComparison.OrdinalIgnoreCase);

            return active;    
        }

        public static string GetProjectCss(
            this HtmlHelper helper,
            MenuItem menuItem)
        {
            var cssClasses = string.Empty;
            if (menuItem.MenuItems.Any())
                cssClasses = IsProjectLinkActive(helper, menuItem.UniqueName) ? "active" : "";
            else
                cssClasses = IsSiteLinkActive(helper, "", menuItem.UniqueName) ? "active" : "";

            cssClasses += menuItem.MenuItems.Any() ?  "" : " no-sub-items";

            return cssClasses;
        }

        private static bool IsProjectLinkActive(HtmlHelper helper, string uniqueProjectName)
        {
            var routeData = helper.ViewContext.RouteData.Values;
            var currentProject = routeData["project"]?.ToString();

            return string.Equals(uniqueProjectName, currentProject, StringComparison.OrdinalIgnoreCase);
        }
    }
}