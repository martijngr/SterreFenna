using SterreFenna.Business.Naming;
using SterreFenna.Domain;
using SterreFenna.Domain.Series;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SterreFenna.Business.Series.Commands
{
    public class RenameSerieNameCommand
    {
        public int SerieId { get; set; }

        public string NewSerieName { get; set; }
    }

    public class RenameSerieNameCommandHandler
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly SeriePathServiceFactory _seriePathServiceFactory;

        public RenameSerieNameCommandHandler(IUnitOfWork unitOfWork, SeriePathServiceFactory seriePathServiceFactory)
        {
            _unitOfWork = unitOfWork;
            _seriePathServiceFactory = seriePathServiceFactory;
        }

        public void Handle(RenameSerieNameCommand command)
        {
            var serie = _unitOfWork.SerieRepository.GetById(command.SerieId);

            if (!serie.Name.Equals(command.NewSerieName))
            {
                RenameSerieFolderOnDisk(serie.Id, serie.Name, command.NewSerieName);

                RenameSerieInDatabase(serie, command);
            }
        }

        private void RenameSerieFolderOnDisk(int serieId, string currentSerieName, string newSerieName)
        {
            var pathService = _seriePathServiceFactory.CreateSeriePathService(serieId, currentSerieName);
            pathService.RenameSerieDirectory(newSerieName);
        }

        private void RenameSerieInDatabase(Serie serie, RenameSerieNameCommand command)
        {
            serie.Name = command.NewSerieName;
            serie.UniqueName = GetUniqueName(command.NewSerieName);
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
