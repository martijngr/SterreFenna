using System.Configuration;
using System.Web.Mvc;

namespace SterreFenna.WebPresentation.Actions
{
    public class SiteOfflineFilterAttribute : ActionFilterAttribute
    {
        private string _offlineUrl;
        public SiteOfflineFilterAttribute(string offlineUrl)
        {
            _offlineUrl = offlineUrl;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (IsOfflineController(filterContext) || IsAdminArea(filterContext))
                return;
          
            if (ConfigurationManager.AppSettings["SiteOffline"] == "1")
            {
                filterContext.Result = new RedirectResult(_offlineUrl);
            }
        }

        private bool IsOfflineController(ActionExecutingContext filterContext)
        {
            return filterContext.RouteData.Values["controller"] != null
                && filterContext.RouteData.Values["controller"].ToString().ToLower() == "offline";
        }

        private bool IsAdminArea(ActionExecutingContext filterContext)
        {
            return filterContext.RouteData.DataTokens["area"] != null 
                && filterContext.RouteData.DataTokens["area"].ToString().ToLower() == "admin";
        }
    }
}