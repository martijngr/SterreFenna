using System.Linq;

namespace SterreFenna.Business.Projects.Commands
{
    public class EditProjectCommand : BaseProjectCommand
    {
        public int ProjectId { get; set; }
    }

    public class EditProjectCommandHandler
    {
        private readonly SFContext _context;

        public EditProjectCommandHandler(SFContext context)
        {
            _context = context;
        }

        public void Handle(EditProjectCommand command)
        {
            var project = _context.Projects.First(p => p.Id == command.ProjectId);

            project.Name = command.Name;
            project.PublicationDate = command.PublicationDate;

            _context.SaveChanges();
        }
    }
}
