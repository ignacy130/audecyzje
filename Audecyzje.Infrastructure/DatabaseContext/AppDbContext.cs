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
        public virtual DbSet<Document> Documents { get; set; }
        public virtual DbSet<Decision> Decisions { get; set; }
        public virtual DbSet<Localization> Localizations { get; set; }
        public virtual DbSet<Person> Persons { get; set; }
    }
}
