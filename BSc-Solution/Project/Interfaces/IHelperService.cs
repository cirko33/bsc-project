using Project.Models;
using System.Runtime.CompilerServices;

namespace Project.Interfaces
{
    public interface IHelperService
    {
        Task SendEmail(string subject, string body, string receiver);
        string SaveImage(IFormFile image);
        string GetToken(User user);
        double GetPrice(Product product);
    }
}
