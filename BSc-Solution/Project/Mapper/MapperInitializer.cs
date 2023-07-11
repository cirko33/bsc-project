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
            CreateMap<ProductSellerDTO, Product>().ReverseMap().ForMember(x => x.Amount, y => y.MapFrom(z => z.Keys!.FindAll(x => !x.Sold).Count));
            CreateMap<Product, CreateProductDTO>().ReverseMap();

            CreateMap<OrderDTO, Order>().ReverseMap().ForMember(x => x.Product, y => y.MapFrom(z => z.ProductKey!.Product));
            CreateMap<Order, BuyerOrderDTO>().ReverseMap();
            CreateMap<ProductKey, ProductKeyDTO>().ReverseMap();
            CreateMap<ProductKey, AddProductKeyDTO>().ReverseMap();
        }
    }
}
