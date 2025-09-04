namespace Booking1.Application.Bookings
{
    public class CreateBookingDto
    {
        public Guid ApartmentId { get; set; }
        public Guid UserId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public decimal CleaningFee { get; set; }
        public decimal AmenitiesUpCharge { get; set; }
    }
}
