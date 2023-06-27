using System.ComponentModel.DataAnnotations;

namespace Project.DTOs
{
    public class ProductDTO : CreateProductDTO
    {
        public string? Image { get; set; }
        public int Amount { get; set; }
        [Required]
        public int Id { get; set; }
        public int SellerId { get; set; }
        public UserShortDTO? Seller { get; set; }
    }
}
