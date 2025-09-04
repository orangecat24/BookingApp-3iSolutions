using Booking1.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Booking1.Domain.Models
{
    public class Apartment
    {

        [Key]
        public Guid Id { get; set; }
        [Required, MaxLength(150)]
        public string Name { get; set; } = null!;
        [Required]
        public string Address { get; set; } = null!;
        [Required]
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public decimal CleaningFee { get; set; }
        public DateTime? LastBookedOnUtc { get; set; }
        public List<Amenity> Amenities { get; set; } = new List<Amenity>();
        public ICollection<ApartmentPhoto> ApartmentPhotos { get; set; } = new List<ApartmentPhoto>();
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        public ICollection<Owner> Owners { get; set; } = new List<Owner>();

    }
}
