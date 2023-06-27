using Project.Models;
using System.ComponentModel.DataAnnotations;

namespace Project.DTOs
{
    public class CreateOrderDTO
    {
        [Required]
        public int ProductId { get; set; }
    }
}
