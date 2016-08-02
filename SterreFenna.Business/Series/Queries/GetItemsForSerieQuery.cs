using SterreFenna.Business.Projects;
using SterreFenna.Business.Series.Views;
using SterreFenna.Domain;
using SterreFenna.Domain.Series;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SterreFenna.Business.Series.Queries
{
    public class GetItemsForSerieQuery
    {
        public string SerieName { get; set; }

        public string ProjectName { get; set; }
    }

    public class GetItemsForSerieQueryHandler
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetItemsForSerieQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public SerieDetailView Handle(GetItemsForSerieQuery query)
        {
            SerieDetailView view = LoadSerieByProject(query);

            return view;
        }

        private SerieDetailView LoadSerieByProject(GetItemsForSerieQuery query)
        {
            var project = _unitOfWork.ProjectRepository.GetByUniqueName(query.ProjectName);
            if (project == null)
                throw new ProjectNotFoundException();

            Serie serie = null;
            if (query.SerieName.IsEmpty())
                serie = project.Series.First();
            else
                serie = project.Series.First(s => s.UniqueName == query.SerieName);

            return new SerieDetailView
            {
                Created = serie.Created,
                Id = serie.Id,
                Name = serie.Name,
                ProjectId = project.Id,
                ProjectName = project.Name,
                Published = serie.Published,
                SerieItems = from i in serie.SerieItems
                             select new SerieItemDetailView
                             {
                                 Created = i.Created,
                                 Id = i.Id,
                                 Location = i.Location,
                                 Rank = i.Rank
                             },
                Credits = serie.Credits.HasValue() ? serie.Credits : string.Empty
            };
        }
    }
}