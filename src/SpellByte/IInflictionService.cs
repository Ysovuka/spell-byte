using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SpellByte
{
    public interface IInflictionService : IDisposable
    {
        Task<bool> CreateInflictionAsync(string name);
        Task<bool> CreateInflictionAsync(string name, string description);
        Task<bool> DeleteInflictionAsync(Guid id);
        Task<bool> EditInflictionAsync(Guid id, string name);
        Task<bool> EditInflictionAsync(Guid id, string name, string description);

        Task<IEnumerable<Infliction>> GetInflictionsAsync();
        Task<Infliction> GetInflictionByIdAsync(Guid id);
    }
}
