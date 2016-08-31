using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using SterreFenna.WebPresentation.Areas.Admin.Models;
using SterreFenna.WebPresentation.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SterreFenna.WebPresentation.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        private AppUserManager AppUserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            }
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var  user = AppUserManager.Find(model.Email, model.Password);

                if (user != null)
                {
                    SignInUser(user, model.RememberMe);
                    return Redirect("/Admin");
                }
            }

            ModelState.AddModelError("", "Invalid username or password");

            return View(model);
        }

        public ActionResult LogOut()
        {
            var authManager = HttpContext.GetOwinContext().Authentication;
            authManager.SignOut();

            return View("Login");
        }

        public ActionResult Register(string name, string password)
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            var user = new IdentityUser(name);
            var result = userManager.Create(user, password);

            if (result.Succeeded)
            {
                SignInUser(user, true);
            }

            return Redirect("/Admin");
        }

        private void SignInUser(IdentityUser user, bool persist)
        {
            var ident = AppUserManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = persist }, ident);
        }
    }
}