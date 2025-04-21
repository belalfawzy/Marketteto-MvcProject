using Marketteto.Data.Base;

namespace Marketteto.Models
{
    public class ShoppingCardItem : IBaseEntity
    {
        public int Id { get; set; }
        public Product product { get; set; }
        public int Amount { get; set; }
        public string ShoppingCardId { get; set; }
    }
}
