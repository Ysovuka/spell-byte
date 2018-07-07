using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SpellByte
{
    public interface IEffectService : IDisposable
    {
        Task<bool> CreateEffectAsync(string name, string description, EffectTypes type, EffectCategory category, 
            NatureTypes nature, TriggerTypes trigger, int triggerCount, float damage, float damagePerTick, DamageStyles damageStyle, DamageStyles damagePerTickStyle,
            Guid duration, Guid durationTick, Guid affliction, Guid immunityDuration, Guid castingSpeed, Guid recastSpeed, Guid recoverySpeed);
        Task<bool> DeleteEffectAsync(Guid id);
        Task<bool> EditEffectAsync(Guid id, string name, string description, EffectTypes type, EffectCategory category,
            NatureTypes nature, TriggerTypes trigger, int triggerCount, float damage, float damagePerTick, DamageStyles damageStyle, DamageStyles damagePerTickStyle,
            Guid duration, Guid durationTick, Guid affliction, Guid immunityDuration, Guid castingSpeed, Guid recastSpeed, Guid recoverySpeed);

        Task<IEnumerable<Effect>> GetEffectsAsync();
        Task<Effect> GetEffectByIdAsync(Guid id);
        Task<Effect> GetEffectByNameAsync(string name);
        bool IsPrimaryEffect(Guid id);

        Task<bool> CreateEffectMapAsync(Guid key, Guid effect);
        Task<bool> DeleteEffectMapByIdAsync(Guid id);
        Task<bool> DeleteEffectMapsByKeyAsync(Guid key);
        Task<IEnumerable<EffectMap>> GetEffectMapsByKeyAsync(Guid key);
    }
}
