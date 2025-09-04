namespace Booking1.Application.Reviews.CreateReview
{
    public class CreateReviewDto
    {
        public Guid BookingId { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
    }
}
