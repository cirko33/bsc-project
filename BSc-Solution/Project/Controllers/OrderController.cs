using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.ExceptionMiddleware.Exceptions;
using Project.Interfaces;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet]

        public async Task<IActionResult> GetOrders()
        {
            var orders = await _orderService.GetOrders();
            return Ok(orders);
        }

        [Authorize(Roles = "Seller")]
        [HttpGet("seller")]
        public async Task<IActionResult> GetSellerOrders()
        {
            if (!int.TryParse(User.Claims.First(c => c.Type == "Id").Value, out int id))
                throw new BadRequestException("Bad ID. Logout and login.");

            var orders = await _orderService.GetSellersOrders(id);
            return Ok(orders);
        }

        [Authorize(Roles = "Buyer")]
        [HttpGet("buyer")]
        public async Task<IActionResult> GetBuyersOrders()
        {
            if (!int.TryParse(User.Claims.First(c => c.Type == "Id").Value, out int id))
                throw new BadRequestException("Bad ID. Logout and login.");

            var orders = await _orderService.GetBuyerOrders(id);
            return Ok(orders);
        }
    }
}
