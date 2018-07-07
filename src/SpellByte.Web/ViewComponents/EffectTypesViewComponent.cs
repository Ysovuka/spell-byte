using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SpellByte.Web.Extensions;
using SpellByte.Web.ViewModels.Effect;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SpellByte.Web.ViewComponents
{
    public class EffectTypesViewComponent : ViewComponent
    {
        private readonly IEffectService _effectService;
        private readonly ITempoService _tempoService;

        public EffectTypesViewComponent(IEffectService effectService,
            ITempoService tempoService)
        {
            _effectService = effectService;
            _tempoService = tempoService;
        }

        public async Task<IViewComponentResult> InvokeAsync(Guid effectId, EffectTypes effectType)
        {

            Effect effect = (effectId == default(Guid)) ? new Effect() : await _effectService.GetEffectByIdAsync(effectId);

            EffectTypesViewModel viewModel = new EffectTypesViewModel
            {
                Trigger = effect.Trigger,
                TriggerCount = effect.TriggerCount,
                DamagePerTick = effect.DamagePerTick,
                DurationTick = effect.DurationTickId,
                Tempos = (await _tempoService.GetTemposAsync())
                    .Select(t => new SelectListItem
                    {
                        Text = $"{t.Value} {t.Timing.GetAttribute<DisplayAttribute>().Name}",
                        Value = t.Id.ToString()
                    }),
            };

            return View($"~/Views/Effect/EffectTypes/{effectType}.cshtml", viewModel);
        }
    }
}
