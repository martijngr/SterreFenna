using SterreFenna.Business.Projects.Queries;
using SterreFenna.Business.Series.Commands;
using SterreFenna.Business.Series.Queries;
using SterreFenna.WebPresentation.Areas.Admin.Models;
using SterreFenna.WebPresentation.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace SterreFenna.WebPresentation.Areas.Admin.Controllers
{
    [Authorize]
    public class SerieController : Controller
    {
        private readonly CreateSerieCommandHandler _createSerieHandler;
        private readonly EditSerieCommandHandler _editSerieHandler;
        private readonly GetProjectListOverviewQuery _getProjectListOverviewQuery;
        private readonly GetSerieByIdQuery _getSerieByIdQuery;
        private readonly GetProjectOverviewQueryHandler _getProjectOverviewQuery;

        public SerieController(
            CreateSerieCommandHandler createGalleryCommand,
            EditSerieCommandHandler editSerieCommand,
            GetProjectListOverviewQuery getProjectListOverviewQuery,
            GetSerieByIdQuery getSerieByIdQuery,
            GetProjectOverviewQueryHandler getProjectOverviewQuery)
        {
            _createSerieHandler = createGalleryCommand;
            _editSerieHandler = editSerieCommand;
            _getProjectListOverviewQuery = getProjectListOverviewQuery;
            _getProjectOverviewQuery = getProjectOverviewQuery;
            _getSerieByIdQuery = getSerieByIdQuery;
        }

        public ActionResult Edit(int id)
        {
            var model = new EditSerieModel
            {
                SerieDetails = _getSerieByIdQuery.Handle(id),
                Projects = _getProjectOverviewQuery.Handle(new GetProjectOverviewQuery()),
            };

            return View(model);
        }

        public ActionResult SubmitEdit(PostedSerieModel model)
        {
            var serie = _getSerieByIdQuery.Handle(model.SerieId);
            var items = new List<UploadedSerieItem>();
            var filesnames = model.filenameOrder.Split(',');
            var rankCounter = 0;

            foreach (var filename in filesnames)
            {
                var file = model.files.FirstOrDefault(f => f != null && f.FileName == filename);
                if (file != null)
                {
                    items.Add(new UploadedSerieItem
                    {
                        Filename = filename,
                        Stream = file.InputStream,
                        Rank = rankCounter,
                    });
                }
                else
                {
                    items.Add(new UploadedSerieItem {
                        Filename = Path.GetFileName(filename),
                        Rank = rankCounter,
                    });
                }
                rankCounter++;
            }

            var command = new EditSerieCommand
            {
                ProjectId = model.ProjectId,
                ProjectName = model.newProjectName,
                SerieId = model.SerieId,
                SerieItems = items,
                SerieName = model.name,
                Credits = model.Credits
            };
            
            if(model.favouriteFilenames.HasValue())
                command.FavouriteItems = model.favouriteFilenames.Split(',').ToList();

            if (model.publicationDate.HasValue())
                command.PublicationDate = DateTime.Parse(model.publicationDate);

            _editSerieHandler.Handle(command);

            return RedirectToAction("Index", "Serie", new { area = "Admin", id = model.SerieId });
        }
        
        public ActionResult New()
        {
            var projects = _getProjectListOverviewQuery.Handle();

            return View(projects);
        }

        public ActionResult SubmitNew(PostedSerieModel model)
        {
            var items = new List<UploadedSerieItem>();
            var filesnames = model.filenameOrder.Split(',');

            foreach (var filename in filesnames)
            {
                var file = model.files.First(f => f.FileName == filename);
                items.Add(new UploadedSerieItem
                {
                    Filename= filename,
                    Stream = file.InputStream
                });
            }

            var command = new CreateSerieCommand
            {
                ProjectId = model.ProjectId,
                ProjectName = model.newProjectName,
                SerieItems = items,
                SerieName = model.name,
                Credits = model.Credits,
            };
            
            if (model.publicationDate.HasValue())
                command.PublicationDate = DateTime.Parse(model.publicationDate);

            _createSerieHandler.Handle(command);

            return RedirectToAction("Index", "Serie", new { area="Admin",  id = command.StoredSerieId });
        }
    }
}