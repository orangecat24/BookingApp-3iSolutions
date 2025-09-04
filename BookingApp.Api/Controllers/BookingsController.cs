using Booking1.Application.Bookings;
using Booking1.Application.Bookings.UpdateBooking;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookingApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingService _service;

        public BookingsController(IBookingService service)
        {
            _service = service;
        }
        private Guid GetUserId()
        {
            var claim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return Guid.Parse(claim);
        }

        private string GetRole()
        {
            return User.FindFirstValue(ClaimTypes.Role);
        }

        [Authorize(Roles = "Guest")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateBookingDto dto)
        {
            dto.UserId = GetUserId();
            var id = await _service.CreateAsync(dto);
            return Ok(id);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var userId = GetUserId();
            var role = GetRole();

            var booking = await _service.GetByIdAsync(id);
            if (booking == null) return NotFound();

            if (role == "Guest" && booking.UserId != userId) return Forbid();
            if (role == "Owner" && !booking.Apartment.Owners.Any(o => o.UserId == userId)) return Forbid();

            return Ok(booking);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userId = GetUserId();
            var role = GetRole();

            var bookings = await _service.GetAllAsync();

            if (role == "Guest")
            {
                var guestBookings = bookings.Where(b => b.UserId == userId).ToList();
                return Ok(guestBookings);
            }

            if (role == "Owner")
            {
                var ownerBookings = bookings.Where(b => b.Apartment.Owners.Any(o => o.UserId == userId)).ToList();
                return Ok(ownerBookings);
            }

            return Ok(bookings);
        }

        [Authorize(Roles = "Guest")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateBookingDto dto)
        {
            var userId = GetUserId();
            var booking = await _service.GetByIdAsync(id);

            if (booking == null) return NotFound();
            if (booking.UserId != userId) return Forbid();

            var updated = await _service.UpdateAsync(id, dto);
            if (!updated) return BadRequest();

            return Ok();
        }

        [Authorize(Roles = "Guest")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var userId = GetUserId();
            var booking = await _service.GetByIdAsync(id);

            if (booking == null) return NotFound();
            if (booking.UserId != userId) return Forbid();

            var deleted = await _service.DeleteAsync(id);
            if (!deleted) return BadRequest();

            return Ok();
        }

        [AllowAnonymous]
        [HttpGet("check-availability")]
        public async Task<IActionResult> CheckAvailability(Guid apartmentId, DateTime start, DateTime end)
        {
            var available = await _service.CheckAvailabilityAsync(apartmentId, start, end);
            return Ok(new { available });
        }

        [Authorize(Roles = "Owner")]
        [HttpPut("{id}/confirm")]
        public async Task<IActionResult> Confirm(Guid id)
        {
            var ownerId = GetUserId();
            var success = await _service.ConfirmBookingAsync(id, ownerId);
            if (!success)
            {
                return BadRequest(new { message = "Could not confirm booking." });
            }

            return Ok(new { message = "Booking confirmed." });
        }

        [Authorize(Roles = "Owner")]
        [HttpPut("{id}/reject")]
        public async Task<IActionResult> Reject(Guid id)
        {
            var ownerId = GetUserId();

            var success = await _service.RejectBookingAsync(id, ownerId);

            if (!success)
            {
                return BadRequest(new { message = "Could not reject booking." });
            }

            return Ok(new { message = "Booking rejected." });
        }

        [Authorize(Roles = "Owner")]
        [HttpPut("{id}/complete")]
        public async Task<IActionResult> Complete(Guid id)
        {
            var ownerId = GetUserId();

            var success = await _service.CompleteBookingAsync(id, ownerId);

            if (!success)
            {
                return BadRequest(new { message = "Could not complete booking." });
            }

            return Ok(new { message = "Booking completed." });
        }





    }
}
