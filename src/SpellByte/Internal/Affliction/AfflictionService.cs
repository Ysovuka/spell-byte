using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpellByte.Internal
{
    internal class AfflictionService : IAfflictionService
    {
        private readonly DbContextOptions<AfflictionDbContext> _options;
        public AfflictionService(DbContextOptions<AfflictionDbContext> options)
        {
            _options = options;

            using (AfflictionDbContext context = AfflictionDbContextFactory.CreateDbContext(options))
            {
                if (!(context.GetService<IDatabaseCreator>() as RelationalDatabaseCreator).Exists())
                    context.Database.Migrate();
                else if ((context.GetService<IDatabaseCreator>() as RelationalDatabaseCreator).Exists()
                    && context.Database.GetPendingMigrations().Any())
                    context.Database.Migrate();
            }
        }

        public void Dispose() { }

        public async Task<bool> CreateAfflictionAsync(string name)
        {
            using (AfflictionDbContext context = AfflictionDbContextFactory.CreateDbContext(_options))
            {
                Affliction Affliction = new Affliction
                {
                    Name = name,
                };
                if (context.Set<Affliction>().Any(n => n.Name == name)) return false;

                await context.AddAsync(Affliction);
                return Convert.ToBoolean(await context.SaveChangesAsync());
            }
        }

        public async Task<bool> DeleteAfflictionAsync(Guid id)
        {
            using (AfflictionDbContext context = AfflictionDbContextFactory.CreateDbContext(_options))
            {
                Affliction Affliction = await context.Set<Affliction>().FirstOrDefaultAsync(n => n.Id == id);
                if (Affliction == null) return false;

                context.Remove(Affliction);
                return Convert.ToBoolean(await context.SaveChangesAsync());
            }
        }

        public async Task<bool> EditAfflictionAsync(Guid id, string name)
        {
            using (AfflictionDbContext context = AfflictionDbContextFactory.CreateDbContext(_options))
            {
                Affliction Affliction = await context.Set<Affliction>().FirstOrDefaultAsync(n => n.Id == id);
                if (Affliction == null) return false;

                Affliction.Name = name;
                context.Update(Affliction);
                return Convert.ToBoolean(await context.SaveChangesAsync());
            }
        }

        public async Task<IEnumerable<Affliction>> GetAfflictionsAsync()
        {
            using (AfflictionDbContext context = AfflictionDbContextFactory.CreateDbContext(_options))
            {
                return (await context.Set<Affliction>().ToListAsync()).OrderBy(n => n.Name);
            }
        }

        public async Task<Affliction> GetAfflictionByIdAsync(Guid id)
        {
            using (AfflictionDbContext context = AfflictionDbContextFactory.CreateDbContext(_options))
            {
                return await context.Set<Affliction>().FirstOrDefaultAsync(n => n.Id == id);
            }
        }
    }
}
