using SterreFenna.Domain;
using SterreFenna.Domain.Projects;
using SterreFenna.Domain.Series;
using SterreFenna.EfDal.Projects;
using SterreFenna.EfDal.Series;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SterreFenna.Domain.SerieItems;
using SterreFenna.EfDal.SerieItems;
using SterreFenna.Domain.Contacts;
using SterreFenna.EfDal.ContactPages;

namespace SterreFenna.EfDal
{
    public class EfUnitOfWork : IUnitOfWork
    {
        private readonly SFContext _context;

        public EfUnitOfWork()
        {
            _context = new SFContext();

            SerieRepository = new SerieRepository(_context);
            ProjectRepository = new ProjectRepository(_context);
            SerieItemRepository = new SerieItemRepository(_context);
            ContactRepository = new ContactRepository(_context);
        }

        public ISerieRepository SerieRepository { get; }
        public IProjectRepository ProjectRepository { get; }
        public ISerieItemRepository SerieItemRepository { get; }
        public IContactRepository ContactRepository { get; }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}