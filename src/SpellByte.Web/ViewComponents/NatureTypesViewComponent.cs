using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SpellByte.Web.ViewModels.Effect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpellByte.Web.ViewComponents
{
    public class NatureTypesViewComponent : ViewComponent
    {
        private readonly IAfflictionService _afflictionService;
        private readonly IInflictionService _inflictionService;
        private readonly IEffectService _effectService;

        public NatureTypesViewComponent(IAfflictionService afflictionService,
            IInflictionService inflictionService,
            IEffectService effectService)
        {
            _afflictionService = afflictionService;
            _inflictionService = inflictionService;
            _effectService = effectService;
        }

        public async Task<IViewComponentResult> InvokeAsync(Guid effectId, NatureTypes nature)
        {
            Effect effect = await _effectService.GetEffectByIdAsync(effectId);
            NatureTypesViewModel viewModel = new NatureTypesViewModel
            {
                Affliction = effect.AfflictionId,
                Afflictions = (await _afflictionService.GetAfflictionsAsync())
                    .Select(t => new SelectListItem
                    {
                        Text = $"{t.Name}",
                        Value = t.Id.ToString()
                    }),

                Damage = effect.Damage,
            };

            return View($"~/Views/Effect/NatureTypes/{nature}.cshtml", viewModel);
        }
    }
}
