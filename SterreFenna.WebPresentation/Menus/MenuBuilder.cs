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
        private readonly GetProjectOverviewQuery _getProjectOverviewQuery;
        private readonly GetSerieOverviewQuery _getSerieOverviewQuery;

        public MenuBuilder(GetProjectOverviewQuery getProjectOverviewQuery,
            GetSerieOverviewQuery getSerieOverviewQuery)
        {
            _getProjectOverviewQuery = getProjectOverviewQuery;
            _getSerieOverviewQuery = getSerieOverviewQuery;
        }

        public MenuModel CreateMenu()
        {
            var menu = new MenuModel();
            _getProjectOverviewQuery.ActiveProjectsOnly = true;
            _getProjectOverviewQuery.WithSeries = true;

            var projects = _getProjectOverviewQuery.Handle();

            foreach (var project in projects)
            {
                // Only use projects with active series
                var serieItems = project.SerieItems.Where(s => s.Published <= DateTime.Now || !s.Published.HasValue);

                // Don't show projects without any active series.
                if (!serieItems.Any())
                    continue;

                var menuItem = new MenuItem();
                menuItem.Id = project.Id;
                menuItem.Name = project.Name;
                menuItem.Rank = project.Rank;
                menuItem.UniqueName = project.UniqueName;

                if (serieItems.Count() > 1)
                {
                    menuItem.MenuItems = from s in serieItems
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
                    // When a project has only 1 serie, use the serie name as the display name
                    menuItem.Name = serieItems.First().Name;
                }

                menu.MenuItems.Add(menuItem);
            }

            return menu;
        }
    }
}