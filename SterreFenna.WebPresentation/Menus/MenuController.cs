using SterreFenna.WebPresentation.Menus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SterreFenna.WebPresentation.Controllers
{
    public class MenuController : Controller
    {
        private readonly MenuBuilder _menuBuilder;

        public MenuController(MenuBuilder menuBuilder)
        {
            _menuBuilder = menuBuilder;
        }
        
        public ActionResult Index()
        {
            var model = _menuBuilder.CreateMenu();

            return View(model);
        }
    }
}