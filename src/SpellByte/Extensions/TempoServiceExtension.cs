using SpellByte;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class TempoServiceExtension
    {
        public static IServiceCollection AddTempoService(this IServiceCollection services,
            string connectionString, bool debug = false)
                => services.AddTransient(serviceProdiver =>
                    (debug) ? TempoServiceFactory.CreateInMemoryService("TempoService")
                            : TempoServiceFactory.CreateSqlService(connectionString));
    }
}
