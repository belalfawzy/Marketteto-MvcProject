using Marketteto.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Marketteto.Data.Card
{
    public class ShoppingCard
    {
        private readonly MarkettetoDbContext _context;
        public string shoppingCardId { get; set; } 
        public ShoppingCard(MarkettetoDbContext context)
        {
            _context = context;
        }
        public static ShoppingCard GetCard(IServiceProvider service)
        {
            var session = service.GetRequiredService<IHttpContextAccessor>().HttpContext.Session;
            var context = service.GetRequiredService<MarkettetoDbContext>();

            string cardId = session.GetString("cardId");

            if (string.IsNullOrEmpty(cardId))
            {
                cardId = Guid.NewGuid().ToString();
                session.SetString("cardId", cardId); 
            }

            return new ShoppingCard(context)
            {
                shoppingCardId = cardId
            };
        }
        public List<ShoppingCardItem> GetShoppingCardItems() 
            => _context.shoppingCardItems
                .Where(i=>i.ShoppingCardId == shoppingCardId).Include(i=>i.product).ToList();
        public Decimal GetShoppingCardTotal()
            => _context.shoppingCardItems.Where(i => i.ShoppingCardId == shoppingCardId)
            .Select(p => p.Amount * p.product.Price).Sum();
        public async Task AddItemToCard(Product product)
        {
            var shoppingCardItem = await _context.shoppingCardItems
                .FirstOrDefaultAsync(x => x.ShoppingCardId == shoppingCardId && x.product.Id == product.Id);
            if(shoppingCardItem == null)
            {
                shoppingCardItem = new ShoppingCardItem()
                {
                    ShoppingCardId = shoppingCardId,
                    Amount = 1,
                    product = product
                };
                await _context.shoppingCardItems.AddAsync(shoppingCardItem);
            }
            else
                shoppingCardItem.Amount++;

            await _context.SaveChangesAsync();
        }
        public async Task RemoveItemFromShoppingCard(Product product)
        {
            var shoppingCardItem = await _context.shoppingCardItems
                .FirstOrDefaultAsync(x => x.ShoppingCardId == shoppingCardId && x.product.Id == product.Id);
            if(shoppingCardItem != null)
                if (shoppingCardItem.Amount > 1)
                    shoppingCardItem.Amount--;
                else
                 _context.shoppingCardItems.Remove(shoppingCardItem);

            await _context.SaveChangesAsync();
        }
        public int GetShoppingCardTotalAmount()
            => _context.shoppingCardItems.Where(i => i.ShoppingCardId == shoppingCardId).Select(i => i.Amount).Sum();
        public void ClearShoppingCard()
        {
            var items =  _context.shoppingCardItems.Where(i => i.ShoppingCardId == shoppingCardId).ToList();
            _context.shoppingCardItems.RemoveRange(items);
            _context.SaveChanges();
            return;
        }
    }
}
