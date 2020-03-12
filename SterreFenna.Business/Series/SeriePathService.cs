using SterreFenna.Business.Settings;
using SterreFenna.Domain.Series;
using System;
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
            if (serieId < 1)
                throw new ArgumentException($"{nameof(serieId)} must be greater than 0");
            if(!serieName.HasValue())
                throw new ArgumentException($"{nameof(serieName)} must have a value");

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
            var seriePath = GetAbsoluteSerieBasePath();

            return Path.Combine(seriePath, itemName);
        }

        public string GetRelativeItemPath(string itemName)
        {
            return Path.Combine("/Series", SerieDirName, itemName);
        }

        public void CreateSerieDirectory()
        {
            var seriePath = GetAbsoluteSerieBasePath();
            if (Directory.Exists(seriePath))
                return;

            Directory.CreateDirectory(seriePath);
        }

        public void RenameSerieDirectory(string newName)
        {
            var currentAbsolutePath = GetAbsoluteSerieBasePath();
            var newDirName = $"{_serieId}_{newName}";
            var newAbsolutePath = Path.Combine("/", _settings.SeriePath, newDirName);

            Directory.Move(currentAbsolutePath, newAbsolutePath);
        }

        public void DeleteSerieDirectory(Serie serie)
        {
            var serieDir = GetAbsoluteSerieBasePath();
            if (Directory.Exists(serieDir))
            {
                Directory.Delete(serieDir, true);
            }
        }

        public string GetAbsoluteSerieBasePath()
        {
            return Path.Combine("/", _settings.SeriePath, SerieDirName);
        }
    }
}
