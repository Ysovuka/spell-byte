using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SpellByte.Web.ViewModels.Effect
{
    public class EffectTypesViewModel
    {
        [Display(Name = "Damage Per Tick")]
        public float DamagePerTick { get; set; }
        [Display(Name = "Damage Per Tick Style")]
        public DamageStyles DamagePerTickStyle { get; set; }
        [Display(Name = "Duration Tick")]
        public Guid DurationTick { get; set; }
        public IEnumerable<SelectListItem> Tempos { get; set; } = new List<SelectListItem>();

        [Display(Name = "Trigger Count")]
        public int TriggerCount { get; set; }
        [Display(Name = "Trigger Type")]
        public TriggerTypes Trigger { get; set; }
    }
}
