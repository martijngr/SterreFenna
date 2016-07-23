using System;

namespace SterreFenna.Business.Series.Views
{
    public class SerieItemDetailView
    {
        public int Id { get; set; }

        public int Rank { get; set; }

        public string Location { get; set; }

        public DateTime Created { get; set; }

        public bool IsLandingPageItem { get; set; }
    }
}
