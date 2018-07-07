using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SpellByte.Web.ViewModels.Nature;

namespace SpellByte.Web.Controllers
{
    public class NatureController : Controller
    {
        private readonly INatureService _natureService;
        public NatureController(INatureService natureService)
        {
            _natureService = natureService;
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

            await _natureService.CreateNatureAsync(viewModel.Name);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == default(Guid)) return BadRequest();

            Nature nature = await _natureService.GetNatureByIdAsync(id);
            DeleteViewModel viewModel = new DeleteViewModel
            {
                Id = nature.Id,
                Name = nature.Name,
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);

            await _natureService.DeleteNatureAsync(viewModel.Id);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == default(Guid)) return BadRequest();

            Nature nature = await _natureService.GetNatureByIdAsync(id);
            EditViewModel viewModel = new EditViewModel
            {
                Id = nature.Id,
                Name = nature.Name,
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);

            await _natureService.EditNatureAsync(viewModel.Id, viewModel.Name);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IndexViewModel viewModel = new IndexViewModel
            {
                Natures = await _natureService.GetNaturesAsync(),
            };
            return View(viewModel);
        }
    }
}