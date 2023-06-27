using Project.Interfaces;
using Project.Models.StoreDbContext;
using Project.Models;

namespace Project.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreDbContext _context;
        public IRepository<User> Users { get; }
        public IRepository<Order> Orders { get; }
        public IRepository<Product> Products { get; }
        public IRepository<ProductKey> Keys { get; }

        public UnitOfWork(StoreDbContext context, IRepository<User> users, IRepository<Order> orders, IRepository<Product> products, IRepository<ProductKey> keys)
        {
            _context = context;
            Users = users;
            Orders = orders;
            Products = products;
            Keys = keys;
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
