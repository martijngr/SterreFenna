using SterreFenna.Business.Naming;
using SterreFenna.Domain;
using SterreFenna.Domain.Projects;
using SterreFenna.Domain.Series;
using System;

namespace SterreFenna.Business.Series.Commands
{
    public class CreateSerieCommand : BaseSerieCommand
    {
        public int StoredSerieId { get; internal set; }
    }

    public class CreateSerieCommandHandler
    {
        private readonly SeriePathManagerFactory _seriePathResolverFactory;
        private readonly AddItemsToSerieCommand _addItemsToSerieCommand;
        private readonly ISerieRepository _serieRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateSerieCommandHandler(
            SeriePathManagerFactory seriePathResolverFactory, 
            AddItemsToSerieCommand addItemsToSerieCommand, 
            ISerieRepository serieRepository,
            IUnitOfWork unitOfWork)
        {
            _seriePathResolverFactory = seriePathResolverFactory;
            _addItemsToSerieCommand = addItemsToSerieCommand;

            _serieRepository = serieRepository;
            _unitOfWork = unitOfWork;
        }
        
        public void Handle(CreateSerieCommand command)
        {
            var serie = CreateSerie(command);
            SaveSerie(serie);

            var seriePathResolver = _seriePathResolverFactory.CreateSeriePathResolver(serie.Id, command.SerieName);
            seriePathResolver.CreateSerieDirectory();

            _addItemsToSerieCommand.SerieItems = command.SerieItems;
            _addItemsToSerieCommand.Handle(serie);

            command.StoredSerieId = serie.Id;

            _unitOfWork.SaveChanges();
        }

        private void SaveSerie(Serie serie)
        {
            _unitOfWork.SerieRepository.Add(serie);
            _unitOfWork.SaveChanges();
        }

        private Serie CreateSerie(CreateSerieCommand command)
        {
            var project = GetProject(command);

            return new Serie
            {
                Created = DateTime.Now,
                Name = command.SerieName,
                UniqueName = GetUniqueName(command.SerieName),
                Published = command.PublicationDate,
                Project = project,
                Credits = command.Credits
            };
        }

        private Project GetProject(CreateSerieCommand command)
        {
            if (command.ProjectId > 0)
                return _unitOfWork.ProjectRepository.GetById(command.ProjectId);
            else
            {
                return new Project
                {
                    Name = command.ProjectName,
                };
            }
        }

        private string GetUniqueName(string name)
        {
            name = InvalidCharsRemover.RemoveInvalidChars(name);

            var counter = 1;
            while (_unitOfWork.ProjectRepository.Any(s => s.UniqueName == name))
            {
                name += $"{name}-{counter}";
            }

            return name;
        }
    }
}