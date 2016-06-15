using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SterreFenna.Business.Series.Views
{
    public class SerieOverviewItem
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string UniqueName { get; set; }

        public DateTime Created { get; set; }

        public int Rank { get; set; }

        public DateTime? Published { get; set; }

        public string ImageLocation { get; set; }
    }
}
