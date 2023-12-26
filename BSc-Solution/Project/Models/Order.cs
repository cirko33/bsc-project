using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public enum OrderState { Waiting, Confirmed, Cancelled }
    public class Order : BaseClass
    {
        [Required]
        public DateTime OrderTime { get; set; }
        [Required]
        public int BuyerId { get; set; }
        public User? Buyer { get; set; }
        [Required]
        public int ProductKeyId { get; set; }
        public string? UniqueHash { get; set; }
        public ProductKey? ProductKey { get; set; }
        [Required]
        public double? Price { get; set; }
        [Required]
        public OrderState State { get; set; } = OrderState.Waiting;
    }
}
