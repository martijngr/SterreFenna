using SterreFenna.Business.Series.Views;
using SterreFenna.Domain;
using System;
using System.Linq;

namespace SterreFenna.Business.Series.Queries
{
    public class GetSerieByUniqueNameQuery
    {
        public string UniqueSerieName { get; set; }
    }

    public class GetSerieByUniqueNameQueryHandler
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetSerieByUniqueNameQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public SerieDetailView Handle(GetSerieByUniqueNameQuery query)
        {
            var serie = _unitOfWork.SerieRepository.GetByUniqueName(query.UniqueSerieName);

            if (serie == null)
                return null;

            var view = new SerieDetailView
            {
                Created = serie.Created,
                Id = serie.Id,
                Name = serie.Name,
                Published = serie.Published,
                SerieItems = (from i in serie.SerieItems
                              select new SerieItemDetailView
                              {
                                  Created = i.Created,
                                  Id = i.Id,
                                  Location = i.Location,
                                  Rank = i.Rank,
                                  IsLandingPageItem = i.IsHomePageItem
                              }).AsEnumerable(),
                ProjectName = serie.Project?.Name,
                ProjectId = serie.ProjectId,
                Credits = serie.Credits
            };

            return view;
        }
    }
}
