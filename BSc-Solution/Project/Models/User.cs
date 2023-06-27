using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public enum UserType { Administrator = 0, Seller = 1, Buyer = 2 }
    public class User : BaseClass
    {
        [Required, MaxLength(100), RegularExpression("[a-zA-Z0-9]+")]
        public string? Username { get; set; }
        [Required, MaxLength(300)]
        public string? Password { get; set; }
        [Required, MaxLength(100), EmailAddress]
        public string? Email { get; set; }
        [Required, MaxLength(100)]
        public string? FullName { get; set; }
        [Required]
        public DateTime Birthday { get; set; }
        [Required, MaxLength(200)]
        public string? Address { get; set; }
        [Required]
        public UserType Type { get; set; }
        public string? Image { get; set; }
        [Required]
        public List<Order>? Orders { get; set; }
        [Required]
        public bool Blocked { get; set; } = false;
        public List<Product>? Products { get; set; }
    }
}
