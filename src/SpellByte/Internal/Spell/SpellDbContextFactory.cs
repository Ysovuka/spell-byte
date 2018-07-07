using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace SpellByte.Internal
{
    internal class SpellDbContextFactory : IDbContextFactory<SpellDbContext>
    {
        internal static SpellDbContext CreateDbContext(DbContextOptions<SpellDbContext> options)
            => new SpellDbContext(options);

        internal static DbContextOptions<SpellDbContext> CreateInMemoryOptions(string databaseName)
            => new DbContextOptionsBuilder<SpellDbContext>()
                .UseInMemoryDatabase(databaseName).Options;

        internal static DbContextOptions<SpellDbContext> CreateSqlOptions(string connectionString)
            => new DbContextOptionsBuilder<SpellDbContext>()
                .UseSqlServer(connectionString, b => b.MigrationsAssembly("SpellByte")).Options;

        public SpellDbContext Create(DbContextFactoryOptions options)
            => new SpellDbContext(new DbContextOptionsBuilder<SpellDbContext>()
                .UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDb;Initial Catalog=SpellByte.Migrations;Integrated Security=True",
                b => b.MigrationsAssembly("SpellByte")).Options);
    }
}
