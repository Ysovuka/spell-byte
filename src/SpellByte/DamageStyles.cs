using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SpellByte
{
    public enum DamageStyles
    {
        [Display(Name = "None")]
        None,
        [Display(Name = "Raw")]
        Raw,
        [Display(Name = "Percentage")]
        Percentage,
    }
}
