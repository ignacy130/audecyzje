using Audecyzje.WebQuickDemo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Audecyzje.WebQuickDemo.Data
{
    public class WarsawContext : DbContext
    {
        public WarsawContext(DbContextOptions<WarsawContext> options) : base(options)
        {
        }
        public DbSet<Decision> Descisions { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Localization> Localizations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Decision>().ToTable("Decision");
            modelBuilder.Entity<Tag>().ToTable("Tag");
            modelBuilder.Entity<Localization>().ToTable("Localization");
            modelBuilder.Entity<DecisionTag>().HasKey(x => new { x.DecisionID, x.TagID });
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=hostnseek.com;database=bene12_mjn;user=bene12_mjn;password=1qaz@WSX");
        }

    }
}
