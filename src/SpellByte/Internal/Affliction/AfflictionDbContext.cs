using Microsoft.EntityFrameworkCore;

namespace SpellByte.Internal
{
    internal class AfflictionDbContext : DbContext
    {
        public AfflictionDbContext() { }
        public AfflictionDbContext(DbContextOptions<AfflictionDbContext> options) : base(options) { }

        public DbSet<Affliction> Afflictions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Affliction>().HasAlternateKey(d => d.Code);
        }
    }
}
