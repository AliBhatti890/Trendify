using Microsoft.AspNetCore.Mvc;
using Trendify.Services;

namespace Trendify.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductsController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index(int? categoryId, string search)
        {
            ViewBag.Categories = await _categoryService.GetAllCategoriesAsync();
            ViewBag.SelectedCategory = categoryId;
            ViewBag.SearchTerm = search;

            if (!string.IsNullOrEmpty(search))
            {
                var searchResults = await _productService.SearchProductsAsync(search);
                return View(searchResults);
            }

            if (categoryId.HasValue)
            {
                var categoryProducts = await _productService.GetProductsByCategoryAsync(categoryId.Value);
                return View(categoryProducts);
            }

            var allProducts = await _productService.GetAllProductsAsync();
            return View(allProducts);
        }

        public async Task<IActionResult> Details(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
    }
}