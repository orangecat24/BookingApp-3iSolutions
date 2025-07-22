using Booking1.Application.Users;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ISender _sender; 

        public UsersController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateUserDto dto)
        {
            var command = new RegisterUserCommand { CreateUserDto = dto };

            var userId = await _sender.Send(command);

            return Ok(new { UserId = userId, Message = "User registered successfully." });
        }
    }
}
