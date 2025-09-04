using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Booking1.Domain.Models
{
    public class Review
    {

        [Key]
        public Guid Id { get; set; }

        [ForeignKey("Booking")]
        public Guid BookingId { get; set; }

        [ForeignKey("User")]
        public Guid UserId { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }

        public string? Comment { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public Booking Booking { get; set; } = null!;
        public User User { get; set; } = null!;
    }
}
