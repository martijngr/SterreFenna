using SterreFenna.Domain.Projects;
using SterreFenna.Domain.SerieItems;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SterreFenna.Domain.Series
{
    public class Serie :IIdentifyable
    {
        public Serie()
        {
            SerieItems = new Collection<SerieItem>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string UniqueName { get; set; }

        public DateTime Created { get; set; }

        public DateTime? Published { get; set; }

        public int? ProjectId { get; set; }

        public Project Project { get; set; }

        public virtual ICollection<SerieItem> SerieItems { get; set; }
    }
}
