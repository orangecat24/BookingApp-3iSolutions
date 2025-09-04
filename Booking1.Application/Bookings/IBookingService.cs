using Booking1.Application.Bookings.UpdateBooking;
using Booking1.Domain.Models;

namespace Booking1.Application.Bookings
{
    public interface IBookingService
    {
        Task<Guid> CreateAsync(CreateBookingDto dto);
        Task<Booking?> GetByIdAsync(Guid id);
        Task<List<Booking>> GetAllAsync();
        Task<bool> UpdateAsync(Guid id, UpdateBookingDto dto);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> CheckAvailabilityAsync(Guid apartmentId, DateTime start, DateTime end);
        Task<bool> ConfirmBookingAsync(Guid bookingId, Guid ownerId);
        Task<bool> RejectBookingAsync(Guid bookingId, Guid ownerId);
        Task<bool> CompleteBookingAsync(Guid bookingId, Guid ownerId);
    }
}
