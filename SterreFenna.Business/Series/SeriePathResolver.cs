using SterreFenna.Business.Settings;
using System.IO;

namespace SterreFenna.Business.Series
{
    public class SeriePathResolver
    {
        private readonly ISettings _settings;
        private int _serieId;
        private string _serieName;

        public SeriePathResolver(ISettings settings, int serieId, string serieName)
        {
            _settings = settings;
            _serieId = serieId;
            _serieName = serieName;
        }
        
        private string SerieDir
        {
            get { return $"{_serieId}_{_serieName}"; }
        }

        public string GetSerieItemPath(string itemName)
        {
            var seriePath = GetSerieBasePath();

            return Path.Combine(seriePath, itemName);
        }

        public string GetRelativeItemPath(string itemName)
        {
            return Path.Combine("/Series", SerieDir, itemName);
        }

        public void CreateSerieDirectory()
        {
            var seriePath = GetSerieBasePath();
            if (Directory.Exists(seriePath))
                return;

            Directory.CreateDirectory(seriePath);
        }

        public string GetSerieBasePath()
        {
            return Path.Combine("/", _settings.SeriePath, SerieDir);
        }
    }
}
