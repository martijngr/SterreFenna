using SterreFenna.Business.Series.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SterreFenna.Business.Projects.Views
{
    public class ProjectOverviewItem
    {
        public ProjectOverviewItem()
        {
            SerieItems = Enumerable.Empty<SerieOverviewItem>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string UniqueName { get; set; }

        public int Rank { get; set; }

        public DateTime? PublicationDate { get; set; }

        public int TotalSeries { get; set; }

        public IEnumerable<SerieOverviewItem> SerieItems { get; set; }
    }
}
