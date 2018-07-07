using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpellByte.Web.ViewModels.Effect
{
    public class DeleteViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public EffectTypes Type { get; set; }

        public string Affliction { get; set; }
        public int Duration { get; set; }
        public string DurationTiming { get; set; }
        public int CastingSpeed { get; set; }
        public string CastingSpeedTiming { get; set; }
        public int RecastSpeed { get; set; }
        public string RecastSpeedTiming { get; set; }
        public int RecoverySpeed { get; set; }
        public string RecoverySpeedTiming { get; set; }
        public string Infliction { get; set; }
        public NatureTypes Nature { get; set; }

        public IEnumerable<DisplayViewModel> Effects { get; set; } = new List<DisplayViewModel>();
    }
}
