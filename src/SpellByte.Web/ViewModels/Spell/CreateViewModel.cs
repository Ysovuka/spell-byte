using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SpellByte.Web.ViewModels.Spell
{
    public class CreateViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Shape Shape { get; set; }
        public float Radius { get; set; }
        public float Range { get; set; }

        public Guid Domain { get; set; }
        public IEnumerable<SelectListItem> Domains { get; set; } = new List<SelectListItem>();

        [Display(Name = "Casting Speed")]
        public Guid CastingSpeed { get; set; }
        [Display(Name = "Recast Speed")]
        public Guid RecastSpeed { get; set; }
        [Display(Name = "Recovery Speed")]
        public Guid RecoverySpeed { get; set; }
        public IEnumerable<SelectListItem> Tempos { get; set; } = new List<SelectListItem>();

        public IEnumerable<Guid> Effects { get; set; } = new List<Guid>();
        public ICollection<SelectListItem> SelectedEffects { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> EffectList { get; set; } = new List<SelectListItem>();
    }
}
