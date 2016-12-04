using SterreFenna.Business.Series.Commands;
using SterreFenna.Domain;
using SterreFenna.Domain.Projects;
using System.Linq;

namespace SterreFenna.Business.Projects.Commands
{
    public class DeleteProjectCommand
    {
        public int Id { get; set; }
    }

    public class DeleteProjectCommandHandler
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly DeleteSerieCommandHandler _deleteSerieHandler;

        public DeleteProjectCommandHandler(
            IUnitOfWork unitOfWork,
            DeleteSerieCommandHandler deleteSerieHandler)
        {
            _unitOfWork = unitOfWork;
            _deleteSerieHandler = deleteSerieHandler;
        }

        public void Handle(DeleteProjectCommand command)
        {
            var project = _unitOfWork.ProjectRepository.GetById(command.Id);

            DeleteSeries(project);

            _unitOfWork.ProjectRepository.Delete(project);
            _unitOfWork.SaveChanges();
        }

        private void DeleteSeries(Project project)
        {
            foreach (var serie in project.Series.ToList())
            {
                _deleteSerieHandler.Handle(new DeleteSerieCommand { Id = serie.Id });
            }
        }
    }
}
