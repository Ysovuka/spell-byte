using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SpellByte
{
    public enum Shape
    {
        [Display(Name = "Conical")]
        Cone,
        [Display(Name = "Cylindrical")]
        Cylinder,
        [Display(Name = "Sphreical")]
        Sphere,
        [Display(Name = "Direct (Self)")]
        Self,
        [Display(Name = "Direct (Other)")]
        Other,
        [Display(Name = "Direct (Object)")]
        Object,
        [Display(Name = "Square")]
        Square,
        [Display(Name = "Line")]
        Line,
        [Display(Name = "Ring")]
        Ring
    }
}
