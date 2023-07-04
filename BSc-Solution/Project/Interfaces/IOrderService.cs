using Project.DTOs;
using Project.Models;

namespace Project.Interfaces
{
    public interface IOrderService
    {
        Task<List<OrderDTO>> GetOrders(int userId);
        Task<List<OrderDTO>> GetOrders();
        Task<OrderDTO> GetOrder(int id, int userId);
        Task<Order> MakeOrder(int productId, int userId);
    }
}
