using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SpellByte
{
    public interface IDomainService : IDisposable
    {
        Task<bool> CreateDomainAsync(string name);
        Task<bool> DeleteDomainAsync(Guid id);
        Task<bool> EditDomainAsync(Guid id, string name);
        Task<IEnumerable<Domain>> GetDomainsAsync();
        Task<Domain> GetDomainByIdAsync(Guid id);
    }
}
