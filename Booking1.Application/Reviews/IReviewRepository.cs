using Booking1.Domain.Models;

namespace Booking1.Application.Reviews
{
    public interface IReviewRepository
    {
        Task<Review> AddAsync(Review review);
        Task<List<Review>> GetByBookingIdAsync(Guid bookingId);
        Task<List<Review>> GetByUserIdAsync(Guid userId);
        Task<Review?> GetByIdAsync(Guid id);
        Task<bool> DeleteAsync(Guid id);
    }
}
