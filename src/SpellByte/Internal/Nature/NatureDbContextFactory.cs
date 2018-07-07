using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace SpellByte.Internal
{
    internal class NatureDbContextFactory : IDbContextFactory<NatureDbContext>
    {
        internal static NatureDbContext CreateDbContext(DbContextOptions<NatureDbContext> options)
            => new NatureDbContext(options);

        internal static DbContextOptions<NatureDbContext> CreateInMemoryOptions(string databaseName)
            => new DbContextOptionsBuilder<NatureDbContext>()
                .UseInMemoryDatabase(databaseName).Options;

        internal static DbContextOptions<NatureDbContext> CreateSqlOptions(string connectionString)
            => new DbContextOptionsBuilder<NatureDbContext>()
                .UseSqlServer(connectionString, b => b.MigrationsAssembly("SpellByte")).Options;

        public NatureDbContext Create(DbContextFactoryOptions options)
            => new NatureDbContext(new DbContextOptionsBuilder<NatureDbContext>()
                .UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDb;Initial Catalog=SpellByte.Migrations;Integrated Security=True",
                b => b.MigrationsAssembly("SpellByte")).Options);
    }
}
