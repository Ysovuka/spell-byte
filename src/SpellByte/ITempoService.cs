using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SpellByte
{
    public interface ITempoService : IDisposable
    {
        Task<bool> CreateTempoAsync(int value, Timing timing);
        Task<bool> DeleteTempoAsync(Guid id);
        Task<bool> EditTempoAsync(Guid id, int value, Timing timing);

        Task<IEnumerable<Tempo>> GetTemposAsync();
        Task<Tempo> GetTempoByIdAsync(Guid id);
    }
}
