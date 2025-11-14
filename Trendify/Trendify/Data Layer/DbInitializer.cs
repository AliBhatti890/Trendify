using Trendify.Models;

namespace Trendify.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            if (!context.Categories.Any())
            {
                var categories = new Category[]
                {
                    new Category { Name = "Shoes", Description = "Footwear for all occasions", ImageUrl = "/images/categories/shoes.jpg" },
                    new Category { Name = "Shirts", Description = "Casual and formal shirts", ImageUrl = "/images/categories/shirts.jpg" },
                    new Category { Name = "Jeans", Description = "Denim jeans collection", ImageUrl = "/images/categories/jeans.jpg" },
                    new Category { Name = "Jewelry", Description = "Elegant jewelry pieces", ImageUrl = "/images/categories/jewelry.jpg" },
                    new Category { Name = "Watches", Description = "Luxury and casual watches", ImageUrl = "/images/categories/watches.jpg" },
                    new Category { Name = "Accessories", Description = "Fashion accessories", ImageUrl = "/images/categories/accessories.jpg" }
                };

                context.Categories.AddRange(categories);
                context.SaveChanges();
            }

            if (!context.Products.Any())
            {
                var shoes = context.Categories.First(c => c.Name == "Shoes");
                var shirts = context.Categories.First(c => c.Name == "Shirts");
                var jeans = context.Categories.First(c => c.Name == "Jeans");
                var jewelry = context.Categories.First(c => c.Name == "Jewelry");
                var watches = context.Categories.First(c => c.Name == "Watches");

                var products = new Product[]
                {
                    // Shoes
                    new Product {
                        Name = "Nike Air Max 270",
                        Description = "Comfortable running shoes with Air Max technology",
                        Price = 129.99m,
                        CategoryId = shoes.Id,
                        StockQuantity = 50,
                        ImageUrl = "/images/products/shoes1.jpg",
                        IsFeatured = true,
                        Brand = "Nike",
                        Size = "9,10,11,12",
                        Color = "Black/White"
                    },
                    new Product {
                        Name = "Adidas Ultraboost",
                        Description = "Premium running shoes with Boost technology",
                        Price = 179.99m,
                        CategoryId = shoes.Id,
                        StockQuantity = 30,
                        ImageUrl = "/images/products/shoes2.jpg",
                        Brand = "Adidas",
                        Size = "8,9,10,11",
                        Color = "White"
                    },
                    
                    // Shirts
                    new Product {
                        Name = "Classic Cotton T-Shirt",
                        Description = "100% cotton comfortable t-shirt",
                        Price = 24.99m,
                        CategoryId = shirts.Id,
                        StockQuantity = 100,
                        ImageUrl = "/images/products/shirt1.jpg",
                        IsFeatured = true,
                        Brand = "Basic Wear",
                        Size = "S,M,L,XL",
                        Color = "White"
                    },
                    new Product {
                        Name = "Formal Business Shirt",
                        Description = "Elegant formal shirt for office wear",
                        Price = 49.99m,
                        CategoryId = shirts.Id,
                        StockQuantity = 40,
                        ImageUrl = "/images/products/shirt2.jpg",
                        Brand = "Executive",
                        Size = "M,L,XL",
                        Color = "Light Blue"
                    },
                    
                    // Jeans
                    new Product {
                        Name = "Slim Fit Denim Jeans",
                        Description = "Modern slim fit denim jeans",
                        Price = 69.99m,
                        CategoryId = jeans.Id,
                        StockQuantity = 60,
                        ImageUrl = "/images/products/jeans1.jpg",
                        IsFeatured = true,
                        Brand = "Denim Co",
                        Size = "30,32,34,36",
                        Color = "Dark Blue"
                    },
                    
                    // Jewelry
                    new Product {
                        Name = "Silver Pendant Necklace",
                        Description = "Elegant silver necklace with diamond pendant",
                        Price = 129.99m,
                        CategoryId = jewelry.Id,
                        StockQuantity = 25,
                        ImageUrl = "/images/products/jewelry1.jpg",
                        Brand = "SilverCraft",
                        Color = "Silver"
                    },
                    
                    // Watches
                    new Product {
                        Name = "Classic Leather Watch",
                        Description = "Luxury watch with genuine leather strap",
                        Price = 199.99m,
                        CategoryId = watches.Id,
                        StockQuantity = 20,
                        ImageUrl = "/images/products/watch1.jpg",
                        IsFeatured = true,
                        Brand = "TimeMaster",
                        Color = "Brown"
                    }
                };

                context.Products.AddRange(products);
                context.SaveChanges();
            }
        }
    }
}