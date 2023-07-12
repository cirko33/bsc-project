using Project.Models;

namespace Project.DTOs
{
    public class BuyerOrderDTO
    {
        public DateTime OrderTime { get; set; }
        public double? Price { get; set; }
        public ProductDTO? Product { get; set; }
        public ProductKeyDTO? ProductKey { get; set; }
        public OrderState State { get; set; }
    }
}
