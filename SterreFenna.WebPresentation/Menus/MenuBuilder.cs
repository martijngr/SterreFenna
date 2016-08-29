using SterreFenna.Business.Projects.Queries;
using SterreFenna.Business.Series.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SterreFenna.WebPresentation.Menus
{
    public class MenuBuilder
    {
        private readonly GetProjectOverviewQueryHandler _getProjectOverviewQuery;
        private readonly GetSerieOverviewQuery _getSerieOverviewQuery;

        public MenuBuilder(GetProjectOverviewQueryHandler getProjectOverviewQuery,
            GetSerieOverviewQuery getSerieOverviewQuery)
        {
            _getProjectOverviewQuery = getProjectOverviewQuery;
            _getSerieOverviewQuery = getSerieOverviewQuery;
        }

        public MenuModel CreateMenu()
        {
            var menu = new MenuModel();
            var query = new GetProjectOverviewQuery
            {
                ActiveProjectsOnly = true,
                WithSeries = true,

            };
            var projects = _getProjectOverviewQuery.Handle(query);

            foreach (var project in projects)
            {
                // Don't show projects without any active series.
                if (!project.SerieItems.Any())
                    continue;

                var menuItem = new MenuItem();
                menuItem.Id = project.Id;
                menuItem.Name = project.Name;
                menuItem.Rank = project.Rank;
                menuItem.UniqueName = project.UniqueName;

                if (project.SerieItems.Count() > 1)
                {
                    menuItem.MenuItems = from s in project.SerieItems
                                         select new MenuItem
                                         {
                                             Id = s.Id,
                                             Name = s.Name,
                                             Rank = s.Rank,
                                             UniqueName = s.UniqueName
                                         };
                }
                else
                {
                    // Check project has a project description
                    if (project.Description.HasValue())
                    {
                        menuItem.MenuItems = from s in project.SerieItems
                                             select new MenuItem
                                             {
                                                 Id = s.Id,
                                                 Name = s.Name,
                                                 Rank = s.Rank,
                                                 UniqueName = s.UniqueName
                                             };
                    }
                    else
                    {
                        // When a project has only 1 serie, use the serie name as the display name and unique name
                        menuItem.Name = project.SerieItems.First().Name;
                        menuItem.UniqueName = project.SerieItems.First().UniqueName;
                    }
                }

                menu.MenuItems.Add(menuItem);
            }

            return menu;
        }
    }
}