using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SpellByte
{
    public interface INatureService : IDisposable
    {
        Task<bool> CreateNatureAsync(string name);
        Task<bool> DeleteNatureAsync(Guid id);
        Task<bool> EditNatureAsync(Guid id, string name);

        Task<IEnumerable<Nature>> GetNaturesAsync();
        Task<Nature> GetNatureByIdAsync(Guid id);
    }
}
