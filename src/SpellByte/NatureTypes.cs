using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SpellByte
{
    public enum NatureTypes
    {
        [Display(Name = "Ailment")]
        Ailment,
        [Display(Name = "Enchantment")]
        Enchantment,
        [Display(Name = "Drain")]
        Drain,
        [Display(Name = "Heal")]
        Heal,
    }
}
