using Project.DTOs;
using Project.Models;

namespace Project.Interfaces
{
    public interface IOrderService
    {
        Task<List<BuyerOrderDTO>> GetOrders(int userId);
        Task<List<OrderDTO>> GetOrders();
        Task<List<OrderDTO>> GetSellersOrders(int userId);
        Task<List<BuyerOrderDTO>> GetBuyerOrders(int userId);
        Task<BuyerOrderDTO> GetOrder(int id, int userId);
        Task<Order> MakeOrder(int productId, int userId);
    }
}
