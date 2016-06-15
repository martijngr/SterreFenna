using SterreFenna.Business.Projects.Views;
using SterreFenna.Business.Series.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SterreFenna.Business.Projects.Queries
{
    public class GetProjectOverviewQuery
    {
        private readonly SFContext _context;

        public GetProjectOverviewQuery()
        {
            _context = new SFContext();
        }

        public bool ActiveProjectsOnly { get; set; }

        public bool WithSeries { get; set; }

        public IEnumerable<ProjectOverviewItem> Handle()
        {
            var projects = _context.Projects.AsQueryable();

            if (ActiveProjectsOnly)
                projects = projects.Where(p => p.PublicationDate == null || p.PublicationDate.Value <= DateTime.Now);

            var projectsToReturn = projects.ToList();

            var view = from p in projectsToReturn
                       select new ProjectOverviewItem
                       {
                           Id = p.Id,
                           Name = p.Name,
                           UniqueName = p.UniqueName,
                           TotalSeries = p.Series.Count,
                           PublicationDate = p.PublicationDate,
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

            return view;
        }
    }
}