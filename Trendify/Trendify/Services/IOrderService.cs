using Trendify.Models;

namespace Trendify.Services
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(Order order, string userId);
        Task<List<Order>> GetUserOrdersAsync(string userId);
        Task<Order> GetOrderByIdAsync(int orderId);
        Task<List<Order>> GetOrdersByStatusAsync(string userId, string status);
        Task<bool> UpdateOrderStatusAsync(int orderId, string status);
    }
}