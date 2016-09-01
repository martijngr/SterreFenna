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
    public class GetFirstActiveSerieQuery
    {
        public string SerieName { get; set; }

        public string ProjectName { get; set; }
    }

    public class GetFirstActiveSerieQueryHandler
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetFirstActiveSerieQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public SerieDetailView Handle(GetFirstActiveSerieQuery query)
        {
            SerieDetailView view = LoadSerieByProject(query);

            return view;
        }

        private SerieDetailView LoadSerieByProject(GetFirstActiveSerieQuery query)
        {
            var project = _unitOfWork.ProjectRepository.GetByUniqueName(query.ProjectName);
            if (project == null)
                throw new ProjectNotFoundException();

            Serie serie = null;
            if (query.SerieName.IsEmpty())
                serie = project.Series.FirstOrDefault(s => s.Published== null || s.Published.Value <= DateTime.Now);
            else
                serie = project.Series.FirstOrDefault(s => s.UniqueName == query.SerieName && s.Published == null || (s.Published != null &&s.Published.Value <= DateTime.Now));

            if (serie == null)
                return null;

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
                Credits = serie.Credits.HasValue() ? serie.Credits : string.Empty,
                UniqueName = serie.UniqueName,
            };
        }
    }
}