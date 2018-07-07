using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpellByte.Web.ViewModels.Effect
{
    public class IndexViewModel
    {
        public IEnumerable<SpellByte.Effect> Effects { get; set; } = new List<SpellByte.Effect>();
    }
}
