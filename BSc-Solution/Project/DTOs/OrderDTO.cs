using System.ComponentModel.DataAnnotations;

namespace Project.DTOs
{
    public class OrderDTO
    {
        public DateTime OrderTime { get; set; }
        public double? Price { get; set; }
        public UserShortDTO? Buyer { get; set; }
        public ProductKeyDTO? ProductKey { get; set; }
    }
}
