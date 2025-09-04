using Booking1.Application.Reviews.CreateReview;
using Booking1.Application.Reviews.DTOResponse;

namespace Booking1.Application.Reviews
{
    public interface IReviewService
    {
        Task<Guid> CreateAsync(CreateReviewDto dto, Guid userId);
        Task<List<ReviewDto>> GetByBookingAsync(Guid bookingId);
        Task<List<ReviewDto>> GetByUserAsync(Guid userId);
        Task<bool> DeleteAsync(Guid id, Guid userId);
    }
}
