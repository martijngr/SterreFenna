using SterreFenna.Business.Projects.Queries;
using SterreFenna.Business.Series.Queries;
using SterreFenna.WebPresentation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SterreFenna.WebPresentation.Controllers
{
    public class HomeController : Controller
    {
        private readonly GetItemsForSerieQuery _getItemsForSerieQuery;
        private readonly GetFirstActiveProjectQueryHandler _getFirstActiveSerieHandler;
        private readonly GetLandingPageItemsQueryHandler _getLandingPageItemsQueryHandler;

        public HomeController(
            GetItemsForSerieQuery getItemsForSerieQuery,
            GetFirstActiveProjectQueryHandler getFirstActiveProjectHandler,
            GetLandingPageItemsQueryHandler getLandingPageItemsQueryHandler)
        {
            _getItemsForSerieQuery = getItemsForSerieQuery;
            _getFirstActiveSerieHandler = getFirstActiveProjectHandler;
            _getLandingPageItemsQueryHandler = getLandingPageItemsQueryHandler;
        }

        public ActionResult Landing()
        {
            var items = _getLandingPageItemsQueryHandler.Handle();
            var model = LandingPageModel.Create(items);

            return View(model);
        }

        public ActionResult Index()
        {
            var query = new GetFirstActiveProjectQuery();
            var serie = _getFirstActiveSerieHandler.Handle(query);

            if (serie.Series.Count() > 1)
                return RedirectToAction("Show", new
                {
                    project = serie.UniqueName,
                    serie = serie.Series.First().UniqueName
                });
            else
                return RedirectToAction("SerieOnly", new
                {
                    serie = serie.Series.First().UniqueName
                });
        }

        public ActionResult SerieOnly(string serie)
        {
            _getItemsForSerieQuery.SerieName = serie;

            var view = _getItemsForSerieQuery.Handle();

            return View("Show", view);
        }

        public ActionResult Show(string project, string serie)
        {
            _getItemsForSerieQuery.ProjectName = project;
            _getItemsForSerieQuery.SerieName = serie;

            var view = _getItemsForSerieQuery.Handle();

            return View(view);
        }
    }
}