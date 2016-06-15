using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SterreFenna.Business.Series.Views
{
    public class SerieItemDetailView
    {
        public int Id { get; set; }

        public int Rank { get; set; }

        public string Location { get; set; }

        public DateTime Created { get; set; }
    }
}
