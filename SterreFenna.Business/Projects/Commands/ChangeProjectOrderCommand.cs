using SterreFenna.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SterreFenna.Business.Projects.Commands
{
    public class ChangeProjectOrderCommand
    {
        private readonly IUnitOfWork _unitOfWork;

        public ChangeProjectOrderCommand(IUnitOfWork unitOfWor0k)
        {
            _unitOfWork = unitOfWor0k;
        }

        public int[] ProjectIds { get; set; }

        public void Handle()
        {
            var rankCounter = 0;

            foreach (var projectId in ProjectIds)
            {
                var project = _unitOfWork.ProjectRepository.GetById(projectId);

                project.Rank = rankCounter;

                rankCounter++;
            }

            _unitOfWork.SaveChanges();
        }
    }
}