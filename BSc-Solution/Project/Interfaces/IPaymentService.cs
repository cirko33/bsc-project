using PayPal.Api;

namespace Project.Interfaces
{
    public interface IPaymentService
    {
        Task<string> CreatePayPalPayment(int productId, int userId);
        Task<Payment> SuccessPayPalPayment(string paymentId, string payerId, int orderId);
        Task CancelPayPalPayment(int orderId);
    }
}
