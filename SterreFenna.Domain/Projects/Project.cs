using SterreFenna.Domain.Series;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SterreFenna.Domain.Projects
{
    public class Project : IIdentifyable
    {
        public Project()
        {
            Series = new Collection<Serie>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string UniqueName { get; set; }

        public int Rank { get; set; }

        public virtual ICollection<Serie> Series { get; set; }
    }
}
