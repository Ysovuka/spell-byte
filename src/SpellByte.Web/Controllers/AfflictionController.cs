using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SpellByte.Web.ViewModels.Affliction;

namespace SpellByte.Web.Controllers
{
    public class AfflictionController : Controller
    {
        private readonly IAfflictionService _afflictionService;
        public AfflictionController(IAfflictionService afflictionService)
        {
            _afflictionService = afflictionService;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);

            await _afflictionService.CreateAfflictionAsync(viewModel.Name);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == default(Guid)) return BadRequest();

            Affliction Affliction = await _afflictionService.GetAfflictionByIdAsync(id);
            DeleteViewModel viewModel = new DeleteViewModel
            {
                Id = Affliction.Id,
                Name = Affliction.Name,
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);

            await _afflictionService.DeleteAfflictionAsync(viewModel.Id);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == default(Guid)) return BadRequest();

            Affliction Affliction = await _afflictionService.GetAfflictionByIdAsync(id);
            EditViewModel viewModel = new EditViewModel
            {
                Id = Affliction.Id,
                Name = Affliction.Name,
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);

            await _afflictionService.EditAfflictionAsync(viewModel.Id, viewModel.Name);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IndexViewModel viewModel = new IndexViewModel
            {
                Afflictions = await _afflictionService.GetAfflictionsAsync(),
            };
            return View(viewModel);
        }
    }
}