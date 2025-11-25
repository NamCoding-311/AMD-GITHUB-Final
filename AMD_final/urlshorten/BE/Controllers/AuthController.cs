using Microsoft.AspNetCore.Mvc;
using URLShortener.API.Services;
using URLShortener.API.Models;

namespace urlshorten.BE.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly AuthService _authService;
        private readonly JwtService _jwtService;

        public AuthController(UserService userService, AuthService authService, JwtService jwtService)
        {
            _userService = userService;
            _authService = authService;
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest req)
        {
            var user = _userService.GetByUsername(req.Username);
            if (user == null)
                return Unauthorized("User not found");

            if (!_authService.VerifyPassword(req.Password, user.PasswordHash))
                return Unauthorized("Wrong password");

            var token = _jwtService.GenerateToken(user.Username);
            return Ok(new { token });
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] LoginRequest req)
        {
            var exists = _userService.GetByUsername(req.Username);
            if (exists != null)
                return BadRequest("Username already exists");

            var user = new User
            {
                Username = req.Username,
                PasswordHash = _authService.HashPassword(req.Password)
            };

            _userService.Create(user);

            return Ok("User created");
        }
    }

    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
