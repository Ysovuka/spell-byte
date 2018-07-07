using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SpellByte.Web.ViewModels.Domain;

namespace SpellByte.Web.Controllers
{

    public class DomainController : Controller
    {
        private readonly IDomainService _domainService;
        public DomainController(IDomainService domainService)
        {
            _domainService = domainService;
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

            await _domainService.CreateDomainAsync(viewModel.Name);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == default(Guid)) return BadRequest();

            Domain domain = await _domainService.GetDomainByIdAsync(id);
            DeleteViewModel viewModel = new DeleteViewModel
            {
                Id = domain.Id,
                Name = domain.Name,
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);

            await _domainService.DeleteDomainAsync(viewModel.Id);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == default(Guid)) return BadRequest();

            Domain domain = await _domainService.GetDomainByIdAsync(id);
            EditViewModel viewModel = new EditViewModel
            {
                Id = domain.Id,
                Name = domain.Name,
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);

            await _domainService.EditDomainAsync(viewModel.Id, viewModel.Name);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult>Index()
        {
            IndexViewModel viewModel = new IndexViewModel()
            {
                Domains = await _domainService.GetDomainsAsync(),
            };
            return View(viewModel);
        }
    }
}