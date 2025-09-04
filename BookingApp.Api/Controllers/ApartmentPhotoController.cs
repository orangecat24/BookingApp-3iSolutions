using Booking1.Application.Apartments.AddApartmentPhotos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApartmentPhotoController : ControllerBase
    {
        private readonly IApartmentPhotoService _service;

        public ApartmentPhotoController(IApartmentPhotoService service)
        {
            _service = service;
        }

        [Authorize(Roles = "Owner")]
        [HttpPost("{apartmentId:guid}/upload-file")]
        public async Task<IActionResult> UploadFile(Guid apartmentId, IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest(new { error = "No file uploaded." });
            }

            using var ms = new MemoryStream();
            await file.CopyToAsync(ms);
            var base64 = Convert.ToBase64String(ms.ToArray());

            var dto = new AddApartmentPhotoDto
            {
                ImageBase64 = base64,
                ImageName = Path.GetFileName(file.FileName),
                ImageType = file.ContentType
            };

            var id = await _service.UploadAsync(apartmentId, dto);

            return Ok(new { id });
        }

        [AllowAnonymous]
        [HttpGet("{apartmentId:guid}/list")]
        public async Task<IActionResult> List(Guid apartmentId)
        {
            var photos = await _service.ListPhotosAsync(apartmentId);

            return Ok(photos);
        }

        [Authorize(Roles = "Owner")]
        [HttpDelete("{photoId:guid}")]
        public async Task<IActionResult> Delete(Guid photoId)
        {
            var ok = await _service.DeleteAsync(photoId);

            if (ok)
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }

        [AllowAnonymous]
        [HttpGet("{photoId:guid}")]
        public async Task<IActionResult> Get(Guid photoId)
        {
            var photo = await _service.GetByIdAsync(photoId);

            if (photo == null) return NotFound();

            return Ok(new { photo.Id, photo.ApartmentId, photo.ImageName, photo.ImageType });
        }

    }
}
