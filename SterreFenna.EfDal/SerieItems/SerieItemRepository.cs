using SterreFenna.Domain.SerieItems;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SterreFenna.EfDal.SerieItems
{
    public class SerieItemRepository : BaseRepository<SerieItem>, ISerieItemRepository
    {
        public SerieItemRepository(SFContext context) 
            : base(context)
        {
        }
    }
}