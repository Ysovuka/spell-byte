using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpellByte.Web.ViewModels.Nature
{
    public class IndexViewModel
    {
        public IEnumerable<SpellByte.Nature> Natures { get; set; } = new List<SpellByte.Nature>();
    }
}
