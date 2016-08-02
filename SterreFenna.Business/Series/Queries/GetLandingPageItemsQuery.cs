using SterreFenna.Business.Series.Views;
using SterreFenna.Domain;
using SterreFenna.EfDal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SterreFenna.Business.Series.Queries
{
    public class GetLandingPageItemsQuery
    {

    }

    public class GetLandingPageItemsQueryHandler
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetLandingPageItemsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<LandingpageItemView> Handle()
        {
            var landingpageItems = _unitOfWork.SerieItemRepository
                                              .Find(si => si.IsHomePageItem)
                                              .Select(si => new LandingpageItemView
                                              {
                                                  Location = si.Location,
                                                  UniqueProjectName = si.Serie.Project.UniqueName,
                                                  UniqueSerieName = si.Serie.UniqueName,
                                              })
                                              .ToList();

            return landingpageItems;
        }
    }
}