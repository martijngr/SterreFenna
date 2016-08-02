using SterreFenna.Business.Projects.Views;
using SterreFenna.Business.Series.Views;
using System.Collections.Generic;
using System.Linq;

namespace SterreFenna.WebPresentation.Models
{
    public class LandingPageModel
    {
        public LandingPageModel()
        {
            LandingPageItems = new List<LandingPageItem>();
        }

        public List<LandingPageItem> LandingPageItems { get; set; }

        public string HomepageUrl { get; set; }

        public static LandingPageModel Create(IEnumerable<LandingpageItemView> items, ProjectDetailsView project)
        {
            var model = new LandingPageModel();

            foreach (var item in items)
            {
                model.LandingPageItems.Add(new LandingPageItem
                {
                    Location = item.Location,
                    Url = UrlBuilders.UrlBuilder.Build(item.UniqueProjectName, item.UniqueSerieName),
                });
            }

            model.HomepageUrl = $"/{project.UniqueProjectName}";

            return model;
        }
    }
}