using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public class ProductKey : BaseClass
    {
        [Required]
        public int ProductId { get; set; }
        public Product? Product { get; set; }
        [Required]
        public string? Key { get; set; }

        public bool Sold { get; set; } = false;
    }
}
