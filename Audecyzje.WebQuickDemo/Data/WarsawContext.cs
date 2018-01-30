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
        public DbSet<DecisionTag> DecisionTags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Decision>().ToTable("Decision");
            modelBuilder.Entity<Tag>().ToTable("Tag");
            modelBuilder.Entity<Localization>().ToTable("Localization");
            modelBuilder.Entity<DecisionTag>().ToTable("DecisionTag");
            modelBuilder.Entity<DecisionTag>().HasKey(x => new { x.DecisionID, x.TagID });
        }

    }
}
