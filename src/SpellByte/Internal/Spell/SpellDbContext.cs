using Microsoft.EntityFrameworkCore;

namespace SpellByte.Internal
{
    internal class SpellDbContext : DbContext
    {
        public SpellDbContext() { }
        public SpellDbContext(DbContextOptions<SpellDbContext> options) : base(options) { }

        public DbSet<Spell> Spells { get; set; }
        public DbSet<SpellMap> SpellMaps { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Spell>().HasAlternateKey(d => d.Code);
        }
    }
}
