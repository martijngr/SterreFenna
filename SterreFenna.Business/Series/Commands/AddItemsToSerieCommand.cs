using SterreFenna.Business.Series;
using SterreFenna.Business.Settings;
using SterreFenna.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SterreFenna.Business.Series.Commands
{
    public class AddItemsToSerieCommand
    {
        private readonly SFContext _context;
        private readonly ISettings _settings;
        private readonly SeriePathManager _seriePathManager;

        public AddItemsToSerieCommand(ISettings settings, SeriePathManager galleryPathManager, SFContext context)
        {
            _context = context;
            _settings = settings;
            _seriePathManager = galleryPathManager;
        }

        public List<UploadedSerieItem> SerieItems { get; set; }

        public void Handle(Serie serie)
        {
            RemoveExistingSerieItems(serie);

            StoreItemsOnDisk(serie.Id, serie.Name);

            StoreItemsInDatabase(serie);
        }

        private void RemoveExistingSerieItems(Serie serie)
        {
            // Remove files from disk
            var removedItems = serie.SerieItems.Where(i => !SerieItems.Any(si => i.FileName == si.Filename));

            // Remove from database
            if (removedItems.Any())
            {
                _context.SerieItems.RemoveRange(removedItems);
                _context.SaveChanges();
            }
        }

        private void StoreItemsOnDisk(int serieId, string serieName)
        {
            foreach (var item in SerieItems)
            {
                if (item.Stream == null)
                    continue;

                var itemPath = _seriePathManager.GetSerieItemPath(serieId, serieName, item.Filename);

                using (var fileStream = File.Create(itemPath))
                {
                    item.Stream.Seek(0, SeekOrigin.Begin);
                    item.Stream.CopyTo(fileStream);
                }
            }
        }

        private void StoreItemsInDatabase(Serie serie)
        {
            var serieItemsToPersist = new List<SerieItem>();

            foreach (var item in SerieItems)
            {
                var existingItem = serie.SerieItems.FirstOrDefault(s => s.FileName.Equals(item.Filename, StringComparison.OrdinalIgnoreCase));

                if (existingItem == null)
                {
                    var itemPath = _seriePathManager.GetRelativeItemPath(serie.Id, serie.Name, item.Filename);

                    serieItemsToPersist.Add(new SerieItem
                    {
                        Created = DateTime.Now,
                        Location = itemPath,
                        SerieId = serie.Id,
                        FileName = item.Filename,
                        Rank = item.Rank
                    });
                }
                else
                {
                    existingItem.Rank = item.Rank;
                }
            }

            _context.SerieItems.AddRange(serieItemsToPersist);
            _context.SaveChanges();
        }
    }
}
