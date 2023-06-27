using AutoMapper;
using Project.DTOs;
using Project.Models;

namespace Project.Mapper
{
    public class MapperInitializer:Profile
    {
        public MapperInitializer()
        {
            CreateMap<User, RegisterDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<User, UpdateUserDTO>().ReverseMap();

            CreateMap<Product, ProductDTO>().ReverseMap().AfterMap((x, y) =>
            {
                x.Amount = y.Keys!.FindAll(z => !z.Sold).Count;
            });
            CreateMap<Product, CreateProductDTO>().ReverseMap();

            CreateMap<Order, OrderDTO>().ReverseMap();
            CreateMap<ProductKey, ProductKeyDTO>().ReverseMap();
        }
    }
}
