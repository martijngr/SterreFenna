using SterreFenna.Business.Projects.Views;
using SterreFenna.Domain;
using System.Linq;

namespace SterreFenna.Business.Projects.Queries
{
    public class GetProjectByIdQuery
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetProjectByIdQuery(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public int ProjectId { get; set; }

        public ProjectDetailsView Handle()
        {
            var project = _unitOfWork.ProjectRepository.GetById(ProjectId);

            var view = new ProjectDetailsView
            {
                Id = project.Id,
                Name = project.Name,
            };

            return view;
        }
    }
}