using SterreFenna.Business.Series.Views;
using SterreFenna.Domain;
using System.Collections.Generic;
using System.Linq;

namespace SterreFenna.Business.Series.Queries
{
    public class GetProjectListOverviewQuery
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetProjectListOverviewQuery(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<ProjectListOverviewItem> Handle()
        {
            var view = (from g in _unitOfWork.ProjectRepository.GetAll()
                        select new ProjectListOverviewItem
                        {
                            Id = g.Id,
                            Name = g.Name
                        }).ToList();

            return view;
        }
    }
}