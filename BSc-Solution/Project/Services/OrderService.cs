using AutoMapper;
using Microsoft.Extensions.FileProviders;
using Microsoft.Identity.Client;
using Project.DTOs;
using Project.ExceptionMiddleware.Exceptions;
using Project.Interfaces;
using Project.Models;
using System.Text.RegularExpressions;

namespace Project.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHelperService _helperService;

        public OrderService(IUnitOfWork unitOfWork, IMapper mapper, IHelperService helperService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _helperService = helperService;
        }

        public async Task<List<BuyerOrderDTO>> GetBuyerOrders(int userId)
        {
            var user = await _unitOfWork.Users.Get(x => x.Id == userId && x.Type == UserType.Buyer, new() { "Orders.ProductKey.Product.Seller" })
                ?? throw new UnauthorizedException("This user doesn't exist!");

            return _mapper.Map<List<BuyerOrderDTO>>(user.Orders!); ;
        }

        public async Task<BuyerOrderDTO> GetOrder(int id, int userId)
        {
            var user = await _unitOfWork.Users.Get(x => x.Id == userId && x.Type == UserType.Buyer)
               ?? throw new UnauthorizedException("This user doesn't exist!");

            var order = await _unitOfWork.Orders.Get(x => x.Id == id && x.BuyerId == userId, new() { "Key.Product" })
                ?? throw new NotFoundException("Product doesn't exist");

            var orderDTO = _mapper.Map<BuyerOrderDTO>(order);
            return orderDTO;
        }

        public async Task<List<BuyerOrderDTO>> GetOrders(int userId)
        {
            var user = await _unitOfWork.Users.Get(x => x.Id == userId && x.Type == UserType.Buyer, new() { "Orders.Key.Product" })
               ?? throw new UnauthorizedException("This user doesn't exist!");

            return _mapper.Map<List<BuyerOrderDTO>>(user.Orders!);
        }

        public async Task<List<OrderDTO>> GetOrders()
        {
            var orders = await _unitOfWork.Orders.GetAll();
            return _mapper.Map<List<OrderDTO>>(orders);
        }

        public async Task<List<OrderDTO>> GetSellersOrders(int userId)
        {
            var user = await _unitOfWork.Users.Get(x => x.Id == userId && x.Type == UserType.Seller, new() { "Products" })
              ?? throw new UnauthorizedException("This user doesn't exist!");

            var orders = await _unitOfWork.Orders.GetAll(null, null, new() { "ProductKey.Product", "Buyer" });
            var ids = user.Products!.Select(x => x.Id);
            orders = orders.FindAll(x => ids.Contains(x.ProductKey!.ProductId));
            orders.Reverse();
            return _mapper.Map<List<OrderDTO>>(orders);
        }

        public async Task<Order> MakeOrder(int productId, int userId)
        {
            var user = await _unitOfWork.Users.Get(x => x.Id == userId && x.Type == UserType.Buyer)
                ?? throw new UnauthorizedException("This user doesn't exist!");

            var product = await _unitOfWork.Products.Get(x => x.Id == productId, new() { "Keys" })
                ?? throw new NotFoundException("Product doesn't exist");

            var key = product.Keys!.Find(x => !x.Sold) 
                ?? throw new BadRequestException("Can't buy product that isn't available"); ;

            var order = new Order
            {
                BuyerId = userId,
                ProductKeyId = key.Id,
                Price = product.Price * (1.0 - product.Discount / 100.0),
                OrderTime = DateTime.Now,
            };

            key.Sold = true;
            _unitOfWork.Keys.Update(key);
            await _unitOfWork.Orders.Insert(order);
            await _unitOfWork.Save();

            _ = Task.Run(async () =>
            {
                await Task.Delay(300000);
                var ord = await _unitOfWork.Orders.Get(x => x.Id == order!.Id, new() { "ProductKey" });
                if (ord != null)
                    if (order.State != OrderState.Confirmed)
                    {
                        order.State = OrderState.Cancelled;
                        order.ProductKey!.Sold = false;
                        _unitOfWork.Orders.Update(order);
                        await _unitOfWork.Save();
                        await _helperService.SendEmail("Order cancelled", $"Your order was cancelled due 5min expiration.", user.Email!);
                    }
            });
            return order;
        }
    }
}
