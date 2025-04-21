using Marketteto.Data.Card;
using Microsoft.AspNetCore.Mvc;

namespace Marketteto.Data.ViewComponents
{
    public class ShoppingCartSummary : ViewComponent
    {
        private readonly ShoppingCard _card;
        public ShoppingCartSummary(ShoppingCard card)
        {
            _card = card;
        }
        public IViewComponentResult Invoke()
        {
            var res = _card.GetShoppingCardItems();
            ViewBag.Total = _card.GetShoppingCardTotal();
            return View(res.Count);
        }
    }
}
