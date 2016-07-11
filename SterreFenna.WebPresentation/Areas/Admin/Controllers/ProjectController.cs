using SterreFenna.Business.Projects.Commands;
using SterreFenna.Business.Projects.Queries;
using SterreFenna.WebPresentation.Areas.Admin.Models;
using System.Web.Mvc;

namespace SterreFenna.WebPresentation.Areas.Admin.Controllers
{
    //[Authorize]
    public class ProjectController : Controller
    {
        private readonly GetProjectOverviewQuery _getProjectOverviewQuery;
        private readonly AddProjectCommand _addProjectCommand;
        private readonly ChangeProjectOrderCommand _changeProjectOrderCommand;
        private readonly GetProjectByIdQuery _getProjectByIdQuery;
        private readonly EditProjectCommandHandler _editProjectHandler;

        public ProjectController(
            GetProjectOverviewQuery getProjectOverviewQuery, 
            AddProjectCommand addProjectCommand,
            ChangeProjectOrderCommand changeProjectOrderCommand,
            GetProjectByIdQuery getProjectByIdQuery,
            EditProjectCommandHandler editProjectHandler)
        {
            _getProjectOverviewQuery = getProjectOverviewQuery;
            _addProjectCommand = addProjectCommand;
            _changeProjectOrderCommand = changeProjectOrderCommand;
            _getProjectByIdQuery = getProjectByIdQuery;
            _editProjectHandler = editProjectHandler;
        }

        public ActionResult Index()
        {
            var model = _getProjectOverviewQuery.Handle();

            return View(model);
        }

        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SubmitNew(NewProjectModel model)
        {
            _addProjectCommand.Name = model.Name;

            _addProjectCommand.Handle();
            
            return RedirectToAction("Index", "Project", new { area = "Admin" });
        }

        public ActionResult Edit(int id)
        {
            _getProjectByIdQuery.ProjectId = id;
            var view = _getProjectByIdQuery.Handle();

            return View(view);
        }

        public ActionResult SubmitEdit(EditProjectCommand command)
        {
            _editProjectHandler.Handle(command);

            return RedirectToAction("Index", "Project", new { area = "Admin" });
        }

        [HttpPost]
        public ActionResult ChangeProjectOrder(int[] projectOrder)
        {
            _changeProjectOrderCommand.ProjectIds = projectOrder;
            _changeProjectOrderCommand.Handle();

            return RedirectToAction("Index", "Project", new { area = "Admin" });
        }
    }
}