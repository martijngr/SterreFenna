using SterreFenna.Business.Series.Queries;
using System.Web.Mvc;

namespace SterreFenna.WebPresentation.Areas.Admin.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly GetSerieOverviewQuery _getSerieOverviewQuery;

        public HomeController(GetSerieOverviewQuery getSerieOverviewQuery)
        {
            _getSerieOverviewQuery = getSerieOverviewQuery;
        }

        public ActionResult Index()
        {
            var model = _getSerieOverviewQuery.Handle();

            return View(model);
        }
    }
}