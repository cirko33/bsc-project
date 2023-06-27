using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public class Product : BaseClass
    {
        [Required, MaxLength(100)]
        public string? Name { get; set; }
        [Range(0, 100)]
        public int Discount { get; set; } = 0;
        [Required]
        public double Price { get; set; }
        [Required, MaxLength(200)]
        public string? Description { get; set; }
        public string? Image { get; set; }
        [Required]
        public int SellerId { get; set; }
        public User? Seller { get; set; }
        public List<ProductKey>? Keys { get; set; } = new List<ProductKey>();
    }
}
