using SterreFenna.Domain.Series;
using System;

namespace SterreFenna.Domain.SerieItems
{
    public class SerieItem : IIdentifyable
    {
        public int Id { get; set; }

        public string Location { get; set; }

        public DateTime Created { get; set; }

        public int Rank { get; set; }

        public string FileName { get; set; }

        public int SerieId { get; set; }

        public virtual Serie Serie { get; set; }

        public bool IsHomePageItem { get; set; }
    }
}
