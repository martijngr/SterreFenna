using SterreFenna.Business.Contacts.Commands;
using SterreFenna.Business.Contacts.Queries;
using System.Web.Mvc;

namespace SterreFenna.WebPresentation.Areas.Admin.Controllers
{
    [Authorize]
    public class ContactController : Controller
    {
        private readonly UpdateContactCommandHandler _updateContactHandler;
        private readonly GetContactQueryHandler _getContactHandler;

        public ContactController(
            UpdateContactCommandHandler updateContactHandler,
            GetContactQueryHandler getContactHandler)
        {
            _updateContactHandler = updateContactHandler;
            _getContactHandler = getContactHandler;
        }

        public ActionResult Index()
        {
            var model = _getContactHandler.Handle(new GetContactQuery());

            return View(model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(UpdateContactCommand command)
        {
            _updateContactHandler.Handle(command);

            return RedirectToAction("Index");
        }
    }
}