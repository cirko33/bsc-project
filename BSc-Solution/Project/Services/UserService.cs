using AutoMapper;
using Google.Apis.Auth;
using MailKit;
using Project.DTOs;
using Project.ExceptionMiddleware.Exceptions;
using Project.Interfaces;
using Project.Models;
using System.Text.RegularExpressions;
using BC = BCrypt.Net;

namespace Project.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IHelperService _helperService;

        public UserService(IUnitOfWork unitOfWork, IConfiguration configuration, IMapper mapper, IHelperService helpersService)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _mapper = mapper;
            _helperService = helpersService;
        }

        public async Task UpdateUser(int id, UpdateUserDTO user)
        {
            var dbUser = await _unitOfWork.Users.Get(x => x.Id == id) 
                ?? throw new UnauthorizedException("Error with id in token. Logout and login again");
            if (!string.IsNullOrEmpty(user.Password) && !string.IsNullOrEmpty(user.NewPassword))
            {
                if (!BC.BCrypt.Verify(user.Password, dbUser.Password))
                    throw new BadRequestException("Password doesn't match");

                dbUser.Password = BC.BCrypt.HashPassword(user.NewPassword);
            }

            dbUser.Address = user.Address;
            if (dbUser.Email != user.Email)
                if ((await _unitOfWork.Users.Get(x => x.Email == user.Email)) != null)
                    throw new BadRequestException("Email already exists.");

            dbUser.Email = user.Email;
            dbUser.Birthday = user.Birthday;
            dbUser.FullName = user.FullName;

            if (dbUser.Username != user.Username)
                if ((await _unitOfWork.Users.Get(x => x.Username == user.Username)) != null)
                    throw new BadRequestException("Username already exists.");
            dbUser.Username = user.Username;
            if(user.ImageFile != null)
            {
                dbUser.Image = _helperService.SaveImage(user.ImageFile);
            }

            _unitOfWork.Users.Update(dbUser);
            await _unitOfWork.Save();
        }

        public async Task<UserDTO> GetUser(int id)
        {
            var user = await _unitOfWork.Users.Get(x => x.Id == id) 
                ?? throw new UnauthorizedException("Error with id in token. Logout and login again");
            return _mapper.Map<UserDTO>(user);
        }

        public async Task<string> GoogleSignIn(TokenDTO token)
        {
            var str = _configuration["Google:ClientID"]!;
            var settings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string>() { _configuration["Google:ClientID"]! }
            };

            var data = await GoogleJsonWebSignature.ValidateAsync(token.Token, settings);

            var user = await _unitOfWork.Users.Get(x => x.Email == data.Email);
            if (user != null)
                return _helperService.GetToken(user);

            user = new User
            {
                Email = data.Email,
                FullName = $"{data.GivenName} {data.FamilyName}",
                Birthday = DateTime.Now,
                Address = $"No address",
                Password = BC.BCrypt.HashPassword("123"),
                Type = UserType.Buyer,
                Username = data.GivenName + (new Random().Next() / 100000).ToString(),
            };

            await _unitOfWork.Users.Insert(user);
            await _unitOfWork.Save();

            return _helperService.GetToken(user);
        }

        public async Task<string> Login(LoginDTO loginDTO)
        {
            var user = await _unitOfWork.Users.Get(x => x.Username == loginDTO.Username)
                ?? throw new NotFoundException($"Incorrect username. Try again.");

            if(user.Blocked && user.Type != UserType.Administrator)
            {
                _ = Task.Run(() =>_helperService.SendEmail("OnlineShop", "You have been blocked by admin. Please contact admins for detailes!", user.Email!));
                throw new UnauthorizedException("Blocked by admin");
            }

            if (!BC.BCrypt.Verify(loginDTO.Password, user.Password))
                throw new BadRequestException("Invalid password");

            return _helperService.GetToken(user);
        }

        public async Task Register(RegisterDTO registerDTO)
        {
            if (registerDTO.Type == UserType.Administrator)
                throw new UnauthorizedException("Can't register admin user!");

            if ((await _unitOfWork.Users.Get(x => x.Email == registerDTO.Email)) != null)
                throw new BadRequestException("Email already exists.");

            if ((await _unitOfWork.Users.Get(x => x.Username == registerDTO.Username)) != null)
                throw new BadRequestException("Username already exists.");

            registerDTO.Password = BC.BCrypt.HashPassword(registerDTO.Password);

            var user = _mapper.Map<User>(registerDTO);

            _ = Task.Run(() => _helperService.SendEmail("Registration to OnlineShop", 
                $"Thank you for registring to OnlineShop! You can {(user.Type == UserType.Seller ? "Sell" : "Buy")} products!", 
                user.Email!));

            await _unitOfWork.Users.Insert(user);
            await _unitOfWork.Save();
        }

        public async Task<List<UserDTO>> GetUsers()
        {
            var users = await _unitOfWork.Users.GetAll(x => x.Type != UserType.Administrator);
            return _mapper.Map<List<UserDTO>>(users);
        }

        public async Task ChangeBlockUser(int id)
        {
            var user = await _unitOfWork.Users.Get(x => x.Id == id)
                ?? throw new BadRequestException("Invalid id");

            user.Blocked = !user.Blocked;
            _unitOfWork.Users.Update(user);
            await _unitOfWork.Save();
        }

        public async Task<byte[]> GetImage(string name)
        {
            if (!Regex.Match(name, "[0-9a-zA-Z]+.(png|jpg|jpeg)").Success)
                throw new BadRequestException("Format: name.ext");

            string img = Path.Combine("Resources", name);
            if (!File.Exists(img))
                throw new NotFoundException("No image with that name");

            return await File.ReadAllBytesAsync(img!);
        }
    }
}
