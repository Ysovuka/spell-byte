using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SpellByte.Web.ViewModels.Infliction;

namespace SpellByte.Web.Controllers
{
    public class InflictionController : Controller
    {
        private readonly IInflictionService _inflictionService;
        public InflictionController(IInflictionService inflictionService)
        {
            _inflictionService = inflictionService;
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

            await _inflictionService.CreateInflictionAsync(viewModel.Name, viewModel.Description);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == default(Guid)) return BadRequest();

            Infliction Infliction = await _inflictionService.GetInflictionByIdAsync(id);
            DeleteViewModel viewModel = new DeleteViewModel
            {
                Id = Infliction.Id,
                Name = Infliction.Name,
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);

            await _inflictionService.DeleteInflictionAsync(viewModel.Id);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == default(Guid)) return BadRequest();

            Infliction Infliction = await _inflictionService.GetInflictionByIdAsync(id);
            EditViewModel viewModel = new EditViewModel
            {
                Id = Infliction.Id,
                Name = Infliction.Name,
                Description = Infliction?.Description
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);

            await _inflictionService.EditInflictionAsync(viewModel.Id, viewModel.Name, viewModel.Description);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IndexViewModel viewModel = new IndexViewModel
            {
                Inflictions = await _inflictionService.GetInflictionsAsync(),
            };
            return View(viewModel);
        }
    }
}