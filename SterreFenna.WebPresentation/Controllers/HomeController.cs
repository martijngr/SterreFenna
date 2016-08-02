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
        private readonly GetItemsForSerieQueryHandler _getItemsForSerieQuery;
        private readonly GetFirstActiveProjectQueryHandler _getFirstActiveSerieHandler;
        private readonly GetLandingPageItemsQueryHandler _getLandingPageItemsQueryHandler;
        private readonly GetProjectByUniqueNameQueryHandler _getProjectByUniqueNameQueryHandler;

        public HomeController(
            GetItemsForSerieQueryHandler getItemsForSerieQuery,
            GetFirstActiveProjectQueryHandler getFirstActiveProjectHandler,
            GetLandingPageItemsQueryHandler getLandingPageItemsQueryHandler,
            GetProjectByUniqueNameQueryHandler getProjectByUniqueNameQueryHandler)
        {
            _getItemsForSerieQuery = getItemsForSerieQuery;
            _getFirstActiveSerieHandler = getFirstActiveProjectHandler;
            _getLandingPageItemsQueryHandler = getLandingPageItemsQueryHandler;
            _getProjectByUniqueNameQueryHandler = getProjectByUniqueNameQueryHandler;
        }

        public ActionResult Landing()
        {
            var query = new GetFirstActiveProjectQuery();
            var project = _getFirstActiveSerieHandler.Handle(query);
            var items = _getLandingPageItemsQueryHandler.Handle();
            var model = LandingPageModel.Create(items, project);

            return View(model);
        }

        public ActionResult Index()
        {
            var query = new GetFirstActiveProjectQuery();
            var project = _getFirstActiveSerieHandler.Handle(query);

            return RedirectToAction("Show", new
            {
                project = project.UniqueProjectName,
                serie = project.Series.First().UniqueName
            });
        }

        //public ActionResult SerieOnly(string serie)
        //{
        //    _getItemsForSerieQuery.SerieName = serie;

        //    var view = _getItemsForSerieQuery.Handle();

        //    return View("Show", view);
        //}

        public ActionResult Show(string project, string serie)
        {
            var projectDetails = _getProjectByUniqueNameQueryHandler.Handle(new GetProjectByUniqueNameQuery
            {
                UniqueName = project
            });

            if (serie.IsEmpty() && projectDetails.Description.HasValue())
            {
                return View("ProjectDescription", projectDetails);
            }
            else
            {
                var query = new GetItemsForSerieQuery
                {
                    ProjectName = project,
                    SerieName = serie
                };
                var view = _getItemsForSerieQuery.Handle(query);

                return View(view);
            }
        }
    }
}