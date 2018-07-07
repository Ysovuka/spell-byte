using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace SpellByte.Internal
{
    internal class AfflictionDbContextFactory : IDbContextFactory<AfflictionDbContext>
    {
        internal static AfflictionDbContext CreateDbContext(DbContextOptions<AfflictionDbContext> options)
            => new AfflictionDbContext(options);

        internal static DbContextOptions<AfflictionDbContext> CreateInMemoryOptions(string databaseName)
            => new DbContextOptionsBuilder<AfflictionDbContext>()
                .UseInMemoryDatabase(databaseName).Options;

        internal static DbContextOptions<AfflictionDbContext> CreateSqlOptions(string connectionString)
            => new DbContextOptionsBuilder<AfflictionDbContext>()
                .UseSqlServer(connectionString, b => b.MigrationsAssembly("SpellByte")).Options;

        public AfflictionDbContext Create(DbContextFactoryOptions options)
            => new AfflictionDbContext(new DbContextOptionsBuilder<AfflictionDbContext>()
                .UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDb;Initial Catalog=SpellByte.Migrations;Integrated Security=True",
                b => b.MigrationsAssembly("SpellByte")).Options);
    }
}
