using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public class Order : BaseClass
    {
        [Required]
        public DateTime OrderTime { get; set; }
        [Required]
        public int BuyerId { get; set; }
        public User? Buyer { get; set; }
        [Required]
        public int ProductKeyId { get; set; }
        public ProductKey? ProductKey { get; set; }
        [Required]
        public double? Price { get; set; }
    }
}
