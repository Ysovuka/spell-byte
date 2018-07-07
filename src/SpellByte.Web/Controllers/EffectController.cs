using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SpellByte.Web.ViewModels.Effect;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using SpellByte.Web.Extensions;

namespace SpellByte.Web.Controllers
{
    public class EffectController : Controller
    {
        private readonly ITempoService _tempoService;
        private readonly INatureService _natureService;
        private readonly IAfflictionService _afflictionService;
        private readonly IInflictionService _inflictionService;
        private readonly IEffectService _effectService;

        public EffectController(IEffectService effectService,
            ITempoService tempoService,
            INatureService natureService,
            IAfflictionService afflictionService,
            IInflictionService inflictionService)
        {
            _effectService = effectService;
            _tempoService = tempoService;
            _natureService = natureService;
            _afflictionService = afflictionService;
            _inflictionService = inflictionService;
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            CreateViewModel viewModel = new CreateViewModel
            {
                Afflictions = (await _afflictionService.GetAfflictionsAsync())
                    .Select(t => new SelectListItem
                    {
                        Text = $"{t.Name}",
                        Value = t.Id.ToString()
                    }),
                Tempos = (await _tempoService.GetTemposAsync())
                    .Select(t => new SelectListItem
                    {
                            Text = $"{t.Value} {t.Timing.GetAttribute<DisplayAttribute>().Name}",
                            Value = t.Id.ToString()
                    }),
                EffectList = (await _effectService.GetEffectsAsync()).Select(
                    e => new SelectListItem { Text = e.Name, Value = e.Id.ToString() }),
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateViewModel viewModel)
        {
            foreach (var effect in viewModel.Effects)
            {
                if (!_effectService.IsPrimaryEffect(effect))
                {
                    ModelState.AddModelError("Effects", "An effect may not have more than 2 layers.");
                    break;
                }
            }

            if (!ModelState.IsValid)
            {
                viewModel.Afflictions = (await _afflictionService.GetAfflictionsAsync())
                    .Select(t => new SelectListItem
                    {
                        Text = $"{t.Name}",
                        Value = t.Id.ToString()
                    });
                viewModel.Tempos = (await _tempoService.GetTemposAsync())
                    .Select(t => new SelectListItem
                    {
                        Text = $"{t.Value} {t.Timing.GetAttribute<DisplayAttribute>().Name}",
                        Value = t.Id.ToString()
                    });
                viewModel.EffectList = (await _effectService.GetEffectsAsync()).Select(
                    e => new SelectListItem { Text = e.Name, Value = e.Id.ToString() });

                if (viewModel.Effects.Any())
                {
                    foreach (var effect in viewModel.Effects)
                    {
                        var selectedEffect = viewModel.EffectList.FirstOrDefault(s => s.Value == effect.ToString());
                        if (selectedEffect != null)
                        {
                            viewModel.SelectedEffects.Add(selectedEffect);
                        }
                    }
                }

                return View(viewModel);
            }

            if (await _effectService.CreateEffectAsync(
                viewModel.Name, viewModel.Description, viewModel.Type, viewModel.Category,
                viewModel.Nature, viewModel.Trigger, viewModel.TriggerCount, viewModel.Damage, viewModel.DamagePerTick, viewModel.DamageStyle, viewModel.DamagePerTickStyle,
                viewModel.Duration, viewModel.DurationTick, viewModel.Affliction, (viewModel.Immunity) ? viewModel.ImmunityDuration : default(Guid),
                viewModel.CastingSpeed, viewModel.RecastSpeed, viewModel.RecoverySpeed))
            {
                Effect effect = await _effectService.GetEffectByNameAsync(viewModel.Name);

                foreach (var _effect in viewModel.Effects)
                {
                    await _effectService.CreateEffectMapAsync(effect.Id, _effect);
                }
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == default(Guid)) return BadRequest();

            Effect effect = await _effectService.GetEffectByIdAsync(id);
            IEnumerable<EffectMap> effectMaps = await _effectService.GetEffectMapsByKeyAsync(effect.Id);
            ICollection<DisplayViewModel> effects = new List<DisplayViewModel>();

            foreach (Guid effectId in effectMaps.Select(m => m.Effect))
            {
                Effect mappedEffect = await _effectService.GetEffectByIdAsync(effectId);
                if (mappedEffect != null)
                {
                    Affliction affliction = (await _afflictionService.GetAfflictionByIdAsync(mappedEffect?.AfflictionId ?? default(Guid)));
                    Tempo duration = (await _tempoService.GetTempoByIdAsync(mappedEffect.DurationId));
                    DisplayViewModel effectViewModel = new DisplayViewModel
                    {
                        Name = mappedEffect.Name,
                        Description = mappedEffect.Description,
                        Type = mappedEffect.Type,

                        Nature = mappedEffect.Nature,
                        Affliction = affliction?.Name,
                        Duration = duration.Value,
                        DurationTiming = duration.Timing.GetAttribute<DisplayAttribute>().Name,
                    };
                    effects.Add(effectViewModel);
                }
            }

            DeleteViewModel viewModel = new DeleteViewModel
            {
                Name = effect.Name,
                Id = effect.Id,
                Description = effect.Description,
                Type = effect.Type,
                Nature = effect.Nature,
                Duration = (await _tempoService.GetTempoByIdAsync(effect.DurationId)).Value,
                DurationTiming = (await _tempoService.GetTempoByIdAsync(effect.DurationId)).Timing.GetAttribute<DisplayAttribute>().Name,
                CastingSpeed = (await _tempoService.GetTempoByIdAsync(effect.CastingSpeedId))?.Value ?? 0,
                CastingSpeedTiming = (await _tempoService.GetTempoByIdAsync(effect.CastingSpeedId)).Timing.GetAttribute<DisplayAttribute>()?.Name,
                RecastSpeed = (await _tempoService.GetTempoByIdAsync(effect.RecastSpeedId))?.Value ?? 0,
                RecastSpeedTiming = (await _tempoService.GetTempoByIdAsync(effect.RecastSpeedId)).Timing.GetAttribute<DisplayAttribute>()?.Name,
                RecoverySpeed = (await _tempoService.GetTempoByIdAsync(effect.RecoverySpeedId))?.Value ?? 0,
                RecoverySpeedTiming = (await _tempoService.GetTempoByIdAsync(effect.RecoverySpeedId)).Timing.GetAttribute<DisplayAttribute>()?.Name,

                Effects = effects,
            };

            switch (effect.Nature)
            {
                case NatureTypes.Drain:
                case NatureTypes.Heal:
                    viewModel.Affliction = (await _afflictionService.GetAfflictionByIdAsync(effect.AfflictionId)).Name;
                    break;
            }

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                Effect effect = await _effectService.GetEffectByIdAsync(viewModel.Id);
                IEnumerable<EffectMap> effectMaps = await _effectService.GetEffectMapsByKeyAsync(effect.Id);
                ICollection<DisplayViewModel> effects = new List<DisplayViewModel>();

                foreach (Guid effectId in effectMaps.Select(m => m.Effect))
                {
                    Effect mappedEffect = await _effectService.GetEffectByIdAsync(effectId);
                    if (mappedEffect != null)
                    {
                        Affliction affliction = (await _afflictionService.GetAfflictionByIdAsync(mappedEffect?.AfflictionId ?? default(Guid)));
                        Tempo duration = (await _tempoService.GetTempoByIdAsync(mappedEffect.DurationId));
                        DisplayViewModel effectViewModel = new DisplayViewModel
                        {
                            Name = mappedEffect.Name,
                            Description = mappedEffect.Description,
                            Type = mappedEffect.Type,

                            Nature = mappedEffect.Nature,
                            Affliction = affliction?.Name,
                            Duration = duration.Value,
                            DurationTiming = duration.Timing.GetAttribute<DisplayAttribute>().Name,
                        };
                        effects.Add(effectViewModel);
                    }
                }

                viewModel.Effects = effects;

                return View(viewModel);
            }

            await _effectService.DeleteEffectAsync(viewModel.Id);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Display(Guid id)
        {
            if (id == default(Guid)) return BadRequest();

            Effect effect = await _effectService.GetEffectByIdAsync(id);
            IEnumerable<EffectMap> effectMaps = await _effectService.GetEffectMapsByKeyAsync(effect.Id);
            ICollection<DisplayViewModel> effects = new List<DisplayViewModel>();

            foreach (Guid effectId in effectMaps.Select(m => m.Effect))
            {
                Effect mappedEffect = await _effectService.GetEffectByIdAsync(effectId);
                if (mappedEffect != null)
                {
                    Affliction affliction = (await _afflictionService.GetAfflictionByIdAsync(mappedEffect?.AfflictionId ?? default(Guid)));
                    Tempo duration = (await _tempoService.GetTempoByIdAsync(mappedEffect.DurationId));
                    DisplayViewModel effectViewModel = new DisplayViewModel
                    {
                        Name = mappedEffect.Name,
                        Description = mappedEffect.Description,
                        Type = mappedEffect.Type,

                        Nature = mappedEffect.Nature,
                        Affliction = affliction?.Name,
                        Duration = duration.Value,
                        DurationTiming = duration.Timing.GetAttribute<DisplayAttribute>().Name,
                    };
                    effects.Add(effectViewModel);
                }
            }

            DisplayViewModel viewModel = new DisplayViewModel
            {
                Name = effect.Name,
                Description = effect.Description,
                Type = effect.Type,
                Nature = effect.Nature,
                Duration = (await _tempoService.GetTempoByIdAsync(effect.DurationId)).Value,
                DurationTiming = (await _tempoService.GetTempoByIdAsync(effect.DurationId)).Timing.GetAttribute<DisplayAttribute>().Name,
                CastingSpeed = (await _tempoService.GetTempoByIdAsync(effect.CastingSpeedId))?.Value ?? 0,
                CastingSpeedTiming = (await _tempoService.GetTempoByIdAsync(effect.CastingSpeedId)).Timing.GetAttribute<DisplayAttribute>()?.Name,
                RecastSpeed = (await _tempoService.GetTempoByIdAsync(effect.RecastSpeedId))?.Value ?? 0,
                RecastSpeedTiming = (await _tempoService.GetTempoByIdAsync(effect.RecastSpeedId)).Timing.GetAttribute<DisplayAttribute>()?.Name,
                RecoverySpeed = (await _tempoService.GetTempoByIdAsync(effect.RecoverySpeedId))?.Value ?? 0,
                RecoverySpeedTiming = (await _tempoService.GetTempoByIdAsync(effect.RecoverySpeedId)).Timing.GetAttribute<DisplayAttribute>()?.Name,

                Effects = effects,
            };

            switch (effect.Nature)
            {
                case NatureTypes.Drain:
                case NatureTypes.Heal:
                    viewModel.Affliction = (await _afflictionService.GetAfflictionByIdAsync(effect.AfflictionId)).Name;
                    break;
            }

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult DisplayAilmentOptions()
        {
            return PartialView($"~/Views/Effect/NatureTypes/Ailment.cshtml");
        }

        [HttpGet]
        public ActionResult DisplayEnchantmentOptions()
        {
            return PartialView($"~/Views/Effect/NatureTypes/Enchantment.cshtml");
        }

        [HttpGet]
        public async Task<IActionResult> DisplayDrainOptions()
        {
            NatureTypesViewModel viewModel = new NatureTypesViewModel
            {
                Afflictions = (await _afflictionService.GetAfflictionsAsync())
                    .Select(t => new SelectListItem
                    {
                        Text = $"{t.Name}",
                        Value = t.Id.ToString()
                    }),
            };
            return PartialView($"~/Views/Effect/NatureTypes/Drain.cshtml", viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> DisplayHealOptions()
        {
            NatureTypesViewModel viewModel = new NatureTypesViewModel
            {
                Afflictions = (await _afflictionService.GetAfflictionsAsync())
                    .Select(t => new SelectListItem
                    {
                        Text = $"{t.Name}",
                        Value = t.Id.ToString()
                    }),
            };
            return PartialView($"~/Views/Effect/NatureTypes/Heal.cshtml", viewModel);
        }

        [HttpGet]
        public IActionResult DisplayInstantOptions()
        {
            return PartialView($"~/Views/Effect/EffectTypes/Instant.cshtml");
        }

        [HttpGet]
        public async Task<IActionResult> DisplayOvertimeOptions()
        {
            EffectTypesViewModel viewModel = new EffectTypesViewModel
            {
                Tempos = (await _tempoService.GetTemposAsync())
        .Select(t => new SelectListItem
        {
            Text = $"{t.Value} {t.Timing.GetAttribute<DisplayAttribute>().Name}",
            Value = t.Id.ToString()
        }),
            };
            return PartialView($"~/Views/Effect/EffectTypes/Overtime.cshtml", viewModel);
        }

        [HttpGet]
        public IActionResult DisplayReactiveOptions()
        {
            return PartialView($"~/Views/Effect/EffectTypes/Reactive.cshtml");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == default(Guid)) return BadRequest();

            Effect effect = await _effectService.GetEffectByIdAsync(id);
            IEnumerable<EffectMap> effectMaps = await _effectService.GetEffectMapsByKeyAsync(id);

            EditViewModel viewModel = new EditViewModel
            {
                Id = effect.Id,
                Name = effect.Name,
                Description = effect.Description,
                Type = effect.Type,
                Category = effect.Category,
                Nature = effect.Nature,
                Affliction = effect.AfflictionId,
                Duration = effect.DurationId,
                DurationTick = effect.DurationTickId,
                Immunity = effect.ImmunityDurationId != default(Guid),
                ImmunityDuration = effect.ImmunityDurationId,
                CastingSpeed = effect.CastingSpeedId,
                RecastSpeed = effect.RecastSpeedId,
                RecoverySpeed = effect.RecoverySpeedId,
                Effects = effectMaps.Select(m => m.Effect),
                Afflictions = (await _afflictionService.GetAfflictionsAsync())
                    .Select(t => new SelectListItem
                    {
                        Text = $"{t.Name}",
                        Value = t.Id.ToString()
                    }),
                Tempos = (await _tempoService.GetTemposAsync())
                    .Select(t => new SelectListItem
                    {
                            Text = $"{t.Value} {t.Timing.GetAttribute<DisplayAttribute>().Name}",
                            Value = t.Id.ToString()
                    }),
                EffectList = (await _effectService.GetEffectsAsync()).Select(
                    e => new SelectListItem { Text = e.Name, Value = e.Id.ToString() }),
            };

            if (viewModel.Effects.Any())
            {
                foreach (var _effect in viewModel.Effects)
                {
                    var selectedEffect = viewModel.EffectList.FirstOrDefault(s => s.Value == _effect.ToString());
                    if (selectedEffect != null)
                    {
                        viewModel.SelectedEffects.Add(selectedEffect);
                    }
                }
            }

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditViewModel viewModel)
        {
            foreach (var effect in viewModel.Effects)
            {
                if (!_effectService.IsPrimaryEffect(effect))
                {
                    ModelState.AddModelError("Effects", "An effect may not have more than 2 layers.");
                    break;
                }
            }

            if (!ModelState.IsValid)
            {
                viewModel.Afflictions = (await _afflictionService.GetAfflictionsAsync())
                    .Select(t => new SelectListItem
                    {
                        Text = $"{t.Name}",
                        Value = t.Id.ToString()
                    });
                viewModel.Tempos = (await _tempoService.GetTemposAsync())
                    .Select(t => new SelectListItem
                    {
                        Text = $"{t.Value} {t.Timing.GetAttribute<DisplayAttribute>().Name}",
                        Value = t.Id.ToString()
                    });
                viewModel.EffectList = (await _effectService.GetEffectsAsync()).Select(
                    e => new SelectListItem { Text = e.Name, Value = e.Id.ToString() });

                if (viewModel.Effects.Any())
                {
                    foreach (var effect in viewModel.Effects)
                    {
                        var effects = viewModel.EffectList.Select(e => { if (e.Value == effect.ToString()) { return e; } else { return null; } });
                        if (effects.FirstOrDefault() != null)
                        {
                            viewModel.SelectedEffects.Add(effects.FirstOrDefault());
                        }
                    }
                }

                return View(viewModel);
            }

            if (await _effectService.EditEffectAsync(
                viewModel.Id, viewModel.Name, viewModel.Description, viewModel.Type, viewModel.Category,
                viewModel.Nature, viewModel.Trigger, viewModel.TriggerCount, viewModel.Damage, viewModel.DamagePerTick, viewModel.DamageStyle, viewModel.DamagePerTickStyle,
                viewModel.Duration, viewModel.DurationTick, viewModel.Affliction, (viewModel.Immunity) ? viewModel.ImmunityDuration : default(Guid),
                viewModel.CastingSpeed, viewModel.RecastSpeed, viewModel.RecoverySpeed))
            {
                await _effectService.DeleteEffectMapsByKeyAsync(viewModel.Id);

                foreach(var effect in viewModel.Effects)
                {
                    await _effectService.CreateEffectMapAsync(viewModel.Id, effect);
                }
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IndexViewModel viewModel = new IndexViewModel()
            {
                Effects = await _effectService.GetEffectsAsync(),
            };
            return View(viewModel);
        }
    }
}