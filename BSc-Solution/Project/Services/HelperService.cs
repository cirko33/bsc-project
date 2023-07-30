using MailKit;
using MailKit.Net.Smtp;
using Microsoft.IdentityModel.Tokens;
using MimeKit;
using Project.ExceptionMiddleware.Exceptions;
using Project.Interfaces;
using Project.Models;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Project.Services
{
    public class HelperService : IHelperService
    {
        private readonly IConfiguration _configuration;
        private readonly int size = 300;
        public HelperService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public double GetPrice(Product product)
        {
            return product.Price - (product.Price * product.Discount / 100);
        }

        public string GetToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Username!),
                new Claim(ClaimTypes.Role, user.Type.ToString()),
                new Claim("Id", user.Id.ToString()),
                new Claim("Email", user.Email!),
            };
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: signIn);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string SaveImage(IFormFile image)
        {
            if (image.Length == 0)
                return "";

            if (image.ContentType != "image/jpg" && image.ContentType != "image/jpeg" && image.ContentType != "image/png")
                throw new BadRequestException("Image must be jpg or png");

            using (var img = Image.Load(image.OpenReadStream()))
            {
                img.Mutate(x => x.Resize(new ResizeOptions
                {
                    Size = new Size(size, size),
                    Mode = ResizeMode.Max
                }));

                string name = DateTime.Now.Ticks.ToString() + Path.GetExtension(image.FileName);
                if (!Directory.Exists("Resources"))
                    Directory.CreateDirectory("Resources");

                img.Save(Path.Combine("Resources", name));
                return name;
            }
        }

        public async Task SendEmail(string subject, string body, string receiver)
        {
            var message = new MimeMessage
            {
                Subject = subject,
                Body = new TextPart(MimeKit.Text.TextFormat.Plain) { Text = body }
            };
            message.From.Add(new MailboxAddress(_configuration["Mail:Name"], _configuration["Mail:Email"]));
            message.To.Add(MailboxAddress.Parse(receiver));

            var client = new SmtpClient();
            await client.ConnectAsync(_configuration["Mail:Host"], int.Parse(_configuration["Mail:Port"]!), MailKit.Security.SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(_configuration["Mail:Email"], _configuration["Mail:Password"]);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }
}
