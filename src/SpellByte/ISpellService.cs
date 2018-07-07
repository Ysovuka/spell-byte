using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SpellByte
{
    public interface ISpellService : IDisposable
    {
        Task<bool> CreateSpellAsync(string name, string description, Shape shape, float radius, float range,
            Guid domain, Guid castingSpeed, Guid recastSpeed, Guid recoverySpeed);
        Task<bool> DeleteSpellAsync(Guid id);
        Task<bool> EditSpellAsync(Guid id, string name, string description, Shape shape, float radius, float range,
            Guid domain, Guid castingSpeed, Guid recastSpeed, Guid recoverySpeed);

        Task<IEnumerable<Spell>> GetSpellsAsync();
        Task<Spell> GetSpellByIdAsync(Guid id);
        Task<Spell> GetSpellByNameAsync(string name);

        Task<bool> CreateSpellMapAsync(Guid spell, Guid effect);
        Task<bool> DeleteSpellMapAsync(Guid spell, Guid effect);
        Task<bool> DeleteSpellMapsBySpellIdAsync(Guid spellId);
        Task<IEnumerable<SpellMap>> GetSpellMapsBySpellIdAsync(Guid spellId);
    }
}
