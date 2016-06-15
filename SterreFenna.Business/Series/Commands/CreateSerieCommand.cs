using SterreFenna.Domain;
using System;
using System.Collections.Generic;

namespace SterreFenna.Business.Series.Commands
{
    public class CreateSerieCommand : BaseSerieCommand
    {
        private readonly SFContext _context;
        private readonly SeriePathManager _seriePathManager;
        private readonly AddItemsToSerieCommand _addItemsToSerieCommand;

        public CreateSerieCommand(SeriePathManager galleryPathManager, AddItemsToSerieCommand addItemsToSerieCommand, SFContext context)
            : base(addItemsToSerieCommand, context)
        {
            _context = context;
            _seriePathManager = galleryPathManager;
            _addItemsToSerieCommand = addItemsToSerieCommand;
        }
        
        public int StoredSerieId { get; private set; }

        public void Handle()
        {
            var projectId = GetProjectId();
            var serie = CreateSerie(projectId);

            _seriePathManager.CreateSerieDirectory(serie.Id, SerieName);

            StoreImages(serie);

            StoredSerieId = serie.Id;
        }

        private Serie CreateSerie(int? projectId)
        {
            var serie = new Serie
            {
                Created = DateTime.Now,
                Name = SerieName,
                UniqueName = SerieName.ReplaceSpaces(),
                Published = PublicationDate,
                ProjectId = projectId
            };

            _context.Series.Add(serie);
            _context.SaveChanges();

            return serie;
        }
    }
}