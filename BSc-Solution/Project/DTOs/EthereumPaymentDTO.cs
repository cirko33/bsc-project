using System.Numerics;

namespace Project.DTOs
{
    public class EthereumPaymentDTO
    {
        public string? To { get; set; }
        public string? Value { get; set; }
        public string? Input { get; set; }
        public int? OrderId { get; set; }
    }
}
