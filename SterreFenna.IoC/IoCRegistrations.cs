using Autofac;
using SterreFenna.Business.Projects.Commands;
using SterreFenna.Business.Series;
using SterreFenna.Business.Series.Commands;
using SterreFenna.Domain;
using SterreFenna.Domain.Projects;
using SterreFenna.Domain.SerieItems;
using SterreFenna.Domain.Series;
using SterreFenna.EfDal;
using SterreFenna.EfDal.Projects;
using SterreFenna.EfDal.SerieItems;
using SterreFenna.EfDal.Series;
using System.Linq;

namespace SterreFenna.IoC
{
    public class IoCRegistrations
    {
        public static void RegisterTypes(ContainerBuilder builder)
        {
            builder.RegisterType<SFContext>().AsSelf().InstancePerRequest();

            RegisterCommands(builder);

            RegisterQueries(builder);

            builder.RegisterType<SeriePathResolver>().AsSelf();
            builder.RegisterType<EfUnitOfWork>().As<IUnitOfWork>().InstancePerRequest();
            builder.RegisterType<ProjectRepository>().As<IProjectRepository>();
            builder.RegisterType<SerieItemRepository>().As<ISerieItemRepository>();
            builder.RegisterType<SerieRepository>().As<ISerieRepository>();
            builder.RegisterType<SeriePathManagerFactory>().AsSelf();

            //builder.RegisterType<EditSerieCommand>().AsSelf();
            //builder.RegisterType<AddItemsToSerieCommand>().AsSelf();
            //builder.RegisterType<GetProjectListOverviewQuery>().AsSelf();
            //builder.RegisterType<GetProjectOverviewQuery>().AsSelf();
        }

        private static void RegisterCommands(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(AddItemsToSerieCommand).Assembly)
                   .Where(t => t.Name.EndsWith("Command"))
                   .AsSelf();

            builder.RegisterAssemblyTypes(typeof(EditProjectCommandHandler).Assembly)
                   .Where(t => t.Name.EndsWith("Handler"))
                   .AsSelf();
        }

        private static void RegisterQueries(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(AddItemsToSerieCommand).Assembly)
                   .Where(t => t.Name.EndsWith("Query"))
                   .AsSelf();
        }
    }
}
