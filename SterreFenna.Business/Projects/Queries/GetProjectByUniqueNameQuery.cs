using SterreFenna.Business.Projects.Views;
using SterreFenna.Domain;

namespace SterreFenna.Business.Projects.Queries
{
    public class GetProjectByUniqueNameQuery
    {
        public string UniqueName { get; set; }
    }

    public class GetProjectByUniqueNameQueryHandler
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetProjectByUniqueNameQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ProjectDetailsView Handle(GetProjectByUniqueNameQuery query)
        {
            var project = _unitOfWork.ProjectRepository.GetByUniqueName(query.UniqueName);

            if (project == null)
                return null;

            var view = new ProjectDetailsView
            {
                Id = project.Id,
                Name = project.Name,
                UniqueProjectName = project.UniqueName,
                Description = project.Description
            };

            return view;
        }
    }
}