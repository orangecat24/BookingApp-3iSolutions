using Booking1.Application.Apartments.CreateApartment;
using Microsoft.AspNetCore.Mvc;

namespace BookingApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApartmentsController : ControllerBase
    {
        private readonly IApartmentService _service;

        public ApartmentsController(IApartmentService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateApartmentDto dto)
        {

            var id = await _service.RegisterApartmentAsync(dto);

            return Ok(new { ApartmentId = id });
        }
    }
}
