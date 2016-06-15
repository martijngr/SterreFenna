using SterreFenna.Business.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SterreFenna.Business.Series
{
    public class SeriePathManager
    {
        private readonly ISettings _settings;

        public SeriePathManager(ISettings settings)
        {
            _settings = settings;
        }

        public string GetSeriePath(int galleryId, string galleryName)
        {
            return $"{_settings.SeriePath}\\{galleryId}_{galleryName}";
        }

        public string GetSerieItemPath(int serieId, string serieName,  string itemName)
        {
            var seriePath = GetSeriePath(serieId, serieName);

            return $"{seriePath}\\{itemName}";
        }

        public string GetRelativeItemPath(int serieId, string serieName, string itemName)
        {
            return $@"/Series/{serieId}_{serieName}/{itemName}";
        }

        public void CreateSerieDirectory(int serieId, string serieName)
        {
            var seriePath = GetSeriePath(serieId, serieName);
            Directory.CreateDirectory(seriePath);
        }
    }
}
