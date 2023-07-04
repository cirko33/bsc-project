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
            CreateMap<User, UserShortDTO>().ReverseMap();

            CreateMap<ProductDTO, Product>().ReverseMap().ForMember(x => x.Amount, y => y.MapFrom(z => z.Keys!.FindAll(x => !x.Sold).Count));
            CreateMap<Product, CreateProductDTO>().ReverseMap();

            CreateMap<Order, OrderDTO>().ReverseMap();
            CreateMap<ProductKey, ProductKeyDTO>().ReverseMap();
        }
    }
}
