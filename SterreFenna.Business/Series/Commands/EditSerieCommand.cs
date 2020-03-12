using SterreFenna.Business.Naming;
using SterreFenna.Domain;
using SterreFenna.Domain.Projects;
using SterreFenna.Domain.Series;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SterreFenna.Business.Series.Commands
{
    public class EditSerieCommand : BaseSerieCommand
    {
        public EditSerieCommand()
        {
            FileIdsToDelete = Enumerable.Empty<int>();
        }

        public int SerieId { get; set; }

        public IEnumerable<int> FileIdsToDelete { get; set; }
    }

    public class EditSerieCommandHandler 
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly AddItemsToSerieCommand _addItemsToSerieCommand;
        private readonly RenameSerieNameCommandHandler _renameSerieNameCommandHandler;

        public EditSerieCommandHandler(
            AddItemsToSerieCommand addItemsToSerieCommand, 
            RenameSerieNameCommandHandler renameSerieNameCommandHandler,
            IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _addItemsToSerieCommand = addItemsToSerieCommand;
            _renameSerieNameCommandHandler = renameSerieNameCommandHandler;
        }
        
        public void Handle(EditSerieCommand command)
        {
            var serie = _unitOfWork.SerieRepository.GetById(command.SerieId);
            
            UpdateSerie(serie, command);

            EditProject(serie, command);

            RemoveSerieItems(command.FileIdsToDelete);

            _addItemsToSerieCommand.SerieItems = command.SerieItems;
            _addItemsToSerieCommand.Handle(serie);
            
            MarkImagesAsFavourite(serie, command);

            _unitOfWork.SaveChanges();
        }

        private void RemoveSerieItems(IEnumerable<int> fileIdsToDelete)
        {
            foreach (var serieItemId in fileIdsToDelete)
            {
                var serieItem = _unitOfWork.SerieItemRepository.GetById(serieItemId);

                _unitOfWork.SerieItemRepository.Remove(serieItem);
            }
        }

        private void UpdateSerie(Serie serie, EditSerieCommand command)
        {
            serie.Published = command.PublicationDate;
            serie.Credits = command.Credits;

            if (!serie.Name.Equals(command.SerieName))
            {
                var renameSerieCommand = new RenameSerieNameCommand
                {
                    NewSerieName = command.SerieName,
                    SerieId = serie.Id
                };

                _renameSerieNameCommandHandler.Handle(renameSerieCommand);
            }
        }

        private void EditProject(Serie serie, EditSerieCommand command)
        {
            if (serie.ProjectId == command.ProjectId)
                return;

            if (command.ProjectId > 0)
                serie.ProjectId = command.ProjectId;

            throw new Exception("No project found for serie");
        }

        private void MarkImagesAsFavourite(Serie serie, EditSerieCommand command)
        {
            if (!command.FavouriteItems.Any())
                return;

            var fileNames = command.FavouriteItems.Select(f => Path.GetFileName(f)).ToList();
            var itemsToMark = serie.SerieItems.Where(s => fileNames.Contains(s.FileName)).ToList();
            var itemsToUnmark = serie.SerieItems.Except(itemsToMark).ToList();

            itemsToMark.ForEach(s => s.IsHomePageItem = true);
            itemsToUnmark.ForEach(s => s.IsHomePageItem = false);

        }
    }
}