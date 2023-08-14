using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nethereum.RPC.Eth.DTOs;
using Project.DTOs;
using Project.ExceptionMiddleware.Exceptions;
using Project.Interfaces;
using System.Text.Json.Serialization.Metadata;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [Authorize(Roles = "Buyer")]
        [HttpGet("paypal/{id}")]
        public async Task<IActionResult> CreatePayPalPayment(int id)
        {
            if (!int.TryParse(User.Claims.First(c => c.Type == "Id").Value, out int userId))
                throw new BadRequestException("Bad ID. Logout and login.");

            string red = await _paymentService.CreatePayPalPayment(id, userId, $"{HttpContext.Request.Host.Host}:{HttpContext.Request.Host.Port}");
            return Ok(red);
        }

        [HttpGet("paypal/success/{id}")]
        public async Task<IActionResult> ConfirmPayPalPayment(int id, string paymentId, string token, string PayerID)
        {
            var result = await _paymentService.SuccessPayPalPayment(paymentId, PayerID, id);
            return Redirect("http://localhost:3000");
        }

        [HttpGet("paypal/cancel/{id}")]
        public async Task<IActionResult> CancelPayPalPayment(int id)
        {

            await _paymentService.CancelPayPalPayment(id);
            return Redirect("http://localhost:3000");
        }

        [Authorize]
        [HttpPost("ethereum/create/{id}")]
        public async Task<IActionResult> CreateEthereumPayment(int id)
        {
            if (!int.TryParse(User.Claims.First(c => c.Type == "Id").Value, out int userId))
                throw new BadRequestException("Bad ID. Logout and login.");

            var response = await _paymentService.CreateEthereumPayment(id, userId);
            return Ok(response);
        }

        [Authorize]
        [HttpGet("ethereum/check/{hash}")]
        public async Task<IActionResult> CheckEthereumPayment(string hash)
        {
            if(string.IsNullOrWhiteSpace(hash))
                throw new BadRequestException("Hash is required");
            await _paymentService.CheckEthereumPayment(hash);
            return Ok();
        }

        [Authorize]
        [HttpGet("ethereum/cancel/{id}")]
        public async Task<IActionResult> CheckEthereumPayment(int id)
        {
            await _paymentService.CancelEthereumPayment(id);
            return Ok();
        }
    }
}
