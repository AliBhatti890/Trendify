using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Trendify.Services;
using Trendify.Models;

namespace Trendify.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // GET: /Orders - Order History
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var orders = await _orderService.GetUserOrdersAsync(userId);
            return View(orders);
        }

        // GET: /Orders/Details/5 - Order Details
        public async Task<IActionResult> Details(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var order = await _orderService.GetOrderByIdAsync(id);

            if (order == null || order.UserId != userId)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: /Orders/Status - Orders by status
        public async Task<IActionResult> Status(string status)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var orders = await _orderService.GetOrdersByStatusAsync(userId, status);
            ViewBag.Status = status;
            return View(orders);
        }

        // POST: /Orders/Cancel/5 - Cancel order
        [HttpPost]
        public async Task<IActionResult> Cancel(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var order = await _orderService.GetOrderByIdAsync(id);

            if (order == null || order.UserId != userId)
            {
                return NotFound();
            }

            if (order.Status == "Pending" || order.Status == "Confirmed")
            {
                await _orderService.UpdateOrderStatusAsync(id, "Cancelled");
                TempData["Success"] = "Order cancelled successfully!";
            }
            else
            {
                TempData["Error"] = "This order cannot be cancelled.";
            }

            return RedirectToAction("Details", new { id });
        }
    }
}