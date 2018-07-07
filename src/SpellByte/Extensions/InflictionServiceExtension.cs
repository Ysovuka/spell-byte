using SpellByte;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class InflictionServiceExtension
    {
        public static IServiceCollection AddInflictionService(this IServiceCollection services,
            string connectionString, bool debug = false)
            => services.AddTransient(serviceProdiver =>
                (debug) ? InflictionServiceFactory.CreateInMemoryService("InflictionService")
                        : InflictionServiceFactory.CreateSqlService(connectionString));
    }
}
