using SterreFenna.Domain;
using SterreFenna.Domain.Projects;
using System;

namespace SterreFenna.Business.Projects.Commands
{
    public class AddProjectCommand : BaseProjectCommand
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddProjectCommand(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Handle()
        {
            var project = new Project
            {
                Name = Name,
                UniqueName = Name.ReplaceSpaces(),
                Rank = 0,
            };

            _unitOfWork.ProjectRepository.Add(project);
            _unitOfWork.SaveChanges();
        }
    }
}
