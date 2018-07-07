using Microsoft.EntityFrameworkCore;

namespace SpellByte.Internal
{
    internal class NatureDbContext : DbContext
    {
        public NatureDbContext() { }
        public NatureDbContext(DbContextOptions<NatureDbContext> options) : base(options) { }

        public DbSet<Nature> Natures { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Nature>().HasAlternateKey(d => d.Code);
        }
    }
}
