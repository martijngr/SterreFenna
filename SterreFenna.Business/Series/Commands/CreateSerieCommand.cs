using SterreFenna.Domain;
using SterreFenna.Domain.Projects;
using SterreFenna.Domain.Series;
using System;
using System.Collections.Generic;

namespace SterreFenna.Business.Series.Commands
{
    public class CreateSerieCommand : BaseSerieCommand
    {
        private readonly SeriePathManagerFactory _seriePathResolverFactory;
        private readonly AddItemsToSerieCommand _addItemsToSerieCommand;
        private readonly ISerieRepository _serieRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateSerieCommand(
            SeriePathManagerFactory seriePathResolverFactory, 
            AddItemsToSerieCommand addItemsToSerieCommand, 
            ISerieRepository serieRepository,
            IUnitOfWork unitOfWork)
            : base(addItemsToSerieCommand)
        {
            _seriePathResolverFactory = seriePathResolverFactory;
            _addItemsToSerieCommand = addItemsToSerieCommand;

            _serieRepository = serieRepository;
            _unitOfWork = unitOfWork;
        }
        
        public int StoredSerieId { get; private set; }

        public void Handle()
        {
            var serie = CreateSerie();
            SaveSerie(serie);

            var seriePathResolver = _seriePathResolverFactory.CreateSeriePathResolver(serie.Id, SerieName);
            seriePathResolver.CreateSerieDirectory();

            StoreImages(serie);

            StoredSerieId = serie.Id;

            _unitOfWork.SaveChanges();
        }

        private void SaveSerie(Serie serie)
        {
            _unitOfWork.SerieRepository.Add(serie);
            _unitOfWork.SaveChanges();
        }

        private Serie CreateSerie()
        {
            var project = GetProject();

            return new Serie
            {
                Created = DateTime.Now,
                Name = SerieName,
                UniqueName = SerieName.ReplaceSpaces(),
                Published = PublicationDate,
                Project = project,
            };
        }

        private Project GetProject()
        {
            if (ProjectId > 0)
                return _unitOfWork.ProjectRepository.GetById(ProjectId);
            else
            {
                return new Project
                {
                    Name = ProjectName,
                };
            }
        }
    }
}