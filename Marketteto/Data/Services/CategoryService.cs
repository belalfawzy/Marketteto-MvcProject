using Marketteto.Models;
using Microsoft.EntityFrameworkCore;

namespace Marketteto.Data.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly MarkettetoDbContext _context;
        public CategoryService(MarkettetoDbContext context)
        {
            _context = context;
        }
        public async Task CreateAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var CatId = await _context.Categories.FirstOrDefaultAsync(i => i.Id == id);
            if(CatId != null)
            {
                _context.Categories.Remove(CatId);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Category>> GetAllAsync() =>
            await _context.Categories.ToListAsync();
       
        public async Task<Category> GetByIdAsync(int id)
        =>  await _context.Categories.FirstOrDefaultAsync(i => i.Id == id);

        public async Task UpdateAysnc(Category category)
        {
            var id = await _context.Categories.FirstOrDefaultAsync(i => i.Id == category.Id);
            if(id != null)
            {
                id.Id = category.Id;
                id.Description = category.Description;
                id.Name = category.Name;
                await _context.SaveChangesAsync();
            }
        }
        
    }
}
