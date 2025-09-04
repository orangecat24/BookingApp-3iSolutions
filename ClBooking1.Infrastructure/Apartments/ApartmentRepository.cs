using Booking1.Application.Apartments;
using Booking1.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ClBooking1.Infrastructure.Apartments
{
    public class ApartmentRepository : IApartmentRepository
    {
        private readonly AppDbContext _context;

        public ApartmentRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Guid> AddAsync(Apartment apartment)
        {
            _context.Apartments.Add(apartment);
            await _context.SaveChangesAsync();
            return apartment.Id;
        }

        public async Task<Apartment?> GetByIdAsync(Guid id)
        {
            return await _context.Apartments
                .Include(a => a.ApartmentPhotos)
                .Include(a => a.Bookings)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<Apartment>> GetAllAsync()
        {
            return await _context.Apartments
                .Include(a => a.ApartmentPhotos)
                .Include(a => a.Bookings)
                .ToListAsync();
        }

        public async Task UpdateAsync(Apartment apartment)
        {
            _context.Apartments.Update(apartment);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Apartment apartment)
        {
            _context.Apartments.Remove(apartment);
            await _context.SaveChangesAsync();
        }
    }
}
