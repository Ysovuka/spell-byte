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
    internal class TempoService : ITempoService
    {
        private readonly DbContextOptions<TempoDbContext> _options;
        public TempoService(DbContextOptions<TempoDbContext> options)
        {
            _options = options;

            using (TempoDbContext context = TempoDbContextFactory.CreateDbContext(options))
            {
                if (!(context.GetService<IDatabaseCreator>() as RelationalDatabaseCreator).Exists())
                    context.Database.Migrate();
                else if ((context.GetService<IDatabaseCreator>() as RelationalDatabaseCreator).Exists()
                    && context.Database.GetPendingMigrations().Any())
                    context.Database.Migrate();
            }
        }

        public void Dispose() { }

        public async Task<bool> CreateTempoAsync(int value, Timing timing)
        {
            using (TempoDbContext context = TempoDbContextFactory.CreateDbContext(_options))
            {
                Tempo tempo = new Tempo
                {
                    Timing = timing,
                    Value = value,
                };

                if (context.Set<Tempo>().Any(t => t.Value == value && t.Timing == timing)) return false;

                await context.AddAsync(tempo);
                return Convert.ToBoolean(await context.SaveChangesAsync());
            }
        }

        public async Task<bool> DeleteTempoAsync(Guid id)
        {
            using (TempoDbContext context = TempoDbContextFactory.CreateDbContext(_options))
            {
                Tempo tempo = await context.Set<Tempo>().FirstOrDefaultAsync(t => t.Id == id);
                if (tempo == null) return false;
                context.Remove(tempo);
                return Convert.ToBoolean(await context.SaveChangesAsync());
            }
        }

        public async Task<bool> EditTempoAsync(Guid id, int value, Timing timing)
        {
            using (TempoDbContext context = TempoDbContextFactory.CreateDbContext(_options))
            {
                Tempo tempo = await context.Set<Tempo>().FirstOrDefaultAsync(t => t.Id == id);
                if (tempo == null) return false;

                tempo.Value = value;
                tempo.Timing = timing;
                context.Update(tempo);
                return Convert.ToBoolean(await context.SaveChangesAsync());
            }
        }

        public async Task<IEnumerable<Tempo>> GetTemposAsync()
        {
            using (TempoDbContext context = TempoDbContextFactory.CreateDbContext(_options))
            {
                return (await context.Set<Tempo>().ToListAsync()).OrderBy(t => t.Timing).ThenBy(t => t.Value);
            }
        }

        public async Task<Tempo> GetTempoByIdAsync(Guid id)
        {
            using (TempoDbContext context = TempoDbContextFactory.CreateDbContext(_options))
            {
                return await context.Set<Tempo>().FirstOrDefaultAsync(t => t.Id == id);
            }
        }
    }
}
