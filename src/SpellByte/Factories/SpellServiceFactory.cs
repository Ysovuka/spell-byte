using SpellByte.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpellByte
{
    public static class SpellServiceFactory
    {
        public static ISpellService CreateInMemoryService(string databaseName)
            => new SpellService(SpellDbContextFactory.CreateInMemoryOptions(databaseName));

        public static ISpellService CreateSqlService(string connectionString)
            => new SpellService(SpellDbContextFactory.CreateSqlOptions(connectionString));
    }
}
