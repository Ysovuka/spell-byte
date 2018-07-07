using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace SpellByte.Internal
{
    internal class TempoDbContextFactory : IDbContextFactory<TempoDbContext>
    {
        internal static TempoDbContext CreateDbContext(DbContextOptions<TempoDbContext> options)
            => new TempoDbContext(options);

        internal static DbContextOptions<TempoDbContext> CreateInMemoryOptions(string databaseName)
            => new DbContextOptionsBuilder<TempoDbContext>()
                .UseInMemoryDatabase(databaseName).Options;

        internal static DbContextOptions<TempoDbContext> CreateSqlOptions(string connectionString)
            => new DbContextOptionsBuilder<TempoDbContext>()
                .UseSqlServer(connectionString, b => b.MigrationsAssembly("SpellByte")).Options;

        public TempoDbContext Create(DbContextFactoryOptions options)
            => new TempoDbContext(new DbContextOptionsBuilder<TempoDbContext>()
                .UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDb;Initial Catalog=SpellByte.Migrations;Integrated Security=True",
                b => b.MigrationsAssembly("SpellByte")).Options);
    }
}
