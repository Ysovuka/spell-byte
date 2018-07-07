using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SpellByte
{
    public enum EffectTypes
    {
        [Display(Name = "Overtime")]
        Overtime,
        [Display(Name = "Reactive")]
        Reactive
    }
}
