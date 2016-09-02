using SterreFenna.Business.Projects.Views;
using SterreFenna.Business.Series.Views;
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
                UniqueProjectName = project.UniqueName,
                Description = project.Description,
                Series = from s in project.Series
                         select new SerieDetailView
                         {
                             Created = s.Created,
                             Credits = s.Credits,
                             Id = s.Id,
                             Name = s.Name,
                             ProjectId = project.Id,
                             ProjectName = project.Name,
                             Published = s.Published,
                             UniqueName= s.UniqueName,
                         }
            };

            return view;
        }
    }
}