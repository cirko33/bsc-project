using Microsoft.EntityFrameworkCore;
using BC = BCrypt.Net.BCrypt;

namespace Project.Models.StoreDbContext
{
    public class StoreDbContext : DbContext
    {
        public DbSet<User>? Users { get; set; }
        public DbSet<Product>? Products { get; set; }
        public DbSet<Order>? Orders { get; set; }
        public DbSet<ProductKey>? Keys { get; set; }

        public StoreDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Order>().HasOne(o => o.Buyer).WithMany(o => o.Orders).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Username = "admin",
                    Email = "admin@gmail.com",
                    Address = "No address",
                    Birthday = DateTime.Now.AddYears(-25),
                    FullName = "admin",
                    Password = BC.HashPassword("123"),
                    Type = UserType.Administrator,
                },
                new User
                {
                    Id = 2,
                    Username = "seller",
                    Email = "seller@gmail.com",
                    Address = "No address",
                    Birthday = DateTime.Now.AddYears(-25),
                    FullName = "seller",
                    Password = BC.HashPassword("123"),
                    Type = UserType.Seller,
                },
                new User
                {
                    Id = 3,
                    Username = "buyer",
                    Email = "buyer@gmail.com",
                    Address = "No address",
                    Birthday = DateTime.Now.AddYears(-25),
                    FullName = "buyer",
                    Password = BC.HashPassword("123"),
                    Type = UserType.Buyer,
                }
            );
        }
    }
}
