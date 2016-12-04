using SterreFenna.Domain.Projects;
using System.Linq;

namespace SterreFenna.EfDal.Projects
{
    public class ProjectRepository : BaseRepository<Project>, IProjectRepository
    {
        private readonly SFContext _context;

        public ProjectRepository(SFContext context) 
            : base(context)
        {
            _context = context;
        }

        public Project GetByUniqueName(string uniqueName)
        {
            return _context.Projects.FirstOrDefault(p => p.UniqueName == uniqueName);
        }

        public void Delete(Project project)
        {
            _context.Projects.Remove(project);
        }
    }
}