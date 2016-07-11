using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SterreFenna.Domain.Series
{
    public interface ISerieRepository
    {
        void Add(Serie serie);

        Serie GetById(int id);

        Serie GetByUniqueName(string uniqueName);

        IEnumerable<Serie> GetAll();
    }
}
