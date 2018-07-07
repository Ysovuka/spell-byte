using SpellByte;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class AfflictionServiceExtension
    {
        public static IServiceCollection AddAfflictionService(this IServiceCollection services,
            string connectionString, bool debug = false)
            => services.AddTransient(serviceProdiver =>
                (debug) ? AfflictionServiceFactory.CreateInMemoryService("AfflictionService")
                        : AfflictionServiceFactory.CreateSqlService(connectionString));
    }
}
