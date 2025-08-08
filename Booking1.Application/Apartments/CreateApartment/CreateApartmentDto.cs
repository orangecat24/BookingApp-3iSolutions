using Booking1.Domain.Enums;

namespace Booking1.Application.Apartments.CreateApartment
{
    public class CreateApartmentDto
    {
        public string Name { get; set; } = null;
        public string Address { get; set; } = null;
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public decimal CleaningFee { get; set; }
        public Amenity? Amenities { get; set; }
    }
}
