using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Trendify.Services;
using Trendify.Models;

namespace Trendify.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;
        private readonly IUserService _userService;

        public CartController(ICartService cartService, IProductService productService, IOrderService orderService, IUserService userService)
        {
            _cartService = cartService;
            _productService = productService;
            _orderService = orderService;
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(Order order)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                    // Get user email
                    var userEmail = await _userService.GetUserEmailAsync(userId);
                    order.Email = userEmail ?? User.Identity.Name;

                    // Create the order
                    var createdOrder = await _orderService.CreateOrderAsync(order, userId);

                    TempData["Success"] = $"Order placed successfully! Your order ID is: {createdOrder.Id}";
                    return RedirectToAction("OrderConfirmation", new { id = createdOrder.Id });
                }
                catch (Exception ex)
                {
                    TempData["Error"] = $"Error placing order: {ex.Message}";
                }
            }

            // If we got this far, something failed; redisplay form
            var userId2 = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cartItems = await _cartService.GetCartItemsAsync(userId2);
            ViewBag.CartItems = cartItems;
            ViewBag.CartTotal = await _cartService.GetCartTotalAsync(userId2);
            return View(order);
        }

        // ... rest of your CartController methods
    }
}