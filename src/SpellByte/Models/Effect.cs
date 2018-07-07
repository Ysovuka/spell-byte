using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SpellByte
{
    public class Effect
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Code { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public EffectTypes Type { get; set; }
        public EffectCategory Category { get; set; }
        public NatureTypes Nature { get; set; }
        public int TriggerCount { get; set; }
        public TriggerTypes Trigger { get; set; }

        public Guid AfflictionId { get; set; }
        public Guid CastingSpeedId { get; set; }
        public Guid RecastSpeedId { get; set; }
        public Guid RecoverySpeedId { get; set; }
        public Guid DurationId { get; set; }
        public Guid DurationTickId { get; set; }
        public Guid InflictionId { get; set; }
        public Guid ImmunityDurationId { get; set; }

        public float Damage { get; set; }
        public DamageStyles DamageStyle { get; set; }
        public float DamagePerTick { get; set; }
        public DamageStyles DamagePerTickStyle { get; set; }

    }
}
