using Booking1.Domain;
using Booking1.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public Amenity? Amenities { get; set; }

        public ICollection<ApartmentPhoto> ApartmentPhotos { get; set; } = new List<ApartmentPhoto>();
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        public ICollection<Owner> Owners { get; set; } = new List<Owner>();
    }
}
