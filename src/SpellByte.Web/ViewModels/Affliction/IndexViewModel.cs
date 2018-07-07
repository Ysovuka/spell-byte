using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpellByte.Web.ViewModels.Affliction
{
    public class IndexViewModel
    {
        public IEnumerable<SpellByte.Affliction> Afflictions { get; set; } = new List<SpellByte.Affliction>();
    }
}
