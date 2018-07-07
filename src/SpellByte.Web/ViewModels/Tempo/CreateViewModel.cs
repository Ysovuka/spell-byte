using Microsoft.AspNetCore.Mvc.Rendering;
using SpellByte.Web.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpellByte.Web.ViewModels.Tempo
{
    public class CreateViewModel
    {
        public int Value { get; set; }
        public Timing Timing { get; set; }
    }
}
