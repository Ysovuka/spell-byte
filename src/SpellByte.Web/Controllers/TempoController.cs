using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SpellByte.Web.ViewModels.Tempo;

namespace SpellByte.Web.Controllers
{
    public class TempoController : Controller
    {
        private readonly ITempoService _tempoService;
        public TempoController(ITempoService tempoService)
        {
            _tempoService = tempoService;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new CreateViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);

            await _tempoService.CreateTempoAsync(viewModel.Value, viewModel.Timing);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == default(Guid)) return BadRequest();

            Tempo tempo = await _tempoService.GetTempoByIdAsync(id);
            DeleteViewModel viewModel = new DeleteViewModel
            {
                Id = tempo.Id,
                Timing = tempo.Timing,
                Value = tempo.Value,
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);

            await _tempoService.DeleteTempoAsync(viewModel.Id);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == default(Guid)) return BadRequest();

            Tempo tempo = await _tempoService.GetTempoByIdAsync(id);
            EditViewModel viewModel = new EditViewModel
            {
                Id = tempo.Id,
                Timing = tempo.Timing,
                Value = tempo.Value,
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);

            await _tempoService.EditTempoAsync(viewModel.Id, viewModel.Value, viewModel.Timing);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IndexViewModel viewModel = new IndexViewModel()
            {
                Tempos = await _tempoService.GetTemposAsync(),
            };
            return View(viewModel);
        }
    }
}