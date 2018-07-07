using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpellByte.Web.ViewModels.Tempo
{
    public class DeleteViewModel
    {
        public Guid Id { get; set; }
        public int Value { get; set; }
        public Timing Timing { get; set; }
    }
}
