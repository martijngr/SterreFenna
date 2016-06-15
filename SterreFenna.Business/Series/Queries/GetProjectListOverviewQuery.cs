using SterreFenna.Business.Series.Views;
using System.Collections.Generic;
using System.Linq;

namespace SterreFenna.Business.Series.Queries
{
    public class GetProjectListOverviewQuery
    {
        private readonly SFContext _context;

        public GetProjectListOverviewQuery()
        {
            _context = new SFContext();
        }

        public IEnumerable<ProjectListOverviewItem> Handle()
        {
            var view = (from g in _context.Projects
                        select new ProjectListOverviewItem
                        {
                            Id = g.Id,
                            Name = g.Name
                        }).ToList();

            return view;
        }
    }
}