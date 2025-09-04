
using Booking1.Application.Authentication;
using Booking1.Application.Users;
using Booking1.Application.Users2;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

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

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateUserDto payload)
        {
            var userId = await _userService.RegisterUserAsync(payload);

            return Ok(new { UserId = userId, Message = "User registered successfully." });
        }

        [AllowAnonymous]
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
                return Unauthorized(new { Errors = new[] { ex.Message } });
            }
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> Me()
        {

            var userIdStr =
                User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value ??
                User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!Guid.TryParse(userIdStr, out var userId))
                return Unauthorized(new { error = "Invalid" });

            var user = await _userService.GetByIdAsync(userId);
            if (user == null) return NotFound();

            return Ok(new
            {
                user.Id,
                user.FirstName,
                user.LastName,
                user.Email,
                user.Country,
                user.CreatedOnUtc,
                user.UpdatedOnUtc
            });
        }
    }
}
