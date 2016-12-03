using SterreFenna.Business.Settings;

namespace SterreFenna.Business.Series
{
    public class SeriePathServiceFactory
    {
        private readonly ISettings _settings;

        public SeriePathServiceFactory(ISettings settings)
        {
            _settings = settings;
        }

        public SeriePathService CreateSeriePathResolver(int serieId, string serieName)
        {
            return new SeriePathService(_settings, serieId, serieName);
        }
    }
}