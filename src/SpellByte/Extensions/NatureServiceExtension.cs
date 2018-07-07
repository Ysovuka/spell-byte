using SpellByte;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class NatureServiceExtension
    {
        public static IServiceCollection AddNatureService(this IServiceCollection services,
            string connectionString, bool debug = false)
            => services.AddTransient(serviceProdiver =>
                (debug) ? NatureServiceFactory.CreateInMemoryService("NatureService")
                        : NatureServiceFactory.CreateSqlService(connectionString));
    }
}
