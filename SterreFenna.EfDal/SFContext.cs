using Microsoft.AspNet.Identity.EntityFramework;
using SterreFenna.Domain.Contacts;
using SterreFenna.Domain.Projects;
using SterreFenna.Domain.SerieItems;
using SterreFenna.Domain.Series;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace SterreFenna.EfDal
{
    public class SFContext : IdentityDbContext<IdentityUser>
    {
        public SFContext() : base("SFContext", throwIfV1Schema: false)
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            //one-to-many 
            modelBuilder.Entity<SerieItem>()
                        .HasRequired<Serie>(s => s.Serie)
                        .WithMany(s => s.SerieItems);

            modelBuilder.Entity<Serie>()
                        .HasOptional<Project>(s => s.Project)
                        .WithMany(s => s.Series);
        }

        public DbSet<Serie> Series { get; set; }

        public DbSet<SerieItem> SerieItems { get; set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<Contact> Contacts { get; set; }
    }
}
