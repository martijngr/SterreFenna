using Autofac;
using Autofac.Integration.Mvc;
using SterreFenna.Business.Settings;
using SterreFenna.IoC;
using SterreFenna.WebPresentation.App_Start;
using SterreFenna.WebPresentation.Controllers;
using SterreFenna.WebPresentation.Menus;
using SterreFenna.WebPresentation.Settings;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SterreFenna.WebPresentation
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

            InitIoC();
        }

        private void InitIoC()
        {
            var builder = new ContainerBuilder();

            // Register your MVC controllers.
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            // OPTIONAL: Register model binders that require DI.
            builder.RegisterModelBinders(Assembly.GetExecutingAssembly());
            builder.RegisterModelBinderProvider();

            // OPTIONAL: Register web abstractions like HttpContextBase.
            builder.RegisterModule<AutofacWebTypesModule>();

            // OPTIONAL: Enable property injection in view pages.
            builder.RegisterSource(new ViewRegistrationSource());

            // OPTIONAL: Enable property injection into action filters.
            builder.RegisterFilterProvider();

            builder.RegisterType<WebSettings>().As<ISettings>();
            builder.RegisterType<MenuBuilder>().AsSelf();

            IoCRegistrations.RegisterTypes(builder);

            // Set the dependency resolver to be Autofac.
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

        protected void Application_EndRequest()
        {
            if (Context.Response.StatusCode == 404)
            {
                //Response.Clear();
                //Response.TrySkipIisCustomErrors = true;

                //var rd = new RouteData();
                //rd.Values["controller"] = "Errors";
                //rd.Values["action"] = "NotFound";

                //IController c = new ErrorsController();
                //c.Execute(new RequestContext(new HttpContextWrapper(Context), rd));
            }
        }
    }
}
