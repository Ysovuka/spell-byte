using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace SpellByte.Internal
{
    internal class InflictionDbContextFactory : IDbContextFactory<InflictionDbContext>
    {
        internal static InflictionDbContext CreateDbContext(DbContextOptions<InflictionDbContext> options)
            => new InflictionDbContext(options);

        internal static DbContextOptions<InflictionDbContext> CreateInMemoryOptions(string databaseName)
            => new DbContextOptionsBuilder<InflictionDbContext>()
                .UseInMemoryDatabase(databaseName).Options;

        internal static DbContextOptions<InflictionDbContext> CreateSqlOptions(string connectionString)
            => new DbContextOptionsBuilder<InflictionDbContext>()
                .UseSqlServer(connectionString, b => b.MigrationsAssembly("SpellByte")).Options;

        public InflictionDbContext Create(DbContextFactoryOptions options)
            => new InflictionDbContext(new DbContextOptionsBuilder<InflictionDbContext>()
                .UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDb;Initial Catalog=SpellByte.Migrations;Integrated Security=True",
                b => b.MigrationsAssembly("SpellByte")).Options);
    }
}
