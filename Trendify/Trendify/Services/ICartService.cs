using Trendify.Models;

namespace Trendify.Services
{
    public interface ICartService
    {
        Task<List<CartItem>> GetCartItemsAsync(string userId);
        Task AddToCartAsync(string userId, int productId, int quantity);
        Task UpdateCartItemQuantityAsync(int cartItemId, int quantity);
        Task RemoveFromCartAsync(int cartItemId);
        Task ClearCartAsync(string userId);
        Task<int> GetCartItemCountAsync(string userId);
        Task<decimal> GetCartTotalAsync(string userId);
    }
}