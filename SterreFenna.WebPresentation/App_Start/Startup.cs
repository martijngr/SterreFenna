using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using SterreFenna.EfDal;
using SterreFenna.WebPresentation.UserManagement;

namespace SterreFenna.WebPresentation
{
    public class Startup
	{
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext(() => new SFContext());
            app.CreatePerOwinContext<AppUserManager>(AppUserManager.Create);

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Admin/Account/Login"),
            });
        }
    }
}