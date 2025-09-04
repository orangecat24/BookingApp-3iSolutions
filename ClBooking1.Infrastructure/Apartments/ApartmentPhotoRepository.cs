using Booking1.Application.Apartments.AddApartmentPhotos;
using Booking1.Domain.Models;
using Microsoft.EntityFrameworkCore;

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
        public async Task<List<ApartmentPhoto>> getPhotosByApartment(Guid apartmentId)
        {
            return await _context.ApartmentPhotos
                .Where(p => p.ApartmentId == apartmentId)
                .ToListAsync();
        }

        public async Task<bool> DeleteAsync(Guid photoId)
        {
            var photo = await _context.ApartmentPhotos.FindAsync(photoId);
            if (photo == null) return false;

            _context.ApartmentPhotos.Remove(photo);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<ApartmentPhoto?> GetPhotoByIdAsync(Guid photoId)
        {
            return await _context.ApartmentPhotos.FindAsync(photoId);
        }


    }
}
