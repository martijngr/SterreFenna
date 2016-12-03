using SterreFenna.Business.Settings;
using SterreFenna.Domain.Series;
using System.IO;

namespace SterreFenna.Business.Series
{
    public class SeriePathService
    {
        private readonly ISettings _settings;
        private int _serieId;
        private string _serieName;

        public SeriePathService(ISettings settings, int serieId, string serieName)
        {
            _settings = settings;
            _serieId = serieId;
            _serieName = serieName;
        }
        
        private string SerieDirName
        {
            get { return $"{_serieId}_{_serieName}"; }
        }

        public string GetSerieItemPath(string itemName)
        {
            var seriePath = GetSerieAbsoluteBasePath();

            return Path.Combine(seriePath, itemName);
        }

        public string GetRelativeItemPath(string itemName)
        {
            return Path.Combine("/Series", SerieDirName, itemName);
        }

        public void CreateSerieDirectory()
        {
            var seriePath = GetSerieAbsoluteBasePath();
            if (Directory.Exists(seriePath))
                return;

            Directory.CreateDirectory(seriePath);
        }

        public void DeleteSerieDirectory(Serie serie)
        {
            var serieDir = GetSerieAbsoluteBasePath();
            if (Directory.Exists(serieDir))
            {
                Directory.Delete(serieDir, true);
            }
        }

        public string GetSerieAbsoluteBasePath()
        {
            return Path.Combine("/", _settings.SeriePath, SerieDirName);
        }
    }
}
