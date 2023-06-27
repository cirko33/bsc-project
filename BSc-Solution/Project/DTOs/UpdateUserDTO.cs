using System.ComponentModel.DataAnnotations;

namespace Project.DTOs
{
    public class UpdateUserDTO
    {
        [Required, MaxLength(100), RegularExpression("[a-zA-Z0-9]+")]
        public string? Username { get; set; }
        [MaxLength(100)]
        public string? Password { get; set; }
        [MaxLength(100)]
        public string? NewPassword { get; set; }
        [Required, MaxLength(100), EmailAddress]
        public string? Email { get; set; }
        [Required, MaxLength(100)]
        public string? FullName { get; set; }
        [Required]
        public DateTime Birthday { get; set; }
        [Required, MaxLength(100)]
        public string? Address { get; set; }
        public string? Image { get; set; }
        public IFormFile? ImageFile { get; set; }
    }
}
