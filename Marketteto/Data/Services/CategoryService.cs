using Marketteto.Data.Base;
using Marketteto.Models;
using Microsoft.EntityFrameworkCore;

namespace Marketteto.Data.Services
{
    public class CategoryService : EntityBaseRepository<Category>, ICategoryService
    {

        public CategoryService(MarkettetoDbContext context) : base(context)
        {

        }
    }
}
