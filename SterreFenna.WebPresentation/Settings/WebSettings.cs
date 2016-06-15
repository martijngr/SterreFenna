using SterreFenna.Business.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SterreFenna.WebPresentation.Settings
{
    public class WebSettings : ISettings
    {
        public string SeriePath
        {
            get
            {
                return HttpContext.Current.Server.MapPath("~/Series");
            }
        }
    }
}