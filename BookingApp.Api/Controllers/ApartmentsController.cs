using Booking1.Application.Apartments;
using Booking1.Application.Apartments.CreateApartment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApartmentsController : ControllerBase
    {
        private readonly IApartmentService _apartmentService;

        public ApartmentsController(IApartmentService apartmentService)
        {
            _apartmentService = apartmentService;
        }

        [AllowAnonymous]
        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var apartments = await _apartmentService.GetAllAsync();
            return Ok(apartments);
        }

        [AllowAnonymous]
        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var apartment = await _apartmentService.GetByIdAsync(id);
            if (apartment == null)
            {
                return NotFound(new { message = "Apartment not found" });
            }

            return Ok(apartment);
        }

        [Authorize(Roles = "Owner")]
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateApartmentDto dto)
        {
            var id = await _apartmentService.CreateAsync(dto);
            return Ok(new { id, message = "Created successfully" });
        }

        [Authorize(Roles = "Owner")]
        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] CreateApartmentDto dto)
        {
            var updated = await _apartmentService.UpdateAsync(id, dto);
            if (!updated)
            {
                return NotFound(new { message = "Apartment not found" });
            }

            return Ok(new { message = "Apartment updated successfully" });
        }

        [Authorize(Roles = "Owner")]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _apartmentService.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound();
            }

            return Ok(new { message = "Apartment deleted successfully" });
        }

    }
}
