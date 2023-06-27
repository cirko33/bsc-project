using System.ComponentModel.DataAnnotations;

namespace Project.DTOs
{
    public class TokenDTO
    {
        [Required]
        public string? Token { get; set; }
    }
}
