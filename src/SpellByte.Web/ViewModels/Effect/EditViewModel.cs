using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SpellByte.Web.ViewModels.Effect
{
    public class EditViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public EffectTypes Type { get; set; }
        public EffectCategory Category { get; set; }
        public NatureTypes Nature { get; set; }
        public TriggerTypes Trigger { get; set; }
        [Display(Name = "Trigger Count")]
        public int TriggerCount { get; set; }
        public float Damage { get; set; }
        [Display(Name = "Damage Per Tick")]
        public float DamagePerTick { get; set; }
        [Display(Name = "Damage Style")]
        public DamageStyles DamageStyle { get; set; }
        [Display(Name = "Damage Per Tick Style")]
        public DamageStyles DamagePerTickStyle { get; set; }

        public Guid Affliction { get; set; }
        public IEnumerable<SelectListItem> Afflictions { get; set; } = new List<SelectListItem>();

        public Guid Duration { get; set; }
        [Display(Name = "Duration Tick")]
        public Guid DurationTick { get; set; }
        public bool Immunity { get; set; }
        [Display(Name="Immunity Duration")]
        public Guid ImmunityDuration { get; set; }
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
