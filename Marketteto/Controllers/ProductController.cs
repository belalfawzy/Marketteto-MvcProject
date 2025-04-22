using Marketteto.Data.Services;
using Marketteto.Data.StaticMember;
using Marketteto.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace Marketteto.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class ProductController : Controller
    {
        private readonly IProductService _service;
        private readonly ICategoryService _Categoryservice;
        public ProductController(IProductService service, ICategoryService categoryservice)
        {
            _service = service;
            _Categoryservice = categoryservice;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index(string SearchTerm)
        {
            var res = await _service.GetAllAsync(i => i.category);
            if (!string.IsNullOrEmpty(SearchTerm))
            {
                res = res.Where(x => x.Name.Contains(SearchTerm)).ToList();
            }
            return View(res);
        }
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var entity = await _service.GetByIdAsync(id,i=>i.category);
            if(entity != null)
            {
                return View(entity);
            }
            return View("NotFound");
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Category = await _Categoryservice.GetAllAsync();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Name", "Description","Color","Price","Image","CategoryId")]Product product)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Category = await _Categoryservice.GetAllAsync();
                return View(product);
            }

            await _service.CreateAsync(product);
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _service.GetByIdAsync(id,x=>x.category);
            ViewBag.Category = await _Categoryservice.GetAllAsync();
            if(product != null)
            {
                return View(product);
            }
            return View("NotFound");
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                await _service.UpdateAysnc(product);
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _service.GetByIdAsync(id);
            if(product != null)
            {
                await _service.DeleteAsync(id);
                return RedirectToAction(nameof(Index));
            }
            return View("NotFound");
        }
    }
}
