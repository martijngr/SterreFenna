using SterreFenna.Business.Contacts.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SterreFenna.WebPresentation.Controllers
{
    public class ContactController : Controller
    {
        private readonly GetContactQueryHandler _getContactHandler;

        public ContactController(GetContactQueryHandler getContactHandler)
        {
            _getContactHandler = getContactHandler;
        }

        public ActionResult Index()
        {
            var model = _getContactHandler.Handle(new GetContactQuery());

            return View(model);
        }

        public ActionResult AboutMe()
        {
            var model = _getContactHandler.Handle(new GetContactQuery());

            return View(model);
        }

        public ActionResult Education()
        {
            var model = _getContactHandler.Handle(new GetContactQuery());

            return View(model);
        }

        public ActionResult ContactMe()
        {
            var model = _getContactHandler.Handle(new GetContactQuery());

            return View(model);
        }
    }
}