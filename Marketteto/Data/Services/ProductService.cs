using Marketteto.Data.Base;
using Marketteto.Models;

namespace Marketteto.Data.Services
{
    public class ProductService : EntityBaseRepository<Product> , IProductService
    {
        public ProductService(MarkettetoDbContext context) : base(context)
        {
            
        }
    }
}
