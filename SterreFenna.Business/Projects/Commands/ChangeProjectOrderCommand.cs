using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SterreFenna.Business.Projects.Commands
{
    public class ChangeProjectOrderCommand
    {
        private readonly SFContext _context;

        public ChangeProjectOrderCommand(SFContext context)
        {
            _context = context;
        }

        public int[] ProjectIds { get; set; }

        public void Handle()
        {
            var rankCounter = 0;

            foreach (var projectId in ProjectIds)
            {
                var project = _context.Projects.First(p => p.Id == projectId);

                project.Rank = rankCounter;

                rankCounter++;
            }

            _context.SaveChanges();
        }
    }
}