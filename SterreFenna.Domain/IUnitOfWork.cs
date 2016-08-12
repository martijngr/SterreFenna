using SterreFenna.Domain.Contacts;
using SterreFenna.Domain.Projects;
using SterreFenna.Domain.SerieItems;
using SterreFenna.Domain.Series;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SterreFenna.Domain
{
    public interface IUnitOfWork
    {
        ISerieRepository SerieRepository { get; }
        IProjectRepository ProjectRepository { get; }
        ISerieItemRepository SerieItemRepository { get; }
        IContactRepository ContactRepository { get; }

        int SaveChanges();
    }
}
