using SterreFenna.Business.Naming;
using SterreFenna.Domain;
using SterreFenna.Domain.Projects;
using SterreFenna.Domain.Series;
using System;
using System.IO;
using System.Linq;

namespace SterreFenna.Business.Series.Commands
{
    public class EditSerieCommand : BaseSerieCommand
    {
        public int SerieId { get; set; }
    }

    public class EditSerieCommandHandler 
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly AddItemsToSerieCommand _addItemsToSerieCommand;

        public EditSerieCommandHandler(AddItemsToSerieCommand addItemsToSerieCommand, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _addItemsToSerieCommand = addItemsToSerieCommand;
        }
        
        public void Handle(EditSerieCommand command)
        {
            var serie = _unitOfWork.SerieRepository.GetById(command.SerieId);
            
            UpdateSerie(serie, command);

            EditProject(serie, command);

            _addItemsToSerieCommand.SerieItems = command.SerieItems;
            _addItemsToSerieCommand.Handle(serie);
            
            MarkImagesAsFavourite(serie, command);

            _unitOfWork.SaveChanges();
        }

        private void UpdateSerie(Serie serie, EditSerieCommand command)
        {
            serie.Published = command.PublicationDate;
            serie.Credits = command.Credits;

            if (!serie.Name.Equals(command.SerieName))
            {
                serie.Name = GetUniqueName(command.SerieName);
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

        private string GetUniqueName(string name)
        {
            name = InvalidCharsRemover.RemoveInvalidChars(name);

            var counter = 1;
            while (_unitOfWork.SerieRepository.Any(s => s.UniqueName == name))
            {
                name += $"{name}-{counter}";
            }

            return name;
        }
    }
}