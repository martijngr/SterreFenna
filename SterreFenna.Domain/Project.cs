using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SterreFenna.Domain
{
    public class Project
    {
        public Project()
        {
            Series = new Collection<Serie>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string UniqueName { get; set; }

        public int Rank { get; set; }

        public DateTime? PublicationDate { get; set; }

        public virtual ICollection<Serie> Series { get; set; }
    }
}
