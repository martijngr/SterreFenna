using SterreFenna.Business.Settings;
using SterreFenna.Domain;
using SterreFenna.Domain.SerieItems;
using SterreFenna.Domain.Series;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SterreFenna.Business.Series.Commands
{
    public class AddItemsToSerieCommand
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISettings _settings;
        private readonly SeriePathServiceFactory _seriePathResolverFactory;
        private SeriePathService _seriePathResolver;

        public AddItemsToSerieCommand(
            ISettings settings,
            SeriePathServiceFactory seriePathResolverFactory,
            IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _settings = settings;
            _seriePathResolverFactory = seriePathResolverFactory;
        }

        public List<UploadedSerieItem> SerieItems { get; set; }

        public void Handle(Serie serie)
        {
            _seriePathResolver = _seriePathResolverFactory.CreateSeriePathResolver(serie.Id, serie.Name);

            RemoveExistingSerieItems(serie);

            StoreItemsOnDisk(serie.Id, serie.Name);

            StoreItemsInDatabase(serie);

            _unitOfWork.SaveChanges();
        }

        private void RemoveExistingSerieItems(Serie serie)
        {
            // Remove files from disk
            var itemsToRemove = serie.SerieItems.Where(MustDeleteItem);

            // Remove from database
            if (itemsToRemove.Any())
            {
                RemoveItemsFromDataStore(itemsToRemove);

                RemoveItemsFromDisk(serie, itemsToRemove);
            }
        }

        private void RemoveItemsFromDataStore(IEnumerable<SerieItem> itemsToRemove)
        {
            _unitOfWork.SerieItemRepository.RemoveRange(itemsToRemove);
        }

        private void RemoveItemsFromDisk(Serie serie, IEnumerable<SerieItem> itemsToRemove)
        {
            foreach (var itemToRemove in itemsToRemove)
            {
                var filePath = _seriePathResolver.GetSerieItemPath(itemToRemove.FileName);

                if (File.Exists(filePath))
                    File.Delete(filePath);
            }
        }

        private void StoreItemsOnDisk(int serieId, string serieName)
        {
            foreach (var item in SerieItems)
            {
                if (item.Stream == null)
                    continue;

                StoreItemOnDisk(item);
            }
        }

        private void StoreItemOnDisk(UploadedSerieItem item)
        {
            var itemPath = _seriePathResolver.GetSerieItemPath(item.Filename);
            using (var fileStream = File.Create(itemPath))
            {
                item.Stream.Seek(0, SeekOrigin.Begin);
                item.Stream.CopyTo(fileStream);
            }
        }

        private void StoreItemsInDatabase(Serie serie)
        {
            var serieItemsToPersist = new List<SerieItem>();

            foreach (var item in SerieItems)
            {
                var existingItem = GetSerieItemByFileName(serie, item);

                if (existingItem == null)
                    serieItemsToPersist.Add(CreateSerieItem(serie, item));
                else
                    existingItem.Rank = item.Rank;
            }

            _unitOfWork.SerieItemRepository.Add(serieItemsToPersist);
        }

        private SerieItem CreateSerieItem(Serie serie, UploadedSerieItem item)
        {
            var itemPath = _seriePathResolver.GetRelativeItemPath(item.Filename);

            return new SerieItem
            {
                Created = DateTime.Now,
                Location = itemPath,
                SerieId = serie.Id,
                FileName = item.Filename,
                Rank = item.Rank
            };
        }

        private static SerieItem GetSerieItemByFileName(Serie serie, UploadedSerieItem item)
        {
            return serie.SerieItems.FirstOrDefault(s => s.FileName.IsSameAs(item.Filename));
        }

        private bool MustDeleteItem(SerieItem serieItem)
        {
            // The specified serie item also exists in the new list of serie items, so deletion is not necessarily
            if (SerieItems.Any(si => si.Filename == serieItem.FileName))
                return false;

            return true;
        }
    }
}
