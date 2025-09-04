using Booking1.Application.Bookings;
using Booking1.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ClBooking1.Infrastructure.Bookings
{
    public class BookingRepository : IBookingRepository
    {
        private readonly AppDbContext _context;

        public BookingRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Booking> AddAsync(Booking booking)
        {
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();
            return booking;
        }

        public async Task<Booking?> GetByIdAsync(Guid id)
        {
            return await _context.Bookings
                .Include(b => b.Apartment)
                .Include(b => b.User)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<List<Booking>> GetAllAsync()
        {
            return await _context.Bookings
                .Include(b => b.Apartment)
                .Include(b => b.User)
                .ToListAsync();
        }

        public async Task<bool> UpdateAsync(Booking booking)
        {
            var existing = await _context.Bookings.FindAsync(booking.Id);
            if (existing == null) return false;

            _context.Entry(existing).CurrentValues.SetValues(booking);

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null) return false;

            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Booking>> GetByApartmentAsync(Guid apartmentId)
        {
            return await _context.Bookings
                .Where(b => b.ApartmentId == apartmentId)
                .ToListAsync();
        }

    }
}
