using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpellByte.Web.ViewModels.Infliction
{
    public class IndexViewModel
    {
        public IEnumerable<SpellByte.Infliction> Inflictions { get; set; } = new List<SpellByte.Infliction>();
    }
}
