using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SpellByte.Web.ViewModels.Spell;
using Microsoft.AspNetCore.Mvc.Rendering;
using SpellByte.Web.Extensions;
using System.ComponentModel.DataAnnotations;

namespace SpellByte.Web.Controllers
{
    public class SpellController : Controller
    {
        private readonly IEffectService _effectService;
        private readonly ISpellService _spellService;
        private readonly ITempoService _tempoService;
        private readonly IDomainService _domainService;
        private readonly INatureService _natureService;
        private readonly IAfflictionService _afflictionService;
        private readonly IInflictionService _inflictionService;

        public SpellController(ISpellService spellService,
            IEffectService effectService,
            ITempoService tempoService,
            IDomainService domainService,
            IAfflictionService afflcitionService,
            IInflictionService inflictionService,
            INatureService natureService)
        {
            _spellService = spellService;
            _effectService = effectService;
            _tempoService = tempoService;
            _domainService = domainService;
            _afflictionService = afflcitionService;
            _inflictionService = inflictionService;
            _natureService = natureService;
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            CreateViewModel viewModel = new CreateViewModel()
            {
                Domains = (await _domainService.GetDomainsAsync()).Select(
                    d => new SelectListItem { Text = d.Name, Value = d.Id.ToString() }),
                Tempos = (await _tempoService.GetTemposAsync()).Select(
                    t => new SelectListItem { Text = $"{t.Value} {t.Timing.GetAttribute<DisplayAttribute>().Name}", Value = t.Id.ToString() }),
                EffectList = (await _effectService.GetEffectsAsync()).Select(
                    e => new SelectListItem { Text = e.Name, Value = e.Id.ToString() }),
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Domains = (await _domainService.GetDomainsAsync()).Select(
                    d => new SelectListItem { Text = d.Name, Value = d.Id.ToString() });
                viewModel.Tempos = (await _tempoService.GetTemposAsync()).Select(
                    t => new SelectListItem { Text = $"{t.Value} {t.Timing.GetAttribute<DisplayAttribute>().Name}", Value = t.Id.ToString() });
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

            if (await _spellService.CreateSpellAsync(viewModel.Name, viewModel.Description,
                viewModel.Shape, viewModel.Radius, viewModel.Range, viewModel.Domain, viewModel.CastingSpeed,
                viewModel.RecastSpeed, viewModel.RecoverySpeed))
            {
                Spell spell = await _spellService.GetSpellByNameAsync(viewModel.Name);

                foreach (var effect in viewModel.Effects)
                {
                    await _spellService.CreateSpellMapAsync(spell.Id, effect);
                }
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == default(Guid)) return BadRequest();

            Spell spell = await _spellService.GetSpellByIdAsync(id);
            IEnumerable<SpellMap> spellMaps = await _spellService.GetSpellMapsBySpellIdAsync(id);
            ICollection<ViewModels.Effect.DisplayViewModel> effects = new List<ViewModels.Effect.DisplayViewModel>();

            foreach (Guid effectId in spellMaps.Select(m => m.Effect))
            {
                Effect effect = await _effectService.GetEffectByIdAsync(effectId);
                IEnumerable<EffectMap> effectMaps = await _effectService.GetEffectMapsByKeyAsync(effectId);
                ICollection<ViewModels.Effect.DisplayViewModel> secondaryEffects = new List<ViewModels.Effect.DisplayViewModel>();
                if (effect != null)
                {
                    Affliction affliction = (await _afflictionService.GetAfflictionByIdAsync(effect?.AfflictionId ?? default(Guid)));
                    Tempo duration = (await _tempoService.GetTempoByIdAsync(effect.DurationId));
                    ViewModels.Effect.DisplayViewModel effectViewModel = new ViewModels.Effect.DisplayViewModel
                    {
                        Name = effect.Name,
                        Description = effect.Description,
                        Type = effect.Type,

                        Nature = effect.Nature,
                        Affliction = affliction?.Name,
                        Duration = duration.Value,
                        DurationTiming = duration.Timing.GetAttribute<DisplayAttribute>().Name,
                    };

                    foreach (Guid subEffectId in effectMaps.Select(m => m.Effect))
                    {
                        Effect subEffect = await _effectService.GetEffectByIdAsync(subEffectId);
                        if (subEffect != null)
                        {
                            Affliction subEffectAffliction = (await _afflictionService.GetAfflictionByIdAsync(subEffect?.AfflictionId ?? default(Guid)));
                            Tempo subEffectDuration = (await _tempoService.GetTempoByIdAsync(subEffect.DurationId));
                            ViewModels.Effect.DisplayViewModel subEffectViewModel = new ViewModels.Effect.DisplayViewModel
                            {
                                Name = subEffect.Name,
                                Description = subEffect.Description,
                                Type = subEffect.Type,

                                Nature = subEffect.Nature,
                                Affliction = subEffectAffliction?.Name,
                                Duration = subEffectDuration.Value,
                                DurationTiming = subEffectDuration.Timing.GetAttribute<DisplayAttribute>().Name,
                            };

                            secondaryEffects.Add(subEffectViewModel);
                        }
                    }
                    effectViewModel.Effects = secondaryEffects;
                    effects.Add(effectViewModel);
                }
            }

            DeleteViewModel viewModel = new DeleteViewModel
            {
                Id = spell.Id,
                Name = spell.Name,
                Description = spell.Description,
                Shape = spell.Shape,
                Radius = spell.Radius,
                Range = spell.Range,

                Domain = (await _domainService.GetDomainByIdAsync(spell.Domain)).Name,
                Effects = effects,
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                Spell spell = await _spellService.GetSpellByIdAsync(viewModel.Id);
                IEnumerable<SpellMap> spellMaps = await _spellService.GetSpellMapsBySpellIdAsync(viewModel.Id);
                ICollection<ViewModels.Effect.DisplayViewModel> effects = new List<ViewModels.Effect.DisplayViewModel>();

                foreach (Guid effectId in spellMaps.Select(m => m.Effect))
                {
                    Effect effect = await _effectService.GetEffectByIdAsync(effectId);
                    if (effect != null)
                    {
                        Affliction affliction = (await _afflictionService.GetAfflictionByIdAsync(effect?.AfflictionId ?? default(Guid)));
                        Tempo duration = (await _tempoService.GetTempoByIdAsync(effect.DurationId));
                        ViewModels.Effect.DisplayViewModel effectViewModel = new ViewModels.Effect.DisplayViewModel
                        {
                            Name = effect.Name,
                            Description = effect.Description,
                            Type = effect.Type,

                            Nature = effect.Nature,
                            Affliction = affliction?.Name,
                            Duration = duration.Value,
                            DurationTiming = duration.Timing.GetAttribute<DisplayAttribute>().Name,
                        };
                        effects.Add(effectViewModel);
                    }
                }

                viewModel.Domain = (await _domainService.GetDomainByIdAsync(spell.Domain)).Name;
                viewModel.Effects = effects;

                return View(viewModel);
            }

            await _spellService.DeleteSpellAsync(viewModel.Id);
            await _spellService.DeleteSpellMapsBySpellIdAsync(viewModel.Id);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Display(Guid id)
        {
            Spell spell = await _spellService.GetSpellByIdAsync(id);
            IEnumerable<SpellMap> spellMaps = await _spellService.GetSpellMapsBySpellIdAsync(id);
            ICollection<ViewModels.Effect.DisplayViewModel> effects = new List<ViewModels.Effect.DisplayViewModel>();

            foreach (Guid effectId in spellMaps.Select(m => m.Effect))
            {
                Effect effect = await _effectService.GetEffectByIdAsync(effectId);
                IEnumerable<EffectMap> effectMaps = await _effectService.GetEffectMapsByKeyAsync(effectId);
                ICollection<ViewModels.Effect.DisplayViewModel> secondaryEffects = new List<ViewModels.Effect.DisplayViewModel>();
                if (effect != null)
                {
                    Affliction affliction = (await _afflictionService.GetAfflictionByIdAsync(effect?.AfflictionId ?? default(Guid)));
                    Tempo duration = (await _tempoService.GetTempoByIdAsync(effect.DurationId));
                    ViewModels.Effect.DisplayViewModel effectViewModel = new ViewModels.Effect.DisplayViewModel
                    {
                        Name = effect.Name,
                        Description = effect.Description,
                        Type = effect.Type,

                        Nature = effect.Nature,
                        Affliction = affliction?.Name,
                        Duration = duration.Value,
                        DurationTiming = duration.Timing.GetAttribute<DisplayAttribute>().Name,
                    };

                    foreach (Guid subEffectId in effectMaps.Select(m => m.Effect))
                    {
                        Effect subEffect = await _effectService.GetEffectByIdAsync(subEffectId);
                        if (subEffect != null)
                        {
                            Affliction subEffectAffliction = (await _afflictionService.GetAfflictionByIdAsync(subEffect?.AfflictionId ?? default(Guid)));
                            Tempo subEffectDuration = (await _tempoService.GetTempoByIdAsync(subEffect.DurationId));
                            ViewModels.Effect.DisplayViewModel subEffectViewModel = new ViewModels.Effect.DisplayViewModel
                            {
                                Name = subEffect.Name,
                                Description = subEffect.Description,
                                Type = subEffect.Type,

                                Nature = subEffect.Nature,
                                Affliction = subEffectAffliction?.Name,
                                Duration = subEffectDuration.Value,
                                DurationTiming = subEffectDuration.Timing.GetAttribute<DisplayAttribute>().Name,
                            };

                            secondaryEffects.Add(subEffectViewModel);
                        }
                    }
                    effectViewModel.Effects = secondaryEffects;

                    effects.Add(effectViewModel);
                }
            }

            DisplayViewModel viewModel = new DisplayViewModel
            {
                Id = spell.Id,
                Name = spell.Name,
                Description = spell.Description,
                Shape = spell.Shape,

                Domain = (await _domainService.GetDomainByIdAsync(spell.Domain)).Name,
                Effects = effects,
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            Spell spell = await _spellService.GetSpellByIdAsync(id);
            IEnumerable<SpellMap> spellMaps = await _spellService.GetSpellMapsBySpellIdAsync(id);

            EditViewModel viewModel = new EditViewModel
            {
                Id = spell.Id,
                Name = spell.Name,
                Description = spell.Description,
                Shape = spell.Shape,
                Domain = spell.Domain,
                Effects = spellMaps.Select(m => m.Effect),
                Domains = (await _domainService.GetDomainsAsync()).Select(
                    d => new SelectListItem { Text = d.Name, Value = d.Id.ToString() }),
                Tempos = (await _tempoService.GetTemposAsync()).Select(
                    t => new SelectListItem { Text = $"{t.Value} {t.Timing.GetAttribute<DisplayAttribute>().Name}", Value = t.Id.ToString() }),
                EffectList = (await _effectService.GetEffectsAsync()).Select(
                    e => new SelectListItem { Text = e.Name, Value = e.Id.ToString() }),
            };

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

        [HttpPost]
        public async Task<IActionResult> Edit(EditViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Domains = (await _domainService.GetDomainsAsync()).Select(
                    d => new SelectListItem { Text = d.Name, Value = d.Id.ToString() });
                viewModel.Tempos = (await _tempoService.GetTemposAsync()).Select(
                    t => new SelectListItem { Text = $"{t.Value} {t.Timing.GetAttribute<DisplayAttribute>().Name}", Value = t.Id.ToString() });
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

            if (await _spellService.EditSpellAsync(viewModel.Id, viewModel.Name, viewModel.Description,
                viewModel.Shape, viewModel.Radius, viewModel.Range, viewModel.Domain,
                viewModel.CastingSpeed, viewModel.RecastSpeed, viewModel.RecoverySpeed))
            {
                await _spellService.DeleteSpellMapsBySpellIdAsync(viewModel.Id);

                foreach (var effect in viewModel.Effects)
                {
                    await _spellService.CreateSpellMapAsync(viewModel.Id, effect);
                }
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IndexViewModel viewModel = new IndexViewModel()
            {
                Spells = await _spellService.GetSpellsAsync(),
            };

            return View(viewModel);
        }
    }
}