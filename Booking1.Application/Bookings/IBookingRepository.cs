using Booking1.Domain.Models;

namespace Booking1.Application.Bookings
{
    public interface IBookingRepository
    {
        Task<Booking> AddAsync(Booking booking);
        Task<Booking?> GetByIdAsync(Guid id);
        Task<List<Booking>> GetAllAsync();
        Task<bool> UpdateAsync(Booking booking);
        Task<bool> DeleteAsync(Guid id);
        Task<List<Booking>> GetByApartmentAsync(Guid apartmentId);


    }
}
