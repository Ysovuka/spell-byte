using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SpellByte
{
    public enum Timing
    {
        [Display(Name = "Millisecond/s")]
        Milliseconds,
        [Display(Name = "Second/s")]
        Seconds,
        [Display(Name = "Minute/s")]
        Minutes,
        [Display(Name = "Hour/s")]
        Hours,
        [Display(Name = "Day/s")]
        Days,
        [Display(Name = "Month/s")]
        Months,
        [Display(Name = "Year/s")]
        Years,
        [Display(Name = "Infinite")]
        Infinite
    }
}
