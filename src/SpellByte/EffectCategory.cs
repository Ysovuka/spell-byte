using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SpellByte
{
    public enum EffectCategory
    {
        [Display(Name = "Basic")]
        Basic = 0,
        [Display(Name = "Intermediate")]
        Intermediate = 1,
        [Display(Name = "Advanced")]
        Advanced = 2,
        [Display(Name = "Master")]
        Master = 3,
        [Display(Name = "Unique")]
        Unique = 255,
    }
}
