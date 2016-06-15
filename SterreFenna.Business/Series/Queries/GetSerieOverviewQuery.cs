using SterreFenna.Business.Series.Views;
using System.Collections.Generic;
using System.Linq;

namespace SterreFenna.Business.Series.Queries
{
    public class GetSerieOverviewQuery
    {
        private readonly SFContext _context;

        public GetSerieOverviewQuery(SFContext context)
        {
            _context = context;
        }

        public bool ActiveProjectsOnly { get; set; }

        public IEnumerable<SerieOverviewItem> Handle()
        {
            var view = (from g in _context.Series
                       select new SerieOverviewItem
                       {
                           Created = g.Created,
                           Id = g.Id,
                           Name = g.Name,
                           Published = g.Published,
                           ImageLocation = g.SerieItems.FirstOrDefault().Location
                       }).ToList();

            return view;
        }
    }
}
