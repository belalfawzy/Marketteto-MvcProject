﻿using Marketteto.Models;

namespace Marketteto.Data.Services
{
    public interface IOrderService
    {
        Task StoreOrdersAsync(List<ShoppingCardItem> items, string userId);
        Task<List<Order>> GetOrderAndRoleByUserIdAsync(string userId,string role);
    }
}
