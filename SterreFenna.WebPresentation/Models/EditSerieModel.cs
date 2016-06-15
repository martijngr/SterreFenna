using SterreFenna.Business.Projects.Views;
using SterreFenna.Business.Series.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SterreFenna.WebPresentation.Models
{
    public class EditSerieModel
    {
        public SerieDetailView SerieDetails { get; set; }

        public IEnumerable<ProjectOverviewItem> Projects { get; set; }
    }
}