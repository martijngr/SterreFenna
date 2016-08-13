using SterreFenna.Business.Naming;
using SterreFenna.Business.Projects.Commands;
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
        private readonly CreateProjectCommandHandler _addProjectCommandHandler;

        public CreateSerieCommandHandler(
            SeriePathManagerFactory seriePathResolverFactory, 
            AddItemsToSerieCommand addItemsToSerieCommand, 
            ISerieRepository serieRepository,
            IUnitOfWork unitOfWork,
            CreateProjectCommandHandler addProjectCommandHandler)
        {
            _seriePathResolverFactory = seriePathResolverFactory;
            _addItemsToSerieCommand = addItemsToSerieCommand;

            _serieRepository = serieRepository;
            _unitOfWork = unitOfWork;
            _addProjectCommandHandler = addProjectCommandHandler;
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
            var project = GetProjectId(command);

            return new Serie
            {
                Created = DateTime.Now,
                Name = command.SerieName.Trim(),
                UniqueName = GetUniqueName(command.SerieName.Trim()),
                Published = command.PublicationDate,
                ProjectId = project,
                Credits = command.Credits
            };
        }

        private int GetProjectId(CreateSerieCommand command)
        {
            if (command.ProjectId > 0)
                return command.ProjectId;
            else
            {
                var newProjectCommand = new CreateProjectCommand { Name = command.ProjectName };
                _addProjectCommandHandler.Handle(newProjectCommand);

                return newProjectCommand.ComittedProjectId;
            }
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