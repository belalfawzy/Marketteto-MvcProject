using Marketteto.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Marketteto.Controllers
{
    public class CategoryController : Controller
    {
        private readonly MarkettetoDbContext _context;
        public CategoryController(MarkettetoDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var res = await _context.Categories.ToListAsync();
            return View();
        }
    }
}
