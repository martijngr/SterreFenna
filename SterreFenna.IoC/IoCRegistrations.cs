using Autofac;
using SterreFenna.Business;
using SterreFenna.Business.Projects.Commands;
using SterreFenna.Business.Projects.Queries;
using SterreFenna.Business.Series;
using SterreFenna.Business.Series.Commands;
using SterreFenna.Business.Series.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SterreFenna.IoC
{
    public class IoCRegistrations
    {
        public static void RegisterTypes(ContainerBuilder builder)
        {
            builder.RegisterType<SFContext>().AsSelf().InstancePerRequest();

            RegisterCommands(builder);

            RegisterQueries(builder);

            builder.RegisterType<SeriePathManager>().AsSelf();
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
