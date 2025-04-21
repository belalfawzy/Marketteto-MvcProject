﻿using Marketteto.Data.Base;

namespace Marketteto.Models
{
    public class OrderItem : IBaseEntity
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public decimal TotalPrice { get; set; }
        public int OrderId { get; set; }
        public int productId { get; set; }
        public Order Order { get; set; }
        public Product Product { get; set; }

    }
}
