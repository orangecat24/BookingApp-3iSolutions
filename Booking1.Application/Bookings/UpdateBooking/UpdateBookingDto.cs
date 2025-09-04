using Booking1.Domain.Enums;

namespace Booking1.Application.Bookings.UpdateBooking
{
    public class UpdateBookingDto
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public BookingStatus Status { get; set; }
    }
}
