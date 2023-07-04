using System.ComponentModel.DataAnnotations;

namespace Project.DTOs
{
    public class ProductDTO : CreateProductDTO
    {
        [Required]
        public int Id { get; set; }
        public string? Image { get; set; }
        public int Amount { get; set; } = 0;
        public int SellerId { get; set; }
        public UserShortDTO? Seller { get; set; }
    }
}
