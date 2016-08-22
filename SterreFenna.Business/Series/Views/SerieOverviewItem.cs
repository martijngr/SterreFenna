using System;

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

        public int TotalMarkedItems { get; set; }

        public int TotalItems { get; set; }
    }
}
