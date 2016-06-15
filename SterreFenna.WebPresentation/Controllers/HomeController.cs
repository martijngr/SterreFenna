using SterreFenna.Business.Projects.Queries;
using SterreFenna.Business.Series.Queries;
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
        private readonly GetFirstActiveProjectQuery _getFirstActiveSerieQuery;

        public HomeController(GetItemsForSerieQuery getItemsForSerieQuery, GetFirstActiveProjectQuery getFirstActiveProjectQuery)
        {
            _getItemsForSerieQuery = getItemsForSerieQuery;
            _getFirstActiveSerieQuery = getFirstActiveProjectQuery;
        }

        public ActionResult Index()
        {
            var serie = _getFirstActiveSerieQuery.Handle();

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