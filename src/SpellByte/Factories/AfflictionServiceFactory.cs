using SpellByte.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpellByte
{
    public static class AfflictionServiceFactory
    {
        public static IAfflictionService CreateInMemoryService(string databaseName)
            => new AfflictionService(AfflictionDbContextFactory.CreateInMemoryOptions(databaseName));

        public static IAfflictionService CreateSqlService(string connectionString)
            => new AfflictionService(AfflictionDbContextFactory.CreateSqlOptions(connectionString));
    }
}
