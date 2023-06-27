using Project.Models;

namespace Project.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<User> Users { get; }
        IRepository<Order> Orders { get; }
        IRepository<Product> Products { get; }
        IRepository<ProductKey> Keys { get; }
        Task Save();
    }
}
