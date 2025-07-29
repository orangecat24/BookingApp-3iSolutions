
using Booking1.Application.Users;
using Booking1.Application.Users2;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateUserDto payload)
        {
            var userId = await _userService.RegisterUserAsync(payload);

            return Ok(new { UserId = userId, Message = "User registered successfully." });
        }
    }
}
