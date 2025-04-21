using Marketteto.Data.Card;
using Marketteto.Data.Services;
using Microsoft.AspNetCore.Mvc;

namespace Marketteto.Controllers
{
    public class OrderController : Controller
    {
        private readonly IProductService _service;
        private readonly ShoppingCard _shoppingCard;
        private readonly IOrderService _orderService;

        public OrderController(IProductService service, ShoppingCard shoppingCard,IOrderService orderService)
        {
            _service = service;
            _shoppingCard = shoppingCard;
            _orderService = orderService;
        }
        public async Task<IActionResult> ListAllOrders()
        {
            var order = await _orderService.GetOrderByUserIdAsync("");
            return View(order);
        }
        public IActionResult Index()
        {
            var res = _shoppingCard.GetShoppingCardItems();
            ViewBag.Total = _shoppingCard.GetShoppingCardTotal();
            return View(res);
        }
        public async Task<IActionResult> AddToCard(int id)
        {
            var item = await _service.GetByIdAsync(id);
            if(item != null)
            {
                await _shoppingCard.AddItemToCard(item);
            }
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> RemoveItemFromCard(int id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item != null)
            {
                await _shoppingCard.RemoveItemFromShoppingCard(item);
            }
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> CompleteOrder()
        {
            var items = _shoppingCard.GetShoppingCardItems();
            await _orderService.StoreOrdersAsync(items,"");
            _shoppingCard.ClearShoppingCard();
            return View("CompleteOrder");
        }
    }
}
