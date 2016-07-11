using SterreFenna.Business.Projects.Views;
using SterreFenna.Business.Series.Views;
using SterreFenna.Domain;
using SterreFenna.Domain.Projects;
using SterreFenna.Domain.Series;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SterreFenna.Business.Projects.Queries
{
    public class GetProjectOverviewQuery
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetProjectOverviewQuery(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool ActiveProjectsOnly { get; set; }

        public bool WithSeries { get; set; }

        public IEnumerable<ProjectOverviewItem> Handle()
        {
            var projects = GetProjects();
            IEnumerable<ProjectOverviewItem> view = CreateView(projects);

            return view;
        }

        private IEnumerable<ProjectOverviewItem> CreateView(IEnumerable<Project> projects)
        {
            return from p in projects
                   select new ProjectOverviewItem
                   {
                       Id = p.Id,
                       Name = p.Name,
                       UniqueName = p.UniqueName,
                       TotalSeries = p.Series.Count,
                       Rank = p.Rank,
                       SerieItems = WithSeries
                                    ?
                                       from s in p.Series
                                       select new SerieOverviewItem
                                       {
                                           Id = s.Id,
                                           Name = s.Name,
                                           UniqueName = s.UniqueName
                                       }
                                    : new List<SerieOverviewItem>()
                   };
        }

        private IEnumerable<Project> GetProjects()
        {
            var projectsToReturn = Enumerable.Empty<Project>();
            if (ActiveProjectsOnly)
            {
                projectsToReturn = _unitOfWork.ProjectRepository
                                              .Find(p => p.Series.Any(IsSerieActive))
                                              .ToList();
            }
            else
                projectsToReturn = _unitOfWork.ProjectRepository.GetAll().ToList();

            return projectsToReturn;
        }

        private bool IsSerieActive(Serie s)
        {
            var today = DateTime.Now;
            return s.Published.Value > today || !s.Published.HasValue;
        }
    }
}