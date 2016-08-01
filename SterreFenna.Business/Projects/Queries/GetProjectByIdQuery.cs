using SterreFenna.Business.Projects.Views;
using SterreFenna.Domain;
using System.Linq;

namespace SterreFenna.Business.Projects.Queries
{
    public class GetProjectByIdQuery
    {
        public int ProjectId { get; set; }
    }

    public class GetProjectByIdQueryHandler
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetProjectByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ProjectDetailsView Handle(GetProjectByIdQuery query)
        {
            var project = _unitOfWork.ProjectRepository.GetById(query.ProjectId);

            var view = new ProjectDetailsView
            {
                Id = project.Id,
                Name = project.Name,
                UniqueName = project.UniqueName,
                Description = project.Description
            };

            return view;
        }
    }
}