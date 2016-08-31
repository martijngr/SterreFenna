using SterreFenna.Business.Projects.Commands;
using SterreFenna.Business.Projects.Queries;
using System.Web.Mvc;

namespace SterreFenna.WebPresentation.Areas.Admin.Controllers
{
    [Authorize]
    public class ProjectController : Controller
    {
        private readonly GetProjectOverviewQueryHandler _getProjectOverviewQuery;
        private readonly CreateProjectCommandHandler _addProjectHandler;
        private readonly ChangeProjectOrderCommand _changeProjectOrderCommand;
        private readonly GetProjectByIdQueryHandler _getProjectByIdHandler;
        private readonly EditProjectCommandHandler _editProjectHandler;

        public ProjectController(
            GetProjectOverviewQueryHandler getProjectOverviewQuery,
            CreateProjectCommandHandler addProjectHandler,
            ChangeProjectOrderCommand changeProjectOrderCommand,
            GetProjectByIdQueryHandler getProjectByIdHandler,
            EditProjectCommandHandler editProjectHandler)
        {
            _getProjectOverviewQuery = getProjectOverviewQuery;
            _addProjectHandler = addProjectHandler;
            _changeProjectOrderCommand = changeProjectOrderCommand;
            _getProjectByIdHandler = getProjectByIdHandler;
            _editProjectHandler = editProjectHandler;
        }

        public ActionResult Index()
        {
            var query = new GetProjectOverviewQuery
            {
                ActiveProjectsOnly = false,
                WithSeries = false,
            };
            var model = _getProjectOverviewQuery.Handle(query);

            return View(model);
        }

        public ActionResult New()
        {
            return View();
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult SubmitNew(CreateProjectCommand model)
        {
            _addProjectHandler.Handle(model);
            
            return RedirectToAction("Index", "Project", new { area = "Admin" });
        }

        public ActionResult Edit(int id)
        {
            var query = new GetProjectByIdQuery { ProjectId = id };
            var view = _getProjectByIdHandler.Handle(query);

            return View(view);
        }

        [ValidateInput(false)]
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