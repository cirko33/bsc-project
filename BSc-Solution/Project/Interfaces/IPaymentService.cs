using PayPal.Api;
using Nethereum.Hex.HexTypes;
using Nethereum.Web3;
using Project.DTOs;
using Nethereum.RPC.Eth.DTOs;

namespace Project.Interfaces
{
    public interface IPaymentService
    {
        Task<string> CreatePayPalPayment(int productId, int userId, string currentAddress);
        Task<Payment> SuccessPayPalPayment(string paymentId, string payerId, int orderId);
        Task CancelPayPalPayment(int orderId);
        Task<EthereumPaymentDTO> CreateEthereumPayment(int productId, int userId);
        Task CancelEthereumPayment(int orderId);
        Task CheckEthereumPayment(string transactionHash);
    }
}
