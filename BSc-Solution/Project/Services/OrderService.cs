using AutoMapper;
using Microsoft.Extensions.FileProviders;
using Microsoft.Identity.Client;
using Project.DTOs;
using Project.ExceptionMiddleware.Exceptions;
using Project.Interfaces;
using Project.Models;

namespace Project.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<OrderDTO> GetOrder(int id, int userId)
        {
            var user = await _unitOfWork.Users.Get(x => x.Id == userId && x.Type == UserType.Buyer)
               ?? throw new UnauthorizedException("This user doesn't exist!");

            var order = await _unitOfWork.Orders.Get(x => x.Id == id && x.BuyerId == userId, new() { "Key.Product" })
                ?? throw new NotFoundException("Product doesn't exist");

            var orderDTO = _mapper.Map<OrderDTO>(order);
            return orderDTO;
        }

        public async Task<List<OrderDTO>> GetOrders(int userId)
        {
            var user = await _unitOfWork.Users.Get(x => x.Id == userId && x.Type == UserType.Buyer, new() { "Orders.Key.Product" })
               ?? throw new UnauthorizedException("This user doesn't exist!");

            return _mapper.Map<List<OrderDTO>>(user.Orders!);
        }

        public async Task<List<OrderDTO>> GetOrders()
        {
            var orders = await _unitOfWork.Orders.GetAll();
            return _mapper.Map<List<OrderDTO>>(orders);
        }

        public async Task MakeOrder(CreateOrderDTO orderDTO, int userId)
        {
            var user = await _unitOfWork.Users.Get(x => x.Id == userId && x.Type == UserType.Buyer, new() { "Orders.Key.Product" })
                ?? throw new UnauthorizedException("This user doesn't exist!");

            var product = await _unitOfWork.Products.Get(x => x.Id == orderDTO.ProductId, new() { "Keys" })
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
        }
    }
}
