using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.DTOs;
using Project.ExceptionMiddleware.Exceptions;
using Project.Interfaces;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var products = await _productService.GetProducts();
            return Ok(products);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var product = await _productService.GetProduct(id);
            return Ok(product);
        }

        [Authorize(Roles = "Seller")]
        [HttpGet("seller")]
        public async Task<IActionResult> GetSellersProducts()
        {
            if (!int.TryParse(User.Claims.First(c => c.Type == "Id").Value, out int id))
                throw new BadRequestException("Bad ID. Logout and login.");

            var products = await _productService.GetSellerProducts(id);
            return Ok(products);
        }

        [Authorize(Roles = "Seller")]
        [HttpPost]
        public async Task<IActionResult> InsertProduct([FromForm]CreateProductDTO productDTO)
        {
            if (!int.TryParse(User.Claims.First(c => c.Type == "Id").Value, out int id))
                throw new BadRequestException("Bad ID. Logout and login.");

            await _productService.InsertProduct(productDTO, id);
            return Ok();
        }

        [Authorize(Roles = "Seller")]
        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromForm]ProductDTO productDTO)
        {
            if (!int.TryParse(User.Claims.First(c => c.Type == "Id").Value, out int id))
                throw new BadRequestException("Bad ID. Logout and login.");

            await _productService.UpdateProduct(productDTO, id);
            return Ok();
        }

        [Authorize(Roles = "Seller")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (!int.TryParse(User.Claims.First(c => c.Type == "Id").Value, out int userId))
                throw new BadRequestException("Bad ID. Logout and login.");

            await _productService.DeleteProduct(id, userId);
            return Ok();
        }

        [Authorize(Roles = "Seller")]
        [HttpPost("key")]
        public async Task<IActionResult> AddKey(AddProductKeyDTO keyDTO)
        {
            if (!int.TryParse(User.Claims.First(c => c.Type == "Id").Value, out int id))
                throw new BadRequestException("Bad ID. Logout and login.");

            return Ok(await _productService.AddKey(id, keyDTO));
        }

        [Authorize(Roles = "Seller")]
        [HttpDelete("key/{id}")]
        public async Task<IActionResult> DeleteKey(int id)
        {
            if (!int.TryParse(User.Claims.First(c => c.Type == "Id").Value, out int userId))
                throw new BadRequestException("Bad ID. Logout and login.");

            await _productService.DeleteKey(userId, id);
            return Ok();
        }

    }
}
