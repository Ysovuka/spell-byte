using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpellByte.Web.ViewModels.Domain
{
    public class IndexViewModel
    {
        public IEnumerable<SpellByte.Domain> Domains { get; set; } = new List<SpellByte.Domain>();
    }
}
