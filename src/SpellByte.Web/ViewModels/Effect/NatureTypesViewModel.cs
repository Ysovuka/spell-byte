using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SpellByte.Web.ViewModels.Effect
{
    public class NatureTypesViewModel
    {
        public float Damage { get; set; }
        [Display(Name = "Damage Style")]
        public DamageStyles DamageStyle { get; set; }

        public Guid Affliction { get; set; }
        public IEnumerable<SelectListItem> Afflictions { get; set; } = new List<SelectListItem>();
    }
}
