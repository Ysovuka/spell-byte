using SpellByte.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpellByte
{
    public static class EffectServiceFactory
    {
        public static IEffectService CreateInMemoryService(string databaseName)
            => new EffectService(EffectDbContextFactory.CreateInMemoryOptions(databaseName));

        public static IEffectService CreateSqlService(string connectionString)
            => new EffectService(EffectDbContextFactory.CreateSqlOptions(connectionString));
    }
}
