using SterreFenna.Domain;

namespace SterreFenna.Business.Projects.Commands
{
    public class EditProjectCommand : BaseProjectCommand
    {
        public int ProjectId { get; set; }
    }

    public class EditProjectCommandHandler
    {
        private readonly IUnitOfWork _unitOfWork;

        public EditProjectCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Handle(EditProjectCommand command)
        {
            var project = _unitOfWork.ProjectRepository.GetById(command.ProjectId);

            project.Name = command.Name;
            project.Description = command.Description;

            _unitOfWork.SaveChanges();
        }
    }
}