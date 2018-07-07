using SpellByte.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpellByte
{
    public static class NatureServiceFactory
    {
        public static INatureService CreateInMemoryService(string databaseName)
            => new NatureService(NatureDbContextFactory.CreateInMemoryOptions(databaseName));

        public static INatureService CreateSqlService(string connectionString)
            => new NatureService(NatureDbContextFactory.CreateSqlOptions(connectionString));
    }
}
