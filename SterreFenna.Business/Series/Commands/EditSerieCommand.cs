using SterreFenna.Domain;
using SterreFenna.Domain.Series;
using System.Collections.Generic;
using System.Linq;
using System;
using SterreFenna.Domain.Projects;

namespace SterreFenna.Business.Series.Commands
{
    public class EditSerieCommand : BaseSerieCommand
    {
        private readonly IUnitOfWork _unitOfWork;

        public EditSerieCommand(AddItemsToSerieCommand addItemsToSerieCommand, IUnitOfWork unitOfWork)
            : base(addItemsToSerieCommand)
        {
            _unitOfWork = unitOfWork;
            FavouriteItems = new List<string>();
        }

        public int SerieId { get; set; }

        public List<string> FavouriteItems { get; set; }

        public void Handle()
        {
            var serie = _unitOfWork.SerieRepository.GetById(SerieId);

            serie.Name = SerieName;
            serie.Published = PublicationDate;

            EditProject(serie);

            StoreImages(serie);

            MarkImagesAsFavourite(serie);
        }

        private void EditProject(Serie serie)
        {
            if (serie.ProjectId == ProjectId)
                return;

            if (ProjectId.HasValue)
                serie.ProjectId = ProjectId;
            else
            {
                serie.Project = new Project
                {
                    Name = ProjectName,
                };
            }
        }

        private void MarkImagesAsFavourite(Serie serie)
        {
            if (!FavouriteItems.Any())
                return;

            var itemsToMark = serie.SerieItems.Where(s => FavouriteItems.Contains(s.FileName)).ToList();
            var itemsToUnmark = serie.SerieItems.Except(itemsToMark).ToList();

            itemsToMark.ForEach(s => s.IsHomePageItem = true);
            itemsToUnmark.ForEach(s => s.IsHomePageItem = false);

            _unitOfWork.SaveChanges();
        }
    }
}