using SterreFenna.Domain;
using System;

namespace SterreFenna.Business.Projects.Commands
{
    public class AddProjectCommand : BaseProjectCommand
    {
        private readonly SFContext _context;

        public AddProjectCommand(SFContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var project = new Project
            {
                Name = Name,
                UniqueName = Name.ReplaceSpaces(),
                PublicationDate = PublicationDate,
                Rank = 0,
            };

            _context.Projects.Add(project);
            _context.SaveChanges();
        }
    }
}
