using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trendify.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string? UserId { get; set; }

        [Required]
        [StringLength(100)]
        public string CustomerName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(15)]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(200)]
        public string ShippingAddress { get; set; }

        [StringLength(100)]
        public string City { get; set; }

        [StringLength(10)]
        public string ZipCode { get; set; }

        [StringLength(500)]
        public string Notes { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.Now;

        [StringLength(20)]
        public string Status { get; set; } = "Pending";

        [StringLength(20)]
        public string PaymentMethod { get; set; } = "COD";

        public List<OrderDetail>? OrderDetails { get; set; } = new List<OrderDetail>();
    }
}