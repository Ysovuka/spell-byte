using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SpellByte
{
    public enum TriggerTypes
    {
        [Display(Name = "None")]
        None,
        [Display(Name = "On Attack")]
        OnAttack,
        [Display(Name = "On Damage")]
        OnDamage,
        [Display(Name = "On Death")]
        OnDeath,
        [Display(Name = "On Dispell")]
        OnDispell,
        [Display(Name = "On Expired")]
        OnExpired,
        [Display(Name = "On Cast")]
        OnCast,
        [Display(Name = "On Resurrect")]
        OnResurrect,
    }
}
