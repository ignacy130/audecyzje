using Audecyzje.Core.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Audecyzje.Infrastructure
{
    public class WarsawContext : IdentityDbContext
    {
        public WarsawContext(DbContextOptions<WarsawContext> options) : base(options)
        {
        }
        public DbSet<Decision> Decisions { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Localization> Localizations { get; set; }
        public DbSet<DecisionTag> DecisionTags { get; set; }
		public DbSet<Post> Posts { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Decision>().ToTable("Decision");
            modelBuilder.Entity<Tag>().ToTable("Tag");
            modelBuilder.Entity<Localization>().ToTable("Localization");
            modelBuilder.Entity<DecisionTag>().ToTable("DecisionTag");
            modelBuilder.Entity<DecisionTag>().HasKey(x => new { x.DecisionId, x.TagId });
        }

    }
}
