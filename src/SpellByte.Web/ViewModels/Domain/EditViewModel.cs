using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpellByte.Web.ViewModels.Domain
{
    public class EditViewModel
    {
        public Guid Id { get; set; } = default(Guid);
        public string Name { get; set; }
    }
}
