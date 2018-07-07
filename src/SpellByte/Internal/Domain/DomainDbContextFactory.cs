using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace SpellByte.Internal
{
    internal class DomainDbContextFactory : IDbContextFactory<DomainDbContext>
    {
        internal static DomainDbContext CreateDbContext(DbContextOptions<DomainDbContext> options)
            => new DomainDbContext(options);

        internal static DbContextOptions<DomainDbContext> CreateInMemoryOptions(string databaseName)
            => new DbContextOptionsBuilder<DomainDbContext>()
                .UseInMemoryDatabase(databaseName).Options;

        internal static DbContextOptions<DomainDbContext> CreateSqlOptions(string connectionString)
            => new DbContextOptionsBuilder<DomainDbContext>()
                .UseSqlServer(connectionString, b => b.MigrationsAssembly("SpellByte")).Options;

        public DomainDbContext Create(DbContextFactoryOptions options)
            => new DomainDbContext(new DbContextOptionsBuilder<DomainDbContext>()
                .UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDb;Initial Catalog=SpellByte.Migrations;Integrated Security=True",
                b => b.MigrationsAssembly("SpellByte")).Options);
    }
}
