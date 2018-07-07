using SpellByte;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class EffectServiceExtension
    {
        public static IServiceCollection AddEffectService(this IServiceCollection services,
            string connectionString, bool debug = false)
            => services.AddTransient(serviceProdiver =>
                (debug) ? EffectServiceFactory.CreateInMemoryService("EffectService")
                        : EffectServiceFactory.CreateSqlService(connectionString));
    }
}
