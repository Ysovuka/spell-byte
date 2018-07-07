using Microsoft.EntityFrameworkCore;

namespace SpellByte.Internal
{
    internal class InflictionDbContext : DbContext
    {
        public InflictionDbContext() { }
        public InflictionDbContext(DbContextOptions<InflictionDbContext> options) : base(options) { }

        public DbSet<Infliction> Inflictions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Infliction>().HasAlternateKey(d => d.Code);
        }
    }
}
