using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpellByte.Internal
{
    internal class NatureService : INatureService
    {
        private readonly DbContextOptions<NatureDbContext> _options;
        public NatureService(DbContextOptions<NatureDbContext> options)
        {
            _options = options;

            using (NatureDbContext context = NatureDbContextFactory.CreateDbContext(options))
            {
                if (!(context.GetService<IDatabaseCreator>() as RelationalDatabaseCreator).Exists())
                    context.Database.Migrate();
                else if ((context.GetService<IDatabaseCreator>() as RelationalDatabaseCreator).Exists()
                    && context.Database.GetPendingMigrations().Any())
                    context.Database.Migrate();
            }
        }

        public void Dispose() { }

        public async Task<bool> CreateNatureAsync(string name)
        {
            using (NatureDbContext context = NatureDbContextFactory.CreateDbContext(_options))
            {
                Nature Nature = new Nature
                {
                    Name = name,
                };
                if (context.Set<Nature>().Any(n => n.Name == name)) return false;

                await context.AddAsync(Nature);
                return Convert.ToBoolean(await context.SaveChangesAsync());
            }
        }

        public async Task<bool> DeleteNatureAsync(Guid id)
        {
            using (NatureDbContext context = NatureDbContextFactory.CreateDbContext(_options))
            {
                Nature Nature = await context.Set<Nature>().FirstOrDefaultAsync(n => n.Id == id);
                if (Nature == null) return false;

                context.Remove(Nature);
                return Convert.ToBoolean(await context.SaveChangesAsync());
            }
        }

        public async Task<bool> EditNatureAsync(Guid id, string name)
        {
            using (NatureDbContext context = NatureDbContextFactory.CreateDbContext(_options))
            {
                Nature Nature = await context.Set<Nature>().FirstOrDefaultAsync(n => n.Id == id);
                if (Nature == null) return false;

                Nature.Name = name;
                context.Update(Nature);
                return Convert.ToBoolean(await context.SaveChangesAsync());
            }
        }

        public async Task<IEnumerable<Nature>> GetNaturesAsync()
        {
            using (NatureDbContext context = NatureDbContextFactory.CreateDbContext(_options))
            {
                return (await context.Set<Nature>().ToListAsync()).OrderBy(n => n.Name);
            }
        }

        public async Task<Nature> GetNatureByIdAsync(Guid id)
        {
            using (NatureDbContext context = NatureDbContextFactory.CreateDbContext(_options))
            {
                return await context.Set<Nature>().FirstOrDefaultAsync(n => n.Id == id);
            }
        }
    }
}
