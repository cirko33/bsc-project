using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.Web3;
using Nethereum.Util;
using Newtonsoft.Json;
using PayPal.Api;
using Project.DTOs;
using Project.ExceptionMiddleware.Exceptions;
using Project.Interfaces;
using Project.Models;
using System;
using System.Numerics;
using System.Text.Json.Serialization;

namespace Project.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOrderService _orderService;
        private readonly IHelperService _helperService;
        private readonly IConfiguration _configuration;

        public PaymentService(IUnitOfWork unitOfWork, IOrderService orderService, IHelperService helperService, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _orderService = orderService;
            _helperService = helperService;
            _configuration = configuration;
        }

        public async Task<string> CreatePayPalPayment(int productId, int userId, string currentAddress)
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
                    cancel_url = $"https://{currentAddress}/api/payment/paypal/cancel/{order.Id}" ,
                    return_url = $"https://{currentAddress}/api/payment/paypal/success/{order.Id}",
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
            _ = Task.Run(() => _helperService.SendEmail($"Thank you for your purchase! Your key for {order.ProductKey!.Product!.Name}", $"KEY: {order.ProductKey.Key}", order.Buyer!.Email!));
            return executedPayment;
        }

        private async Task<decimal> GetPriceInEth(double price)
        {
            using (var cli = new HttpClient())
            {
                HttpResponseMessage response = await cli.GetAsync(_configuration["Ethereum:ExchangeAPI"]! + "&symbols=ETH");
                if (!response.IsSuccessStatusCode)
                    throw new InternalServerErrorException("Api isn't available, please consider paying with PayPal");

                var str = await response.Content.ReadAsStringAsync()
                    ?? throw new InternalServerErrorException("Api isn't available, please consider paying with PayPal");

                dynamic obj = JsonConvert.DeserializeObject<dynamic>(str)!;
                return decimal.Parse(price.ToString()) / decimal.Parse(obj.rates.ETH.ToString());
            }
        }

        public async Task<EthereumPaymentDTO> CreateEthereumPayment(int productId, int userId)
        {
            var user = await _unitOfWork.Users.Get(x => x.Id == userId && x.Type == UserType.Buyer)
                ?? throw new UnauthorizedException("User isn't buyer");

            var product = await _unitOfWork.Products.Get(x => x.Id == productId, new() { "Keys", "Seller" })
                ?? throw new NotFoundException("Product doesn't exist");

            var key = product.Keys!.Find(x => !x.Sold)
                ?? throw new BadRequestException("No product available");

            var order = await _orderService.MakeOrder(productId, userId);
            order.UniqueHash = new BigInteger(new Random().Next()).ToHex(true);
            _unitOfWork.Orders.Update(order);
            await _unitOfWork.Save();

            decimal price = await GetPriceInEth(_helperService.GetPrice(product));

            return new EthereumPaymentDTO { To = product.Seller!.EthereumAddress ?? _configuration["Ethereum:Address"], Value = UnitConversion.Convert.ToWei(price).ToString(), Input = order.UniqueHash!};
        }

        public async Task CheckEthereumPayment(string transactionHash)
        {
            var web3 = new Web3(_configuration["Ethereum:RPC_API"]!);
            var block = await web3.Eth.Transactions.GetTransactionByHash.SendRequestAsync(transactionHash);
            var order = await _unitOfWork.Orders.Get(x => x.UniqueHash == block.Input, new() { "ProductKey.Product.Seller", "Buyer" })
                ?? throw new NotFoundException("You made wrong transaction.");


            if (!order.ProductKey!.Product!.Seller!.EthereumAddress!.ToLower().Contains(block.To.ToLower()))
                throw new BadRequestException("You made wrong transaction.");

            decimal price = await GetPriceInEth((double)order.Price!);
            if (UnitConversion.Convert.ToWei(price) > block.Value.Value)
                throw new BadRequestException("You made wrong transaction value.");

            order.State = OrderState.Confirmed;
            _unitOfWork.Orders.Update(order);
            await _unitOfWork.Save();
            _ = Task.Run(() => _helperService.SendEmail($"Thank you for your purchase! Your key for {order.ProductKey!.Product!.Name}", $"KEY: {order.ProductKey.Key}", order.Buyer!.Email!));
        }

        public Task<string> SuccessEthereumPayment(int orderId, int userId)
        {
            throw new NotImplementedException();
        }
    }
}