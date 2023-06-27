using System.ComponentModel.DataAnnotations;

namespace Project.DTOs
{
    public class CreateProductDTO
    {
        [Required, MaxLength(100)]
        public string? Name { get; set; }
        [Required, Range(0, double.MaxValue)]
        public double Price { get; set; }
        [Range(0, 100)]
        public int Discount { get; set; } = 0;
        [Required, MaxLength(200)]
        public string? Description { get; set; }
        public IFormFile? ImageFile { get; set; }
    }
}
