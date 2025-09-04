
using Booking1.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Booking1.Domain.Models
{
    public class User
    {
        public User()
        {
        }

        [Key]
        public Guid Id { get; set; }

        [Required, MaxLength(50)]
        public string FirstName { get; set; } = null!;

        [Required, MaxLength(50)]
        public string LastName { get; set; } = null!;

        [Required, EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;

        [Required]
        public string Country { get; set; } = null!;

        public DateTime CreatedOnUtc { get; set; }
        public DateTime? UpdatedOnUtc { get; set; }

        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();

        [Required]
        public UserRole Role { get; set; }
    }
}
