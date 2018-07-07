using SpellByte;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DomainServiceExtension
    {
        public static IServiceCollection AddDomainService(this IServiceCollection services,
            string connectionString, bool debug = false)
            => services.AddTransient(serviceProdiver =>
                (debug) ? DomainServiceFactory.CreateInMemoryService("DomainService")
                        : DomainServiceFactory.CreateSqlService(connectionString));
    }
}
