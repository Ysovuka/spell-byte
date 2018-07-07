using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpellByte.Web.ViewModels.Tempo
{
    public class IndexViewModel
    {
        public IEnumerable<SpellByte.Tempo> Tempos { get; set; } = new List<SpellByte.Tempo>();
    }
}
