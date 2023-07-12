using PayPal.Api;
using Project.ExceptionMiddleware.Exceptions;
using Project.Interfaces;
using Project.Models;

namespace Project.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOrderService _orderService;
        private readonly IHelperService _helperService;
        public PaymentService(IUnitOfWork unitOfWork, IOrderService orderService, IHelperService helperService)
        {
            _unitOfWork = unitOfWork;
            _orderService = orderService;
            _helperService = helperService;
        }
        public async Task<string> CreatePayPalPayment(int productId, int userId)
        {
            var user = await _unitOfWork.Users.Get(x => x.Id == userId && x.Type == UserType.Buyer)
                ?? throw new UnauthorizedException("User isn't buyer");

            var product = await _unitOfWork.Products.Get(x => x.Id == productId, new() { "Keys" })
                ?? throw new NotFoundException("Product doesn't exist");

            var key = product.Keys!.Find(x => !x.Sold)
                ?? throw new BadRequestException("No product available");

            var order = await _orderService.MakeOrder(productId, userId);

            var _config = ConfigManager.Instance.GetProperties();
            var _accessToken = new OAuthTokenCredential(_config).GetAccessToken();

            var apiContext = new APIContext
            {
                AccessToken = _accessToken,
                Config = ConfigManager.Instance.GetProperties()
            };

            var payment = new Payment
            {
                intent = "sale",
                payer = new Payer { payment_method = "paypal" },
                transactions = new List<Transaction>
                {
                    new Transaction
                    {
                        amount = new Amount
                        {
                            currency = "EUR",
                            total = (product.Price * (1.0 - product.Discount / 100.0)).ToString()
                        },
                        description = $"{product.Name}\n{product.Description}"
                    }
                },
                redirect_urls = new RedirectUrls
                {
                    cancel_url = "https://localhost:5001/api/payment/paypal/cancel/" + order.Id,
                    return_url = "https://localhost:5001/api/payment/paypal/success/" + order.Id,
                }
            };

            var createdPayment = payment.Create(apiContext);

            var approval = createdPayment.links.Find(x => x.rel == "approval_url")
                ?? throw new NotFoundException("No approval link");

            return approval.href;
        }

        public async Task CancelPayPalPayment(int orderId)
        {
            var order = await _unitOfWork.Orders.Get(x => x.Id == orderId, new() { "ProductKey.Product" })
                ?? throw new NotFoundException("No order");

            order.ProductKey!.Sold = false;
            order.State = OrderState.Cancelled;
            _unitOfWork.Orders.Update(order);
            await _unitOfWork.Save();
        }

        public async Task<Payment> SuccessPayPalPayment(string paymentId, string payerId, int orderId)
        {

            var order = await _unitOfWork.Orders.Get(x => x.Id == orderId, new() { "ProductKey.Product", "Buyer" })
                ?? throw new NotFoundException("No order");

            var _config = ConfigManager.Instance.GetProperties();
            var _accessToken = new OAuthTokenCredential(_config).GetAccessToken();

            var apiContext = new APIContext
            {
                AccessToken = _accessToken,
                Config = ConfigManager.Instance.GetProperties()
            };

            var paymentExecution = new PaymentExecution { payer_id = payerId };
            var payment = new Payment { id = paymentId };
            var executedPayment =  await Task.Run(() => payment.Execute(apiContext, paymentExecution));
            order.State = OrderState.Confirmed;
            _unitOfWork.Orders.Update(order);
            await _unitOfWork.Save();
            _ = Task.Run(() => _helperService.SendEmail($"Your key for {order.ProductKey!.Product!.Name}", $"KEY: {order.ProductKey.Key}", order.Buyer!.Email!));
            return executedPayment;
        }
    }
}