using SterreFenna.Business.Series.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SterreFenna.Business.Projects.Views
{
    public class ProjectDetailsView
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string UniqueProjectName { get; set; }

        public string Description { get; set; }

        public DateTime? PublicationDate { get; set; }

        public IEnumerable<SerieDetailView> Series { get; set; }
    }
}
