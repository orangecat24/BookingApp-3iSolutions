using Booking1.Application.Bookings;
using Booking1.Application.Reviews.CreateReview;
using Booking1.Application.Reviews.DTOResponse;
using Booking1.Domain.Enums;
using Booking1.Domain.Models;
using FluentValidation;

namespace Booking1.Application.Reviews
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepo;
        private readonly IBookingRepository _bookingRepo;
        private readonly IValidator<CreateReviewDto> _validator;

        public ReviewService(
            IReviewRepository reviewRepo,
            IBookingRepository bookingRepo,
            IValidator<CreateReviewDto> validator)
        {
            _reviewRepo = reviewRepo;
            _bookingRepo = bookingRepo;
            _validator = validator;
        }

        public async Task<Guid> CreateAsync(CreateReviewDto dto, Guid userId)
        {
            var validation = _validator.Validate(dto);
            if (!validation.IsValid)
            {
                throw new ArgumentException(string.Join(", ", validation.Errors.Select(e => e.ErrorMessage)));
            }

            var booking = await _bookingRepo.GetByIdAsync(dto.BookingId);
            if (booking == null)
            {
                throw new ArgumentException("Booking not found.");
            }

            if (booking.Status != BookingStatus.Completed)
            {
                throw new InvalidOperationException("Only completed bookings can be reviewed.");
            }

            if (booking.UserId != userId)
            {
                throw new UnauthorizedAccessException("You can only review your own bookings.");
            }

            var review = new Review
            {
                Id = Guid.NewGuid(),
                BookingId = dto.BookingId,
                UserId = userId,
                Rating = dto.Rating,
                Comment = dto.Comment,
                CreatedOnUtc = DateTime.UtcNow
            };

            await _reviewRepo.AddAsync(review);
            return review.Id;
        }

        public async Task<List<ReviewDto>> GetByBookingAsync(Guid bookingId)
        {
            var reviews = await _reviewRepo.GetByBookingIdAsync(bookingId);

            return reviews.Select(r => new ReviewDto
            {
                Id = r.Id,
                Rating = r.Rating,
                Comment = r.Comment,
                CreatedOnUtc = r.CreatedOnUtc,
                ReviewerName = r.User.FirstName,
                ReviewerSurname = r.User.LastName
            }).ToList();
        }


        public async Task<List<ReviewDto>> GetByUserAsync(Guid userId)
        {
            var reviews = await _reviewRepo.GetByUserIdAsync(userId);

            return reviews.Select(r => new ReviewDto
            {
                Id = r.Id,
                Rating = r.Rating,
                Comment = r.Comment,
                CreatedOnUtc = r.CreatedOnUtc,
                ReviewerName = r.User.FirstName,
                ReviewerSurname = r.User.LastName
            }).ToList();
        }

        public async Task<bool> DeleteAsync(Guid id, Guid userId)
        {
            var review = await _reviewRepo.GetByIdAsync(id);

            if (review == null || review.UserId != userId)
            {
                return false;
            }

            var success = await _reviewRepo.DeleteAsync(id);
            return success;
        }
    }
}
