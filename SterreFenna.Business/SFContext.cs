using SterreFenna.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SterreFenna.Business
{
    public class SFContext : DbContext
    {
        public SFContext()
            : base("SFContext")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
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
    }
}
