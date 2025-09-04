using Booking1.Application.Owners;
using Booking1.Application.Owners.CreateOwner;
using Booking1.Domain.Models;
using Microsoft.AspNetCore.Authorization;
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

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateOwnerDto dto)
        {
            var ownerId = await _ownerService.RegisterOwnerAsync(dto);
            return Ok(new { OwnerId = ownerId, Message = "Owner registered successfully." });
        }

        [Authorize(Roles = "Owner")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var owner = await _ownerService.GetByIdAsync(id);
            if (owner == null)
            {
                return NotFound(new { message = "Owner not found" });
            }

            return Ok(owner);
        }

        [Authorize(Roles = "Owner")]
        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var owners = await _ownerService.GetAllAsync();
            return Ok(owners);
        }

        [Authorize(Roles = "Owner")]
        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Owner owner)
        {
            var updated = await _ownerService.UpdateAsync(id, owner);
            if (!updated)
            {
                return NotFound(new { message = "Owner not found" });
            }

            return Ok(new { message = "Owner updated successfully" });
        }

        [Authorize(Roles = "Owner")]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _ownerService.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound(new { message = "Owner not found" });
            }

            return Ok(new { message = "Owner deleted successfully" });
        }
    }
}
