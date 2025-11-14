using Microsoft.EntityFrameworkCore;
using Trendify.Data;
using Trendify.Models;

namespace Trendify.Services
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;
        private readonly ICartService _cartService;

        public OrderService(ApplicationDbContext context, ICartService cartService)
        {
            _context = context;
            _cartService = cartService;
        }

        public async Task<Order> CreateOrderAsync(Order order, string userId)
        {
            // Get cart items
            var cartItems = await _cartService.GetCartItemsAsync(userId);

            if (!cartItems.Any())
                throw new Exception("Cart is empty");

            // Check stock availability
            foreach (var cartItem in cartItems)
            {
                var product = await _context.Products.FindAsync(cartItem.ProductId);
                if (product == null)
                    throw new Exception($"Product not found: {cartItem.ProductId}");

                if (product.StockQuantity < cartItem.Quantity)
                {
                    throw new Exception($"Not enough stock for {product.Name}. Available: {product.StockQuantity}, Requested: {cartItem.Quantity}");
                }
            }

            // Calculate total
            order.TotalAmount = cartItems.Sum(ci => ci.Quantity * ci.Product.Price);
            order.UserId = userId;
            order.OrderDate = DateTime.Now;
            order.Status = "Pending"; // Default status

            // Create order
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            // Create order details and update stock
            foreach (var cartItem in cartItems)
            {
                var orderDetail = new OrderDetail
                {
                    OrderId = order.Id,
                    ProductId = cartItem.ProductId,
                    Quantity = cartItem.Quantity,
                    UnitPrice = cartItem.Product.Price
                };
                _context.OrderDetails.Add(orderDetail);

                // Update product stock
                var product = await _context.Products.FindAsync(cartItem.ProductId);
                if (product != null)
                {
                    product.StockQuantity -= cartItem.Quantity;
                }
            }

            // Clear cart
            await _cartService.ClearCartAsync(userId);
            await _context.SaveChangesAsync();

            return order;
        }

        public async Task<List<Order>> GetUserOrdersAsync(string userId)
        {
            return await _context.Orders
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
                .Where(o => o.UserId == userId)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();
        }

        public async Task<List<Order>> GetOrdersByStatusAsync(string userId, string status)
        {
            return await _context.Orders
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
                .Where(o => o.UserId == userId && o.Status == status)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();
        }

        public async Task<bool> UpdateOrderStatusAsync(int orderId, string status)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order != null)
            {
                order.Status = status;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            return await _context.Orders
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
                .FirstOrDefaultAsync(o => o.Id == orderId);
        }
    }
}