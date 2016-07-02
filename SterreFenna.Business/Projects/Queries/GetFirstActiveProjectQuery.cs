using SterreFenna.Business.Projects.Views;
using SterreFenna.Business.Series.Views;
using System;
using System.Linq;

namespace SterreFenna.Business.Projects.Queries
{
    public class GetFirstActiveProjectQuery
    {
        private readonly SFContext _context;

        public GetFirstActiveProjectQuery(SFContext context)
        {
            _context = context;
        }

        public ProjectDetailsView Handle()
        {
            var project = _context.Projects.Where(p => p.Series.Any()).OrderBy(p => p.Rank).First();

            return new ProjectDetailsView
            {
                Id = project.Id,
                Name = project.Name,
                PublicationDate = project.PublicationDate,
                UniqueName = project.UniqueName,
                Series = from s in project.Series
                         select new SerieDetailView
                         {
                             Created = s.Created,
                             Id = s.Id,
                             Name = s.Name,
                             ProjectId = project.Id,
                             ProjectName = project.Name,
                             Published = s.Published,
                             UniqueName = s.UniqueName,
                         }
            };
        }
    }
}