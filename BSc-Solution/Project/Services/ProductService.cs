using AutoMapper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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


        public async Task<int> AddKey(int userId, AddProductKeyDTO keyDTO)
        {
            var user = await _unitOfWork.Users.Get(x => x.Id == userId && x.Type == UserType.Seller, new() { "Products" })
                ?? throw new UnauthorizedException("This user doesn't exist!");

            var product = user.Products!.Find(x => x.Id == keyDTO.ProductId)
                ?? throw new UnauthorizedException("This isn't your product!");

            var key = _mapper.Map<ProductKey>(keyDTO);
            await _unitOfWork.Keys.Insert(key);
            await _unitOfWork.Save();
            return key.Id;
        }

        public async Task DeleteKey(int userId, int id)
        {
            var user = await _unitOfWork.Users.Get(x => x.Id == userId && x.Type == UserType.Seller, new() { "Products" })
                ?? throw new UnauthorizedException("This user doesn't exist!");

            var key = await _unitOfWork.Keys.Get(x => x.Id == id && user.Products!.Select(y => y.Id).Contains(x.ProductId)) 
                ?? throw new UnauthorizedException("This isnt your key");

            _unitOfWork.Keys.Delete(key);
            await _unitOfWork.Save();
        }

        public async Task DeleteProduct(int id, int userId)
        {
            var user = await _unitOfWork.Users.Get(x => x.Id == userId && x.Type == UserType.Seller, new() { "Products" }) 
                ?? throw new UnauthorizedException("This user doesn't exist!");

            var product = user.Products!.Find(x => x.Id == id)
                ?? throw new UnauthorizedException("This isn't your product!");

            _unitOfWork.Products.Delete(product);
            await _unitOfWork.Save();
        }

        public async Task<ProductDTO> GetProduct(int id)
        {
            var product = await _unitOfWork.Products.Get(x => x.Id == id, new() { "Seller", "Keys" })
                ?? throw new NotFoundException("Product doesn't exist!");

            return _mapper.Map<ProductDTO>(product);
        }

        public async Task<List<ProductDTO>> GetProducts()
        {
            var products = await _unitOfWork.Products.GetAll(null, x => x.OrderByDescending(y => y.Id), new() { "Seller", "Keys" });
            var res = _mapper.Map<List<ProductDTO>>(products);
            return res;
        }

        public async Task<List<ProductSellerDTO>> GetSellerProducts(int userId)
        {
            var user = await _unitOfWork.Users.Get(x => x.Id == userId && x.Type == UserType.Seller, new() { "Products.Keys" })
                ?? throw new UnauthorizedException("This user doesn't exist!");

            user.Products = user.Products!.FindAll(x => !x.IsDeleted);
            user.Products!.ForEach(p => p.Keys = p.Keys!.FindAll(k => !k.IsDeleted && !k.Sold));
            return _mapper.Map<List<ProductSellerDTO>>(user.Products!);
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
                ?? throw new UnauthorizedException("This isn't your product!");

            _mapper.Map(productDTO, product);
            product.SellerId = userId;
            if(productDTO.ImageFile != null)
                product.Image = _helperService.SaveImage(productDTO.ImageFile);
            
            _unitOfWork.Products.Update(product); 
            await _unitOfWork.Save();
        }
    }
}
