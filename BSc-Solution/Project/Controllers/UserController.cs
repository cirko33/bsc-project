using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Bcpg.OpenPgp;
using Project.DTOs;
using Project.ExceptionMiddleware.Exceptions;
using Project.Interfaces;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            var token = await _userService.Login(loginDTO);
            return Ok(token);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] RegisterDTO registerDTO)
        {
            await _userService.Register(registerDTO);
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("google-sign-in")]
        public async Task<IActionResult> GoogleSignIn(TokenDTO token)
        {
            var returnToken = await _userService.GoogleSignIn(token);
            return Ok(returnToken);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            if (!int.TryParse(User.Claims.First(c => c.Type == "Id").Value, out int id))
                throw new BadRequestException("Bad ID. Logout and login.");

            var profile = await _userService.GetUser(id);
            return Ok(profile);
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> EditProfile([FromForm]UpdateUserDTO updateUserDTO)
        {
            if (!int.TryParse(User.Claims.First(c => c.Type == "Id").Value, out int id))
                throw new BadRequestException("Bad ID. Logout and login.");

            await _userService.UpdateUser(id, updateUserDTO);
            return Ok();
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetUsers();
            return Ok(users);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost("block/{id}")]
        public async Task<IActionResult> ChangeBlockStatus(int id)
        {
            await _userService.ChangeBlockUser(id);
            return Ok();
        }

        [Authorize]
        [HttpGet("image/{name}")]
        public async Task<IActionResult> GetImage(string name)
        {
            return File(_userService.GetImage(name), "image/*");
        }
    }
}
