using SterreFenna.Business.Projects.Views;
using System.Linq;

namespace SterreFenna.Business.Projects.Queries
{
    public class GetProjectByIdQuery
    {
        private readonly SFContext _context;

        public GetProjectByIdQuery(SFContext context)
        {
            _context = context;
        }

        public int ProjectId { get; set; }

        public ProjectDetailsView Handle()
        {
            var project = _context.Projects.First(p => p.Id == ProjectId);

            var view = new ProjectDetailsView
            {
                Id = project.Id,
                Name = project.Name,
                PublicationDate = project.PublicationDate,
            };

            return view;
        }
    }
}