using Marketteto.Data.Card;
using Marketteto.Data.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier), 
                roleId = User.FindFirstValue(ClaimTypes.Role);
            var order = await _orderService.GetOrderAndRoleByUserIdAsync(userId,roleId);
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
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var items = _shoppingCard.GetShoppingCardItems();
            await _orderService.StoreOrdersAsync(items, userId);
            _shoppingCard.ClearShoppingCard();
            return View("CompleteOrder");
        }
        
    }
}
