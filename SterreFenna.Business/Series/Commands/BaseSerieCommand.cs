using System;
using System.Collections.Generic;

namespace SterreFenna.Business.Series.Commands
{
    public class BaseSerieCommand
    {
        public BaseSerieCommand()
        {
            FavouriteItems = new List<string>();
        }

        public string SerieName { get; set; }

        public string ProjectName { get; set; }

        public int ProjectId { get; set; }

        public DateTime? PublicationDate { get; set; }

        public string Credits { get; set; }

        public List<UploadedSerieItem> SerieItems { get; set; }

        public List<string> FavouriteItems { get; set; }
    }
}