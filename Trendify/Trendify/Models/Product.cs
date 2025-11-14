using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trendify.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public string ImageUrl { get; set; } = "/images/default-product.png";

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public int StockQuantity { get; set; }

        public bool IsFeatured { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public bool IsActive { get; set; } = true;

        [StringLength(20)]
        public string Size { get; set; }

        [StringLength(30)]
        public string Color { get; set; }

        [StringLength(50)]
        public string Brand { get; set; }
    }
}   