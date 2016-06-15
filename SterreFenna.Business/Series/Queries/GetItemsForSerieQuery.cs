using SterreFenna.Business.Projects;
using SterreFenna.Business.Series.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SterreFenna.Business.Series.Queries
{
    public class GetItemsForSerieQuery
    {
        private readonly SFContext _context;

        public GetItemsForSerieQuery(SFContext context)
        {
            _context = context;
        }

        public string SerieName { get; set; }

        public string ProjectName { get; set; }

        public SerieDetailView Handle()
        {
            SerieDetailView view = null;

            if (ProjectName.HasValue())
                view = LoadSerieByProject();
            else
                view = LoadSerie();
            
            return view;
        }

        private SerieDetailView LoadSerie()
        {
            var serie = _context.Series.First(s => s.UniqueName == SerieName);
            if (serie == null)
                throw new SerieNotFoundException();

            return new SerieDetailView
            {
                Created = serie.Created,
                Id = serie.Id,
                Name = serie.Name,
                Published = serie.Published,
                SerieItems = from i in serie.SerieItems
                             select new SerieItemDetailView
                             {
                                 Created = i.Created,
                                 Id = i.Id,
                                 Location = i.Location,
                                 Rank = i.Rank
                             }
            };
        }

        private SerieDetailView LoadSerieByProject()
        {
            var project = _context.Projects.FirstOrDefault(p => p.UniqueName == ProjectName);
            if (project == null)
                throw new ProjectNotFoundException();

            var serie = project.Series.First(s => s.UniqueName == SerieName);
            if (serie == null)
                throw new SerieNotFoundException();

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
                             }
            };
        }
    }
}