using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpellByte.Internal
{
    internal class EffectService : IEffectService
    {
        private readonly DbContextOptions<EffectDbContext> _options;
        public EffectService(DbContextOptions<EffectDbContext> options)
        {
            _options = options;

            using (EffectDbContext context = EffectDbContextFactory.CreateDbContext(options))
            {
                if (!(context.GetService<IDatabaseCreator>() as RelationalDatabaseCreator).Exists())
                    context.Database.Migrate();
                else if ((context.GetService<IDatabaseCreator>() as RelationalDatabaseCreator).Exists()
                    && context.Database.GetPendingMigrations().Any())
                    context.Database.Migrate();
            }
        }

        public void Dispose() { }

        public async Task<bool> CreateEffectAsync(string name, string description, 
            EffectTypes type, EffectCategory category, NatureTypes nature, TriggerTypes trigger,
            int triggerCount, float damage, float damagePerTick, DamageStyles damageStyle, DamageStyles damagePerTickStyle,
            Guid duration, Guid durationTick, Guid affliction, Guid immunityDuration,
            Guid castingSpeed, Guid recastSpeed, Guid recoverySpeed)
        {
            using (EffectDbContext context = EffectDbContextFactory.CreateDbContext(_options))
            {
                Effect effect = new Effect
                {
                    Name = name,
                    Description = description,
                    Type = type,
                    Category = category,
                    Nature = nature,
                    Trigger = trigger,
                    TriggerCount = triggerCount,
                    Damage = damage,
                    DamagePerTick = damagePerTick,
                    DamagePerTickStyle = damagePerTickStyle,
                    DamageStyle = damageStyle,
                    DurationId = duration,
                    DurationTickId = durationTick,
                    AfflictionId = affliction,
                    ImmunityDurationId = immunityDuration,
                    CastingSpeedId = castingSpeed,
                    RecastSpeedId = recastSpeed,
                    RecoverySpeedId = recoverySpeed,
                };

                if (context.Set<Effect>().Any(e => e.Name == name)) return false;

                await context.AddAsync(effect);

                return Convert.ToBoolean(await context.SaveChangesAsync());
            }
        }

        public async Task<bool> DeleteEffectAsync(Guid id)
        {
            using (EffectDbContext context = EffectDbContextFactory.CreateDbContext(_options))
            {
                Effect effect = await context.Set<Effect>().FirstOrDefaultAsync(e => e.Id == id);
                if (effect == null) return false;

                context.Remove(effect);

                return Convert.ToBoolean(await context.SaveChangesAsync());
            }
        }

        public async Task<bool> EditEffectAsync(
            Guid id, string name, string description, 
            EffectTypes type, EffectCategory category, NatureTypes nature, TriggerTypes trigger,
            int triggerCount, float damage, float damagePerTick, DamageStyles damageStyle, DamageStyles damagePerTickStyle,
            Guid duration, Guid durationTick,
            Guid affliction, Guid immunityDuration,
            Guid castingSpeed, Guid recastSpeed, Guid recoverySpeed)
        {
            using (EffectDbContext context = EffectDbContextFactory.CreateDbContext(_options))
            {
                Effect effect = await context.Set<Effect>().FirstOrDefaultAsync(e => e.Id == id);
                if (effect == null) return false;

                effect.Name = name;
                effect.Description = description;
                effect.Type = type;
                effect.Category = category;
                effect.Nature = nature;
                effect.Trigger = trigger;
                effect.TriggerCount = triggerCount;
                effect.Damage = damage;
                effect.DamagePerTick = damagePerTick;
                effect.DamagePerTickStyle = damagePerTickStyle;
                effect.DamageStyle = damageStyle;
                effect.DurationId = duration;
                effect.DurationTickId = durationTick;
                effect.AfflictionId = affliction;
                effect.ImmunityDurationId = immunityDuration;
                effect.CastingSpeedId = castingSpeed;
                effect.RecastSpeedId = recastSpeed;
                effect.RecoverySpeedId = recoverySpeed;

                context.Update(effect);
                return Convert.ToBoolean(await context.SaveChangesAsync());
            }
        }

        public async Task<IEnumerable<Effect>> GetEffectsAsync()
        {
            using (EffectDbContext context = EffectDbContextFactory.CreateDbContext(_options))
            {
                return (await context.Set<Effect>().ToListAsync()).OrderBy(e => e.Name);
            }
        }

        public async Task<Effect> GetEffectByIdAsync(Guid id)
        {
            using (EffectDbContext context = EffectDbContextFactory.CreateDbContext(_options))
            {
                return await context.Set<Effect>().FirstOrDefaultAsync(e => e.Id == id);
            }
        }

        public async Task<Effect> GetEffectByNameAsync(string name)
        {
            using (EffectDbContext context = EffectDbContextFactory.CreateDbContext(_options))
            {
                return await context.Set<Effect>().FirstOrDefaultAsync(e => e.Name == name);
            }
        }

        public bool IsPrimaryEffect(Guid id)
        {
            using (EffectDbContext context = EffectDbContextFactory.CreateDbContext(_options))
            {
                return (!context.Set<EffectMap>().Any(m => m.Key == id));
            }
        }

        public async Task<bool> CreateEffectMapAsync(Guid key, Guid effect)
        {
            using (EffectDbContext context = EffectDbContextFactory.CreateDbContext(_options))
            {
                EffectMap effectMap = new EffectMap
                {
                    Key = key,
                    Effect = effect,
                };
                if (key == effect) return false; // !recursion
                if (context.Set<EffectMap>().Any(m => m.Key == key && m.Effect == effect)) return false;

                await context.AddAsync(effectMap);
                return Convert.ToBoolean(await context.SaveChangesAsync());
            }
        }

        public async Task<bool> DeleteEffectMapByIdAsync(Guid id)
        {
            using (EffectDbContext context = EffectDbContextFactory.CreateDbContext(_options))
            {
                EffectMap effectMap = await context.Set<EffectMap>().FirstOrDefaultAsync(m => m.Id == id);
                if (effectMap == null) return false;

                context.Remove(effectMap);
                return Convert.ToBoolean(await context.SaveChangesAsync());
            }
        }

        public async Task<bool> DeleteEffectMapsByKeyAsync(Guid key)
        {
            using (EffectDbContext context = EffectDbContextFactory.CreateDbContext(_options))
            {
                IEnumerable<EffectMap> effectMaps = context.Set<EffectMap>().Where(m => m.Key == key);
                foreach(EffectMap effectMap in effectMaps)
                {
                    context.Remove(effectMap);
                }

                return Convert.ToBoolean(await context.SaveChangesAsync());
            }
        }

        public async Task<IEnumerable<EffectMap>> GetEffectMapsByKeyAsync(Guid key)
        {
            using (EffectDbContext context = EffectDbContextFactory.CreateDbContext(_options))
            {
                return await context.Set<EffectMap>().Where(m => m.Key == key).ToListAsync();
            }
        }
    }
}
