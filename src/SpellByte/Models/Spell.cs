using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SpellByte
{
    public class Spell
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Code { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public Shape Shape { get; set; }
        public float Radius { get; set; }
        public float Range { get; set; }

        public Guid Domain { get; set; }
        public Guid CastingSpeed { get; set; }
        public Guid RecastSpeed { get; set; }
        public Guid RecoverySpeed { get; set; }
    }
}
