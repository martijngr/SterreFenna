using System;
using System.Linq;
using SterreFenna.Domain.Series;

namespace SterreFenna.EfDal.Series
{
    public class SerieRepository : BaseRepository<Serie>, ISerieRepository
    {
        private readonly SFContext _context;

        public SerieRepository(SFContext context) 
            : base(context)
        {
            _context = context;
        }

        public void Delete(Serie serie)
        {
            _context.SerieItems.RemoveRange(serie.SerieItems);
            _context.Series.Remove(serie);
        }

        public Serie GetByUniqueName(string uniqueName)
        {
            return _context.Series.FirstOrDefault(p => p.UniqueName == uniqueName);
        }
    }
}