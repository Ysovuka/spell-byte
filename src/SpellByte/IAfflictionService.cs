using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SpellByte
{
    public interface IAfflictionService : IDisposable
    {
        Task<bool> CreateAfflictionAsync(string name);
        Task<bool> DeleteAfflictionAsync(Guid id);
        Task<bool> EditAfflictionAsync(Guid id, string name);

        Task<IEnumerable<Affliction>> GetAfflictionsAsync();
        Task<Affliction> GetAfflictionByIdAsync(Guid id);
    }
}
