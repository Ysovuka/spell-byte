using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpellByte.Internal
{
    internal class InflictionService : IInflictionService
    {
        private readonly DbContextOptions<InflictionDbContext> _options;
        public InflictionService(DbContextOptions<InflictionDbContext> options)
        {
            _options = options;

            using (InflictionDbContext context = InflictionDbContextFactory.CreateDbContext(options))
            {
                if (!(context.GetService<IDatabaseCreator>() as RelationalDatabaseCreator).Exists())
                    context.Database.Migrate();
                else if ((context.GetService<IDatabaseCreator>() as RelationalDatabaseCreator).Exists()
                    && context.Database.GetPendingMigrations().Any())
                    context.Database.Migrate();
            }
        }

        public void Dispose() { }

        public async Task<bool> CreateInflictionAsync(string name)
        {
            return await CreateInflictionAsync(name, string.Empty);
        }

        public async Task<bool> CreateInflictionAsync(string name, string description)
        {
            using (InflictionDbContext context = InflictionDbContextFactory.CreateDbContext(_options))
            {
                Infliction Infliction = new Infliction
                {
                    Name = name,
                    Description = description
                };
                if (context.Set<Infliction>().Any(n => n.Name == name)) return false;

                await context.AddAsync(Infliction);
                return Convert.ToBoolean(await context.SaveChangesAsync());
            }
        }

        public async Task<bool> DeleteInflictionAsync(Guid id)
        {
            using (InflictionDbContext context = InflictionDbContextFactory.CreateDbContext(_options))
            {
                Infliction Infliction = await context.Set<Infliction>().FirstOrDefaultAsync(n => n.Id == id);
                if (Infliction == null) return false;

                context.Remove(Infliction);
                return Convert.ToBoolean(await context.SaveChangesAsync());
            }
        }

        public async Task<bool> EditInflictionAsync(Guid id, string name)
        {
            return await EditInflictionAsync(id, name, string.Empty);
        }

        public async Task<bool> EditInflictionAsync(Guid id, string name, string description)
        {
            using (InflictionDbContext context = InflictionDbContextFactory.CreateDbContext(_options))
            {
                Infliction Infliction = await context.Set<Infliction>().FirstOrDefaultAsync(n => n.Id == id);
                if (Infliction == null) return false;

                Infliction.Name = name;
                Infliction.Description = description;
                context.Update(Infliction);
                return Convert.ToBoolean(await context.SaveChangesAsync());
            }
        }

        public async Task<IEnumerable<Infliction>> GetInflictionsAsync()
        {
            using (InflictionDbContext context = InflictionDbContextFactory.CreateDbContext(_options))
            {
                return (await context.Set<Infliction>().ToListAsync()).OrderBy(n => n.Name);
            }
        }

        public async Task<Infliction> GetInflictionByIdAsync(Guid id)
        {
            using (InflictionDbContext context = InflictionDbContextFactory.CreateDbContext(_options))
            {
                return await context.Set<Infliction>().FirstOrDefaultAsync(n => n.Id == id);
            }
        }
    }
}
