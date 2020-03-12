using SterreFenna.Business.Projects.Views;
using SterreFenna.Business.Series.Views;
using SterreFenna.Domain;
using SterreFenna.Domain.Projects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SterreFenna.Business.Projects.Queries
{
    public class GetProjectOverviewQuery
    {
        public bool ActiveProjectsOnly { get; set; }

        public bool WithSeries { get; set; }
    }

    public class GetProjectOverviewQueryHandler
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetProjectOverviewQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<ProjectOverviewItem> Handle(GetProjectOverviewQuery query)
        {
            var projects = GetProjects(query);
            IEnumerable<ProjectOverviewItem> view = CreateView(projects, query);

            return view;
        }

        private IEnumerable<ProjectOverviewItem> CreateView(IEnumerable<Project> projects, GetProjectOverviewQuery query)
        {
            return from p in projects
                   select new ProjectOverviewItem
                   {
                       Id = p.Id,
                       Name = p.Name,
                       UniqueName = p.UniqueName,
                       TotalSeries = p.Series.Count,
                       Rank = p.Rank,
                       Description = p.Description,
                       SerieItems = query.WithSeries
                                    ?
                                        query.ActiveProjectsOnly
                                        ?
                                            from s in p.Series
                                            where s.Published == null || s.Published.Value <= DateTime.Now
                                            select new SerieOverviewItem
                                            {
                                                Id = s.Id,
                                                Name = s.Name,
                                                UniqueName = s.UniqueName
                                            }
                                        :
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

        private IEnumerable<Project> GetProjects(GetProjectOverviewQuery query)
        {
            var projectsToReturn = Enumerable.Empty<Project>();
            if (query.ActiveProjectsOnly)
            {
                var today = DateTime.Now;
                projectsToReturn = _unitOfWork.ProjectRepository
                                              .Find(p => p.Series.Any(s => s.Published.Value <= today || !s.Published.HasValue))
                                              .ToList();
            }
            else
                projectsToReturn = _unitOfWork.ProjectRepository.GetAll().ToList();

            return projectsToReturn;
        }
    }
}