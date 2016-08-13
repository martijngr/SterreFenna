using SterreFenna.Business.Naming;
using SterreFenna.Domain;
using SterreFenna.Domain.Projects;

namespace SterreFenna.Business.Projects.Commands
{
    public class CreateProjectCommand : BaseProjectCommand
    {
        public int ComittedProjectId { get; internal set; }
    }

    public class CreateProjectCommandHandler
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateProjectCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Handle(CreateProjectCommand command)
        {
            var project = new Project
            {
                Name = command.Name.Trim(),
                UniqueName = GetUniqueName(command.Name.Trim()),
                Rank = 0,
                Description = command.Description,
            };

            _unitOfWork.ProjectRepository.Add(project);
            _unitOfWork.SaveChanges();

            command.ComittedProjectId = project.Id;
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
