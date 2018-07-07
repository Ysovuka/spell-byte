using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpellByte.Web.ViewModels.Spell
{
    public class IndexViewModel
    {
        public IEnumerable<SpellByte.Spell> Spells { get; set; } = new List<SpellByte.Spell>();
    }
}
