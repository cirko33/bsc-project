using Project.DTOs;

namespace Project.Interfaces
{
    public interface IUserService
    {
        Task<string> Login(LoginDTO loginDTO);
        Task Register(RegisterDTO registerDTO);
        Task<string> GoogleSignIn(TokenDTO token);
        Task<UserDTO> GetUser(int id);
        Task UpdateUser(int id, UpdateUserDTO user);
        Task<List<UserDTO>> GetUsers();
        Task ChangeBlockUser(int id);
        Task<byte[]> GetImage(string name);
    }
}
