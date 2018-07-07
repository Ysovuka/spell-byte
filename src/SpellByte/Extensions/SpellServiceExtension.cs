using SpellByte;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class SpellServiceExtension
    {
        public static IServiceCollection AddSpellService(this IServiceCollection services,
            string connectionString, bool debug = false)
            => services.AddTransient(serviceProdiver =>
                (debug) ? SpellServiceFactory.CreateInMemoryService("SpellService")
                        : SpellServiceFactory.CreateSqlService(connectionString));
    }
}
