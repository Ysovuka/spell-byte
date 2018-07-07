using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace SpellByte.Internal
{
    internal class EffectDbContextFactory : IDbContextFactory<EffectDbContext>
    {
        internal static EffectDbContext CreateDbContext(DbContextOptions<EffectDbContext> options)
            => new EffectDbContext(options);

        internal static DbContextOptions<EffectDbContext> CreateInMemoryOptions(string databaseName)
            => new DbContextOptionsBuilder<EffectDbContext>()
                .UseInMemoryDatabase(databaseName).Options;

        internal static DbContextOptions<EffectDbContext> CreateSqlOptions(string connectionString)
            => new DbContextOptionsBuilder<EffectDbContext>()
                .UseSqlServer(connectionString, b => b.MigrationsAssembly("SpellByte")).Options;

        public EffectDbContext Create(DbContextFactoryOptions options)
            => new EffectDbContext(new DbContextOptionsBuilder<EffectDbContext>()
                .UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDb;Initial Catalog=SpellByte.Migrations;Integrated Security=True",
                b => b.MigrationsAssembly("SpellByte")).Options);
    }
}
