using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SterreFenna.Domain.SerieItems
{
    public interface ISerieItemRepository
    {
        void Add(SerieItem project);

        SerieItem GetById(int id);

        IEnumerable<SerieItem> Find(Expression<Func<SerieItem, bool>> predicate);

        IEnumerable<SerieItem> GetAll();

        void RemoveRange(IEnumerable<SerieItem> serieItems);

        void Add(IEnumerable<SerieItem> serieItems);
    }
}