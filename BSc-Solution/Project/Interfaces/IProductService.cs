    using Project.DTOs;

namespace Project.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductDTO>> GetProducts();
        Task<ProductDTO> GetProduct(int id);
        Task InsertProduct(CreateProductDTO productDTO, int userId);
        Task DeleteProduct(int id, int userId);
        Task UpdateProduct(ProductDTO productDTO, int userId);
        Task<List<ProductSellerDTO>> GetSellerProducts(int userId);
        Task<int> AddKey(int userId, AddProductKeyDTO keyDTO);
        Task DeleteKey(int userId, int id);
    }
}
