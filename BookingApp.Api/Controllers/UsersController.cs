
using Booking1.Application.Authentication;
using Booking1.Application.Users;
using Booking1.Application.Users2;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace BookingApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        private readonly IAuthService _authService;

        public UsersController(IUserService userService, IAuthService authService)
        {
            _userService = userService;
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateUserDto payload)
        {
            var userId = await _userService.RegisterUserAsync(payload);

            return Ok(new { UserId = userId, Message = "User registered successfully." });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            try
            {
                var token = await _authService.AuthenticateAsync(dto);
                return Ok(new { Token = token });
            }
            catch (ValidationException ex)
            {
                return Unauthorized(new { Errors = ex.Errors.Select(e => e.ErrorMessage) });
            }
        }
    }
}
