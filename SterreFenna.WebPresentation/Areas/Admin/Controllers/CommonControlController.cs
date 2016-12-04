using System.Web.Mvc;

namespace SterreFenna.WebPresentation.Areas.Admin.Controllers
{
    public class CommonControlController: Controller
    {
        public ActionResult Popup()
        {
            return PartialView();
        }
    }
}