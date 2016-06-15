using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SterreFenna.Business.Series.Views
{
    public class SerieDetailView
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string UniqueName { get; set; }

        public DateTime Created { get; set; }

        public DateTime? Published { get; set; }

        public IEnumerable<SerieItemDetailView> SerieItems {get;set;}

        public int? ProjectId { get; set; }

        public string ProjectName { get; set; }
    }
}
