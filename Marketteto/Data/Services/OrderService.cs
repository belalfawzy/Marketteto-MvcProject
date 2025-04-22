using Marketteto.Models;
using Microsoft.EntityFrameworkCore;

namespace Marketteto.Data.Services
{
    public class OrderService : IOrderService
    {
        private readonly MarkettetoDbContext _context;
        public OrderService(MarkettetoDbContext context)
        {
            _context = context;
        }
        public async Task<List<Order>> GetOrderAndRoleByUserIdAsync(string userId, string role)
        {
            var order = await _context.Orders.Include(i => i.OrderItems)
            .ThenInclude(i => i.Product).Include(i=>i.appUser)
            .ToListAsync();
            if (role != "Admin")
            {
                order = order.Where(i => i.UserId == userId).ToList();
            }
            return order;
        }

        public async Task StoreOrdersAsync(List<ShoppingCardItem> items, string userId)
        {
            Order order = new Order()
            {
                UserId = userId
            };
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            foreach(var item in items)
            {
                var orderItem = new OrderItem()
                {
                    Amount = item.Amount,
                    TotalPrice = item.product.Price,
                    OrderId = order.Id,
                    productId = item.product.Id
                };
                await _context.OrderItems.AddAsync(orderItem);
            }
            await _context.SaveChangesAsync();
        }
    }
}
