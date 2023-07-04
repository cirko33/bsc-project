using AutoMapper;
using Microsoft.Identity.Client;
using Project.DTOs;
using Project.ExceptionMiddleware.Exceptions;
using Project.Interfaces;
using Project.Models;

namespace Project.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHelperService _helperService;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper, IHelperService helperService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _helperService = helperService;
        }

        public async Task DeleteProduct(int id, int userId)
        {
            var user = await _unitOfWork.Users.Get(x => x.Id == userId && x.Type == UserType.Seller, new() { "Products" }) 
                ?? throw new UnauthorizedException("This user doesn't exist!");

            var product = user.Products!.Find(x => x.Id == id)
                ?? throw new BadRequestException("This isn't your product!");

            _unitOfWork.Products.Delete(product);
            await _unitOfWork.Save();
        }

        public async Task<ProductDTO> GetProduct(int id)
        {
            var product = await _unitOfWork.Products.Get(x => x.Id == id, new() { "Seller", "Keys" })
                ?? throw new BadRequestException("Product doesn't exist!");

            return _mapper.Map<ProductDTO>(product);
        }

        public async Task<List<ProductDTO>> GetProducts()
        {
            var products = await _unitOfWork.Products.GetAll(null, x => x.OrderByDescending(y => y.Id), new() { "Seller", "Keys" });
            var res = _mapper.Map<List<ProductDTO>>(products);
            return res;
        }

        public async Task<List<ProductDTO>> GetSellerProducts(int userId)
        {
            var user = await _unitOfWork.Users.Get(x => x.Id == userId && x.Type == UserType.Seller, new() { "Products" })
                ?? throw new UnauthorizedException("This user doesn't exist!");

            return _mapper.Map<List<ProductDTO>>(user.Products!);
        }

        public async Task InsertProduct(CreateProductDTO productDTO, int userId)
        {
            var user = await _unitOfWork.Users.Get(x => x.Id == userId && x.Type == UserType.Seller)
                ?? throw new UnauthorizedException("This user doesn't exist!");

            var product = _mapper.Map<Product>(productDTO);
            product.SellerId = userId;
            if (productDTO.ImageFile != null)
                product.Image = _helperService.SaveImage(productDTO.ImageFile);

            await _unitOfWork.Products.Insert(product);
            await _unitOfWork.Save();
        }

        public async Task UpdateProduct(ProductDTO productDTO, int userId)
        {
            var user = await _unitOfWork.Users.Get(x => x.Id == userId && x.Type == UserType.Seller, new() { "Products" })
                ?? throw new UnauthorizedException("This user doesn't exist!");

            var product = user.Products!.Find(x => x.Id == productDTO.Id)
                ?? throw new BadRequestException("This isn't your product!");

            var updated = _mapper.Map<Product>(productDTO);
            updated.SellerId = userId;
            updated.Image = productDTO.ImageFile == null ? product.Image : _helperService.SaveImage(productDTO.ImageFile);
            _unitOfWork.Products.Update(updated); 
            await _unitOfWork.Save();
        }
    }
}
