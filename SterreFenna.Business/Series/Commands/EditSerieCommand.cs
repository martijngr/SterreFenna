using SterreFenna.Domain;
using System.Collections.Generic;
using System.Linq;

namespace SterreFenna.Business.Series.Commands
{
    public class EditSerieCommand : BaseSerieCommand
    {
        private readonly SFContext _context;

        public EditSerieCommand(AddItemsToSerieCommand addItemsToSerieCommand, SFContext context)
            : base(addItemsToSerieCommand, context)
        {
            _context = context;
            FavouriteItems = new List<string>();
        }

        public int SerieId { get; set; }

        public List<string> FavouriteItems { get; set; }

        public void Handle()
        {
            var serie = _context.Series.First(s => s.Id == SerieId);

            serie.Name = SerieName;
            serie.Published = PublicationDate;
            serie.ProjectId = GetProjectId();
            
            StoreImages(serie);

            MarkImagesAsFavourite(serie);
        }

        private void MarkImagesAsFavourite(Serie serie)
        {
            if (!FavouriteItems.Any())
                return;

            var itemsToMark = serie.SerieItems.Where(s => FavouriteItems.Contains(s.FileName)).ToList();
            var itemsToUnmark = serie.SerieItems.Except(itemsToMark).ToList();

            itemsToMark.ForEach(s => s.IsHomePageItem = true);
            itemsToUnmark.ForEach(s => s.IsHomePageItem = false);

            _context.SaveChanges();
        }
    }
}