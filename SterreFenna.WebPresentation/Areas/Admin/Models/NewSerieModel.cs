using SterreFenna.Business.Series.Views;
using System.Collections.Generic;

namespace SterreFenna.WebPresentation.Areas.Admin.Models
{
    public class NewSerieModel
    {
        public IEnumerable<ProjectListOverviewItem> ProjectList { get; set; }
        public int? SelectedProjectId { get; set; }
    }
}