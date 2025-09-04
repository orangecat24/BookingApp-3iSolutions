using Booking1.Application.Reviews;
using Booking1.Application.Reviews.CreateReview;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookingApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewsController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [Authorize(Roles = "Guest")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateReviewDto dto)
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            try
            {
                var reviewId = await _reviewService.CreateAsync(dto, userId);
                return Ok(new { id = reviewId, message = "Review created successfully" });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpGet("booking/{bookingId:guid}")]
        public async Task<IActionResult> GetByBooking(Guid bookingId)
        {
            var reviews = await _reviewService.GetByBookingAsync(bookingId);

            return Ok(reviews);
        }

        [Authorize(Roles = "Guest")]
        [HttpGet("MyReviews")]
        public async Task<IActionResult> GetMyReviews()
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var reviews = await _reviewService.GetByUserAsync(userId);

            return Ok(reviews);
        }

        [Authorize(Roles = "Guest")]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var success = await _reviewService.DeleteAsync(id, userId);

            if (!success)
            {
                return NotFound(new { message = "Review not found or not yours" });
            }

            return Ok(new { message = "Review deleted successfully" });
        }
    }
}
