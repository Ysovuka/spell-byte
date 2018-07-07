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
    internal class SpellService : ISpellService
    {
        private readonly DbContextOptions<SpellDbContext> _options;
        public SpellService(DbContextOptions<SpellDbContext> options)
        {
            _options = options;

            using (SpellDbContext context = SpellDbContextFactory.CreateDbContext(options))
            {
                if (!(context.GetService<IDatabaseCreator>() as RelationalDatabaseCreator).Exists())
                    context.Database.Migrate();
                else if ((context.GetService<IDatabaseCreator>() as RelationalDatabaseCreator).Exists()
                    && context.Database.GetPendingMigrations().Any())
                    context.Database.Migrate();
            }
        }

        public void Dispose() { }

        public async Task<bool> CreateSpellAsync(string name, string description, Shape shape, float radius, float range,
            Guid domain, Guid castingSpeed, Guid recastSpeed, Guid recoverySpeed)
        {
            using (SpellDbContext context = SpellDbContextFactory.CreateDbContext(_options))
            {
                Spell spell = new Spell
                {
                    Name = name,
                    Description = description,
                    Shape = shape,
                    Radius = radius,
                    Range = range,

                    Domain = domain,
                    CastingSpeed = castingSpeed,
                    RecastSpeed = recastSpeed,
                    RecoverySpeed = recoverySpeed
                };

                if (context.Set<Spell>().Any(s => s.Name == name)) return false;

                await context.AddAsync(spell);

                return Convert.ToBoolean(await context.SaveChangesAsync());
            }
        }

        public async Task<bool> DeleteSpellAsync(Guid id)
        {
            using (SpellDbContext context = SpellDbContextFactory.CreateDbContext(_options))
            {
                Spell spell = await context.Set<Spell>().FirstOrDefaultAsync(s => s.Id == id);
                if (spell == null) return false;

                context.Remove(spell);
                return Convert.ToBoolean(await context.SaveChangesAsync());
            }
        }

        public async Task<bool> EditSpellAsync(Guid id, string name, string description, Shape shape, float radius, float range,
            Guid domain, Guid castingSpeed, Guid recastSpeed, Guid recoverySpeed)
        {
            using (SpellDbContext context = SpellDbContextFactory.CreateDbContext(_options))
            {
                Spell spell = await context.Set<Spell>().FirstOrDefaultAsync(s => s.Id == id);
                if (spell == null) return false;

                spell.Name = name;
                spell.Description = description;
                spell.Shape = shape;
                spell.Radius = radius;
                spell.Range = range;

                spell.Domain = domain;
                spell.CastingSpeed = castingSpeed;
                spell.RecastSpeed = recastSpeed;
                spell.RecoverySpeed = recoverySpeed;

                context.Update(spell);
                return Convert.ToBoolean(await context.SaveChangesAsync());
            }
        }

        public async Task<IEnumerable<Spell>> GetSpellsAsync()
        {
            using (SpellDbContext context = SpellDbContextFactory.CreateDbContext(_options))
            {
                return (await context.Set<Spell>().ToListAsync()).OrderBy(s => s.Name);
            }
        }

        public async Task<Spell> GetSpellByIdAsync(Guid id)
        {
            using (SpellDbContext context = SpellDbContextFactory.CreateDbContext(_options))
            {
                return await context.Set<Spell>().FirstOrDefaultAsync(s => s.Id == id);
            }
        }

        public async Task<Spell> GetSpellByNameAsync(string name)
        {
            using (SpellDbContext context = SpellDbContextFactory.CreateDbContext(_options))
            {
                return await context.Set<Spell>().FirstOrDefaultAsync(s => s.Name == name);
            }
        }

        public async Task<bool> CreateSpellMapAsync(Guid spellId, Guid effectId)
        {
            using (SpellDbContext context = SpellDbContextFactory.CreateDbContext(_options))
            {
                SpellMap spellMap = new SpellMap
                {
                    Spell = spellId,
                    Effect = effectId,
                };
                if (context.Set<SpellMap>().Any(s => s.Spell == spellId && s.Effect == effectId)) return false;

                await context.AddAsync(spellMap);
                return Convert.ToBoolean(await context.SaveChangesAsync());
            }
        }

        public async Task<bool> DeleteSpellMapAsync(Guid spellId, Guid effectId)
        {
            using (SpellDbContext context = SpellDbContextFactory.CreateDbContext(_options))
            {
                SpellMap spellMap = await context.Set<SpellMap>().FirstOrDefaultAsync(s => s.Spell == spellId && s.Effect == effectId);
                if (spellMap == null) return false;

                context.Remove(spellMap);
                return Convert.ToBoolean(await context.SaveChangesAsync());
            }
        }

        public async Task<bool> DeleteSpellMapsBySpellIdAsync(Guid spellId)
        {
            using (SpellDbContext context = SpellDbContextFactory.CreateDbContext(_options))
            {
                IEnumerable<SpellMap> spellMaps = context.Set<SpellMap>().Where(s => s.Spell == spellId);

                foreach(SpellMap spellMap in spellMaps)
                {
                    context.Remove(spellMap);
                }

                return Convert.ToBoolean(await context.SaveChangesAsync());
            }
        }

        public async Task<IEnumerable<SpellMap>> GetSpellMapsBySpellIdAsync(Guid spellId)
        {
            using (SpellDbContext context = SpellDbContextFactory.CreateDbContext(_options))
            {
                return await context.Set<SpellMap>().Where(s =>s.Spell == spellId).ToListAsync();
            }
        }
    }
}
