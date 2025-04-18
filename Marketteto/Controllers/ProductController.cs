using Marketteto.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Marketteto.Controllers
{
    public class ProductController : Controller
    {
        private readonly MarkettetoDbContext _context;
        public ProductController(MarkettetoDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var res = await _context.Products.ToListAsync();
            return View();
        }
    }
}
