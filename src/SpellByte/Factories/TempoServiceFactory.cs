using SpellByte.Internal;

namespace SpellByte
{
    public static class TempoServiceFactory
    {
        public static ITempoService CreateInMemoryService(string databaseName)
            => new TempoService(TempoDbContextFactory.CreateInMemoryOptions(databaseName));

        public static ITempoService CreateSqlService(string connectionString)
            => new TempoService(TempoDbContextFactory.CreateSqlOptions(connectionString));
    }
}
