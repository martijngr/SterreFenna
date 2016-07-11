using SterreFenna.Business.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SterreFenna.Business.Series
{
    public class SeriePathManagerFactory
    {
        private readonly ISettings _settings;

        public SeriePathManagerFactory(ISettings settings)
        {
            _settings = settings;
        }

        public SeriePathResolver CreateSeriePathResolver(int serieId, string serieName)
        {
            return new SeriePathResolver(_settings, serieId, serieName);
        }
    }
}
