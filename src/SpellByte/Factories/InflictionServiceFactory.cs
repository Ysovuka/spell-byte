using SpellByte.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpellByte
{
    public static class InflictionServiceFactory
    {
        public static IInflictionService CreateInMemoryService(string databaseName)
            => new InflictionService(InflictionDbContextFactory.CreateInMemoryOptions(databaseName));

        public static IInflictionService CreateSqlService(string connectionString)
            => new InflictionService(InflictionDbContextFactory.CreateSqlOptions(connectionString));
    }
}
