using SterreFenna.Domain.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

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
    }
}