using Marketteto.Models;

namespace Marketteto.Data.Services
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAllAsync();
        Task<Category> GetByIdAsync(int id);
        Task CreateAsync(Category category);
        Task UpdateAysnc(Category category);
        Task DeleteAsync(int id);

    }
}
