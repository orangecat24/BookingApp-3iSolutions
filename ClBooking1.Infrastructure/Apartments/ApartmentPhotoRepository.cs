using Booking1.Application.Apartments.AddApartmentPhotos;
using Booking1.Domain.Models;

namespace ClBooking1.Infrastructure.Apartments
{
    public class ApartmentPhotoRepository : IApartmentPhotoRepository
    {
        AppDbContext _context;

        public ApartmentPhotoRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Guid> AddAsync(ApartmentPhoto photo)
        {
            _context.ApartmentPhotos.Add(photo);
            await _context.SaveChangesAsync();
            return photo.Id;
        }
    }
}
