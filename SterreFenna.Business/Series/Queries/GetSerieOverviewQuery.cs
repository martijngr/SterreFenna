using SterreFenna.Business.Series.Views;
using SterreFenna.Domain;
using System.Collections.Generic;
using System.Linq;

namespace SterreFenna.Business.Series.Queries
{
    public class GetSerieOverviewQuery
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetSerieOverviewQuery(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool ActiveProjectsOnly { get; set; }

        public IEnumerable<SerieOverviewItem> Handle()
        {
            var view = (from g in _unitOfWork.SerieRepository.GetAll().ToList()
                       select new SerieOverviewItem
                       {
                           Created = g.Created,
                           Id = g.Id,
                           Name = g.Name,
                           Published = g.Published,
                           ImageLocation = g.SerieItems.FirstOrDefault().Location,
                           TotalItems = g.SerieItems.Count,
                           TotalMarkedItems = g.SerieItems.Count(s => s.IsHomePageItem)
                       }).ToList();

            return view;
        }
    }
}
