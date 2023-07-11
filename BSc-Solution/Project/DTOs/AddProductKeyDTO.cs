using System.ComponentModel.DataAnnotations;

namespace Project.DTOs
{
    public class AddProductKeyDTO
    {
        [Required]
        public int ProductId { get; set; }
        [Required]
        public string? Key { get; set; }
    }
}
