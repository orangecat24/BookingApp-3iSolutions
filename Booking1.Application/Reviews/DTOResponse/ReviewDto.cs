namespace Booking1.Application.Reviews.DTOResponse
{
    public class ReviewDto
    {
        public Guid Id { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public string ReviewerName { get; set; } = string.Empty;
        public string ReviewerSurname { get; set; } = string.Empty;
    }
}
