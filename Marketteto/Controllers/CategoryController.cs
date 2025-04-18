﻿using Marketteto.Data.Services;
using Marketteto.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Marketteto.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _service;
        public CategoryController(ICategoryService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var res = await _service.GetAllAsync();
            return View(res);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Name,Description")]Category category)
        {
            if (ModelState.IsValid)
            {
                await _service.CreateAsync(category);
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }
        public async Task<IActionResult> Details(int Id)
        {
            var res = await _service.GetByIdAsync(Id);
            if(res != null)
            {
                return View(res);
            }
            return View("NotFound");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var res = await _service.GetByIdAsync(id);
            if (res != null)
            {
                return View(res);
            }
            return View("NotFound");
        }
        [HttpPost]
        public async Task<IActionResult> Edit([Bind("Name,Description")]Category category)
        {
            var id = await _service.GetByIdAsync(category.Id);
            if (ModelState.IsValid && id != null)
            {
                await _service.UpdateAysnc(category);
                return RedirectToAction(nameof(Index));
            }
            return View("NotFound");
        }
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
