using SterreFenna.Domain;

namespace SterreFenna.Business.Series.Commands
{
    public class DeleteSerieCommand
    {
        public int Id { get; set; }
    }

    public class DeleteSerieCommandHandler
    {
        private readonly SeriePathServiceFactory _seriePathResolverFactory;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteSerieCommandHandler(SeriePathServiceFactory seriePathResolverFactory, IUnitOfWork unitOfWork)
        {
            _seriePathResolverFactory = seriePathResolverFactory;
            _unitOfWork = unitOfWork;
        }

        public void Handle(DeleteSerieCommand command)
        {
            var serie = _unitOfWork.SerieRepository.GetById(command.Id);
            var pathResolver = _seriePathResolverFactory.CreateSeriePathResolver(command.Id, serie.Name);

            pathResolver.DeleteSerieDirectory(serie);

            _unitOfWork.SerieRepository.Delete(serie);
            _unitOfWork.SaveChanges();
        }
    }
}