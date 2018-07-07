using Microsoft.EntityFrameworkCore;

namespace SpellByte.Internal
{
    internal class DomainDbContext : DbContext
    {
        public DomainDbContext() { }
        public DomainDbContext(DbContextOptions<DomainDbContext> options) : base(options) { }

        public DbSet<Domain> Domains { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain>().HasAlternateKey(d => d.Code);
        }
    }
}
