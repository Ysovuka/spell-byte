using Microsoft.EntityFrameworkCore;

namespace SpellByte.Internal
{
    internal class EffectDbContext : DbContext
    {
        public EffectDbContext() { }
        public EffectDbContext(DbContextOptions<EffectDbContext> options) : base(options) { }

        public DbSet<Effect> Effects { get; set; }
        public DbSet<EffectMap> EffectMaps { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Effect>().HasAlternateKey(d => d.Code);
        }
    }
}
