using SpellByte.Internal;

namespace SpellByte
{
    public static class DomainServiceFactory
    {
        public static IDomainService CreateInMemoryService(string databaseName)
            => new DomainService(DomainDbContextFactory.CreateInMemoryOptions(databaseName));

        public static IDomainService CreateSqlService(string connectionString)
            => new DomainService(DomainDbContextFactory.CreateSqlOptions(connectionString));
    }
}
