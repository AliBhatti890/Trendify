using Trendify.Models;

namespace Trendify.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetFeaturedProductsAsync();
        Task<List<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(int id);
        Task<List<Product>> GetProductsByCategoryAsync(int categoryId);
        Task<List<Category>> GetAllCategoriesAsync();
        Task<List<Product>> SearchProductsAsync(string searchTerm);
    }
}