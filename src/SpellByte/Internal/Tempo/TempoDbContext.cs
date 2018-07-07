using Microsoft.EntityFrameworkCore;

namespace SpellByte.Internal
{
    internal class TempoDbContext : DbContext
    {
        public TempoDbContext() { }
        public TempoDbContext(DbContextOptions<TempoDbContext> options) : base(options) { }

        public DbSet<Tempo> Tempos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tempo>().HasAlternateKey(d => d.Code);
        }
    }
}
