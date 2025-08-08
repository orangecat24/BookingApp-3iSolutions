using Booking1.Application.Owners;
using Booking1.Application.Owners.CreateOwner;
using Microsoft.AspNetCore.Mvc;

namespace BookingApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnersController : ControllerBase
    {
        private readonly IOwnerService _ownerService;

        public OwnersController(IOwnerService ownerService)
        {
            _ownerService = ownerService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateOwnerDto dto)
        {
            var ownerId = await _ownerService.RegisterOwnerAsync(dto);
            return Ok(new { OwnerId = ownerId, Message = "Owner registered successfully." });
        }
    }
}
