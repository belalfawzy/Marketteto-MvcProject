using Marketteto.Data.Base;

namespace Marketteto.Models
{
    public class Order : IBaseEntity
    {
        public Order()
        {
            OrderItems = new List<OrderItem>();
        }
        public int Id { get; set; }
        public string UserId { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}
