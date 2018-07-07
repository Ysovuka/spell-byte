using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpellByte.Internal
{
    internal class DomainService : IDomainService
    {
        private readonly DbContextOptions<DomainDbContext> _options;
        public DomainService(DbContextOptions<DomainDbContext> options)
        {
            _options = options;

            using (DomainDbContext context = DomainDbContextFactory.CreateDbContext(options))
            {
                if (!(context.GetService<IDatabaseCreator>() as RelationalDatabaseCreator).Exists())
                    context.Database.Migrate();
                else if ((context.GetService<IDatabaseCreator>() as RelationalDatabaseCreator).Exists() 
                    && context.Database.GetPendingMigrations().Any())
                    context.Database.Migrate();
            }
        }

        public void Dispose() { }

        public async Task<bool> CreateDomainAsync(string name)
        {
            using (DomainDbContext context = DomainDbContextFactory.CreateDbContext(_options))
            {
                Domain domain = new Domain
                {
                    Name = name,
                };

                if (context.Set<Domain>().Any(d => d.Name == name)) return false;

                await context.AddAsync(domain);
                return Convert.ToBoolean(await context.SaveChangesAsync());
            }
        }

        public async Task<bool> DeleteDomainAsync(Guid id)
        {
            using (DomainDbContext context = DomainDbContextFactory.CreateDbContext(_options))
            {
                Domain domain = await context.Set<Domain>().FirstOrDefaultAsync(d => d.Id == id);
                if (domain == null) return false;

                context.Remove(domain);
                return Convert.ToBoolean(await context.SaveChangesAsync());
            }
        }

        public async Task<bool> EditDomainAsync(Guid id, string name)
        {
            using (DomainDbContext context = DomainDbContextFactory.CreateDbContext(_options))
            {
                Domain domain = await context.Set<Domain>().FirstOrDefaultAsync(d => d.Id == id);

                if (domain == null) return false;

                domain.Name = name;
                context.Update(domain);
                return Convert.ToBoolean(await context.SaveChangesAsync());
            }
        }

        public async Task<IEnumerable<Domain>> GetDomainsAsync()
        {
            using (DomainDbContext context = DomainDbContextFactory.CreateDbContext(_options))
            {
                return (await context.Set<Domain>().ToListAsync()).OrderBy(d => d.Name);
            }
        }

        public async Task<Domain> GetDomainByIdAsync(Guid id)
        {
            using (DomainDbContext context = DomainDbContextFactory.CreateDbContext(_options))
            {
                return await context.Set<Domain>().FirstOrDefaultAsync(d => d.Id == id);
            }
        }
    }
}
