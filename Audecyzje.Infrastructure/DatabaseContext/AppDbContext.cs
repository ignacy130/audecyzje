using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Audecyzje.Core.Domain;

namespace Audecyzje.Infrastructure.DatabaseContext
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public virtual DbSet<Decision> Decisions { get; set; }
		public DbSet<Tag> Tags { get; set; }
		public virtual DbSet<Localization> Localizations { get; set; }
		public DbSet<DecisionTag> DecisionTags { get; set; }
		public virtual DbSet<Person> Persons { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Decision>().ToTable("Decision");
			modelBuilder.Entity<Tag>().ToTable("Tag");
			modelBuilder.Entity<Localization>().ToTable("Localization");
			modelBuilder.Entity<DecisionTag>().ToTable("DecisionTag");
			modelBuilder.Entity<DecisionTag>().HasKey(x => new { x.DecisionId, x.TagId });
		}
	}
}
