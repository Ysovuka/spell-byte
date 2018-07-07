using SpellByte.Web.ViewModels.Effect;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SpellByte.Web.ViewModels.Spell
{
    public class DeleteViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Shape Shape { get; set; }
        public float Radius { get; set; }
        public float Range { get; set; }

        public string Domain { get; set; }
        [Display(Name="Casting Speed")]
        public string CastingSpeed { get; set; }
        [Display(Name = "Recast Speed")]
        public string RecastSpeed { get; set; }
        [Display(Name = "Recovery Speed")]
        public string RecoverySpeed { get; set; }

        public IEnumerable<Effect.DisplayViewModel> Effects { get; set; } = new List<Effect.DisplayViewModel>();
    }
}
