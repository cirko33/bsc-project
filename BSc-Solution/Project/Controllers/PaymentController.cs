using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

            string red = await _paymentService.CreatePayPalPayment(id, userId);
            return Ok(red);
        }

        [HttpGet("paypal/success/{id}")]
        public async Task<IActionResult> ConfirmPayPalPayment(int id, string paymentId, string token, string PayerID)
        {
            var result = await _paymentService.SuccessPayPalPayment(paymentId, PayerID, id);
            return Redirect("http://localhost:5173");
        }

        [HttpGet("paypal/cancel/{id}")]
        public async Task<IActionResult> CancelPayPalPayment(int id)
        {

            await _paymentService.CancelPayPalPayment(id);
            return Redirect("http://localhost:5173");
        }
    }
}
