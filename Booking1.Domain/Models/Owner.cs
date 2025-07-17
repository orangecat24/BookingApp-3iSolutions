using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking1.Domain.Models
{
    public class Owner
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("User")]
        public Guid UserId { get; set; }

        [Required]
        public string IdentityCardNumber { get; set; } = null!;

        [Required]
        public string BankAccount { get; set; } = null!;

        [Required]
        public string PhoneNumber { get; set; } = null!;

        public User User { get; set; } = null!;
        public ICollection<Apartment> Apartments { get; set; } = new List<Apartment>();
    }
}
