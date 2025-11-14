using System.ComponentModel.DataAnnotations;

namespace Trendify.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public List<Product> Products { get; set; } = new List<Product>();
    }
}