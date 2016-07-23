using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace SterreFenna.WebPresentation.UrlBuilders
{
    public class UrlBuilder
    {
        public static string Build(string uniqueProjectName, string uniqueSerieName)
        {
            var url = new StringBuilder();
            url.Append("/");

            if (uniqueProjectName.HasValue())
                url.Append($"{uniqueProjectName}/");

            url.Append(uniqueSerieName);

            return url.ToString();
        }
    }
}