using SterreFenna.Business.Series.Views;
using SterreFenna.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SterreFenna.Business.Series.Commands
{
    public class GetSerieByIdQuery
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetSerieByIdQuery(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public SerieDetailView Handle(int serieId)
        {
            var serie = _unitOfWork.SerieRepository.GetById(serieId);

            if (serie == null)
                throw new Exception($"No serie found with id {serieId}");

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
               ProjectId = serie.ProjectId ,
               Credits = serie.Credits
            };

            return view;
        }
    }
}
